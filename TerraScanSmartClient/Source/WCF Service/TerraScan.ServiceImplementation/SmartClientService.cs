// -------------------------------------------------------------------------------------------
// <copyright file="SmartClientService.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access SmartClientService</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------

namespace TerraScan.ServiceImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.ServiceContracts;
    using TerraScan.DalHelper;
    using System.Data;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using System.Security;
    using System.Web.Security;
    using System.Security.Cryptography;
    using System.Reflection;
    using System.Collections;

    /// <summary>
    /// Smart Client Service Class
    /// </summary>
    /// [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SmartClientService : ISmartClientService
    {
        #region
        /// <summary>
        /// used to store the userID
        /// </summary>
        private int userIdValue;

        #endregion

        #region Check Installation

        /// <summary>
        /// Checks the installation.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <returns>return true to check webservice installation</returns>
        public string CheckInstallation(string test)
        {
            if (test == "Success")
            {
                return "Success";
            }
            else
            {
                return "Failure";
            }
        }

        #endregion Check Installation

        #region Receipt Engine

        #region ListHistoryGrid

        /// <summary>
        /// Lists the history information of the statement.
        /// </summary>
        /// <param name="statementId"> The statement id of the statement's history to be fetched.</param>
        /// <returns> The Typed dataset containing the history information of the statementid.</returns>
        public string ListHistoryGrid(int statementId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            receiptEngineData = Helper.ListHistoryGrid(statementId);
            return receiptEngineData.GetXml();
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// Get a Receipt Details.
        /// </summary>
        /// <param name="receiptId"> The reciept id of the reciept details to be fetched.</param>
        /// <returns> The typed dataset containing the details of the reciept.</returns>
        public string GetReceiptDetails(int receiptId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            receiptEngineData = Helper.GetReceiptDetails(receiptId);
            return receiptEngineData.GetXml();
        }

        /// <summary>
        /// Get a Receipt Details.
        /// </summary>
        /// <param name="receiptId"> The reciept id of the reciept details to be fetched.</param>
        /// <returns> The typed dataset containing the details of the reciept.</returns>
        public string GetReceiptListDetails(int receiptId)
        {
            F15100ReceiptHeaderData form15100ReceiptHeaderData = new F15100ReceiptHeaderData();
            form15100ReceiptHeaderData = Helper.GetReceiptListDetails(receiptId);
            return form15100ReceiptHeaderData.GetXml();
        }

        #endregion GetReceiptDetails

        #region ListTenderType

        /// <summary>
        /// Lists the tender type.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>
        /// The typed dataset containing the types of tender.
        /// </returns>
        public string ListTenderType(bool allowOverUnder)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            receiptEngineData = Helper.ListTenderType(allowOverUnder);
            return receiptEngineData.GetXml();
        }

        #endregion ListTenderType

        #region GetValidReceiptTest

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        /// <returns> The string containing the recipiet's validity information.</returns>
        public string GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return Helper.GetValidReceiptTest(statementId, receiptDate);
        }

        #endregion GetValidReceiptTest

        #region SaveReceipt

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The return value specifying status of the save action.
        /// </returns>
        public string SaveReceipt(string receiptItems, string paymentItems, int userId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            receiptEngineData = Helper.SaveReceipt(receiptItems, paymentItems, userId);
            return receiptEngineData.GetXml();
        }

        #endregion SaveReceipt

        #region Tax CalCulation for Receipt Engine

        #region GetMinTaxDue

        /// <summary>
        /// Gets the minimum tax due amount
        /// </summary>
        /// <param name="statmentId"> The statement id of the statement's minimum tax due amount to be fetched.</param>
        /// <param name="interestDate"> The interest date of the reciept.</param>
        /// <returns> The decimal containing minimum tax amount due.</returns>
        public decimal GetMinTaxDue(int statmentId, string interestDate)
        {
            return Helper.GetMinTaxDue(statmentId, interestDate);
        }

        #endregion GetMinTaxDue

        #region GetInterestAmount

        /// <summary>
        /// Get the interest amoount.
        /// </summary>
        /// <param name="statmentId">The statement id for which interest amount to be fetched.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public decimal GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            return Helper.GetInterestAmount(statmentId, interestDate, taxDueAmount);
        }

        #endregion GetInterestAmount

        #endregion Tax CalCulation for Receipt Engine

        #endregion Receipt Engine

        #region Receipt Management

        #region ReceiptHeaderDetails

        /// <summary>
        /// Gets the receipt header details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ReceiptHeaderDetails</returns>
        public string GetReceiptHeaderDetails(int receiptId)
        {
            F15100ReceiptHeaderData form15100ReceiptHeaderData = new F15100ReceiptHeaderData();
            form15100ReceiptHeaderData = Helper.GetReceiptHeaderDetails(receiptId);
            return form15100ReceiptHeaderData.GetXml();
        }

        #region Save Receipt Header

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="userId">UserID</param>
        public void F15100_SaveReceiptHeaderreceiptNumber(int receiptId, string receiptNumber, int userId)
        {
            Helper.F15100_SaveReceiptHeaderreceiptNumber(receiptId, receiptNumber, userId);
        }

        #endregion Save Receipt Header

        #endregion ReceiptHeaderDetails

        #region ReceiptItems
        /// <summary>
        /// Lists the ReceiptItems.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ReceiptItems</returns>
        public string ListReceiptItems(int receiptId)
        {
            F15101ReceiptItemsData form15101ReceiptItemsData = new F15101ReceiptItemsData();
            form15101ReceiptItemsData = Helper.ListReceiptItems(receiptId);
            return form15101ReceiptItemsData.GetXml();
        }
        #endregion ReceiptItems

        #region Update Transaction Items

        /// <summary>
        /// F15101_s the update transaction items.
        /// </summary>
        /// <param name="transactionItems">The transaction items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status</returns>
        public int F15101_UpdateTransactionItems(string transactionItems, int userId)
        {
            return Helper.F15101_UpdateTransactionItems(transactionItems, userId);
        }

        #endregion Update Transaction Items

        #region ReceiptstatementHeaderDetails

        /// <summary>
        ///  Gets the receipt statement header details.
        /// </summary>
        ///<param name="receiptId">The receipt id.</param>
        ///<returns>ReceiptStatementHeaderDetails</returns>
        public string GetReceiptStatementHeaderDetails(int receiptId)
        {
            F15102ReceiptStatementHeaderData form15102ReceiptStatementHeaderData = new F15102ReceiptStatementHeaderData();
            form15102ReceiptStatementHeaderData = Helper.GetReceiptStatementHeaderDetails(receiptId);
            return form15102ReceiptStatementHeaderData.GetXml();
        }

        #endregion ReceiptstatementHeaderDetails

        #region ReceiptOwners
        /// <summary>
        /// Lists the ReceiptOwners
        /// </summary>
        /// <param name="receiptId">The receipt id</param>
        /// <returns>ReceiptOwners</returns>
        public string ListReceiptOwners(int receiptId)
        {
            F15103ReceiptOwnersData form15103ReceiptOwnersData = new F15103ReceiptOwnersData();
            form15103ReceiptOwnersData = Helper.ListReceiptOwners(receiptId);
            return form15103ReceiptOwnersData.GetXml();
        }
        #endregion ReceiptOwners

        #endregion Receipt Management

        #region Payee Details

        /// <summary>
        /// Get Payee detail based of payeeID
        /// </summary>
        /// <param name="payeeId">The payeeid.</param>
        /// <returns>
        /// The typed dataset containing the Payee Details.
        /// </returns>
        public string F1019_GetPayeeDetails(int PayeeID)
        {
            PaymentEngineData OwnerDetailDataset = new PaymentEngineData();
            OwnerDetailDataset = Helper.F1019_GetPayeeDetails(PayeeID);
            return OwnerDetailDataset.GetXml();
        }


        #endregion

        #region Payment Engine

        #region GetPayment

        /// <summary>
        /// Get Payment detail based of ppaymentId
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>
        /// The typed dataset containing the Payments.
        /// </returns>
        public string GetPayment(int ppaymentId)
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            paymentEngineData = Helper.GetPayment(ppaymentId);
            return paymentEngineData.GetXml();
        }

        /// <summary>
        /// Get Payment detail based of ppaymentId
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>
        /// The typed dataset containing the Payments.
        /// </returns>
        public string GetMultiplePayment(string ppaymentId)
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            paymentEngineData = Helper.GetMultiplePayment(ppaymentId);
            return paymentEngineData.GetXml();
        }

        #endregion GetPayment

        #region Save Payment

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Return PPayment ID</returns>
        public int SavePayment(int ppaymentId, string paymentItems, int userId, int ownerId)
        {
            return Helper.SavePayment(ppaymentId, paymentItems, userId, ownerId);
        }
        #endregion Save Payment

        #region Get Tender Type Configuration

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <returns>Tender type configuartion information</returns>
        public string GetTenderTypeConfiguration()
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            paymentEngineData = Helper.GetTenderTypeConfiguration();
            return paymentEngineData.GetXml();
        }

        #endregion Get Tender Type Configuration

        #endregion Payment Engine

        #region Comments

        #region GetComments

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <returns> The Typed dataset containing the comments.</returns>
        public string GetComments(int keyId, int formId, int userId)
        {
            CommentsData commentsData = new CommentsData();
            commentsData = Helper.GetComments(keyId, formId, userId);
            return commentsData.GetXml();

            ////return Helper.GetComments(keyId, formId, userId);
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>The string containing the comment and priority.</returns>
        public string F9075_GetComment(int keyId, int formId)
        {
            return Helper.F9075_GetComment(keyId, formId).GetXml();
        }

        #endregion GetComments

        #region F9075_DeleteCommentIds

        /// <summary>
        /// F9075_s the delete comment.
        /// </summary>
        /// <param name="commentId">The comment ids.</param>
        /// <param name="userId">The user id.</param>
        public void F9075_DeleteCommentIds(string commentIds, int userId)
        {
            Helper.F9075_DeleteCommentIds(commentIds, userId);
        }

        #endregion F9075_DeleteCommentIds


        #region SaveComments

        /// <summary>
        /// Save the comment entered by the userid.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentDate">The comment date.</param>
        /// <param name="userId">The user id of the user logged in.</param>
        /// <param name="comments">The comment to be saved.</param>
        /// <param name="printOnReceipt">The print on receipt.</param>
        /// <param name="publicComment">The public comment.</param>
        /// <param name="ispriority">The Priority</param>
        /// <param name="isroll">Roll</param>
        /// <param name="commentPriorityId">commentPriorityId</param>
        public void SaveComments(int commentId, int formId, int keyId, DateTime commentDate, int userId, string comments, int printOnReceipt, int publicComment, int ispriority, int isroll, int commentPriorityId)
        {
            Helper.SaveComments(commentId, formId, keyId, commentDate, userId, comments, printOnReceipt, publicComment, ispriority, isroll, commentPriorityId);
        }

        #endregion SaveComments

        #region DeleteComments

        /// <summary>
        /// Delete the comment based on the commentid, formid and keyid.
        /// </summary>
        /// <param name="keyId">Reciept or statement information in xml format.</param>
        /// <param name="formId">The form id  of the form being used.</param>
        /// <param name="commentId">The commentid of the comment to be deletd.</param>
        /// <param name="userId">UserID</param>
        public void DeleteComments(int keyId, int formId, int commentId, int userId)
        {
            Helper.DeleteComments(keyId, formId, commentId, userId);
        }

        #endregion DeleteComments

        #region GetCommentsCount

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The Typed count of comments.</returns>
        public string GetCommentsCount(int formId, int keyId, int userId)
        {
            CommentsData commentsData = new CommentsData();
            commentsData = Helper.GetCommentsCount(formId, keyId, userId);
            return commentsData.GetXml();
        }
        #endregion GetCommentsCount

        #region GetCommentsConfigDetails

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The Typed dataset containing the comments.</returns>
        public string GetConfigDetails(string configName)
        {
            CommentsData commentsData = new CommentsData();
            commentsData = Helper.GetConfigDetails(configName);
            return commentsData.GetXml();
            ////return Helper.GetConfigDetails(configName);
        }

        #endregion GetCommentsConfigDetails

        #region GetconnectionString

        /// <summary>
        /// GetConnectionString
        /// </summary>
        /// <param name="msversionId">The msversion id.</param>
        /// <returns>Typed dataSet</returns>
        public string GetConnectionString(int msversionId)
        {
            CommonData commonData = new CommonData();
            commonData = Helper.GetConnectionString(msversionId);
            return commonData.GetXml();
        }
        #endregion GetCommentsCount

        #endregion Comments

        #region Attachments

        #region GetAttachmentCount

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The count of attachments.</returns>
        public int GetAttachmentCount(int formId, int receiptId, int userId)
        {
            return Helper.GetAttachmentCount(formId, receiptId, userId);
        }

        #endregion GetAttachmentCount

        #region GetAttachmentItems

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The typed dataset containing the attachment items.</returns>
        public string GetAttachmentItems(int formId, int keyId, int userId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            attachmentsData = Helper.GetAttachmentItems(formId, keyId, userId);
            return attachmentsData.GetXml();
        }

        #endregion GetAttachmentItems

        #region GetAttachementFunctionName

        /// <summary>
        /// Get the attachment function name.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The typed dataset containing the attachment function name.</returns>
        public string GetAttachementFunctionName(int formId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            attachmentsData = Helper.GetAttachementFunctionName(formId);
            return attachmentsData.GetXml();
        }

        #endregion GetAttachementFunctionName

        #region SaveAttachments

        /// <summary>
        /// Saves the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="fileTypeId">The file type id.</param>
        /// <param name="source">The source.</param>
        /// <param name="primary">The primary.</param>
        /// <param name="description">The description.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="publicValue">The public value.</param>
        /// <param name="isroll">The isroll.</param>
        /// <param name="linktype">The linktype.</param>
        /// <param name="aurl">The aurl.</param>
        /// <param name="pfileid">The pfileid.</param>
        /// <param name="sourceConfig">The source config</param>
        /// <returns>File path data</returns>
        public string SaveAttachments(int? fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isroll, int linktype, string aurl, int pfileid, string sourceConfig)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            attachmentsData = Helper.SaveAttachments(fileId, extension, formId, keyId, fileTypeId, source, primary, description, eventDate, userId, publicValue, isroll, linktype, aurl, pfileid, sourceConfig);
            return attachmentsData.GetXml();
        }

        #endregion SaveAttachments

        #region DeleteAttachments

        /// <summary>
        /// Deletes the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">UserID</param>
        public void DeleteAttachments(int fileId, int userId)
        {
            Helper.DeleteAttachments(fileId, userId);
        }

        #endregion DeleteAttachments

        #region GetProgramId

        /// <summary>
        /// GetProgramId
        /// </summary>
        /// <param name="fileTypeId"> The integer name of the file type.</param>
        /// <returns> The typed dataset containing the attachment file type.</returns>
        public string GetProgramId(int fileTypeId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            attachmentsData = Helper.GetProgramId(fileTypeId);
            return attachmentsData.GetXml();
        }

        #endregion GetProgramId

        #region GetFilePath

        /// <summary>
        /// Gets the files path
        /// </summary>
        /// <param name="source"> The source path of the file.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="extension"> The file extension.</param>
        /// <param name="userId">userId</param>
        /// <returns> The typed dataset containing the path of the file.</returns>
        public string GetFilePath(string source, int formId, int keyId, string extension, int userId)
        {
            AttachmentsData attachmentsData = new AttachmentsData();
            attachmentsData = Helper.GetFilePath(source, formId, keyId, extension, userId);
            return attachmentsData.GetXml();
        }

        /// <summary>
        /// F9005_s the get original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <returns>FilePath</returns>
        public string F9005_GetOriginalFilePath(int fileId, int userId)
        {
            return Helper.F9005_GetOriginalFilePath(fileId, userId);
        }

        #endregion GetFilePath

        #region Create Thumbnails

        /// <summary>
        /// Create Thumbnails
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The userId.</param>
        public void GenerateThumbnail(int? fileId, int userId, string fileIdXml)
        {
            Helper.GenerateThumbnail(fileId, userId, fileIdXml);
        }

        #endregion Create Thumbnaila

        #endregion Attachments

        #region Real Estate

        #region Real Estate Statements

        #region Get Real Estate Statement Count

        /// <summary>
        /// Gets the real estate statementcount
        /// </summary>
        /// <returns> The count of statements.</returns>
        public int GetRealEstateStatementCount()
        {
            // call to get the real estate statement count.
            return Helper.GetRealEstateStatementCount();
        }

        #endregion Get Real Estate Statement Count

        #region Get Real Estate Statement IDs

        /// <summary>
        /// Gets the real estate statement Id's
        /// </summary>
        /// <param name="orderField"> The orderbycolumn name.</param>
        /// <param name="orderBy"> The orderby direction.</param>
        /// <returns> The Typed dataset containing the list of real estate statementids.</returns>
        public string GetRealEstateStatementIds(string orderField, string orderBy)
        {
            // call to get the real estate statement ids.
            RealEstateData realEstateData = new RealEstateData();
            realEstateData = Helper.GetRealEstateStatementIds(orderField, orderBy);
            return realEstateData.GetXml();
        }

        #endregion Get Real Estate Statement IDs

        #region Get Real Estate Statement

        /// <summary>
        /// Gets the real estate statement based on the statement id
        /// </summary>
        /// <param name="statementId"> The statement id of the statement to be fetched.</param>
        /// <returns> The Typed dataset containing the statement information of the statementid.</returns>
        public string GetRealEstateStatement(int statementId)
        {
            // call to get the real estate statement for the statement id.
            RealEstateData realEstateData = new RealEstateData();
            realEstateData = Helper.GetRealEstateStatement(statementId);
            return realEstateData.GetXml();
        }

        #endregion Get Real Estate Statement

        #endregion Real Estate Statements

        #region Query

        #region List Query

        /// <summary>
        /// Lists the query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Dataset</returns>
        public DataSet ListQuery(int formId, int userId)
        {
            return Helper.ListQuery(formId, userId);
        }

        #endregion List Query

        #region List Sort Query

        /// <summary>
        /// Lists the Sorted query.
        /// </summary>
        /// <param name="savedQueryId">The savedquery Id .</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form Id.</param>
        /// <returns> The dataset containing sorted order of the query result.</returns>
        public DataSet ListSortQuery(int savedQueryId, string orderByCondition, int formId)
        {
            return Helper.ListSortQuery(savedQueryId, orderByCondition, formId);
        }

        #endregion List Sort Query

        #region Save Query

        /// <summary>
        /// Saves the Query.
        /// </summary>
        /// <param name="savedQueryName">Name of the saved query.</param>
        /// <param name="formId">The form ID.</param>
        /// <param name="savedQueryNote">The saved query note.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="savedQueryDate">The saved query date.</param>
        /// <param name="savedQuery">The saved query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="canOverride">if set to <c>true</c> [is override].</param>
        /// <returns>DataSet</returns>
        public DataSet SaveQuery(string savedQueryName, int formId, string savedQueryNote, int userId, DateTime savedQueryDate, string savedQuery, string whereCondn, bool canOverride)
        {
            return Helper.SaveQuery(savedQueryName, formId, savedQueryNote, userId, savedQueryDate, savedQuery, whereCondn, canOverride);
        }

        #endregion Save Query

        #region Execute Query

        /// <summary>
        ///  method to execute query
        /// </summary>
        /// <param name="whereCondition"> The whereCondition to be applied on query</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>Typed Dataset</returns>
        public string ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            QueryData queryData = new QueryData();
            queryData = Helper.ExecuteQuery(whereCondition, orderByCondition, formId);
            return queryData.GetXml();
        }

        #endregion Execute Query

        #region Check Query Exist

        /// <summary>
        /// Checks the snap shot exist.
        /// </summary>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedQueryName">Name of the saved query.</param>
        /// <returns>
        /// True if query name exist and False if not exist.
        /// </returns>
        public int CheckQueryExist(int formId, string savedQueryName)
        {
            return Helper.CheckQueryExist(formId, savedQueryName);
        }

        #endregion Check Query Exist

        #region Get Query Result

        /// <summary>
        /// Gets the snap shot.
        /// </summary>
        /// <param name="queryId">The query ID.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns>All Statement Ids</returns>
        public string GetQueryResult(int queryId, string orderByCondn)
        {
            QueryData queryData = new QueryData();
            queryData = Helper.GetQueryResult(queryId, orderByCondn);
            return queryData.GetXml();
        }

        #endregion Get Query Result

        #region Delete Query

        /// <summary>
        /// Deletes the query.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="userId">User ID</param>
        public void DeleteQuery(int queryId, int userId)
        {
            Helper.DeleteQuery(queryId, userId);
        }

        #endregion Delete Query

        #endregion Query

        #region SnapShot

        #region Execute Snapshot

        /// <summary>
        ///  method to execute query
        /// </summary>
        /// <param name="snapshotId"> The id used to retrieve snapshotitems to which filter applied</param>
        /// <param name="whereCondition">wherecondition used to query snapshotitems</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>Typed Dataset</returns>
        public string ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            QueryData queryData = new QueryData();
            queryData = Helper.ExecuteSnapshot(snapshotId, whereCondition, orderByCondition, formId);
            return queryData.GetXml();
        }

        #endregion

        #region Save Snapshot

        /// <summary>
        /// Saves the snap shot.
        /// </summary>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="formId">The form Id.</param>
        /// <param name="snapshotNote">The snapshot note.</param>
        /// <param name="userId">The user Id.</param>
        /// <param name="snapshotDate">The snapshot date.</param>
        /// <param name="snapshotQuery">The snapshot query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="keyIDs">The key I ds.</param>
        /// <param name="canOverride">if set to <c>true</c> [is override].</param>
        /// <returns>first column will return true/false </returns>
        public DataSet SaveSnapShot(string snapshotName, int formId, string snapshotNote, int userId, DateTime snapshotDate, string snapshotQuery, string whereCondn, string keyIDs, bool canOverride)
        {
            return Helper.SaveSnapShot(snapshotName, formId, snapshotNote, userId, snapshotDate, snapshotQuery, whereCondn, keyIDs, canOverride);
        }

        #endregion Save Snapshot

        #region Check SnapShot Exist

        /// <summary>
        /// Checks the snap shot exist.
        /// </summary>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedSnapShotName">Name of the saved snap shot.</param>
        /// <returns>True if snapshot name exist and False if not exist.</returns>
        public int CheckSnapShotExist(int formId, string savedSnapShotName)
        {
            return Helper.CheckSnapShotExist(formId, savedSnapShotName);
        }

        #endregion Check SnapShot Exist

        #region Get SnapShot Result

        /// <summary>
        /// Get the result of the snapshot.
        /// </summary>
        /// <param name="snapShotId"> The snapshot ID of the snapshot to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The Typed datset containing the snapshot results.</returns>
        public string GetSnapShotResult(int snapShotId, string orderByCondn)
        {
            QueryData queryData = new QueryData();
            queryData = Helper.GetSnapShotResult(snapShotId, orderByCondn);
            return queryData.GetXml();
        }

        #endregion Get SnapShot Result

        #region List SnapShot

        /// <summary>
        /// Lists the snap shot.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns> The datset containing the list of the snapshots.</returns>
        public DataSet ListSnapShot(int formId)
        {
            return Helper.ListSnapShot(formId);
        }

        #endregion List SnapShot

        #region List Sort SnapShot

        /// <summary>
        /// Lists the Sorted Sanpshot.
        /// </summary>
        /// <param name="snapShotId">The snapshot id .</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form Id.</param>
        /// <returns> The dataset containing sorted order of the snapshot result.</returns>
        public DataSet ListSortSnapShot(int snapShotId, string orderByCondition, int formId)
        {
            return Helper.ListSortSnapShot(snapShotId, orderByCondition, formId);
        }

        #endregion List Sort SnapShot

        #region Delete SnapShot

        /// <summary>
        /// Deletes the snap snot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="userId">UserID</param>
        public void DeleteSnapShot(int snapShotId, int userId)
        {
            Helper.DeleteSnapShot(snapShotId, userId);
        }

        #endregion Delete SnapShot

        #endregion SnapShot

        #endregion SnapShot

        #region Report

        /// <summary>
        /// Gets the report details.
        /// </summary>
        /// <param name="reportId">The report ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>DataSet With Details for Report path</returns>
        public DataSet GetReportDetails(int reportId, int userId)
        {
            // call to get the Report Details 
            return Helper.GetReportDetails(reportId, userId);
        }

        /// <summary>
        /// F9008s the get report details.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>F9008ReportDetailsData</returns>
        public string F9008_GetReportDetails(int userId)
        {
            // call to get the Report Details
            F9008ReportDetailsData form9008ReportDetailsData = new F9008ReportDetailsData();
            form9008ReportDetailsData = Helper.F9008GetReportDetails(userId);
            return form9008ReportDetailsData.GetXml();
        }

        #region SaveReportDetails

        /// <summary>
        /// To Save Report Details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="printerConf">The printer conf.</param>
        public void F9008_SaveReportDetails(int userId, string printerConf)
        {
            Helper.F9008_SaveReportDetails(userId, printerConf);
        }

        #endregion SaveReportDetails

        #region GetAutoPrintStatus

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns true/false</returns>
        public int GetAutoPrintStatus(int formId, int userId)
        {
            return Helper.GetAutoPrintStatus(formId, userId);
        }

        #endregion GetAutoPrintStatus

        #region SaveAutoPrint

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [auto print].</param>
        public void SaveAutoPrint(int formId, int userId, bool autoPrint)
        {
            Helper.SaveAutoPrint(formId, userId, autoPrint);
        }

        #endregion SaveAutoPrint

        #endregion

        #region GetTheMenuItems

        /// <summary>
        ///  used to get the menuItems depends on the userID and applicationId
        /// </summary>
        /// <param name="userId">userid to get the menuitems</param>
        /// <param name="applicationId">applicationId to get the menuitems</param>
        /// <returns>dataset with menuitems</returns>
        public DataSet GetMenuItems(int userId, int applicationId)
        {
            return Helper.GetMenuItems(userId, applicationId);
        }
        #endregion

        #region GetFormItems

        /// <summary>
        /// Gets the form items.
        /// </summary>
        /// <returns>return dataset</returns>
        public DataSet GetFormItems()
        {
            return Helper.GetFormItems();
        }

        #endregion

        #region GetFormTitle

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String with Title</returns>
        public string GetFormTitle(int formId)
        {
            return Helper.GetFormTitle(formId);
        }

        #endregion

        #region GetFormPermissions
        /// <summary>
        ///  Used to get the Form Permissions depends on the userID and applicationId
        /// </summary>
        /// <param name="userId">userid to get the Form Permissions</param>
        /// <param name="applicationId">applicationId to get the Form Permissions</param>
        /// <returns>dataset with Form Permissions</returns>
        public DataSet GetFormPermissions(int userId, int applicationId)
        {
            return Helper.GetFormPermissions(userId, applicationId);
        }
        #endregion

        #region  usermanagement

        #region User Tab

        #region  GetUserGroupDetails

        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>DataSet With Details of User and Group</returns>
        public string GetUserGroupDetails(int applicationId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.GetUserGroupDetails(applicationId);
            return userManagementData.GetXml();
        }

        #endregion

        #region InserUserDetails

        /// <summary>
        /// Save the user details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="nameDisplay">The name display.</param>
        /// <param name="nameFull">The name full.</param>
        /// <param name="nameNet">The name net.</param>
        /// <param name="email">The email.</param>
        /// <param name="active">if set to <c>true</c> [active]. else Not Active</param>
        /// <param name="administrator">if set to <c>true</c> [administrator]. else Not administrator</param>
        /// <param name="applicationId">The application id.</param>
        /// <param name="loginUserId">The login user id.</param>
        /// <returns>Return Dataset 0 valid insert or 1</returns>
        public string SaveUserDetails(int userId, string nameDisplay, string nameFull, string nameNet, string email, int active, int administrator, int appraiser, int applicationId, int loginUserId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.SaveUserDetails(userId, nameDisplay, nameFull, nameNet, email, active, administrator,appraiser, applicationId, loginUserId);
            return userManagementData.GetXml();
        }

        #endregion SaveComments

        #region DeleteUserDetails

        /// <summary>
        /// Delete the particular user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="loginUserId">Login User ID</param>
        public void DeleteUserDetails(int userId, int loginUserId)
        {
            Helper.DeleteUserDetails(userId, loginUserId);
        }

        #endregion

        #endregion

        #region Group Tab

        #region  GetUserGroupDetails

        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet With Details of User and Group</returns>
        public string GetGroupDetails(int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.GetGroupDetails(userId);
            return userManagementData.GetXml();
        }

        #endregion

        #region InsertGroupDetails

        /// <summary>
        /// Save the user details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="userGroup">The user group.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet 1 for success insert 0 for error</returns>
        public string InsertGroupDetails(int groupId, string groupName, string description, string userGroup, int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.InsertGroupDetails(groupId, groupName, description, userGroup, userId);
            return userManagementData.GetXml();
        }

        #endregion SaveComments

        #region DeleteGroupDetails

        /// <summary>
        /// Delete the user details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="userId">DeleteGroupDetails</param>
        public void DeleteGroupDetails(int groupId, int userId)
        {
            Helper.DeleteGroupDetails(groupId, userId);
        }

        #endregion

        #endregion

        #region PermissionsTab

        #region  GetUserGroupDetails

        /// <summary>
        /// Gets the user Group Permission Details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// DataSet With Details of Forms Name  and relative permissions
        /// </returns>
        public string GetGroupPermissionDetails(int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.GetGroupPermissionDetails(userId);
            return userManagementData.GetXml();
        }

        #endregion

        #region SaveUserGroupDetails

        /// <summary>
        /// Save the user Group Permission Details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="formPermissions">The form permissions.</param>
        /// <param name="userId">The user id.</param>
        public void SaveGroupPermissionDetails(int groupId, string formPermissions, int userId)
        {
            Helper.SaveGrouPermissionDetails(groupId, formPermissions, userId);
        }

        #endregion

        #endregion

        #endregion

        #region Exception

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="title">The title.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="appDomainName">Name of the app domain.</param>
        /// <param name="processId">The process id.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="managedThreadName">Name of the managed thread.</param>
        /// <param name="win32ThreadId">The win32 thread id.</param>
        /// <param name="message">The message.</param>
        /// <param name="formattedMessage">The formatted message.</param>
        public void LogException(int eventId, int priority, string severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processId, string processName, string managedThreadName, string win32ThreadId, string message, string formattedMessage)
        {
            Helper.LogException(eventId, priority, severity, title, timeStamp, machineName, appDomainName, processId, processName, managedThreadName, win32ThreadId, message, formattedMessage);
        }

        #endregion

        #region Query Utility

        /// <summary>
        /// Gets the query utility list.
        /// </summary>
        /// <param name="formId">The form ID.</param>
        /// <returns>return dataset</returns>
        public string GetQueryUtilityList(int formId)
        {
            QueryUtilityData queryUtilityData = new QueryUtilityData();
            queryUtilityData = Helper.GetQueryUtilityList(formId);
            return queryUtilityData.GetXml();
        }

        /// <summary>
        /// Deletes the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="userId">UserID</param>
        public void DeleteQueryUtility(int queryId, int userId)
        {
            Helper.DeleteQueryUtility(queryId, userId);
        }

        /// <summary>
        /// Inserts the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="queryName">Name of the query.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="userWhereCondition">The user where condition.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <returns>return integer</returns>
        public int InsertQueryUtility(int queryId, string queryName, int formId, string description, int userId, string whereCondition, string userWhereCondition, int overrideValue)
        {
            return Helper.InsertQueryUtility(queryId, queryName, formId, description, userId, whereCondition, userWhereCondition, overrideValue);
        }

        #endregion

        #region Snapshot Utility

        /// <summary>
        /// Gets the snapshot utility list.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        public string GetSnapshotUtilityList(int formId)
        {
            SnapshotUtilityData snapshotUtilityData = new SnapshotUtilityData();
            snapshotUtilityData = Helper.GetSnapshotUtilityList(formId);
            return snapshotUtilityData.GetXml();
        }

        /// <summary>
        /// Deletes the snapshot utility.
        /// </summary>
        /// <param name="snapshotId">SnapShot ID</param>
        /// <param name="userId">User ID</param>
        public void DeleteSnapshotUtility(int snapshotId, int userId)
        {
            Helper.DeleteSnapshotUtility(snapshotId, userId);
        }

        /// <summary>
        /// Inserts the snapshot utility.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="snapshotFormId">The snapshot form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <returns>return integer</returns>
        public int InsertSnapshotUtility(int snapshotId, string snapshotName, int snapshotFormId, string description, int recordCount, int userId, int overrideValue, string keyIds)
        {
            return Helper.InsertSnapshotUtility(snapshotId, snapshotName, snapshotFormId, description, recordCount, userId, overrideValue, keyIds);
        }

        #endregion

        #region Membership API

        /// <summary>
        /// validation of the user
        /// </summary>
        /// <param name="userNameText">the user name</param>
        /// <param name="passwordText">the password</param>
        /// <returns>validation result</returns>
        public bool Validation(string userNameText, string passwordText)
        {
            try
            {
                string dN = ConfigurationManager.AppSettings["DomainName"].ToString();
                string userName = userNameText.Trim().ToString() + dN;
                string password = passwordText.Trim().ToString();

                if (Membership.ValidateUser(userName, password))
                {
                    return true;
                }
            }
            catch (System.Security.Cryptography.CryptographicException sec)
            {
                sec.Message.ToString();
                sec.Source.ToString();
            }

            return false;
        }

        #endregion

        #region Membership API

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>return dataset</returns>
        public DataSet GetUserInformation(string userName, int applicationId)
        {
            return Helper.GetUserInformation(userName, applicationId);
        }

        /// <summary>
        /// Used To get the Net_Name for a particular User
        /// </summary>
        /// <param name="userFullName">User FullName</param>
        /// <returns>NetName for a particular fullname</returns>
        public DataSet GetUserNetName(string userFullName)
        {
            DataSet userIDDetails = new DataSet();
            userIDDetails = Helper.GetUserNetName(userFullName);
            if (userIDDetails != null && userIDDetails.Tables.Count > 0 && userIDDetails.Tables[0].Rows.Count > 0)
            {
                int.TryParse(userIDDetails.Tables[0].Rows[0][0].ToString(), out this.userIdValue);
            }
            else
            {
                this.userIdValue = 0;
            }

            return userIDDetails;
        }

        /// <summary>
        /// Gets the config information.
        /// </summary>
        /// <returns>returns dataset</returns>
        public DataSet GetConfigInformation()
        {
            return Helper.GetConfigInformation();
        }

        /// <summary>
        /// Gets the state of the authentication.
        /// </summary>
        /// <returns>returns Dataset for GetAuthenticationState</returns>
        public DataSet GetAuthenticationState()
        {
            return Helper.GetAuthenticationState();
        }

        #endregion

        #region SQL Support

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>return dataset</returns>
        public DataSet GetSQLQueryResult(string sqlQuery)
        {
            return Helper.GetSQLQueryResult(sqlQuery);
        }

        /// <summary>
        /// Get the SQL Description
        /// </summary>
        /// <param name="categoryId">Category</param>
        /// <returns>String contains SQLDescription</returns>
        public string GetSQLDescription(int categoryId)
        {
            SQLSupportData sqlsupport = new SQLSupportData();
            sqlsupport = Helper.GetSQLDescription(categoryId);
            return sqlsupport.GetXml();
        }

        /// <summary>
        /// Gets SQLString
        /// </summary>
        /// <param name="categoryId">Category</param>
        /// <param name="sqlId">Description</param>
        /// <returns>String</returns>
        public string GetSQLString(int categoryId, int sqlId)
        {
            SQLSupportData sqlSupport = new SQLSupportData();
            sqlSupport = Helper.GetSQLString(categoryId, sqlId);
            return sqlSupport.GetXml();
        }

        /// <summary>
        /// Get the SQL Category
        /// </summary>
        /// <returns>string</returns>
        public string GetSQLCategory()
        {
            SQLSupportData sqlsupport = new SQLSupportData();
            sqlsupport = Helper.GetSQLCategory();
            return sqlsupport.GetXml();
        }

        /// <summary>
        /// Saves the SQL query.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="description">The description.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>Integer</returns>
        public int SaveSQLQuery(int categoryId, string description, string statement, int moduleId, int userId, int sqlId)
        {
            return Helper.SaveSQLQuery(categoryId, description, statement, moduleId, userId, sqlId);
        }

        /// <summary>
        /// F9015_s the delete query.
        /// </summary>
        /// <param name="sqlId">The SQL id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>QueryId</returns>
        public int F9015_DeleteQuery(int sqlId, int userId)
        {
            return Helper.F9015_DeleteQuery(sqlId, userId);
        }

        #endregion

        #region Countyconfiguration

        #region GEtConfig

        /// <summary>
        /// Gets the county configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">The User id.</param>
        /// <returns>County Config Details</returns>
        public DataSet GetCountyConfiguration(int applicationId, int userId)
        {
            return Helper.GetCountyConfiguration(applicationId, userId);
        }

        #endregion

        #region save

        /// <summary>
        /// Gets the county configuration.
        /// </summary>
        /// <param name="configId">The config id.</param>
        /// <param name="configDescription">The config description.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateCountyConfigDetails(int configId, string configDescription, int userId)
        {
            Helper.UpdateCountyConfigDetails(configId, configDescription, userId);
        }

        #endregion

        #endregion

        #region Mortgage Import Template

        #region Get

        /// <summary>
        /// Gets the Mortgage Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Mortgage Import Template Details</returns>
        public string GetMortgageTemplate(int templateId)
        {
            MortgageImpotTemplateData mortgageImpotTemplateData = new MortgageImpotTemplateData();
            mortgageImpotTemplateData = Helper.GetMortgageTemplate(templateId);
            return mortgageImpotTemplateData.GetXml();
        }

        #endregion

        #region List Mortgage Import Template

        /// <summary>
        /// Lists the mortgage template.
        /// </summary>
        /// <returns>Mortgage template list</returns>
        public string ListMortgageTemplate()
        {
            MortgageImpotTemplateData mortgageImpotTemplateData = new MortgageImpotTemplateData();
            mortgageImpotTemplateData = Helper.ListMortgageTemplate();
            return mortgageImpotTemplateData.GetXml();
        }

        #endregion

        #region List MortgageImportFileType

        /// <summary>
        /// Lists the type of the mortgage import file.
        /// </summary>
        /// <returns>returns the dataset containing the Mortgage Import FileType</returns>
        public string ListMortgageImportFileType()
        {
            MortageImportData mortgageImportData = new MortageImportData();
            mortgageImportData = Helper.ListMortgageImportFileType();
            return mortgageImportData.GetXml();
        }

        #endregion

        #region Save Mortgage Import Template



        /// <summary>
        /// Saves the mortgage import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="statementIdPos">The statement id pos.</param>
        /// <param name="statementIdWid">The statement id wid.</param>
        /// <param name="statementNumPos">The statement num pos.</param>
        /// <param name="statementNumWid">The statement num wid.</param>
        /// <param name="amountPos">The amount pos.</param>
        /// <param name="amountWid">The amount wid.</param>
        /// <param name="commentPos">The comment pos.</param>
        /// <param name="commentWid">The comment wid.</param>
        /// <param name="bankCodePos">The bank code pos.</param>
        /// <param name="bankCodeWid">The bank code wid.</param>
        /// <param name="loanNumPos">The loan num pos.</param>
        /// <param name="loanNumWid">The loan num wid.</param>
        /// <param name="taxPayNamePos">The tax pay name pos.</param>
        /// <param name="taxPayNameWid">The tax pay name wid.</param>
        /// <param name="ParcelNumPos">The parcel num pos.</param>
        /// <param name="ParcelNumWid">The parcel num wid.</param>
        /// <param name="PostTypePos">The post type pos.</param>
        /// <param name="PostTypeWid">The post type wid.</param>
        /// <param name="OwnerIDPos">The owner ID pos.</param>
        /// <param name="OwnerIDWid">The owner ID wid.</param>
        public void SaveMortgageImportTemplate(int templateId, string templateName, int typeId, int userId, string description, string filePath, int statementIdPos, int statementIdWid, int statementNumPos, int statementNumWid, int amountPos, int amountWid, int commentPos, int commentWid, int bankCodePos, int bankCodeWid, int loanNumPos, int loanNumWid, int taxPayNamePos, int taxPayNameWid, int ParcelNumPos, int ParcelNumWid, int PostTypePos, int PostTypeWid, int OwnerIDPos, int OwnerIDWid, int CartIdPos, int CartidWid)
        {
            Helper.SaveMortgageImportTemplate(templateId, templateName, typeId, userId, description, filePath, statementIdPos, statementIdWid, statementNumPos, statementNumWid, amountPos, amountWid, commentPos, commentWid, bankCodePos, bankCodeWid, loanNumPos, loanNumWid, taxPayNamePos, taxPayNameWid, ParcelNumPos, ParcelNumWid, PostTypePos, PostTypeWid, OwnerIDPos, OwnerIDWid,CartIdPos,CartidWid);
        }

        #endregion

        #region Delete Mortgage Template

        /// <summary>
        /// Deletes the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// returns dataset containing templateId and overrideStatus.
        /// </returns>
        public int DeleteMortgageTemplate(int templateId, bool overrideStatus, int userId)
        {
            return Helper.DeleteMortgageTemplate(templateId, overrideStatus, userId);
        }

        #endregion

        #endregion

        #region Mortgage Import

        #region Mortgage Import Statement Ids

        /// <summary>
        /// Gets the Mortgage Import statement Id's
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import statementids.</returns>
        public DataSet GetMortgageImportStatementIds()
        {
            return Helper.GetMortgageImportStatementIds();
        }

        #endregion

        #region MortgageImport Statement

        /// <summary>
        /// Gets the Mortgage Import statement based on the import id
        /// </summary>
        /// <param name="importId">The importEd of the statement to be fetched.</param>
        /// <param name="nextAvailableRecord">true fetch next available record if current record deleted,false previoud record</param>
        /// <returns>
        /// The dataset containing the statement information of the importId.
        /// </returns>
        public string GetMortgageImportStatement(int importId, bool nextAvailableRecord)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.GetMortgageImportStatement(importId, nextAvailableRecord);
            return mortageImportData.GetXml();
        }

        #endregion

        #region Save Mortgage Import Entries

        /// <summary>
        /// Saves Mortgage Import Entries
        /// </summary>
        /// <param name="importId">the Import id</param>
        /// <param name="templateId">The template id</param>
        /// <param name="templateName">The template name</param>
        /// <param name="typeId">The type id</param>
        /// <param name="filePath">The file path</param>
        /// <param name="receiptDate">The receipt date</param>
        /// <param name="interestDate">The interest date</param>
        /// <param name="payCode">The pay code</param>
        /// <param name="userId">the userId</param>
        /// <param name="rollYear">The rollyear</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <param name="mortgageImportEntries">The Mortgage Import Entries</param>
        /// <returns>
        /// The DataSet containg inserted entries details
        /// </returns>
        public string SaveMortgageImportEntries(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfpaycode, string mortgageImportEntries)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.SaveMortgageImportEntries(importId, templateId, templateName, typeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId,firstHalfpaycode, mortgageImportEntries);
            return mortageImportData.GetXml();
        }

        #endregion

        #region Mortgage Import Template Selection

        /// <summary>
        /// Gets the Mortgage Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import Template Details.</returns>
        public string GetMortgageImportTemplateDetails()
        {
            MortgageImportTemplateSelectData mortgageImportTemplateSelectData = new MortgageImportTemplateSelectData();
            mortgageImportTemplateSelectData = Helper.GetMortgageImportTemplateDetails();
            return mortgageImportTemplateSelectData.GetXml();
        }

        #endregion


        #region Permit Import Template Selection

        /// <summary>
        /// Gets the Permit Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Permit Import Template Details.</returns>
        public string GetPermitImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListPermitImportTemplateData permitImportTemplateSelectData = new ListPermitImportTemplateData();
            permitImportTemplateSelectData = Helper.GetPermitImportTemplateDetails(TemplateName, Description, FileType);
            return permitImportTemplateSelectData.GetXml();
        }

        #endregion


        #region MAD Import Template Selection

        /// <summary>
        /// Gets the MAD Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of MAD Import Template Details.</returns>
        public string GetMADImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListMADimportTemplateData MADImportTemplateSelectData = new ListMADimportTemplateData();
            MADImportTemplateSelectData = Helper.GetMADImportTemplateDetails(TemplateName, Description, FileType);
            return MADImportTemplateSelectData.GetXml();
        }

        #endregion

        #region MortgageImport Error Check

        /// <summary>
        /// Method Will Check the Error Records for given parameters
        /// </summary>
        /// <param name="importId">importId</param>
        /// <param name="templateId">templateId</param>
        /// <param name="templateName">templateName</param>
        /// <param name="typeId">typeId</param>
        /// <param name="filePath">filePath</param>
        /// <param name="recieptDate">recieptDate</param>
        /// <param name="interestDate">interestDate</param>
        /// <param name="payCode">payCode</param>
        /// <param name="userId">userId</param>
        /// <param name="rollYear">rollYear</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <returns>
        /// the DataSet Containing the Error Records Information
        /// </returns>
        public string MortgageImportCheckErrors(int importId, int templateId, string templateName, int typeId, string filePath, DateTime recieptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int firstHalfpaycode, int ppaymentId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.MortgageImportCheckErrors(importId, templateId, templateName, typeId, filePath, recieptDate, interestDate, payCode, userId, rollYear,firstHalfpaycode, ppaymentId);
            return mortageImportData.GetXml();
        }

        #endregion

        #region Mortgage Import Check Valid Receipt

        /// <summary>
        /// Check For Valid Receipt
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="receiptDate">The receipt date</param>
        /// <returns>
        /// The DataSet containg valid receipt details
        /// </returns>
        public string CheckMortgageImportValidReceipt(int importId, DateTime receiptDate)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.CheckMortgageImportValidReceipt(importId, receiptDate);
            return mortageImportData.GetXml();
        }

        #endregion

        #region Save Mortgage Import

        /// <summary>
        /// Saves Mortgage Import
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="templateId">The template id</param>
        /// <param name="templateName">The template name</param>
        /// <param name="typeId">The type id</param>
        /// <param name="filePath">The file path</param>
        /// <param name="receiptDate">The receipt date</param>
        /// <param name="interestDate">The interest date</param>
        /// <param name="payCode">The pay code</param>
        /// <param name="userId">The user id</param>
        /// <param name="rollYear">The roll year</param>
        /// <param name="ppaymentId">The ppaymentId</param>
        /// <param name="resetErrorCheck">resetErrorCheck</param>
        /// <returns>
        /// The DataSet containg inserted entries import id
        /// </returns>
        public string SaveMortgageImport(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, bool resetErrorCheck)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.SaveMortgageImport(importId, templateId, templateName, typeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId,firstHalfPayCode, resetErrorCheck);
            return mortageImportData.GetXml();
        }

        #endregion

        #region Create Receipt

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="resetErrorCheck">if set to <c>true</c> [reset error check].</param>
        /// <returns>DataSet Holds the Reciept</returns>
        public string CreateReceipt(int importId, int templateId, string templateName, string filePath, int typeId, DateTime receiptDate, DateTime interestDate, bool payCode, int firstHalfPaycode, int userId, int rollYear, int? ppaymentId, bool resetErrorCheck)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.CreateReceipt(importId, templateId, templateName, filePath, typeId, receiptDate, interestDate, payCode,firstHalfPaycode, userId, rollYear, ppaymentId, resetErrorCheck);
            return mortageImportData.GetXml();
        }
        #endregion  Create Receipt

        #region Delete Mortgage Import

        /// <summary>
        /// Delete Mortgage import record
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">UserID</param>
        /// <returns>The DataSet</returns>
        public string DeleteMortgageImport(int importId, int userId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.DeleteMortgageImport(importId, userId);
            return mortageImportData.GetXml();
        }

        /// <summary>
        /// Delete Mortgage import file entries
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">UserID</param>
        /// <returns>The DataSet</returns>
        public string DeleteMortgageImportFileEntries(int importId, int userId)
        {
            MortageImportData mortageImportData = new MortageImportData();
            mortageImportData = Helper.DeleteMortgageImportFileEntries(importId, userId);
            return mortageImportData.GetXml();
        }

        #endregion

        #endregion

        #region Error Engine

        /// <summary>
        /// Gets the error engine.
        /// </summary>
        /// <param name="errorTypeId">The error type id.</param>
        /// <returns>return Error engine data.</returns>
        public string GetErrorEngine(int errorTypeId)
        {
            //// return Helper.GetErrorEngine(errorTypeId);
            ErrorEngineData errorEngineData = new ErrorEngineData();
            errorEngineData = Helper.GetErrorEngine(errorTypeId);
            return errorEngineData.GetXml();
        }

        /// <summary>
        /// Inserts the error engine.
        /// </summary>
        /// <param name="errorDate">The error date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userIP">The user IP.</param>
        /// <param name="errorTypeId">The error type id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="comment">The comment.</param>
        public void InsertErrorEngine(string errorDate, int userId, string userIP, int errorTypeId, string parameter, string comment)
        {
            Helper.InsertErrorEngine(errorDate, userId, userIP, errorTypeId, parameter, comment);
        }

        #endregion

        #region NextNumber Configuration

        #region List NextNumber Configuration

        /// <summary>
        /// List the NextNumber Configuration details
        /// </summary>
        /// <returns>
        /// The dataset containing the list of NextNumber Configuration.
        /// </returns>
        public string ListNextNumberConfiguration()
        {
            NextNumberData nextNumberData = new NextNumberData();
            nextNumberData = Helper.ListNextNumberConfiguration();
            return nextNumberData.GetXml();
        }

        #endregion

        #region Check Next Number

        /// <summary>
        /// Check for valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>
        /// The dataset containing the valid Next Number details.
        /// </returns>
        public DataSet CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return Helper.CheckNextNumber(rollYear, nextNum, formula);
        }

        #endregion

        #region Update NextNumber ConfigDetails

        /// <summary>
        /// Saves Next Number configuration details
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            Helper.UpdateNextNumberConfigDetails(nextNumId, nextNum, formula, userId);
        }

        #endregion

        #endregion

        #region ExciseWorkQueue

        #region Affidavit Work Queue

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
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>Return Dataset for affidavitWorkQueue</returns>
        public string F1107_ExciseWorkQueue_GetWorkQueueSearchResult(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, string statementNumber)
        {
            AffidavitWorkQueueData affidavitWorkQueue = new AffidavitWorkQueueData();
            affidavitWorkQueue = Helper.F1107_ExciseWorkQueue_GetWorkQueueSearchResult(parcelNumber, name, receiptDate, address, taxCode, treasurer, assessor, statementNumber);
            return affidavitWorkQueue.GetXml();
        }

        #endregion

        #region Management Work Queue

        /// <summary>
        /// F1109_s the list management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="receiptDate">The Receipt date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="treasurer">The treasurer.</param>
        /// <param name="assessor">The assessor.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>Returns ManagementWorkQueue DataSet</returns>
        public string F1109_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, int rollYear, string statementNumber)
        {
            AffidavitManagementQueue managementWorkQueue = new AffidavitManagementQueue();
            managementWorkQueue = Helper.F1109_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, treasurer, assessor, rollYear, statementNumber);
            return managementWorkQueue.GetXml();
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
        public string F1109_ManagementQueueFilterResult(int statusFilterId, int rollYear, string filterFromDate, string filterToDate)
        {
            AffidavitManagementQueue managementWorkQueue = new AffidavitManagementQueue();
            managementWorkQueue = Helper.F1109_ManagementQueueFilterResult(statusFilterId, rollYear, filterFromDate, filterToDate);
            return managementWorkQueue.GetXml();
        }

        /// <summary>
        /// F1109_s the filter search affidavit.
        /// </summary>
        /// <param name="filterXml">The filter XML.</param>
        /// <returns>Returns DataSet</returns>
        public string F1109_FilterSearchAffidavit(string filterXml)
        {
            AffidavitManagementQueue managementWorkQueue = new AffidavitManagementQueue();
            managementWorkQueue = Helper.F1109_FilterSearchAffidavit(filterXml);
            return managementWorkQueue.GetXml();
        }

        /// <summary>
        /// F1109_s the list roll year.
        /// </summary>
        /// <returns>Returns Rollyear DataSet</returns>
        public string F1109_ListRollYear()
        {
            AffidavitManagementQueue managementWorkQueue = new AffidavitManagementQueue();
            managementWorkQueue = Helper.F1109_ListRollYear();
            return managementWorkQueue.GetXml();
        }

        #endregion

        #region Submittal Queue

        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public string F1108_GetSubmitAffidavit(string statementId)
        {
            REETA submitDataset = new REETA();
            submitDataset = Helper.F1108_GetSubmitAffidavit(statementId);
            return submitDataset.GetXml();
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
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>Returns SubmittalQueue dataset</returns>
        public string F1108_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string receiptNumber, string statementNumber)
        {
            SubmittalQueueData submittalDataset = new SubmittalQueueData();
            submittalDataset = Helper.F1108_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, receiptNumber, statementNumber);
            return submittalDataset.GetXml();
        }

        /// <summary>
        /// F1108_s the list configuration detail.
        /// </summary>
        /// <returns>Returns Configuration Detail</returns>
        public string F1108_ListConfigurationDetail()
        {
            SubmittalQueueData submittalDataset = new SubmittalQueueData();
            submittalDataset = Helper.F1108_ListConfigurationDetail();
            return submittalDataset.GetXml();
        }

        /// <summary>
        /// F1108_s the get submit affidavit reply.
        /// </summary>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>Returns SubmitAffidavit reply datatSet</returns>
        public string F1108_GetSubmitAffidavitReply(string reetReplyXml, int userId)
        {
            REETA reetDataset = new REETA();
            reetDataset = Helper.F1108_GetSubmitAffidavitReply(reetReplyXml, userId);
            return reetDataset.GetXml();
        }

        /// <summary>
        /// Used to save the xml for testing
        /// </summary>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public string F1108_SaveReplyReetXml(string reetXml, string reetReplyXml, int userId)
        {
            return Helper.F1108_SaveReplyReetXml(reetXml, reetReplyXml, userId).ToString();
        }
        #endregion

        #endregion

        #region Excise Tax Statement

        #region Get Excise Tax Statement

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="statementId">statementId</param>
        /// <returns>Excise Tax Statement Details</returns>
        public string GetExciseTaxStatement(int statementId)
        {
            ExciseTaxStatementData exciseTaxStatementData = new ExciseTaxStatementData();
            exciseTaxStatementData = Helper.GetExciseTaxStatement(statementId);
            return exciseTaxStatementData.GetXml();
        }

        #endregion

        #region Get Excise Tax Statement

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="statementId">statementId</param>
        /// <returns>Excise Tax Statement Details</returns>
        public string GetExciseTaxReceipt(int statementId)
        {
            ExciseTaxReceiptData exciseTaxReceiptData = new ExciseTaxReceiptData();
            exciseTaxReceiptData = Helper.GetExciseTaxReceipt(statementId);
            return exciseTaxReceiptData.GetXml();
        }

        #endregion

        #region List Excise Tax Statement

        /// <summary>
        /// List the Excise Tax Statemnet ID
        /// </summary>
        /// <returns>Excise Tax Statement ID Details</returns>
        public string ListExciseTaxStatement()
        {
            ExciseTaxStatementData exciseTaxStatementData = new ExciseTaxStatementData();
            exciseTaxStatementData = Helper.ListExciseTaxStatement();
            return exciseTaxStatementData.GetXml();
        }

        #endregion List Excise Tax Statement

        #region Save Tax Receipt

        /// <summary>
        /// Saves the excise tax recipt.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns statementItems</returns>
        public string SaveExciseTaxReceipt(string statementItems, int userId)
        {
            ExciseTaxStatementData exciseTaxStatementData = new ExciseTaxStatementData();
            exciseTaxStatementData = Helper.SaveExciseTaxReceipt(statementItems, userId);
            return exciseTaxStatementData.GetXml();
        }

        #endregion Save Tax Receipt

        #endregion

        #region ExciseTax Affidavit validation

        #region Get ExciseTaxAffidavitStatus

        /// <summary>
        /// Gets the excise tax affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <returns>
        /// The dataset containing statementID status
        /// </returns>
        public string GetExciseTaxAffidavitStatus(int statementId, int treasurerStatus)
        {
            ExciseAffidavitValidationData affidavitValidationData = new ExciseAffidavitValidationData();
            affidavitValidationData = Helper.GetExciseTaxAffidavitStatus(statementId, treasurerStatus);
            return affidavitValidationData.GetXml();
        }

        #endregion Get ExciseTaxAffidavitStatus

        #region  UpdateExciseTaxAffidavitStatus

        /// <summary>
        /// Updates the excise affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateExciseAffidavitStatus(int statementId, int treasurerStatus, int statusId, int userId)
        {
            Helper.UpdateExciseAffidavitStatus(statementId, treasurerStatus, statusId, userId);
        }

        #endregion UpdateExciseTaxAffidavitStatus

        #endregion

        #region Excise Rate District Selection

        #region List Excise Rate District

        /// <summary>
        /// Lists the excise rate district.
        /// </summary>
        /// <param name="district">The district.</param>
        /// <param name="year">The year.</param>
        /// <param name="description">The description.</param>
        /// <returns>return dataset for Excise Rate District</returns>
        public string ListExciseRateDistrict(string district, int year, string description)
        {
            ExciseRateDistrictSelectionData exciseRateDistrictSelectionData = new ExciseRateDistrictSelectionData();
            exciseRateDistrictSelectionData = Helper.ListExciseRateDistrict(district, year, description);
            return exciseRateDistrictSelectionData.GetXml();
        }

        #endregion

        #endregion

        #region Excise District Copy

        #region Get Excise District Copy

        /// <summary>
        /// Get the district and base year details
        /// </summary>
        /// <param name="exciserateId">ExciserateId</param>
        /// <returns>District And base year</returns>
        public string GetExciseDistrictCopy(int exciserateId)
        {
            ExciseDistrictCopyData exciseDistrictCopyData = new ExciseDistrictCopyData();
            exciseDistrictCopyData = Helper.GetExciseDistrictCopy(exciserateId);
            return exciseDistrictCopyData.GetXml();
        }

        #endregion Get Excise District Copy

        #region Save Excise District Copy

        /// <summary>
        /// Save the the excise district copy
        /// The returns values from Database
        /// 0 = When The record is successfully saved.
        /// 1 = When Invalid Source Record
        /// 2 = When Invalid Destination Record
        /// </summary>
        /// <param name="district">The district</param>
        /// <param name="basedOnYear">The based on year.</param>
        /// <param name="newDistrictYear">The new district year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The returns values from Database
        /// 0 = When The record is successfully saved.
        /// 1 = When Invalid Source Record
        /// 2 = When Invalid Destination Record
        /// </returns>
        public int SaveExciseDistrcitCopy(int district, int basedOnYear, int newDistrictYear, int userId)
        {
            return Helper.SaveExciseDistrcitCopy(district, basedOnYear, newDistrictYear, userId);
        }

        #endregion Save Excise District Copy

        #endregion Excise District Copy

        #region MasterNameSearch

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>Returns MasterNameSearchData dataset</returns>
        public string GetMasterNameSearch(string lastName, string firstName, string address)
        {
            MasterNameSearchData masterNameSearch = new MasterNameSearchData();
            masterNameSearch = Helper.GetMasterNameSearch(lastName, firstName, address);
            return masterNameSearch.GetXml();
        }

        #endregion

        #region ExciseTaxAffidavit

        /// <summary>
        /// Gets the IndividualType Used To Fill The Type Combo
        /// </summary>
        /// <returns>returun string with type details</returns>
        public string GetExciseIndividualType()
        {
            ExciseIndividualType exciseIndividualType = new ExciseIndividualType();
            exciseIndividualType = Helper.GetExciseIndividualType();
            return exciseIndividualType.GetXml();
        }

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>
        /// Retruns excise Tax Affidatvit Details For all six header
        /// </returns>
        public string GetExciseTaxAffidavitDetails(int statementId)
        {
            ExciseTaxAffidavitData exciseData = new ExciseTaxAffidavitData();
            exciseData = Helper.GetExciseTaxAffidavitDetails(statementId);
            return exciseData.GetXml();
        }

        /// <summary>
        /// Get Amount Due Details
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Amount Due Details</returns>
        public string GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            ExciseTaxAffidavitAmountDueData exciseTaxAffidavitAmountDue = new ExciseTaxAffidavitAmountDueData();
            exciseTaxAffidavitAmountDue = Helper.GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateId, taxCode, taxableSaleAmount);
            return exciseTaxAffidavitAmountDue.GetXml();
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public string GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            ExciseTaxAffidavitData affidavitStatementIdData = new ExciseTaxAffidavitData();
            affidavitStatementIdData = Helper.GetAffidavitStatementId(formId, orderField, orderBy);
            return affidavitStatementIdData.GetXml();
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// returns dataset contains AffiDavit Details
        /// </returns>
        public int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, int userId)
        {
            return Helper.SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, userId);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>
        /// returns dastaset containing Owner Details
        /// </returns>
        public string GetOwnerDetails(int ownerId)
        {
            PartiesOwnerDetailsData ownerDetailData = new PartiesOwnerDetailsData();
            ownerDetailData = Helper.GetOwnerDetails(ownerId);
            return ownerDetailData.GetXml();
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public string GetDistrictSelection(int exciseRateId)
        {
            AffidavitDistrictSelectionData districtSelection = new AffidavitDistrictSelectionData();
            districtSelection = Helper.GetDistrictSelection(exciseRateId);
            return districtSelection.GetXml();
        }

        /// <summary>
        /// Deletet The Particular StatmentID Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="userId">UserID</param>
        public void DeleteAffidavitDetails(int statementId, int userId)
        {
            Helper.DeleteAffidavitDetails(statementId, userId);
        }

        /// <summary>
        /// Executes the affdvt query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>Returns ExecuteAffdvtQuery Dataset</returns>
        public string ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn)
        {
            QueryByFormData queryByForm = new QueryByFormData();
            queryByForm = Helper.ExecuteAffdvtQuery(formId, whereCondnSql, orderByCondn);
            return queryByForm.GetXml();
        }

        #endregion

        #region Excise Tax Rate

        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>
        /// returns dataset contains Excise Tax Rate Details
        /// </returns>
        public string GetExciseTaxRate(int exciseRateId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            exciseTaxRateData = Helper.GetExciseTaxRate(exciseRateId);
            return exciseTaxRateData.GetXml();
        }
        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public string ListExciseTaxRate()
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            exciseTaxRateData = Helper.ListExciseTaxRate();
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// Save the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">The user id.</param>
        public void SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            Helper.SaveExciseTaxRate(exciseRateId, exciseTaxDetails, userId);
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">The excise rate Id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            return Helper.DeleteExciseTaxRate(exciseRateId, userId);
        }

        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public string GetDistrictName(int districtId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            exciseTaxRateData = Helper.GetDistrictName(districtId);
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public string GetAccountName(int accountId)
        {
            ExciseTaxRateData exciseTaxRateData = new ExciseTaxRateData();
            exciseTaxRateData = Helper.GetAccountName(accountId);
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #endregion

        #region Account Slection

        #region Get Account Slection Data

        /// <summary>
        /// Gets the account selection data.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="objectname">The objectname.</param>
        /// <param name="line">The line.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="desciption">The desciption.</param>
        /// <param name="iscash">The iscash.</param>
        /// <returns>The account selection dataset string.</returns>
        public string GetAccountSelectionData(string subFund, string bars, string functionName, string objectname, string line, int rollYear, string desciption, int iscash)
        {
            AccountSelectionData accountSelectionData = new AccountSelectionData();
            accountSelectionData = Helper.GetAccountSelectionData(subFund, bars, functionName, objectname, line, rollYear, desciption, iscash);
            return accountSelectionData.GetXml();
        }

        #endregion Account Slection data

        #endregion Account Slection

        #region F1512 District Slection

        #region Get District Slection Data

        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtID">The district ID.</param>
        /// <param name="district">The district</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Returns Distrcit Selection details</returns>
        public string F1512_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            F1512DistrictSelectionData districtSelectionData = new F1512DistrictSelectionData();
            districtSelectionData = Helper.F1512_GetDistrictSelectionData(districtId, district, description, rollYear);
            return districtSelectionData.GetXml();
        }

        #endregion Get District Slection data

        #endregion F1512 District Slection

        #region Help Engine

        #region List Help Engine

        /// <summary>
        /// Lists the Help Documents
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns>
        /// returns dataset contains Help Form details
        /// </returns>
        public string ListHelpDocumentForm(string formFile)
        {
            HelpEngineData helpEngineData = new HelpEngineData();
            helpEngineData = Helper.ListHelpDocumentForm(formFile);
            return helpEngineData.GetXml();
        }

        #endregion

        #endregion

        #region GDocEventEngine

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="eventEngineInsertData">The event engine insert data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns eventEngineData</returns>
        public int InsertGDocEventEngineData(string eventEngineInsertData, int userId)
        {
            return Helper.InsertGDocEventEngineData(eventEngineInsertData, userId);
        }

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>returns eventEngineData</returns>
        public int GetGDocEventEngineFeatureClassId(int featureId)
        {
            return Helper.GetGDocEventEngineFeatureClassId(featureId);
        }

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>
        /// String Contians ListEventTypeStatusDetails
        /// </returns>
        public string ListEventTypeStatusDetails(int featureClassId)
        {
            GDocEventEngineTypeStatusData gdocEventEngineTypeStatusData = new GDocEventEngineTypeStatusData();
            gdocEventEngineTypeStatusData = Helper.ListEventTypeStatusDetails(featureClassId);
            return gdocEventEngineTypeStatusData.GetXml();
        }

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>
        /// String Contians ListEventTypeStatusDetails
        /// </returns>
        public string LoadEventEngineData(int featureClassId, int featureId)
        {
            GDocEventEngineData gdocEventEngineData = new GDocEventEngineData();
            gdocEventEngineData = Helper.LoadEventEngineData(featureClassId, featureId);
            return gdocEventEngineData.GetXml();
        }

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>
        /// String Contians ListEventTypeStatusDetails
        /// </returns>
        public string GetEventEngineDataHeader(int featureClassId, int featureId)
        {
            GDocEventEngineData gdocEventEngineData = new GDocEventEngineData();
            gdocEventEngineData = Helper.GetEventEngineDataHeader(featureClassId, featureId);
            return gdocEventEngineData.GetXml();
        }

        #region Active Work Order Details

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureClassId">The featureClass id.</param>
        /// <returns>
        /// typed dataset containing the WOID,Date,Type and Comments
        /// </returns>
        public string GetWorkOrderDetails(int featureClassId)
        {
            GDocWorkOrderData docWorkOrderData = new GDocWorkOrderData();
            docWorkOrderData = Helper.GetWorkOrderDetails(featureClassId);
            return docWorkOrderData.GetXml();
        }

        #endregion Active Work Order Details

        #endregion

        #region GDoc Comment

        #region Get GDoc Comment

        /// <summary>
        /// Gets the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// Typed Dataset containing the GDoc comment
        /// </returns>
        public string GetGDocComment(int eventId)
        {
            GDocCommentData gdocCommentData = new GDocCommentData();
            gdocCommentData = Helper.GetGDocComment(eventId);
            return gdocCommentData.GetXml();
        }

        #endregion Get GDoc Comment

        #region Save GDoc Comment

        /// <summary>
        /// Saves the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="comment">The GDoc comment.</param>
        /// <param name="userId">The user id.</param>
        public void SaveGDocComment(int eventId, string comment, int userId)
        {
            Helper.SaveGDocComment(eventId, comment, userId);
        }

        #endregion Save GDoc Comment

        #endregion GDoc Comment

        #region GDoc Event Header

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// Typed dataset containing the Event,Event date,Work Order and Is complete.
        /// </returns>
        public string GetGDocEventHeader(int eventId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            gdocEventHeaderData = Helper.GetGDocEventHeader(eventId);
            return gdocEventHeaderData.GetXml();
        }

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>
        /// Typed status containing Event Engine status.
        /// </returns>
        public string ListGDocEventHeaderStatus(int eventId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            gdocEventHeaderData = Helper.ListGDocEventHeaderStatus(eventId);
            return gdocEventHeaderData.GetXml();
        }

        #endregion ListGDocEventHeaderStatus

        #region DeleteGDocEventHeader

        /// <summary>
        /// Deletes the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="childFlag">The child flag.</param>
        /// <param name="userId">GDocEventHeader</param>
        public void DeleteGDocEventHeader(int eventId, int childFlag, int userId)
        {
            Helper.DeleteGDocEventHeader(eventId, childFlag, userId);
        }

        #endregion DeleteGDocEventHeader

        #region SaveGDocEventHeader

        /// <summary>
        /// Saves the GDoc event header.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed dataset</returns>
        public string SaveGDocEventHeader(string eventItems, int userId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            gdocEventHeaderData = Helper.SaveGDocEventHeader(eventItems, userId);
            return gdocEventHeaderData.GetXml();
        }

        #endregion SaveGDocEventHeader

        #endregion GDoc Event Header

        #region GDoc Work Order Engine

        #region GetSystemID

        /// <summary>
        /// Gets the system id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <returns>The System Id.</returns>
        public int GetSystemId(int userId, int formNumber)
        {
            return Helper.GetSystemId(userId, formNumber);
        }

        #endregion GetSystemId

        #region GetWorkOrderEngine

        /// <summary>
        /// Gets the work order engine.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <param name="isopen">The is open.</param>
        /// <returns>
        /// Typed Dataset containing the Work Order Engine Values
        /// </returns>
        public string F8901_GetWorkOrderEngine(int systemId, int isopen)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            gdocWorkOrderEngineData = Helper.F8901_GetWorkOrderEngine(systemId, isopen);
            return gdocWorkOrderEngineData.GetXml();
        }

        #endregion GetWorkOrderEngine

        #region GetWorkOrderType

        /// <summary>
        /// Gets the type of the work order.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <returns>
        /// Typed Dataset containing the Work Order Type Values
        /// </returns>
        public string F8901_GetWorkOrderType(int systemId)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            gdocWorkOrderEngineData = Helper.F8901_GetWorkOrderType(systemId);
            return gdocWorkOrderEngineData.GetXml();
        }

        #endregion  GetWorkOrderType

        #region SaveWorkOrderEngine

        /// <summary>
        /// Saves the work order engine.
        /// </summary>
        /// <param name="workOrderItems">The work order items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed Dataset containing the Work Order Engine Values
        /// </returns>
        public string F8901_SaveWorkOrderEngine(string workOrderItems, int userId)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            gdocWorkOrderEngineData = Helper.F8901_SaveWorkOrderEngine(workOrderItems, userId);
            return gdocWorkOrderEngineData.GetXml();
        }

        #endregion SaveWorkOrderEngine

        #endregion GDoc Work Order Engine

        #region GDoc Work order CallIn

        #region Get GDoc Work order CallIn

        /// <summary>
        /// Get work order call In values  for F8912.
        /// </summary>
        /// <param name="workorderId">The work order id.</param>
        /// <returns>
        /// Typed DataSet Containing the Gdoc Work Order CallIn Values
        /// </returns>
        public string F8912_GetWorkOrderCallIn(int workorderId)
        {
            GDocWorkorderCallInData gdocWorkorderCallInData = new GDocWorkorderCallInData();
            gdocWorkorderCallInData = Helper.F8912_GetWorkOrderCallIn(workorderId);
            return gdocWorkorderCallInData.GetXml();
        }

        #endregion Get GDoc Work order CallIn

        #region Get GDoc Addresses

        /// <summary>
        /// To Get Addresses for GDOC Form Slices.
        /// </summary>
        /// <returns>
        /// Typed DataSet Containing the Gdoc Addresses
        /// </returns>
        public string wListAddresses()
        {
            GDocWorkorderCallInData gdocWorkorderCallInData = new GDocWorkorderCallInData();
            gdocWorkorderCallInData = Helper.wListAddresses();
            return gdocWorkorderCallInData.GetXml();
        }

        #endregion Get GDoc Addresses

        #region Save GDoc Work order CallIn

        /// <summary>
        /// Save GDoc work order call In Values.
        /// </summary>
        /// <param name="workOrderCall">The work order call.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing the Gdoc Work Order CallIn Values
        /// </returns>
        public string F8912_SaveWorkOrderCallIn(string workOrderCall, int userId)
        {
            GDocWorkorderCallInData gdocWorkorderCallInData = new GDocWorkorderCallInData();
            gdocWorkorderCallInData = Helper.F8912_SaveWorkOrderCallIn(workOrderCall, userId);
            return gdocWorkorderCallInData.GetXml();
        }

        #endregion Save GDoc Work order CallIn

        #endregion GDoc Work order CallIn

        #region GDoc Work order General

        #region Get GDoc Work order General

        /// <summary>
        /// Get work order general values for F8910.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>
        /// Typed DataSet containing the GDoc Work order General Values
        /// </returns>
        public string F8910_GetWorkOrderGeneral(int workorderId)
        {
            GDocWorkOrderGeneralData gdocWorkOrderGeneralData = new GDocWorkOrderGeneralData();
            gdocWorkOrderGeneralData = Helper.F8910_GetWorkOrderGeneral(workorderId);
            return gdocWorkOrderGeneralData.GetXml();
        }

        #endregion Get GDoc Work order General

        #region Save GDoc Work order General

        /// <summary>
        /// Save work order general values for F8910.
        /// </summary>
        /// <param name="workOrderGeneral">The work order general.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet containing the GDoc Work order General Values
        /// </returns>
        public string F8910_SaveWorkOrderGeneral(string workOrderGeneral, int userId)
        {
            GDocWorkOrderGeneralData gdocWorkOrderGeneralData = new GDocWorkOrderGeneralData();
            gdocWorkOrderGeneralData = Helper.F8910_SaveWorkOrderGeneral(workOrderGeneral, userId);
            return gdocWorkOrderGeneralData.GetXml();
        }

        #endregion Save GDoc Work order General

        #endregion GDoc Work order General

        #region Stoppage Event Details
        /// <summary>
        /// Gets the Stoppage event Details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>String</returns>
        public string F8016_GetStoppageEventDetails(int eventId)
        {
            StoppageEventData stoppageEventData = new StoppageEventData();
            stoppageEventData = Helper.F8016_GetStoppageEventDetails(eventId);
            return stoppageEventData.GetXml();
        }

        /// <summary>
        /// Saves the Stoppage event Details to DB
        /// </summary>
        /// <param name="eventItems">Stoppage Event Details as XML of Type string is passed</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        /// ///
        public string F8016_SaveStoppageEventDetails(string eventItems, int userId)
        {
            StoppageEventData stoppageEventData = new StoppageEventData();
            stoppageEventData = Helper.F8016_SaveStoppageEventDetails(eventItems, userId);
            return stoppageEventData.GetXml();
        }

        #endregion Stoppage Event Details

        #region Time Footer
        /// <summary>
        /// Gets the Time Footer Details
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="formId">form Id</param>
        /// <returns>XML as String</returns>
        public string F8042_GetTimeFooterDetails(int eventId, int formId)
        {
            TimeFooterData timeFooterData = new TimeFooterData();
            timeFooterData = Helper.F8042_GetTimeFooterDetails(eventId, formId);
            return timeFooterData.GetXml();
        }
        #endregion Time Footer

        #region Materials Footer
        /// <summary>
        /// Gets the Materials Footer Details
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="formId">form Id</param>
        /// <returns>XML as String</returns>
        /// ///
        public string F8046_GetMaterialsFooterDetails(int eventId, int formId)
        {
            MaterialsFooterData materialsFooterData = new MaterialsFooterData();
            materialsFooterData = Helper.F8046_GetMaterialsFooterDetails(eventId, formId);
            return materialsFooterData.GetXml();
        }
        #endregion Materials Footer

        #region ParcelheaderSlim
        /// <summary>
        /// Gets the ParcelheaderSlim Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>XML as String</returns>
        public string F27007_GetParcelHeaderSlimDetails(int parcelId)
        {
            F27007ParcelHeaderSlimData parcelHeaderSlimData = new F27007ParcelHeaderSlimData();
            parcelHeaderSlimData = Helper.F27007_GetParcelHeaderSlimDetails(parcelId);
            return parcelHeaderSlimData.GetXml();
        }
        #endregion ParcelheaderSlim

        #region StatementheaderSlim

        /// <summary>
        /// Gets the StatementheaderSlim Details
        /// </summary>
        /// <param name="statementlId">The statementl id.</param>
        /// <returns>XML as String</returns>
        public string F15016_GetStatementHeaderSlimDetails(int statementlId)
        {
            F15016StatementHeaderData statementHeaderData = new F15016StatementHeaderData();
            statementHeaderData = Helper.F15016_GetstatementHeaderSlimDetails(statementlId);
            return statementHeaderData.GetXml();
        }

        #endregion StatementheaderSlim

        #region MakeDeposits

        #region GetPaymentItemsDetails

        /// <summary>
        /// Gets the payment items details.
        /// </summary>
        /// <returns>
        /// string which Contains the PaymentItemsDetials
        /// </returns>
        public string GetPaymentItemsDetails()
        {
            MakeDepositsData makeDepositsData = new MakeDepositsData();
            makeDepositsData = Helper.GetPaymentItemsDetails();
            return makeDepositsData.GetXml();
        }

        #endregion

        #region SavePaymentItemsDetails

        /// <summary>
        /// Saves the payment items details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsDetails">The payment items details.</param>
        public void SavePaymentItemsDetails(int registerId, decimal amount, int userId, string paymentItemsDetails)
        {
            Helper.SavePaymentItemsDetails(registerId, amount, userId, paymentItemsDetails);
        }

        #endregion

        #endregion

        #region PostingHistory

        #region ListPostTypes

        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>returns PostTypes</returns>
        public string ListPostTypes(int form)
        {
            PostingHistoryData postingHistoryData = new PostingHistoryData();
            postingHistoryData.ListPostType.Clear();
            postingHistoryData.ListPostType.Merge(Helper.ListPostTypes(form));
            ////DataSet ds = new DataSet();
            ////if (ds.Tables.Count != 0)
            ////{
            ////    ds.Tables.Clear();
            ////}
            ////ds.Tables.Add (postingHistoryData.ListPostType) ;
            return postingHistoryData.GetXml();
            ////return ds.GetXml(); 
        }

        #endregion

        #region ListPostingHistory

        /// <summary>
        /// Lists the posting history.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="postTypeId">The post type id.</param>
        /// <returns>the string</returns>
        public string ListPostingHistory(int count, int postTypeId)
        {
            PostingHistoryData postingHistoryData = new PostingHistoryData();
            ////DataSet ds = new DataSet();
            postingHistoryData.ListPostingHistory.Clear();
            postingHistoryData.ListPostingHistory.Merge(Helper.ListPostingHistory(count, postTypeId));
            ////ds.Tables.Add(postingHistoryData.ListPostingHistory);
            return postingHistoryData.GetXml();
        }

        #endregion

        #endregion

        #region PostingErrors

        #region ListPostErrors

        /// <summary>
        /// Lists the posting errors.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>postingErrorsData</returns>
        public string ListPostingErrors(int userId)
        {
            PostingErrorsData postingErrorsData = new PostingErrorsData();
            postingErrorsData = Helper.ListPostErrors(userId);
            return postingErrorsData.GetXml();
        }

        #endregion

        #region InsertAccount

        /// <summary>
        /// Inserts the account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="errorTypeId">The error type id.</param>
        public void InsertAccount(int userId, int errorTypeId)
        {
            Helper.InsertAccount(userId, errorTypeId);
        }

        #endregion

        #endregion

        #region Posting

        #region ListPostTypes

        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <returns>the string</returns>
        public string ListPostingQueue()
        {
            PostingData postingData = new PostingData();
            postingData = Helper.ListPostTypes();
            return postingData.GetXml();
        }

        #endregion

        #region ListPostingHistory

        /// <summary>
        /// Lists the preview posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>the string</returns>
        public string ListPreviewPosting(DateTime postDate)
        {
            PostingData postingData = new PostingData();
            postingData = Helper.ListPreviewPosting(postDate);
            return postingData.GetXml();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Clears the temporary records.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void ClearTemporaryRecords(int userId)
        {
            Helper.ClearTemporaryRecords(userId);
        }

        #endregion Delete

        #region Save

        /// <summary>
        /// Compiles the posting record set.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the string</returns>
        public string CompilePostingRecordSet(DateTime postDate, string selectedPostTypes, int userId)
        {
            PostingData postingData = new PostingData();
            postingData = Helper.CompilePostingRecordSet(postDate, selectedPostTypes, userId);
            return postingData.GetXml();
        }

        /// <summary>
        /// Performs the posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the string</returns>
        public string PerformPosting(DateTime postDate, string selectedPostTypes, int userId)
        {
            PostingData postingData = new PostingData();
            postingData = Helper.PerformPosting(postDate, selectedPostTypes, userId);
            return postingData.GetXml();
        }

        #endregion Save

        #endregion

        #region Reverse GL Post

        #region Get PostId Details

        /// <summary>
        /// Gets the post id details.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>returns GetPostIdDetails</returns>
        public string GetPostIdDetails(int postId)
        {
            PostIdDetailsData postIdDetailsData = new PostIdDetailsData();
            postIdDetailsData = Helper.GetPostIdDetails(postId);
            return postIdDetailsData.GetXml();
        }

        #endregion

        #region Inset Reverse GL Post

        /// <summary>
        /// Inserts the reverse GL post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        public void InsertReverseGLPost(int postId, int userId)
        {
            Helper.InsertReverseGLPost(postId, userId);
        }

        #endregion

        #endregion

        #region Sanitary Pipe Inspection

        #region Get Event Properties

        /// <summary>
        /// Gets the Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Event Properties Details</returns>
        public string GetEventEngineEventProperties(int eventId)
        {
            SanitaryPipeInspectionData sanitaryPipeInspectionData = new SanitaryPipeInspectionData();
            sanitaryPipeInspectionData = Helper.GetEventEngineEventProperties(eventId);
            return sanitaryPipeInspectionData.GetXml();
        }

        #endregion

        #region Save Event Properties

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void SaveEventEngineEventProperties(string eventItems, int userId)
        {
            Helper.SaveEventEngineEventProperties(eventItems, userId);
        }

        #endregion

        #endregion

        #region Sanitary Pipe Inspection Details

        #region List Event Engine TV Details

        /// <summary>
        /// Lists the EventEngineTVDetails
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains EventEngine TVDetails</returns>
        public string ListEventEngineTVDetails(int eventId)
        {
            SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            sanitaryPipeInspectionDetailsData = Helper.ListEventEngineTVDetails(eventId);
            return sanitaryPipeInspectionDetailsData.GetXml();
        }

        #endregion

        #region List EventEngine Detail Types

        /// <summary>
        /// Lists the EventEngine DetailTypes
        /// </summary>
        /// <returns>returns dataset contains DetailTypes</returns>
        public string ListEventEngineDetailTypes()
        {
            SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            sanitaryPipeInspectionDetailsData = Helper.ListEventEngineDetailTypes();
            return sanitaryPipeInspectionDetailsData.GetXml();
        }

        #endregion

        #region Save EventEngine TV Details

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void SaveEventEngineTVDetails(string eventItems, int userId)
        {
            Helper.SaveEventEngineTVDetails(eventItems, userId);
        }

        #endregion

        #region Update EventEngine TV Details

        /// <summary>
        /// Updates the Event Engine TV Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateEventEngineTVDetails(string eventItems, int userId)
        {
            Helper.UpdateEventEngineTVDetails(eventItems, userId);
        }

        #endregion

        #region Delete EventEngine TV Details

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="detailId">The detail id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int DeleteEventEngineTVDetails(int detailId, int userId)
        {
            return Helper.DeleteEventEngineTVDetails(detailId, userId);
        }

        #endregion

        #endregion

        #region FormMaster

        #region GetSandwichAndItsSliceInformation

        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// returns Sandwich And Its Slice Information
        /// </returns>
        public string GetSandwichAndItsSliceInformation(int form, int keyId, int userId)
        {
            FormMasterData formMasterDataSet = new FormMasterData();
            formMasterDataSet = Helper.GetSandwichAndItsSliceInformation(form, keyId, userId);
            return formMasterDataSet.GetXml();
        }

        #endregion GetSandwichAndItsSliceInformation

        #region GetSandwichSubTitleInformation

        /// <summary>
        /// Gets the sandwich sub title information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Sandwich And Its Slice Information</returns>
        public string GetSandwichSubTitleInformation(int form, int keyId, int userId)
        {
            FormMasterData formMasterDataSet = new FormMasterData();
            formMasterDataSet = Helper.GetSandwichSubTitleInformation(form, keyId, userId);
            return formMasterDataSet.GetXml();
        }

        #endregion GetSandwichSubTitleInformation

        #endregion FormMaster

        #region SupportFormCall

        #region GetFormDetails

        /// <summary>
        /// Get FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        public string GetFormDetails(int form, int userId)
        {
            SupportFormData supportForm = new SupportFormData();
            supportForm = Helper.GetFormDetails(form, userId);
            return supportForm.GetXml();
        }

        public string GetParcelDetails(int keyID, bool IsNext)
        {
            F25006ParcelNavigation parcelDetails = new F25006ParcelNavigation();
            parcelDetails = Helper.GetParcelDetails(keyID, IsNext);
            return parcelDetails.GetXml();
        }
        /// <summary>
        /// Gets the translated form details.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>DataSet</returns>
        public string GetTranslatedFormDetails(int formNo, string keyValue)
        {
            SupportFormData supportForm = new SupportFormData();
            supportForm = Helper.GetTranslatedFormDetails(formNo, keyValue);
            return supportForm.GetXml();
        }

        #endregion

        #region GetFormMangementDetails

        /// <summary>
        /// F9002_s the get form management details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed dataset</returns>
        public string F9002_GetFormManagementDetails(int form, int userId)
        {
            SupportFormData form9002GetFormMgmtData = new SupportFormData();
            form9002GetFormMgmtData = Helper.F9002_GetFormManagementDetails(form, userId);
            return form9002GetFormMgmtData.GetXml();
        }

        #endregion GetFormMangementDetails

        #region ListUserNames

        /// <summary>
        /// List UserNames
        /// </summary>
        /// <returns>String</returns>
        public string ListUserNames()
        {
            SupportFormData supportForm = new SupportFormData();
            supportForm = Helper.ListUserNames();
            return supportForm.GetXml();
        }

        #endregion

        #endregion

        #region Deposit History

        #region List DepositHistroy Details

        /// <summary>
        /// Lists the deposit history details.
        /// </summary>
        /// <returns>string Contains the Deposit History Details</returns>
        public string ListDepositHistoryDetails()
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            depositHistoryData = Helper.ListDepositHistoryDetails();
            return depositHistoryData.GetXml();
        }

        #endregion

        #region Get DepositHistory Serach Results

        /// <summary>
        /// Gets the deposit history search result.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>String contains the SearchResults</returns>
        public string GetDepositHistorySearchResult(int form, string whereCondnSql)
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            depositHistoryData = Helper.GetDepositHistorySearchResult(form, whereCondnSql);
            return depositHistoryData.GetXml();
        }

        #endregion

        #region Update Deposit History

        /// <summary>
        /// Updates the deposit history.
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        public void UpdateDepositHistory(int clid, int userId)
        {
            Helper.UpdateDepositHistory(clid, userId);
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>String containing the AccountNames.</returns>
        public string ListAccountNames()
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            depositHistoryData.ListAccountName.Clear();
            depositHistoryData.ListAccountName.Merge(Helper.ListAccountNames());
            return depositHistoryData.GetXml();
        }

        #endregion

        #endregion

        #region Linear Event Type

        #region Get Linear Event Type

        /// <summary>
        /// Gets the Linear Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Linear Event Properties</returns>
        public string GetLinearEventType(int eventId)
        {
            LinearEventData linearEventData = new LinearEventData();
            linearEventData = Helper.GetLinearEventType(eventId);
            return linearEventData.GetXml();
        }

        #endregion

        #region Save Linear Event Type

        /// <summary>
        /// Save the Linear Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void SaveLinearEventType(string eventItems, int userId)
        {
            Helper.SaveLinearEventType(eventItems, userId);
        }

        #endregion

        #endregion

        #region Point Event Type

        #region Get Point Event Type

        /// <summary>
        /// Gets the Point Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// returns dataset contains Point Event Properties
        /// </returns>
        public string GetPointEventType(int eventId)
        {
            PointEventData pointEventData = new PointEventData();
            pointEventData = Helper.GetPointEventType(eventId);
            return pointEventData.GetXml();
        }

        #endregion

        #region Save Point Event Type

        /// <summary>
        /// Save the Point Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void SavePointEventType(string eventItems, int userId)
        {
            Helper.SavePointEventType(eventItems, userId);
        }

        #endregion

        #endregion

        #region 1211 DisbursementCheckStaging

        /// <summary>
        /// Gets the disbursement check list.
        /// </summary>
        /// <returns>Returns Datset</returns>
        public string F1211_GetDisbursementCheckList()
        {
            DisbursementCheckStagingData disbursementCheck = new DisbursementCheckStagingData();
            disbursementCheck = Helper.F1211_GetDisbursementCheckList();
            return disbursementCheck.GetXml();
        }

        /// <summary>
        /// F1211_s the update check staging.
        /// </summary>
        /// <param name="tclId">The TCL id.</param>
        /// <param name="disbursementCheck">The disbursement check.</param>
        /// <param name="checkItems">The check items.</param>
        /// <param name="userId">User Id</param>
        public void F1211_UpdateCheckStaging(int tclId, string disbursementCheck, string checkItems, int userId)
        {
            Helper.F1211_UpdateCheckStaging(tclId, disbursementCheck, checkItems, userId);
        }

        /// <summary>
        /// F1211_s the update agency valid status.
        /// </summary>
        /// <param name="tclIds">The TCL ids.</param>
        /// <param name="validStatus">The valid status.</param>
        /// <param name="userId">User Id</param>
        public void F1211_UpdateAgencyValidStatus(string tclIds, int validStatus, int userId)
        {
            Helper.F1211_UpdateAgencyValidStatus(tclIds, validStatus, userId);
        }

        /// <summary>
        /// F1211_s the delete check staging.
        /// </summary>
        /// <param name="tclIdDelete">The TCL id delete.</param>
        /// <param name="userId">User Id</param>
        public void F1211_DeleteCheckStaging(string tclIdDelete, int userId)
        {
            Helper.F1211_DeleteCheckStaging(tclIdDelete, userId);
        }

        /// <summary>
        /// F1211_s the create checks.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="createChecksTclId">The create checks TCL id.</param>
        /// <returns>Returns Count</returns>
        public int F1211_CreateChecks(int userId, string createChecksTclId)
        {
            return Helper.F1211_CreateChecks(userId, createChecksTclId);
        }

        #endregion

        #region Inspection Event

        #region List Inspection Details

        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// returns dataset contains Inspection Details
        /// </returns>
        public string F8056_ListInspectionDetails(int eventId)
        {
            InspectionEventData inspectionEventData = new InspectionEventData();
            inspectionEventData = Helper.F8056_ListInspectionDetails(eventId);
            return inspectionEventData.GetXml();
        }

        #endregion

        #region Save Inspection Details

        /// <summary>
        /// Save the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void F8056_SaveInspectionDetails(string eventItems, int userId)
        {
            Helper.F8056_SaveInspectionDetails(eventItems, userId);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// Updates the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        public void F8056_UpdateInspectionDetails(string eventItems, int userId)
        {
            Helper.F8056_UpdateInspectionDetails(eventItems, userId);
        }

        #endregion

        #region Delete a Inspection Item

        /// <summary>
        /// Deletes the Inspection Item
        /// </summary>
        /// <param name="inspectionId">The inspection id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int F8056_DeleteInspectionDetails(int inspectionId, int userId)
        {
            return Helper.F8056_DeleteInspectionDetails(inspectionId, userId);
        }

        #endregion

        #endregion

        #region 1210 Disbursement

        #region List Disbursement Agency/SubFund Details

        /// <summary>
        /// F1210_s the get disbursement details.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>string contains the Disbursement Details</returns>
        public string F1210_GetDisbursementDetails(DateTime postDate)
        {
            DisbursementData disbursementData = new DisbursementData();
            disbursementData = Helper.F1210_GetDisbursementDetails(postDate);
            return disbursementData.GetXml();
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// F1210_s the disbursement account names.
        /// </summary>
        /// <returns>String containing the AccountNames.</returns>
        public string F1210_DisbursementAccountNames()
        {
            DisbursementData disbursementData = new DisbursementData();
            disbursementData.ListAccountName.Clear();
            disbursementData.ListAccountName.Merge(Helper.F1210_DisbursementAccountNames());
            return disbursementData.GetXml();
        }

        #endregion

        #region Save Disbursement

        /// <summary>
        /// F1210s the save disbursement.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="postDate">The post date.</param>
        /// <param name="agencies">The agencies.</param>
        /// <param name="overrideStatus">The override status.</param>
        /// <returns>bit Value to Override the Checks</returns>
        public int F1210_SaveDisbursement(int registerId, int userId, DateTime postDate, string agencies, int overrideStatus)
        {
            return Helper.F1210_SaveDisbursement(registerId, userId, postDate, agencies, overrideStatus);
        }

        #endregion

        #endregion

        #region 1214 Refund Management

        #region List AccontNames

        /// <summary>
        /// F1214 the account names.
        /// </summary>
        /// <returns>String Contains the Account Names</returns>
        public string F1214_AccountNames()
        {
            RefundManagementData refundManagementData = new RefundManagementData();
            refundManagementData.ListAccountNames.Clear();
            refundManagementData.ListAccountNames.Merge(Helper.F1214_AccountNames());
            return refundManagementData.GetXml();
        }

        #endregion

        #region List RefundPayments

        /// <summary>
        /// F1214_s the list refund payments.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>RefundPayments dataset</returns>
        public string F1214_ListRefundPayments(int form, string whereCondnSql)
        {
            RefundManagementData refundManagementData = new RefundManagementData();
            refundManagementData.ListRefundPayments.Clear();
            refundManagementData.ListRefundPayments.Merge(Helper.ListRefundPayments(form, whereCondnSql));
            return refundManagementData.GetXml();
        }

        #endregion

        #region Prepare Checks

        /// <summary>
        /// F1214_s the prepare checks.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <returns>ChecksId</returns>
        public int F1214_PrepareChecks(int registerId, int ownerId, DateTime interestDate, int userId, string paymentItems)
        {
            return Helper.F1214_PrepareChecks(registerId, ownerId, interestDate, userId, paymentItems);
        }

        #endregion

        #endregion

        #region Check Detail

        #region Get and List Check Detail

        /// <summary>
        /// Gets the Cash Ledger ID
        /// </summary>
        /// <returns>Cash Ledger ID</returns>
        public string F1226_ListCashLedger()
        {
            return Helper.F1226_ListCashLedger().GetXml();
        }

        /// <summary>
        /// Gets the Cash Ledger(check) Detail
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public string F1226_GetCashLedger(int clid)
        {
            return Helper.F1226_GetCashLedger(clid).GetXml();
        }

        #endregion

        #region Update Check Detail And Status

        /// <summary>
        /// Updates the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="overRide">The over ride.</param>
        /// <param name="checkDetails">The check details.</param>
        /// <param name="userId">User ID</param>
        /// <returns>1 if checkno already exist else 0</returns>
        public int F1221_UpdateCashLedger(int clid, int overRide, string checkDetails, int userId)
        {
            return Helper.F1221_UpdateCashLedger(clid, overRide, checkDetails, userId);
        }

        /// <summary>
        /// Updates the Cash Ledger Status
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="functionDate">The function date.</param>
        /// <param name="functionId">The function id.</param>
        /// <param name="loginUserId">Login UserId</param>
        public void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId)
        {
            Helper.F1226_UpdateCashLedgerStatus(clid, userId, functionDate, functionId, loginUserId);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">User ID</param>
        public void F1226_DeleteCashLedger(int clid, int userId)
        {
            Helper.F1226_DeleteCashLedger(clid, userId);
        }

        #endregion

        #endregion Check Detail

        #region 8040 Time

        #region List Time

        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>List TimeDataTable</returns>
        public string F8040_ListTimeInformation(int formId, int keyId)
        {
            return Helper.F8040_ListTimeInformation(formId, keyId).GetXml();
        }
        #endregion List Time

        #region List Time Resource
        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="isactive">The active value</param>
        /// <returns>ListTimeDataTable</returns>
        public string F8040_ListTimeResourceInformation(int isactive)
        {
            return Helper.F8040_ListTimeResourceInformation(isactive).GetXml();
        }
        #endregion List Time Resource

        #region Save
        /// <summary>
        /// F8040_s the save time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">The user id.</param>
        public void F8040_SaveTime(string timeDetails, int userId)
        {
            Helper.F8040_SaveTime(timeDetails, userId);
        }
        #endregion Save

        #region Update
        /// <summary>
        /// F8040_s the update time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">The user id.</param>
        public void F8040_UpdateTime(string timeDetails, int userId)
        {
            Helper.F8040_UpdateTime(timeDetails, userId);
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// F8040_s the delete time.
        /// </summary>
        /// <param name="timeId">The time id.</param>
        /// <param name="userId">The user id.</param>
        public void F8040_DeleteTime(int timeId, int userId)
        {
            Helper.F8040_DeleteTime(timeId, userId);
        }
        #endregion Delete

        #region F8040_CheckEventId

        /// <summary>
        /// F8040_s the check event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Intger value</returns>
        public int F8040_CheckEventId(int formId, int keyId)
        {
            return Helper.F8040_CheckEventId(formId, keyId);
        }

        #endregion F8040_CheckEventId

        #endregion 8040 Time

        #region 8902 WorkOrderHeader

        #region Get

        /// <summary>
        /// F8902 get Event information.
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>dataset</returns>
        public string F8902_GetHeader(int workId)
        {
            return Helper.F8902_GetHeader(workId).GetXml();
        }

        #endregion

        #region Save

        /// <summary>
        /// F8902 SaveHeader
        /// </summary>
        /// <param name="header">header</param>
        /// <param name="userId">The user id.</param>
        public void F8902_SaveHeader(string header, int userId)
        {
            Helper.F8902_SaveHeader(header, userId);
        }

        #endregion

        #region Delete

        /// <summary>
        /// F8902 delete Header.
        /// </summary>
        /// <param name="workId">The work id.</param>
        /// <param name="userId">The user id.</param>
        public void F8902_Delete(int workId, int userId)
        {
            Helper.F8902_DeleteHeader(workId, userId);
        }

        #endregion Delete

        #endregion

        #region 8044 Materials

        #region List Material Details

        /// <summary>
        /// F8044_s the list material details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// returns dataset contains Material Details
        /// </returns>
        public string F8044_ListMaterialDetails(int formId, int keyId)
        {
            F8044MaterialsData materialsData = new F8044MaterialsData();
            materialsData = Helper.F8044_ListMaterialDetails(formId, keyId);
            return materialsData.GetXml();
        }

        #endregion

        #region List Materials Resource Types

        /// <summary>
        /// F8044_s the type of the list materials resource.
        /// </summary>
        /// <param name="flagActiveAndAll">The flag active and all.</param>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// returns dataset contains Materials Resource Types
        /// </returns>
        public string F8044_ListMaterialsResourceType(int flagActiveAndAll, int eventId)
        {
            F8044MaterialsData materialsData = new F8044MaterialsData();
            materialsData = Helper.F8044_ListMaterialsResourceType(flagActiveAndAll, eventId);
            return materialsData.GetXml();
        }

        #endregion

        #region Save Material Details

        /// <summary>
        /// Save the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">The user id.</param>
        public void F8044_SaveMaterialDetails(string materialItems, int userId)
        {
            Helper.F8044_SaveMaterialDetails(materialItems, userId);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// F8044_s the update material details.
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">The user id.</param>
        public void F8044_UpdateMaterialDetails(string materialItems, int userId)
        {
            Helper.F8044_UpdateMaterialDetails(materialItems, userId);
        }

        #endregion

        #region Delete a Material Item

        /// <summary>
        /// F8044_s the delete material item.
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int F8044_DeleteMaterialItem(int materialId, int userId)
        {
            return Helper.F8044_DeleteMaterialItem(materialId, userId);
        }

        #endregion

        #endregion

        #region 1220 Account Register

        #region List AccontNames

        /// <summary>
        /// F1220_s the account names.
        /// </summary>
        /// <returns>String Contains the Account Names</returns>
        public string F1220_AccountNames()
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            accountRegisterData.ListAccountNames.Clear();
            accountRegisterData.ListAccountNames.Merge(Helper.F1220_AccountNames());
            return accountRegisterData.GetXml();
        }

        #endregion

        #region Get ReconciledDetails

        /// <summary>
        /// F1220_s the get reconciled details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>string Contains the ReconcieledDetails</returns>
        public string F1220_GetReconciledDetails(int registerId)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            accountRegisterData.ReconciledDetails.Clear();
            accountRegisterData.ReconciledDetails.Merge(Helper.F1220_GetReconciledDetails(registerId));
            return accountRegisterData.GetXml();
        }

        #endregion

        #region List AccountRegister

        /// <summary>
        /// F1220_s the list account register.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>string Contains the account register details</returns>
        public string F1220_ListAccountRegister(int registerId, DateTime beginningDate)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            accountRegisterData.ListAccountRegister.Clear();
            accountRegisterData.ListAccountRegister.Merge(Helper.F1220_ListAccountRegister(registerId, beginningDate));
            return accountRegisterData.GetXml();
        }

        #endregion

        #region GetAccountRegisterDetails

        /// <summary>
        /// F1220_s the get account register details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>string Contains the AccountRegister Details</returns>
        public string F1220_GetAccountRegisterDetails(int registerId, DateTime beginningDate)
        {
            F1220AccountRegisterData accountRegisterData = new F1220AccountRegisterData();
            accountRegisterData = Helper.F1220_GetAccountRegisterDetails(registerId, beginningDate);
            return accountRegisterData.GetXml();
        }

        #endregion

        #endregion

        #region 8902 Event Grid

        #region Get

        /// <summary>
        /// Get Event Grid Details
        /// </summary>
        /// <param name="workId">WorkId</param>
        /// <returns>Dataset</returns>
        public string F8904_GetEventGridDetails(int workId)
        {
            return Helper.F8904_GetEventGridDetails(workId).GetXml();
        }

        #endregion

        #endregion

        #region 9002 GetUserDetails

        /// <summary>
        /// F9001_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>User Details</returns>
        public string F9002_GetUserDetails(int applicationId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData = Helper.F9002_GetUserDetails(applicationId);
            return userManagementData.GetXml();
        }

        #endregion

        #region 1224 Check Print Queue

        #region List AccontNames

        /// <summary>
        /// F1224 the account names.
        /// </summary>
        /// <returns>String Contains the Account Names</returns>
        public string F1224_AccountNames()
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            checkPrintQueueData.ListAccountNames.Clear();
            checkPrintQueueData.ListAccountNames.Merge(Helper.F1224_AccountNames());
            return checkPrintQueueData.GetXml();
        }

        #endregion

        #region List Get Check Number

        /// <summary>
        /// F1224_s the get check number.
        /// </summary>
        /// <param name="registerId">Register ID</param>
        /// <returns>Check Numbers</returns>
        public string F1224_GetCheckNumber(int registerId)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            checkPrintQueueData = Helper.F1224_GetCheckNumber(registerId);
            return checkPrintQueueData.GetXml();
        }

        #endregion

        #region List UnPrinted Checks

        /// <summary>
        /// F1224_s the list un printed checks.
        /// </summary>
        /// <param name="registerId">Register ID</param>
        /// <returns>Un Printed Checks</returns>
        public string F1224_ListUnPrintedChecks(int registerId)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            checkPrintQueueData = Helper.F1224_ListUnPrintedChecks(registerId);
            return checkPrintQueueData.GetXml();
        }

        #endregion

        #region Print Checks

        /// <summary>
        /// F1224_s the create checks.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="startingCheckNumber">The starting check number.</param>
        /// <param name="checkItems">The check items.</param>
        /// <returns>printed Check Numbers</returns>
        public string F1224_CreateChecks(int registerId, int userId, Int64 startingCheckNumber, string checkItems)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            checkPrintQueueData = Helper.F1224_CreateChecks(registerId, userId, startingCheckNumber, checkItems);
            return checkPrintQueueData.GetXml();
        }

        #endregion

        #endregion

        #region F1502 Account Element Management

        #region GetAccountElementMgmt

        /// <summary>
        /// To get Account Element Management details
        /// </summary>
        /// <param name="function">The Function Id</param>
        /// <param name="description">The Description</param>
        /// <param name="type">The Type - SemiAnnualCode </param>
        /// <returns>Typed Dataset containing the Account Element Management details</returns>
        public string F1502_GetAccountElementMgmt(string function, string description, int type)
        {
            F1502AccountManagementData accountElementManagementData = new F1502AccountManagementData();
            accountElementManagementData = Helper.F1502_GetAccountElementMgmt(function, description, type);
            return accountElementManagementData.GetXml();
        }

        #endregion GetAccountElementMgmt

        #region SaveAccountElementMgmt

        /// <summary>
        /// To Save Account Element Management details
        /// </summary>
        /// <param name="functionElemnts">The xml string which contains the Account elements mgmt Grid values</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// Integer value containing whether save is compleded or not
        /// </returns>
        public int F1502_SaveAccountElementMgmt(string functionElemnts, int userId)
        {
            return Helper.F1502_SaveAccountElementMgmt(functionElemnts, userId);
        }

        #endregion SaveAccountElementMgmt

        #region DeleteAccountElementMgmt

        /// <summary>
        /// To Delete Account Element Management details
        /// </summary>
        /// <param name="functionId">The Functional Id</param>
        /// <param name="userId">User ID</param>
        public void F1502_DeleteAccountElementMgmt(string functionId, int userId)
        {
            Helper.F1502_DeleteAccountElementMgmt(functionId, userId);
        }

        #endregion DeleteAccountElementMgmt

        #endregion F1502 Account Element Management

        #region F9600 Search Engine

        #region ListSearchResult

        /// <summary>
        /// F9600_s the list searchresult.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <returns>Search result</returns>
        public string F9600_ListSearchresult(string searchValue, int appId)
        {
            return Helper.F9600ListSearchResult(searchValue, appId).GetXml();
        }

        #endregion

        #region SortSearch

        /// <summary>
        /// F9600_s the list sort result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <param name="searchOrder">if set to <c>true</c> [search order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        /// <returns>XML String</returns>
        public string F9600_ListSortResult(string searchValue, int appId, bool searchOrder, bool groupOrder)
        {
            return Helper.F9600ListSortResult(searchValue, appId, searchOrder, groupOrder).GetXml();
        }

        #endregion

        #endregion

        #region 1530 Cash Account Management

        /// <summary>
        /// Gets the institution list, institution detail, cash account list and institution contact list
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <returns>F1530CashAccountManagementData with institution Detail</returns>
        public string F1530_GetInstitutionDetail(int institutionId)
        {
            return Helper.F1530_GetInstitutionDetail(institutionId).GetXml();
        }

        /// <summary>
        /// insert/update the Institution
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="institutionElements">The institution elements.</param>
        /// <param name="userId">user Id</param>
        /// <returns>saved institution id</returns>
        public int F1530_SaveInstitution(int institutionId, string institutionElements, int userId)
        {
            return Helper.F1530_SaveInstitution(institutionId, institutionElements, userId);
        }

        #endregion

        #region 1531 Cash Account

        /// <summary>
        /// Gets the cash account detail
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with cash account Detail
        /// </returns>
        public string F1531_GetCashAccountDetail(int registerId)
        {
            return Helper.F1531_GetCashAccountDetail(registerId).GetXml();
        }

        /// <summary>
        /// saves cash account.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="registerItems">The register items.</param>
        /// <param name="userId">UserId</param>
        /// <returns>
        /// subfund validated result,-1- validation failed else registerId
        /// </returns>
        public int F1531_SaveCashAccount(int registerId, string registerItems, int userId)
        {
            return Helper.F1531_SaveCashAccount(registerId, registerItems, userId);
        }

        #endregion

        #region 1532 Institution Contact

        /// <summary>
        /// Gets the institution contact detail
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with institution contact Detail
        /// </returns>
        public string F1532_GetInstitutionContactDetail(int contactId)
        {
            return Helper.F1532_GetInstitutionContactDetail(contactId).GetXml();
        }

        /// <summary>
        /// saves the Institution Contact.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">User ID</param>
        /// <returns>saved contact id</returns>
        public int F1532_SaveInstitutionContact(int contactId, string acctEmelemts, int userId)
        {
            return Helper.F1532_SaveInstitutionContact(contactId, acctEmelemts, userId);
        }

        #endregion

        #region F1030SpecialDistrict

        #region F1030SpecialDistrictDefinition

        #region ListMethods

        /// <summary>
        /// F1030_s the type of the list district definition.
        /// </summary>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public string F1030_ListDistrictDefinitionType()
        {
            return Helper.F1030_ListDistrictDefinitionType().GetXml();
        }

        #endregion ListMethods

        #region GetMethods

        /// <summary>
        /// F1030_s the get district definition details.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <returns>DistrictDefinition</returns>
        public string F1030_GetDistrictDefinitionDetails(int districtNo)
        {
            return Helper.F1030_GetDistrictDefinitionDetails(districtNo).GetXml();
        }

        #endregion GetMethods

        #region DeleteMethods

        /// <summary>
        /// F1030_s the delete district definition.
        /// </summary>
        /// <param name="specialDistrictId">Special District ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>DistrictId</returns>
        public int F1030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            return Helper.F1030_DeleteDistrictDefinition(specialDistrictId, userId);
        }

        /// <summary>
        /// F1030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item id.</param>
        /// <param name="userId">User ID</param>
        public void F1030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            Helper.F1030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
        }

        #endregion DeleteMethods

        #region SaveMethods

        /// <summary>
        /// F1030_s the save district definition.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <param name="districtItem">The district item.</param>
        /// <param name="rateItem">The rate item.</param>
        /// <param name="distributionItem">The distribution item.</param>
        /// <param name="flagOverride">if set to <c>true</c> [flag override].</param>
        /// <param name="userId">UserId</param>
        /// <returns>KeyID</returns>
        public string F1030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, int userId)
        {
            return Helper.F1030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, userId);
        }

        #endregion SaveMethods

        #endregion F1030SpecialDistrictDefinition

        #region F1033SpecialDistrictSelection

        #region ListPostTypes

        /// <summary>
        /// F1033_s the list post types.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// F1033SpecialDistrictSelectionData Return the PostType Data
        /// </returns>
        public string F1033_ListPostTypes(int? form)
        {
            F1033SpecialDistrictSelectionData postType = new F1033SpecialDistrictSelectionData();
            postType = Helper.F1033_ListPostTypes(form);
            return postType.GetXml();
        }

        #endregion ListPostTypes

        #region ListSpecialDistricts
        /// <summary>
        /// List the Special Districts
        /// </summary>
        /// <param name="district">district</param>
        /// <param name="rollYear">rollYear</param>
        /// <param name="description">description</param>
        /// <param name="postTypeId">Post TypeID</param>
        /// <returns>Return the ListSpecialDistricts</returns>
        public string F1033_ListSpecialDistricts(int? district, int? rollYear, string description, int? postTypeId)
        {
            F1033SpecialDistrictSelectionData specialDistricts = new F1033SpecialDistrictSelectionData();
            specialDistricts = Helper.F1033_ListSpecialDistricts(district, rollYear, description, postTypeId);
            return specialDistricts.GetXml();
        }
        #endregion ListSpecialDistricts

        #endregion F1033SpecialDistrictSelection

        #endregion F1030SpecialDistrict

        #region F16030SpecialDistrictDefinition

        #region ListMethods

        /// <summary>
        /// F16030_s the type of the list district definition.
        /// </summary>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public string F16030_ListDistrictDefinitionType()
        {
            return Helper.F16030_ListDistrictDefinitionType().GetXml();
        }

        #endregion ListMethods

        #region GetMethods

        /// <summary>
        /// F16030_s the get district definition details.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <returns>DistrictDefinition</returns>
        public string F16030_GetDistrictDefinitionDetails(int districtNo)
        {
            return Helper.F16030_GetDistrictDefinitionDetails(districtNo).GetXml();
        }

        #endregion GetMethods

        #region DeleteMethods

        /// <summary>
        /// F16030_s the delete district definition.
        /// </summary>
        /// <param name="specialDistrictId">special DistrictID</param>
        /// <param name="userId">userId</param>
        /// <returns>DistrictId</returns>
        public int F16030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            return Helper.F16030_DeleteDistrictDefinition(specialDistrictId, userId);
        }

        /// <summary>
        /// F16030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">Special DistrictRate ItemID</param>
        /// <param name="userId">User ID</param>
        public void F16030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            Helper.F16030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
        }

        #endregion DeleteMethods

        #region SaveMethods

        /// <summary>
        /// F16030_s the save district definition.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <param name="districtItem">The district item.</param>
        /// <param name="rateItem">The rate item.</param>
        /// <param name="distributionItem">The distribution item.</param>
        /// <param name="flagOverridebool">if set to <c>true</c> [flag overridebool].</param>
        /// <param name="flagValidation">if set to <c>true</c> [flag validation].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>KeyId</returns>
        public string F16030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, bool flagValidation, int userId)
        {
            return Helper.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, flagValidation, userId);
        }

        #endregion SaveMethods

        #endregion F16030SpecialDistrictDefinition

        #region F1500AccountManagement

        #region Getdescription

        /// <summary>
        /// F1500_s the get description.
        /// </summary>
        /// <param name="keyId">key ID</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>Description</returns>
        public string F1500_GetDescription(string keyId, string elementName)
        {
            return Helper.F1500_GetDescription(keyId, elementName).GetXml();
        }

        #endregion.

        #region Get SubFund Items

        /// <summary>
        /// F1500_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>sub fund items</returns>
        public string F1500_GetSubFundItems(string subFund, short rollYear)
        {
            return Helper.F1500_GetSubFundItems(subFund, rollYear).GetXml();
        }

        #endregion

        #region Get Function Items

        /// <summary>
        /// F1500_s the get function items.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>function items</returns>
        public string F1500_GetFunctionItems(string function)
        {
            return Helper.F1500_GetFunctionItems(function).GetXml();
        }

        #endregion

        #region Get AccountIDs and Details

        /// <summary>
        /// F1500_s the list account details.
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>accountID</returns>
        public string F1500_ListAccountDetails(int accountId)
        {
            return Helper.F1500_ListAccountDetails(accountId).GetXml();
        }

        #endregion

        #region Save and Edit the Account Details

        /// <summary>
        /// F1500_s the create or edit account.
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">User ID</param>
        /// <returns>returns EditAccount</returns>
        public int F1500_CreateOrEditAccount(int accountId, string acctEmelemts, int userId)
        {
            return Helper.F1500_CreateOrEditAccount(accountId, acctEmelemts, userId);
        }

        #endregion

        /// <summary>
        /// List the register types.
        /// </summary>
        /// <returns>AccountManagementData with register type - xml string</returns>
        public string F1500_ListRegisterType()
        {
            AccountManagementData accountManagement = new AccountManagementData();
            accountManagement = Helper.F1500_ListRegisterType();
            return accountManagement.GetXml();
        }

        #region Get Configuration Value

        /// <summary>
        /// F1500_s the get configuration value.
        /// </summary>
        /// <param name="cfgName">Name of the CFG.</param>
        /// <returns>Configuration Name</returns>
        public string F1500_GetConfigurationValue(string cfgName)
        {
            return Helper.F1500_GetConfigurationValue(cfgName).GetXml();
        }

        #endregion

        #endregion F1500AccountManagement

        #region F1503 Generic Management Comp

        #region GetGenericElementMgmt

        /// <summary>
        /// To Get the Generic Element Management details
        /// </summary>
        /// <param name="keyValue">The key value(Element ID)</param>
        /// <param name="description">The Description</param>
        /// <param name="formName">The Form Name</param>
        /// <returns>Typed Dataset containing the Element ID and Description Value</returns>
        public string F1503_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            F1503GenericManagementData genericElementManagementData = new F1503GenericManagementData();
            genericElementManagementData = Helper.F1503_GetGenericElementMgmt(keyValue, description, formName);
            return genericElementManagementData.GetXml();
        }

        #endregion GetGenericElementMgmt

        #region SaveGenericElementMgmt

        /// <summary>
        /// To Save the Generic Element Management details
        /// </summary>
        /// <param name="functionElemnts">The Xml string containing Element ID and Description Value</param>
        /// <param name="formName">The Form name</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// Integer value containing whether save is compleded or not
        /// </returns>
        public int F1503_SaveGenericElementMgmt(string functionElemnts, string formName, int userId)
        {
            return Helper.F1503_SaveGenericElementMgmt(functionElemnts, formName, userId);
        }

        #endregion SaveGenericElementMgmt

        #region DeleteGenericElementMgmt

        /// <summary>
        /// To Delete the Generic Element Management details
        /// </summary>
        /// <param name="elementId">The Particular Element ID</param>
        /// <param name="formName">The Form name</param>
        /// <param name="userId">User ID</param>
        public void F1503_DeleteGenericElementMgmt(string elementId, string formName, int userId)
        {
            Helper.F1503_DeleteGenericElementMgmt(elementId, formName, userId);
        }

        #endregion DeleteGenericElementMgmt

        #endregion F1503 Generic Management Comp

        #region F1515 Sub Fund Selection

        #region F1515_GetSubFundSelection

        /// <summary>
        /// To Get the Sub Fund Selection Details
        /// </summary>
        /// <param name="subFund">The Sub fund</param>
        /// <param name="description">The Description</param>
        /// <param name="rollYear">The Roll year</param>
        /// <param name="iscash">The iscash.</param>
        /// <returns>
        /// Typed Dataset containing the Sub Fund Selection Details
        /// </returns>
        public string F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            F1515SubFundSelectionData subFundSelectionData = new F1515SubFundSelectionData();
            subFundSelectionData = Helper.F1515_GetSubFundSelection(subFund, description, rollYear, iscash);
            return subFundSelectionData.GetXml();
        }

        #endregion F1515_GetSubFundSelection

        #endregion F1515 Sub Fund Selection

        #region F1513 Fund Selection

        #region F1513_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        public string F1513_GetFundSelection(string fund, string description)
        {
            F1513FundSelectionData fundSelectionData = new F1513FundSelectionData();
            fundSelectionData = Helper.F1513_GetFundSelection(fund, description);
            return fundSelectionData.GetXml();
        }
        /// <summary>
        /// F1513_CentralFundItemValidation
        /// </summary>
        /// <param name="fundId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        public  int F1513_CentralFundItemValidation(int fundId, int rollYear)
        {
            return Helper.F1513_CentralFundItemValidation(fundId, rollYear);
        }

        #endregion F1513_GetFundSelection

        #endregion F1513 Fund Selection

        #region 16031 Special District Assessment for Working FileID

        #region List Special District Assessment Details Working fileId

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public string F16031_ListDistrictAssessmentDetails(int workingfileId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F16031_ListDistrictAssessmentDetails(workingfileId);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List Special District

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="districtId">The sa district id.</param>
        /// <returns>returns dataset containing specialDistrict Details</returns>
        public string F16031_ListDistrictAssessment(int districtId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F16031_ListDistrictAssessment(districtId);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List Special District Assessment ParcelID

        /// <summary>
        /// F1031_s the get district assessment parcel ID.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public string F16031_GetSpecialAssessmentParcel(string parcelNumber, int? parcelId, int? rollYear)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F16031_GetSpecialAssessmentParcel(parcelNumber, parcelId, rollYear);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region Delete District Assessment

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public string F16031_DeleteDistrictAssessment(int workingfileId, int userId)
        {
            F1031SpecialDistrictAssessmentData insertData = new F1031SpecialDistrictAssessmentData();
            insertData = Helper.F16031_DeleteDistrictAssessment(workingfileId, userId);
            return insertData.GetXml();
        }

        #endregion

        #region Save District Assessment Details

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [is override].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">user Id</param>
        /// <returns>Key ID</returns>
        public int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, int userId)
        {
            return Helper.F16031_SaveDistrictAssessmentDetails(districtProperty, districtRates, userId);
        }

        #endregion

        #region Check Duplicate Statement/Owner

        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public string F16031_CheckSpecialAssessment(string districtProperty)
        {
            F1031SpecialDistrictAssessmentData insertData = new F1031SpecialDistrictAssessmentData();
            insertData = Helper.F16031_CheckSpecialAssessment(districtProperty);
            return insertData.GetXml();
        }

        #endregion

        #region ExecWriterStatements

        public void F16031_ExeWriteTaxStatement(int workingFileId, int userId, bool isCancel)
        {
            Helper.F16031_ExeWriteTaxStatement(workingFileId, userId, isCancel);
        }


        #endregion ExecWriterStatements

        #endregion 16031 Special District Assessment for Working FileID

        #region 1031 Special District Assessment

        #region List Special District Assessment Details

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public string F1031_ListDistrictAssessmentDetails(int statementId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F1031_ListDistrictAssessmentDetails(statementId);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List Special District Assessment IDs

        /// <summary>
        /// F1031_s the list district assessment I ds.
        /// </summary>
        /// <returns>
        /// returns dataset containing District Assessment IDs
        /// </returns>
        public string F1031_ListDistrictAssessmentIDs()
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F1031_ListDistrictAssessmentIDs();
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List Special District Assessment ParcelID

        /// <summary>
        /// F1031_s the get district assessment parcel ID.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public string F1031_GetDistrictAssessmentParcelID(string parcelNumber, int? parcelId, int? rollYear)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F1031_GetDistrictAssessmentParcelID(parcelNumber, parcelId, rollYear);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List District Assessment ParcelID

        /// <summary>
        /// F1031_s the get district assessment parcel ID.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public string f25050GetDistrictAssessmentParcelID(string parcelNumber, int parcelId)
        {
            F2200EditScheduleData specialDistrictAssessmentData = new F2200EditScheduleData();
            specialDistrictAssessmentData = Helper.f25050GetDistrictAssessmentParcelID(parcelNumber, parcelId);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region List Special District

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="districtId">The sa district id.</param>
        /// <returns>returns dataset containing specialDistrict Details</returns>
        public string F1031_ListDistrictAssessment(int districtId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            specialDistrictAssessmentData = Helper.F1031_ListDistrictAssessment(districtId);
            return specialDistrictAssessmentData.GetXml();
        }

        #endregion

        #region Save District Assessment Details

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [is override].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">user Id</param>
        /// <returns>Key ID</returns>
        public int F1031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool overrideStatus, bool ownerRide, int userId)
        {
            return Helper.F1031_SaveDistrictAssessmentDetails(districtProperty, districtRates, overrideStatus, ownerRide, userId);
        }

        #endregion

        #region Delete District Assessment

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int F1031_DeleteDistrictAssessment(int statementId, int userId)
        {
            return Helper.F1031_DeleteDistrictAssessment(statementId, userId);
        }

        #endregion

        #region Check Duplicate Statement/Owner

        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public int F1031_CheckSpecialDistrictStatementOrOwner(string districtProperty, bool statementFlag)
        {
            return Helper.F1031_CheckSpecialDistrictStatementOrOwner(districtProperty, statementFlag);
        }

        #endregion

        #endregion

        #region F16040 Improvement District Definition.
        
        /// <summary>
        /// List Interest Method.
        /// </summary>
        /// <returns></returns>
        public string ListInterestMethod()
        {
            F16040ImprovementDistrictDefinition getListInterestData = new F16040ImprovementDistrictDefinition();
            getListInterestData = Helper.ListInterestMethod();
            return getListInterestData.GetXml();
        }

        /// <summary>
        /// List Delq details.
        /// </summary>
        /// <returns></returns>
        public string ListInterestDelqDetails()
        {
            F16040ImprovementDistrictDefinition getInterestDelqdata = new F16040ImprovementDistrictDefinition();
            getInterestDelqdata = Helper.ListInterestDelqDetails();
            return getInterestDelqdata.GetXml();
        }

        /// <summary>
        /// Get District Details.
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public string GetDistrictDetails(int districtId)
        {
            F16040ImprovementDistrictDefinition getDistrictdetailsdata = new F16040ImprovementDistrictDefinition();
            getDistrictdetailsdata = Helper.GetDistrictDetails(districtId);
            return getDistrictdetailsdata.GetXml();
        }

        /// <summary>
        /// Improvement District Type list.
        /// </summary>
        /// <returns></returns>
        public string ImprovementDistrictTypelist(string districtType)
        {
            F16040ImprovementDistrictDefinition getImprovementlistdata = new F16040ImprovementDistrictDefinition();
            getImprovementlistdata = Helper.ImprovementDistrictTypelist(districtType);
            return getImprovementlistdata.GetXml();
        }

        /// <summary>
        /// Get distribution details.
        /// </summary>
        /// <returns></returns>
        public string GetDistributionDetails()
        {
            F16040ImprovementDistrictDefinition getDistributionlistdata = new F16040ImprovementDistrictDefinition();
            getDistributionlistdata = Helper.GetDistributionDetails();
            return getDistributionlistdata.GetXml();
        }

        /// <summary>
        /// Get District Definition data.
        /// </summary>
        /// <returns></returns>
        public string GetDistrictDefinitionDetails(int districtId)
        {
            F16040ImprovementDistrictDefinition getDistrictDefinitiondata = new F16040ImprovementDistrictDefinition();
            getDistrictDefinitiondata = Helper.GetDistrictDefinitionDetails(districtId);
            return getDistrictDefinitiondata.GetXml();
        }

        /// <summary>
        /// Executes Improvement district.
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string RollOver_ImprovementDistrict(int districtId, int userId)
        {
            F16040ImprovementDistrictDefinition getImprovementdata = new F16040ImprovementDistrictDefinition();
            getImprovementdata = Helper.RollOver_ImprovementDistrict(districtId,userId);
            return getImprovementdata.GetXml();
        }

        /// <summary>
        /// Save Improve District Definition.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string F16040_SaveImproveDistrictDefinition(string districtItem,string distributionItem, int userId)
        {
            return Helper.F16040_SaveImproveDistrictDefinition(districtItem,distributionItem,userId);
        }

        /// <summary>
        /// Update Improvement District Details.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="districtItem"></param>
        /// <param name="distributionItem"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string F16040_UpdateImproveDistrictDefinition(int districtNo, string districtItem, string distributionItem, int userid)
        {
            return Helper.F16040_UpdateImproveDistrictDefinition(districtNo, districtItem, distributionItem, userid);
        }

        #endregion Improvement District Definition.

        #region F16041 Improvement District Parcels
        
        /// <summary>
        /// Get District Parcels.
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public string GetDistrictParcels(int districtId)
        {
            F16041ImprovementDistrictParcels getDistrictDefinitiondata = new F16041ImprovementDistrictParcels();
            getDistrictDefinitiondata = Helper.GetDistrictParcels(districtId);
            return getDistrictDefinitiondata.GetXml();
        }

        /// <summary>
        /// List District Parcels
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public string ListDistrictParcelsDetails(string parcelval, int? parcelId, int? rollYear)
        {
            F16041ImprovementDistrictParcels getDistrictparceldata = new F16041ImprovementDistrictParcels();
            getDistrictparceldata = Helper.ListDistrictParcelsDetails(parcelval, parcelId, rollYear);
            return getDistrictparceldata.GetXml();
        }

        /// <summary>
        ///Save Improvement District Parcels.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string F16041_SaveDistrictParcels(string districtProperty, int userId)
        {
            return Helper.F16041_SaveDistrictParcels(districtProperty, userId);
        }

        /// <summary>
        /// Delete Improvement District Parcels..
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="loginUserId">Login User ID</param>
        public string F16041_DeleteDistrictParcels(int workingFileItemId, int userId)
        {
            return Helper.F16041_DeleteDistrictParcels(workingFileItemId, userId);
        }

        /// <summary>
        ///Check Parcel Details
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string CheckParcelDetails(string districtProperty)
        {
            return Helper.CheckParcelDetails(districtProperty);
        }

        #endregion Improvement District Parcels

        #region 9503 SubFund Management

        #region List SubFund Details

        /// <summary>
        /// F9503_s the get sub fund management details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <returns>String Contains the SubFund Details</returns>
        public string F9503_GetSubFundManagementDetails(int? subFundId)
        {
            F9503SubFundManagementData subFungMgmtData = new F9503SubFundManagementData();
            subFungMgmtData = Helper.F9503_GetSubFundManagementDetails(subFundId);
            return subFungMgmtData.GetXml();
        }

        #endregion

        #region Get SubFund Items

        /// <summary>
        /// F9503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>sub fund items</returns>
        public string F9503_GetSubFundItems(string subFund, short rollYear)
        {
            return Helper.F9503_GetSubFundItems(subFund, rollYear).GetXml();
        }

        #endregion

        #region Save and Edit SubFund Management Data

        /// <summary>
        /// F15005_s the check sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>error id </returns>
        public int F15005_CheckSubFund(int? subFundId, string subFund, int rollYear)
        {
            return Helper.F15005_CheckSubFund(subFundId, subFund, rollYear);
        }

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns primaryId</returns>
        public int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments, int userId)
        {
            return Helper.F9503_CreateOrEditSubFund(subFundId, subFundElments, userId);
        }

        #endregion

        #endregion

        #region F1501 General Ledger Configuration

        #region List RollYear

        /// <summary>
        /// F1501_s the list roll year.
        /// </summary>
        /// <returns>Roll years</returns>
        public string F1501_ListRollYear()
        {
            return Helper.F1501_ListRollYear().GetXml();
        }

        #endregion

        #region List GL Config Details

        /// <summary>
        /// F1501_s the list GL config details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>GL config details.</returns>
        public string F1501_ListGLConfigDetails(int rollYear)
        {
            return Helper.F1501_ListGLConfigDetails(rollYear).GetXml();
        }
        #endregion

        #region Get GL config Details

        /// <summary>
        /// F1501_s the get GL config details.
        /// </summary>
        /// <param name="configId">Configuration ID</param>
        /// <returns>General Ledger Configuration Details</returns>
        public string F1501_GetGLConfigDetails(int configId)
        {
            return Helper.F1501_GetGLConfigDetails(configId).GetXml();
        }

        #endregion

        #region Save and Edit the List GL Config Details

        /// <summary>
        /// F1501_s the create or edit GL config details.
        /// </summary>
        /// <param name="configId">Configuration ID</param>
        /// <param name="configElements">The g L config elements.</param>
        /// <param name="userId">User ID</param>
        /// <returns>ErrorStatus</returns>
        public int F1501_CreateOrEditGLConfigDetails(int configId, string configElements, int userId)
        {
            return Helper.F1501_CreateOrEditGLConfigDetails(configId, configElements, userId);
        }

        #endregion

        #endregion

        #region F1410 Owner Receipting

        /// <summary>
        /// F1410_s the get owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns Owner Reeipting DataSet</returns>
        public string F1410_GetOwnerReceipting(string interestDate, string ownerId, string parcelIDs)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            ownerReceiptingDataSet = Helper.F1410_GetOwnerReceipting(interestDate, ownerId, parcelIDs);
            return ownerReceiptingDataSet.GetXml();
        }

        /// <summary>
        /// F1410_s the list owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="formBackColor">Form Backcolor</param>
        /// <returns>OwnerId</returns>
        public string F1410_ListOwnerReceipting(string interestDate, string statementXml, string formBackColor)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            ownerReceiptingDataSet = Helper.F1410_ListOwnerReceipting(interestDate, statementXml, formBackColor);
            return ownerReceiptingDataSet.GetXml();
        }

        /// <summary>
        /// F1410_s the delete owner receipting.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerXml">The owner XML.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="userId">User ID</param>
        /// <param name="formBackColor">Form BackColor</param>
        /// <returns>Returns OwnerReceipting Dataset</returns>
        public string F1410_DeleteOwnerReceipting(int ownerId, string ownerXml, string statementXml, int userId, string formBackColor)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            ownerReceiptingDataSet = Helper.F1410_DeleteOwnerReceipting(ownerId, ownerXml, statementXml, userId, formBackColor);
            return ownerReceiptingDataSet.GetXml();
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
        public string F1410_SaveOwnerReceipting(int userId, string receiptDate, string interestDate, int ppaymentId, int paymentOption, string statementXml)
        {
            return Helper.F1410_SaveOwnerReceipting(userId, receiptDate, interestDate, ppaymentId, paymentOption, statementXml);
            //F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            //ownerReceiptingDataSet = Helper.F1410_SaveOwnerReceipting(userId, receiptDate, interestDate, ppaymentId, paymentOption, statementXml);
            //return ownerReceiptingDataSet.GetXml();
        }

        /// <summary>
        /// F1410_SaveOwnerReceiptPreview
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="statementDetails">statementDetails</param>
        /// <returns>int</returns>
        public int F1410_SaveOwnerReceiptPreview(int userId, string statementDetails)
        {
            return Helper.F1410_SaveOwnerReceiptPreview(userId, statementDetails);
        }

        #region List Attachment Details

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public string F1410_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            F1410OwnerReceiptingData listAttachmentData = new F1410OwnerReceiptingData();
            listAttachmentData = Helper.F1410_ListAttachmentDetails(formId, keyIds, userId, moduleId);
            return listAttachmentData.GetXml();
        }

        #endregion List Attachment Details
        #endregion

        #region F8000 GDoc Commons

        #region Get GDocBusiness

        /// <summary>
        /// To Load GDoc Business ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public string F8000_GetGDocBusiness()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            gdocCommonData = Helper.F8000_GetGDocBusiness();
            return gdocCommonData.GetXml();
        }

        #endregion Get GDocBusiness

        #region Get GDocDiameter

        /// <summary>
        /// To Load GDoc Diameter ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public string F8000_GetGDocDiameter(int featureClassId)
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            gdocCommonData = Helper.F8000_GetGDocDiameter(featureClassId);
            return gdocCommonData.GetXml();
        }

        #endregion Get GDocDiameter

        #region Get GDocPropertyReference

        /// <summary>
        /// To Load GDoc PropertyReference ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <param name="refField">The Ref Field</param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public string F8000_GetGDocPropertyReference(int featureClassId, string refField)
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            gdocCommonData = Helper.F8000_GetGDocPropertyReference(featureClassId, refField);
            return gdocCommonData.GetXml();
        }

        #endregion Get GDocPropertyReference

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public string wListStreets()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            gdocCommonData = Helper.wListStreets();
            return gdocCommonData.GetXml();
        }

        #endregion Get GDocStreet

        #region Get GDocUser

        /// <summary>
        /// To Load GDoc User ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public string F8000_GetGDocUser()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            gdocCommonData = Helper.F8000_GetGDocUser();
            return gdocCommonData.GetXml();
        }

        #endregion Get GDocUser

        #endregion F8000 GDoc Commons

        #region F84121 Sanitary Manhole Properties

        #region Get Sanitary Manhole Properties

        /// <summary>
        ///  To Load F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The Manhole ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Manhole properties Details</returns>
        public string F84121_GetSanitaryManholeProperties(int manholeId)
        {
            F84121SanitaryManholePropertiesData sanitaryManholePropertiesData = new F84121SanitaryManholePropertiesData();
            sanitaryManholePropertiesData = Helper.F84121_GetSanitaryManholeProperties(manholeId);
            return sanitaryManholePropertiesData.GetXml();
        }

        #endregion

        #region Save Sanitary Manhole Properties

        /// <summary>
        /// To Save F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The Manhole ID.</param>
        /// <param name="sanitaryManholeProperties">The XML string Containing All values in Sanitary Manhole properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing Manhole id</returns>
        public int F84121_SaveSanitaryManholeProperties(int manholeId, string sanitaryManholeProperties, int userId)
        {
            return Helper.F84121_SaveSanitaryManholeProperties(manholeId, sanitaryManholeProperties, userId);
        }

        #endregion

        #region Delete Sanitary Manhole Properties

        /// <summary>
        /// To Delete F84121 Sanitary Manhole properties
        /// </summary>
        /// <param name="manholeId">The Manhole Id</param>
        /// <param name="userId">The user id.</param>
        public void F84121_DeleteSanitaryManholeProperties(int manholeId, int userId)
        {
            Helper.F84121_DeleteSanitaryManholeProperties(manholeId, userId);
        }

        #endregion

        #endregion

        #region F84122 Sanitary Manhole Properties

        #region Get Sanitary Manhole Location

        /// <summary>
        /// To Load F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Sanitary Manhole Loaction Details
        /// </returns>
        public string F84122_GetSanitaryManholeLocation(int keyId)
        {
            F84122SanitaryManholeLocationData sanitaryManholeLocationData = new F84122SanitaryManholeLocationData();
            sanitaryManholeLocationData = Helper.F84122_GetSanitaryManholeLocation(keyId);
            return sanitaryManholeLocationData.GetXml();
        }

        #endregion

        #region Save Sanitary Manhole Location

        /// <summary>
        /// To Save F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryManholeLocation">The Sanitary Manhole location.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing key id</returns>
        public int F84122_SaveSanitaryManholeLocation(int keyId, string sanitaryManholeLocation, int userId)
        {
            return Helper.F84122_SaveSanitaryManholeLocation(keyId, sanitaryManholeLocation, userId);
        }

        #endregion

        #endregion

        #region F84721 Water Valve Properties

        #region Get Water Valve Properties

        /// <summary>
        ///  To Load F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <returns>Typed DataSet Containing All the Water valve properties Details</returns>
        public string F84721_GetWaterValveProperties(int valveId)
        {
            F84721WaterValvePropertiesData waterValvePropertiesData = new F84721WaterValvePropertiesData();
            waterValvePropertiesData = Helper.F84721_GetWaterValveProperties(valveId);
            return waterValvePropertiesData.GetXml();
        }

        #endregion Get Water Valve Properties

        #region Save Water Valve Properties

        /// <summary>
        /// To Save F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <param name="waterValveProperties">The XML string Containing All values in Water valve properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing valve id</returns>
        public int F84721_SaveWaterValveProperties(int valveId, string waterValveProperties, int userId)
        {
            return Helper.F84721_SaveWaterValveProperties(valveId, waterValveProperties, userId);
        }

        #endregion Save Water Valve Properties

        #region Delete Water Valve Properties

        /// <summary>
        /// To Delete F84721 Water valve properties
        /// </summary>
        /// <param name="valveId">The ValveId</param>
        /// <param name="userId">The user id.</param>
        public void F84721_DeleteWaterValveProperties(int valveId, int userId)
        {
            Helper.F84721_DeleteWaterValveProperties(valveId, userId);
        }

        #endregion Delete Water Valve Properties

        #endregion F84721 Water Valve Properties

        #region F9033 Query Engine

        #region ListQueryView

        /// <summary>
        /// F9033_s the list query view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>string</returns>
        public string F9033_ListQueryView(int formId)
        {
            F9033QueryEngineData queryViewData = new F9033QueryEngineData();
            queryViewData = Helper.F9033ListQueryView(formId);
            return queryViewData.GetXml();
        }

        #endregion

        #region ListQuerySnapShot

        /// <summary>
        /// F9033_ListQuerySnapShot
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>string</returns>
        public string F9033_ListQuerySnapShot(int queryViewId)
        {
            F9033QueryEngineData listQuerySnapShotData = new F9033QueryEngineData();
            listQuerySnapShotData = Helper.F9033_ListQuerySnapShot(queryViewId);
            return listQuerySnapShotData.GetXml();
        }

        #endregion

        #region ListQueryLayout

        /// <summary>
        /// F9033_s the list query layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        public string F9033_ListQueryLayout(int queryViewId, int userId)
        {
            F9033QueryEngineData listQueryLayoutData = new F9033QueryEngineData();
            listQueryLayoutData = Helper.F9033_ListQueryLayout(queryViewId, userId);
            return listQueryLayoutData.GetXml();
        }

        #endregion

        #region GetDefaultLayout

        /// <summary>
        /// F9033_s the get default layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>string</returns>
        public string F9033_GetDefaultLayout(int queryViewId)
        {
            F9033QueryEngineData getDefaultLayoutData = new F9033QueryEngineData();
            getDefaultLayoutData = Helper.F9033GetDefaultLayout(queryViewId);
            return getDefaultLayoutData.GetXml();
        }

        #endregion

        #region ListQueryEngine

        /// <summary>
        /// F9033_s the query engine.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>string</returns>
        public DataSet F9033_QueryEngine(int queryViewId)
        {
            return Helper.F9033ListQueryEngine(queryViewId);
        }

        #endregion

        #region GetSnapShotRecordSet

        /// <summary>
        /// F9033_s the get snap shot record set.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>dataset</returns>
        public DataSet F9033_GetSnapShotRecordSet(int snapShotId, int queryViewId)
        {
            return Helper.F9033_GetSnapShotRecordSet(snapShotId, queryViewId);
        }

        #endregion GetSnapShotRecordSet

        #region InsertSnapShotItems

        /// <summary>
        /// F9033_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotxml">The system snap shotxml.</param>
        /// <returns>Intgere Value</returns>
        public int F9033_InsertSnapShotItems(int? userId, string systemSnapShotxml)
        {
            return Helper.F9033_InsertSnapShotItems(userId, systemSnapShotxml);
        }

        #endregion InsertSnapShotItems

        #region GetSystemSnapShotId

        /// <summary>
        /// F9033_Get System Snapshot Count
        /// </summary>
        /// <param name="systemSnapShotId">The system SnapShot Id</param>
        /// <returns>String</returns>
        public string F9033_GetSystemSnapshotCount(int systemSnapShotId)
        {
            F9033QueryEngineData getSystemSnapShotId = new F9033QueryEngineData();
            getSystemSnapShotId = Helper.F9033_GetSystemSnapshotCount(systemSnapShotId);
            return getSystemSnapShotId.GetXml();
        }

        #endregion GetSystemSnapShotId

        #region GetSystemSnapShotRecordSet

        /// <summary>
        /// F9033_s the get system snap shot record set.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <param name="masterFormNO">The master form NO.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">The key id collection.</param>
        /// <param name="isFitler">The is fitler.</param>
        /// <returns>Dataset value in string formate</returns>
        public DataSet F9033_GetSystemSnapShotRecordSet(int systemSnapShotId, int masterFormNO, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFitler)
        {
            return Helper.F9033_GetSystemSnapShotRecordSet(systemSnapShotId, masterFormNO, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFitler);
        }

        #endregion GetSystemSnapShotRecordSet

        #endregion

        #region F9039QueryUpdate

        #region ListQueryViewColumn

        /// <summary>
        /// Lists the query view column.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>String or dataset</returns>
        public string F9039ListQueryViewColumn(int queryViewId)
        {
            F9039QueryUpdate queryUpdateData = new F9039QueryUpdate();
            queryUpdateData = Helper.F9039ListQueryViewColumn(queryViewId);
            return queryUpdateData.GetXml();
        }

        #endregion ListQueryViewColumn

        #region GetCommandResult

        /// <summary>
        /// F9039s the get command result.
        /// </summary>
        /// <param name="replaceId">The replace ID.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns>String or dataset</returns>
        public string F9039GetCommandResult(int replaceId, string commandResult)
        {
            DataSet queryUpdateData = new DataSet();
            queryUpdateData = Helper.F9039GetCommandResult(replaceId, commandResult);
            return queryUpdateData.GetXml();
        }

        #endregion GetCommandResult

        #region UpdateQueryData

        /// <summary>
        /// F9039s the update query data.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="keyField">The key field.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="updateField">The update field.</param>
        /// <param name="doprocessValue">The do process value.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>string</returns>
        public string F9039UpdateQueryData(int queryViewId, string keyField, string keyId, string updateField, int doprocessValue, int userId)
        {
            return Helper.F9039UpdateQueryData(queryViewId, keyField, keyId, updateField, doprocessValue, userId);
        }

        #endregion UpdateQueryData

        #endregion F9039QueryUpdate

        #region F84722 Water Valve Properties

        #region Get Water Valve Location

        /// <summary>
        /// To Load F84722 Water valve Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Water Valve Loaction Details
        /// </returns>
        public string F84722_GetWaterValveLocation(int keyId, int formId)
        {
            F84722WaterValveLocationData waterValveLocationData = new F84722WaterValveLocationData();
            waterValveLocationData = Helper.F84722_GetWaterValveLocation(keyId, formId);
            return waterValveLocationData.GetXml();
        }

        #endregion Get Water Valve Location

        #region Save Water Valve Location

        /// <summary>
        /// To Save F84722 Water valve Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="waterValveLocation">The water valve location.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing key id</returns>
        public int F84722_SaveWaterValveLocation(int keyId, string waterValveLocation, int formId, int userId)
        {
            return Helper.F84722_SaveWaterValveLocation(keyId, waterValveLocation, formId, userId);
        }

        #endregion Save Water Valve Location

        #endregion F84722 Water Valve Properties

        #region F84723 Water Hydrant Properties

        #region Get Water Hydrant Properties

        /// <summary>
        /// To Load Water Hydrant Properties
        /// </summary>
        /// <param name="hydrantId">The hydrantId.</param>
        /// <returns>Typed DataSet Containing the Water Hydrant Properties Details.</returns>
        public string F84723_GetWaterHydrantProperties(int hydrantId)
        {
            F84723WaterHydrantPropertiesData waterHydrantPropertiesData = new F84723WaterHydrantPropertiesData();
            waterHydrantPropertiesData = Helper.F84723_GetWaterHydrantProperties(hydrantId);
            return waterHydrantPropertiesData.GetXml();
        }

        #endregion Get Water Hydrant Properties

        #region Check Main Valve ID

        /// <summary>
        /// To Check the Main Valve ID
        /// </summary>
        /// <param name="mainValveId">The main valve id.</param>
        /// <returns>
        /// The Integer Value containing whether Main Valve Id exists are not
        /// </returns>
        public int F84723_CheckMainValveId(int mainValveId)
        {
            return Helper.F84723_CheckMainValveId(mainValveId);
        }

        #endregion Check Main Valve ID

        #region Save Water Hydrant Properties

        /// <summary>
        /// To Save Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="waterHydrantPropties">The XML String containing the Water Hydrant Properties Details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The integer valu containing the hydrantId
        /// </returns>
        public int F84723_SaveWaterHydrantProperties(int hydrantId, string waterHydrantPropties, int userId)
        {
            return Helper.F84723_SaveWaterHydrantProperties(hydrantId, waterHydrantPropties, userId);
        }

        #endregion Save Water Hydrant Properties

        #region Delete Water Hydrant Properties

        /// <summary>
        /// To Delete Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">hydrantId</param>
        /// <param name="userId">The user id.</param>
        public void F84723_DeleteWaterHydrantProperties(int hydrantId, int userId)
        {
            Helper.F84723_DeleteWaterHydrantProperties(hydrantId, userId);
        }

        #endregion Delete Water Hydrant Properties

        #endregion F84723 Water Hydrant Properties

        #region F84725 Water Pipe Properties

        #region Get Water Pipe Properties

        /// <summary>
        /// To Load Water Pipe Properties
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        /// <returns>Typed DataSet Containing the Water Pipe Properties details</returns>
        public string F84725_GetWaterPipeProperties(int pipeId)
        {
            F84725WaterPipePropertiesData waterPipePropertiesData = new F84725WaterPipePropertiesData();
            waterPipePropertiesData = Helper.F84725_GetWaterPipeProperties(pipeId);
            return waterPipePropertiesData.GetXml();
        }

        #endregion Get Water Pipe Properties

        #region Save Water Pipe Properties

        /// <summary>
        /// To Save water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeProperties">The XML String Containing the Water Pipe Properties details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the integer value containing the pipeid</returns>
        public int F84725_SaveWaterPipeProperties(int pipeId, string waterPipeProperties, int userId)
        {
            return Helper.F84725_SaveWaterPipeProperties(pipeId, waterPipeProperties, userId);
        }

        #endregion Save Water Pipe Properties

        #region Delete Water Pipe Properties

        /// <summary>
        /// To Delete water pipe properties.
        /// </summary>
        /// <param name="pipeId">the pipe Id</param>
        /// <param name="userId">The user id.</param>
        public void F84725_DeleteWaterPipeProperties(int pipeId, int userId)
        {
            Helper.F84725_DeleteWaterPipeProperties(pipeId, userId);
        }

        #endregion Delete Water Pipe Properties

        #endregion F84725 Water Pipe Properties

        #region F84726 Water Pipe Location

        #region Get Water Pipe Location

        /// <summary>
        /// To Load Water Pipe Location.
        /// </summary>
        /// <param name="pipeId">The Pipe Id.</param>
        /// <returns>Typed Dataset Containg the Water Pipe Location Details.</returns>
        public string F84726_GetWaterPipeLocation(int pipeId)
        {
            F84726WaterPipeLocationData waterPipeLocationData = new F84726WaterPipeLocationData();
            waterPipeLocationData = Helper.F84726_GetWaterPipeLocation(pipeId);
            return waterPipeLocationData.GetXml();
        }

        #endregion Get Water Pipe Location

        #region Save Water Pipe Location

        /// <summary>
        /// To Save Water Pipe Location.
        /// </summary>
        /// <param name="pipeId">The Pipe Id.</param>
        /// <param name="waterPipeLocation">The Xml String containing the Water Pipe Location details</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The Integer value containing pipe Id value
        /// </returns>
        public int F84726_SaveWaterPipeLocation(int pipeId, string waterPipeLocation, int userId)
        {
            return Helper.F84726_SaveWaterPipeLocation(pipeId, waterPipeLocation, userId);
        }

        #endregion Save Water Pipe Location

        #endregion F84726 Water Pipe Location

        #region F15002 District Fund Management

        #region Get Distict Fund Details

        /// <summary>
        /// F15002_s the get distirct fund details.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>string contains the DistrictFund Details</returns>
        public string F15002_GetDistirctFundDetails(int? districtId)
        {
            return Helper.F15002_GetDistirctFundDetails(districtId).GetXml();
        }

        /// <summary>
        /// F15002_s the list all funds.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>string contians the All Funds</returns>
        public string F15002_ListAllFunds(int? fundId, string fund, int? rollYear)
        {
            return Helper.F15002_ListAllFunds(fundId, fund, rollYear).GetXml();
        }

        #endregion

        #region Save and Edit District and Fund

        /// <summary>
        /// F15002_s the check district.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>the error id or primaryKeyId </returns>
        public int F15002_CheckDistrict(int? districtId, string district, int rollYear)
        {
            return Helper.F15002_CheckDistrict(districtId, district, rollYear);
        }

        /// <summary>
        /// F15002_s the create or edit district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtDetails">The district details.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>primaryId or ErrorMessage</returns>
        public int F15002_CreateOrEditDistrictMgmt(int? districtId, string districtDetails, string districtFundItems, int userId)
        {
            return Helper.F15002_CreateOrEditDistrictMgmt(districtId, districtDetails, districtFundItems, userId);
        }

        #endregion

        #region F15002 Get District Type

        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public string F15002_GetDistrictType(int userId)
        {
            F15002DistMgmtData getDistrictType = new F15002DistMgmtData();
            getDistrictType = Helper.F15002_GetDistrictType(userId);
            return getDistrictType.GetXml();
        }

        #endregion

        #endregion

        #region F84123 Sanitary Pipe Properties

        #region Get Sanitary Pipe Properties

        /// <summary>
        ///  To Load F84123 Sanitary Pipe properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe properties Details</returns>
        public string F84123_GetSanitaryPipeProperties(int pipeId)
        {
            F84123SanitaryPipePropertiesData sanitaryPipePropertiesData = new F84123SanitaryPipePropertiesData();
            sanitaryPipePropertiesData = Helper.F84123_GetSanitaryPipeProperties(pipeId);
            return sanitaryPipePropertiesData.GetXml();
        }

        #endregion Get Sanitary Pipe Properties

        #region Save Sanitary Pipe Properties

        /// <summary>
        /// To Save F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <param name="sanitaryPipeProperties">The XML string Containing All values in Sanitary Pipe properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing pipe id</returns>
        public int F84123_SaveSanitaryPipeProperties(int pipeId, string sanitaryPipeProperties, int userId)
        {
            return Helper.F84123_SaveSanitaryPipeProperties(pipeId, sanitaryPipeProperties, userId);
        }

        #endregion Save Sanitary Pipe Properties

        #region Delete Sanitary Pipe Properties

        /// <summary>
        /// To Delete F84123 Sanitary Pipe Properties
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        /// <param name="userId">The user id.</param>
        public void F84123_DeleteSanitaryPipeProperties(int pipeId, int userId)
        {
            Helper.F84123_DeleteSanitaryPipeProperties(pipeId, userId);
        }

        #endregion Delete Sanitary Pipe Properties

        #endregion F84123 Sanitary Pipe Properties

        #region F84124  Sanitary Pipe Location

        #region Get Sanitary Pipe Location

        /// <summary>
        /// To Load F84124 Sanitary Pipe Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Sanitary Pipe Location Details
        /// </returns>
        public string F84124_GetSanitaryPipeLocation(int keyId, int formId)
        {
            F84124SanitaryPipeLocationData sanitaryPipeLocationData = new F84124SanitaryPipeLocationData();
            sanitaryPipeLocationData = Helper.F84124_GetSanitaryPipeLocation(keyId, formId);
            return sanitaryPipeLocationData.GetXml();
        }

        #endregion Get Sanitary Pipe Location

        #region Save Sanitary Pipe Location

        /// <summary>
        /// To Save F84124 Sanitary Pipe Location
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryPipeLocation">The Sanitary Pipe Location.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing key id</returns>
        public int F84124_SaveSanitaryPipeLocation(int keyId, string sanitaryPipeLocation, int userId)
        {
            return Helper.F84124_SaveSanitaryPipeLocation(keyId, sanitaryPipeLocation, userId);
        }

        #endregion Save Sanitary Pipe Location

        #endregion F84124 Sanitary Pipe Location

        #region 11020 Real Property

        #region Get Real Property Statement

        /// <summary>
        /// Gets the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id of the statement to be fetched.</param>
        /// <returns>
        /// the string containing the statement information of the statementid.
        /// </returns>
        public string F11020_GetRealPropertyStatement(int statementId)
        {
            return Helper.F11020_GetRealPropertyStatement(statementId).GetXml();
        }

        #endregion Get Real Property Statement

        #region F11030

        /// <summary>
        /// Get Real Property Statements
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        public string F15030_GetRealPropertyStatements(int statementId)
        {
            return Helper.F15030_GetRealPropertyStatements(statementId).GetXml();
        }

        #endregion

        #region update Real Property Statement

        /// <summary>
        /// update the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">User ID</param>
        public void F1423_UpdateRealPropertyStatement(int statementId, string statementItems, int userId)
        {
            Helper.F1423_UpdateRealPropertyStatement(statementId, statementItems, userId);
        }

        #endregion update Real Property Statement

        #region List Mortgage Name

        /// <summary>
        /// list the mortgage name.
        /// </summary>
        /// <returns>the string with morgage name list</returns>
        public string F1423_ListMortgageName()
        {
            return Helper.F1423_ListMortgageName().GetXml();
        }

        #endregion List Mortgage Name

        #endregion 11020 Real Property

        #region 15020 receipt engine

        #region ListHistoryGrid

        /// <summary>
        /// list history grid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>the string with receipt history and Detail</returns>
        public string F15020_ListHistoryGrid(int statementId)
        {
            return Helper.F15020_ListHistoryGrid(statementId).GetXml();
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// get receipt details and payment items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>the string with receipt detail</returns>
        public string F15020_GetReceiptDetails(int receiptId)
        {
            return Helper.F15020_GetReceiptDetails(receiptId).GetXml();
        }

        #endregion GetReceiptDetails

        #region Tax CalCulation for Receipt Engine

        #region GetMinTaxDue

        /// <summary>
        /// Gets the minimum tax due amount
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <returns>
        /// The decimal containing minimum tax amount due.
        /// </returns>
        public decimal F1003_GetMinTaxDue(int statmentId, string interestDate)
        {
            return Helper.F1003_GetMinTaxDue(statmentId, interestDate);
        }

        #endregion GetMinTaxDue

        #region GetInterestAmount

        /// <summary>
        /// Get the interest amoount.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public decimal F1004_GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            return Helper.F1004_GetInterestAmount(statmentId, interestDate, taxDueAmount);
        }

        #endregion GetInterestAmount

        #endregion Tax CalCulation for Receipt Engine

        #region GetValidReceiptTest

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        public string F1009_GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return Helper.F1009_GetValidReceiptTest(statementId, receiptDate);
        }

        #endregion GetValidReceiptTest

        #endregion 15020 receipt engine

        #region 1405 Master Receipting

        #region SaveReceipt

        /// <summary>
        /// saves the master receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="otherParameterInfo">The other parameter info.</param>
        /// <param name="sharedPaymentId">Shared Payment ID</param>
        /// <returns>the integer - receipt id</returns>
        public int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {
            return Helper.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
        }

        #endregion SaveReceipt

        #endregion

        #region 15104 Receipt Payment

        /// <summary>
        /// F15104_s the get receipt payment.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Receipt Payment Dataset</returns>
        public string F15104_GetReceiptPayment(int receiptId)
        {
            return Helper.F15104_GetReceiptPayment(receiptId).GetXml();
        }

        /// <summary>
        /// F15104_s the update receipt payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">User ID</param>
        public void F15104_UpdateReceiptPayment(string receiptPayment, int userId)
        {
            Helper.F15104_UpdateReceiptPayment(receiptPayment, userId);
        }

        #endregion

        #region 15004 Agency Details

        #region Get AgencyDetails

        /// <summary>
        /// F15004_s the get agency details.
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <returns>F15004Agencymanagement Dataset</returns>
        public string F15004_GetAgencyDetails(int agencyId)
        {
            return Helper.F15004_GetAgencyDetails(agencyId).GetXml();
        }

        #endregion

        #region Check for Agency Dupilcate Record

        /// <summary>
        /// F15004_s the check duplicate agency.
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <param name="agencyName">Name of the agency.</param>
        /// <returns>errorId</returns>
        public int F15004_CheckDuplicateAgency(int agencyId, string agencyName)
        {
            return Helper.F15004_CheckDuplicateAgency(agencyId, agencyName);
        }

        #endregion

        #region Create and  Edit the Agency Details

        /// <summary>
        /// F15004_s the create or edit agency details.
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">User ID</param>
        /// <returns>PrimaryKeyId</returns>
        public int F15004_CreateOrEditAgencyDetails(int agencyId, string acctEmelemts, int userId)
        {
            return Helper.F15004_CreateOrEditAgencyDetails(agencyId, acctEmelemts, userId);
        }

        #endregion

        #endregion

        #region 15007 Account Management Slice

        /// <summary>
        /// F15007_s the check duplicate account.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>errorId</returns>
        public int F15007_CheckDuplicateAccount(int accountId, string acctEmelemts)
        {
            return Helper.F15007_CheckDuplicateAccount(accountId, acctEmelemts);
        }

        #endregion

        #region F9038 LayoutManagement

        #region Get Layout Management

        /// <summary>
        /// F15004_s the get agency details.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>F15004Agencymanagement Dataset</returns>
        public string F9038_LoadLayoutInformation(int queryViewId, int userId)
        {
            return Helper.F9038_LoadLayoutInformation(queryViewId, userId).GetXml();
        }

        #endregion Get Layout Management

        #region Delete LoadLayoutManagement

        /// <summary>
        /// F9038_s the delete layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout ID.</param>
        /// <param name="userId">The user id.</param>
        public void F9038_DeleteLayoutInformation(int queryLayoutId, int userId)
        {
            Helper.F9038_DeleteLayoutInformation(queryLayoutId, userId);
        }

        #endregion Delete LoadLayoutManagement

        #region Save LoadLayoutManagement

        /// <summary>
        /// F9038_s the save layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout id.</param>
        /// <param name="layoutManagement">The layout management.</param>
        /// <param name="layoutxml">The layoutxml.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>THE SAVED LAYOUTID</returns>
        public int F9038_SaveLayoutInformation(int queryLayoutId, string layoutManagement, string layoutxmlValue, int userId)
        {
            return Helper.F9038_SaveLayoutInformation(queryLayoutId, layoutManagement, layoutxmlValue, userId);
        }

        #endregion Save LoadLayoutManagement

        #endregion F9038 LayoutManagement

        #region Get Working Day

        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public DateTime F9001_GetNextWorkingDay()
        {
            return Helper.F9001_GetNextWorkingDay();
        }

        #endregion

        #region F15003 Fund Management

        #region Get Fund and SubFund Details

        /// <summary>
        /// F15003_s the get fund sub fund details.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <returns>string contains the FundSubFund Detials</returns>
        public string F15003_GetFundSubFundDetails(int? fundId)
        {
            return Helper.F15003_GetFundSubFundDetails(fundId).GetXml();
        }

        /// <summary>
        /// F15003_s the list available sub funds.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="fundId">The fund id.</param>
        /// <returns>string contains the Available Fund Details</returns>
        public string F15003_ListAvailableSubFunds(string subFund, string description, int? rollYear, int? fundId)
        {
            return Helper.F15003_ListAvailableSubFunds(subFund, description, rollYear, fundId).GetXml();
        }

        /// <summary>
        /// Lists the type of the fund.
        /// </summary>
        /// <returns>string contains the fundGroutID Details</returns>
        public string F15003_ListFundType()
        {
            F15003FundMgmtData fundMgmtData = new F15003FundMgmtData();
            fundMgmtData.ListFundType.Clear();
            fundMgmtData.ListFundType.Merge(Helper.F15003_ListFundType());
            return fundMgmtData.GetXml();
        }

        #endregion

        #region Save and Edit Fund Details

        /// <summary>
        /// F15003_s the check fund.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>fund status</returns>
        public int F15003_CheckFund(int? fundId, string fund, int rollYear)
        {
            return Helper.F15003_CheckFund(fundId, fund, rollYear);
        }

        /// <summary>
        /// F15003_s the create or edit fund MGMT.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="fundGroupId">The fund group id.</param>
        /// <param name="fundItems">The fund items.</param>
        /// <param name="userId">User ID</param>
        /// <returns>returns the Insert status</returns>
        public int F15003_CreateOrEditFundMgmt(int? fundId, string fund, int rollYear, string description, int? fundGroupId, string fundItems, int userId)
        {
            return Helper.F15003_CreateOrEditFundMgmt(fundId, fund, rollYear, description, fundGroupId, fundItems, userId);
        }

        #endregion

        #endregion


        #region F1505District Copy Form


        /// <summary>
        /// F1505_s the district Copy Form
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtText">The district details.</param>
        /// <param name="rollyear">The rollyear.</param>
        /// <param name="description">The description.</param>
        /// <param name="isactive">The isactive.</param>
        /// <param name="districtTypeId">The districtTypeId.</param>
        /// <param name="ExciseId">The ExciseRateID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message</returns>
        public string F1505ExecuteCopyDistrict(int districtId, string districtText, int rollyear, string description, bool isactive, int districtTypeId, int ExciseId, int userId)
        {
            return Helper.F1505ExecuteCopyDistrict(districtId, districtText, rollyear, description, isactive, districtTypeId, ExciseId, userId);       
        }

        #endregion F1505District Copy Form

        #region F11011 Excise Statement

        #region Get Excise Receipt

        /// <summary>
        /// Gets the Excise Receipt details 
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Receipt Details</returns>
        public string F15012_GetExciseReceipt(int statementId)
        {
            return Helper.F15012_GetExciseReceipt(statementId).GetXml();
        }

        #endregion

        #region Excise Statement Summary

        /// <summary>
        /// Gets the Excise Statement Summary
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Statement Summary Details</returns>
        public string F15011_GetExciseStatement(int statementId)
        {
            return Helper.F15011_GetExciseStatement(statementId).GetXml();
        }

        /// <summary>
        /// update the Excise Statement - receipt and interest date
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="userId">User ID</param>
        public void F15011_SaveExciseStatement(int statementId, DateTime interestDate, DateTime receiptDate, int userId)
        {
            Helper.F15011_SaveExciseStatement(statementId, interestDate, receiptDate, userId);
        }

        #endregion

        #endregion

        #region F15010 Excise Affidavit

        /// <summary>
        /// Gets the IndividualType Used To Fill The Type Combo
        /// </summary>
        /// <returns>returun string with type details</returns>
        public string F15010_GetExciseIndividualType()
        {
            F15010ExciseAffidavitData exciseIndividualType = new F15010ExciseAffidavitData();
            exciseIndividualType = Helper.F15010_GetExciseIndividualType();
            return exciseIndividualType.GetXml();
        }

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <returns>
        /// Retruns excise Tax Affidatvit Details For all six header
        /// </returns>
        public string F15010_GetExciseTaxAffidavitDetails(int statementId)
        {
            F15010ExciseAffidavitData exciseData = new F15010ExciseAffidavitData();
            exciseData = Helper.F15010_GetExciseTaxAffidavitDetails(statementId);
            return exciseData.GetXml();
        }

        /// <summary>
        /// Get Amount Due Details
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">ExciseRateID</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Amount Due Details</returns>
        public string F15010_GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            F15010ExciseAffidavitData exciseTaxAffidavitAmountDue = new F15010ExciseAffidavitData();
            exciseTaxAffidavitAmountDue = Helper.F15010_GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateId, taxCode, taxableSaleAmount);
            return exciseTaxAffidavitAmountDue.GetXml();
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public string F15010_GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            F15010ExciseAffidavitData affidavitStatementIdData = new F15010ExciseAffidavitData();
            affidavitStatementIdData = Helper.F15010_GetAffidavitStatementId(formId, orderField, orderBy);
            return affidavitStatementIdData.GetXml();
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="mobileHomeDetails">Mobile Home Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// returns dataset contains AffiDavit Details
        /// </returns>
        public int F15010_SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, string mobileHomeDetails, int userId)
        {
            return Helper.F15010_SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, mobileHomeDetails, userId);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>returns dastaset containing Owner Details</returns>
        public string F15010_GetOwnerDetails(int ownerId)
        {
            F15010ExciseAffidavitData ownerDetailData = new F15010ExciseAffidavitData();
            ownerDetailData = Helper.F15010_GetOwnerDetails(ownerId);
            return ownerDetailData.GetXml();
        }

        /// <summary>
        /// Get Owner Status.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public string F15010_GetOwnerStatus(int ownerId)
        {
            F15010ExciseAffidavitData ownerStatusData = new F15010ExciseAffidavitData();
            ownerStatusData = Helper.F15010_GetOwnerStatus(ownerId);
            return ownerStatusData.GetXml();
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public string F15010_GetDistrictSelection(int exciseRateId)
        {
            F15010ExciseAffidavitData districtSelection = new F15010ExciseAffidavitData();
            districtSelection = Helper.F15010_GetDistrictSelection(exciseRateId);
            return districtSelection.GetXml();
        }

        /// <summary>
        /// Deletet The Particular StatmentID Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="userId">userId</param>
        public void F15010_DeleteAffidavitDetails(int statementId, int userId)
        {
            Helper.F15010_DeleteAffidavitDetails(statementId, userId);
        }

        /// <summary>
        /// F15010_s the get parcel detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <returns>String</returns>
        public string F15010_GetParcelDetail(int? parcelId, string parcelNumber)
        {
            F15010ExciseAffidavitData districtSelection = new F15010ExciseAffidavitData();
            districtSelection = Helper.F15010_GetParcelDetail(parcelId, parcelNumber);
            return districtSelection.GetXml();
        }

        /// <summary>
        /// F15010_s the list excise WAC.
        /// </summary>
        /// <returns></returns>
        public string F15010_ListExciseWAC()
        {
            F15010ExciseAffidavitData excisewac = new F15010ExciseAffidavitData();
            excisewac = Helper.F15010_ListExciseWAC();
            return excisewac.GetXml();
        }


        /// <summary>
        /// F5010_s the list excise individual.
        /// </summary>
        /// <param name="ExciseIndividualElements">The excise individual elements.</param>
        /// <returns></returns>
        public string F15010_ListExciseIndividual(string ExciseIndividualElements)
        {
            F15010ExciseAffidavitData exciseIndividualElements = new F15010ExciseAffidavitData();
            exciseIndividualElements = Helper.F15010_ListExciseIndividual(ExciseIndividualElements);
            return exciseIndividualElements.GetXml();
        }

        public string F15010_ListOpenSpaceField(string parcelIds)
        {
            F15010ExciseAffidavitData openSpaceData = new F15010ExciseAffidavitData();
            openSpaceData = Helper.F15010_ListOpenSpaceField(parcelIds);
            return openSpaceData.GetXml();
        }

        #endregion F15010 Excise Affidavit

        #region F15013 Excise Tax Rate

        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public string F15013_GetExciseTaxRate(int exciseRateId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            exciseTaxRateData = Helper.F15013_GetExciseTaxRate(exciseRateId);
            return exciseTaxRateData.GetXml();
        }
        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public string F15013_ListExciseTaxRate()
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            exciseTaxRateData = Helper.F15013_ListExciseTaxRate();
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// F15013_s the save excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">User ID</param>
        /// <returns>errordId/PrimaryKey Id</returns>
        public int F15013_SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            return Helper.F15013_SaveExciseTaxRate(exciseRateId, exciseTaxDetails, userId);
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">The excise rate Id.</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// The return value specifying status of the delete action.
        /// </returns>
        public int F15013_DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            return Helper.F15013_DeleteExciseTaxRate(exciseRateId, userId);
        }

        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public string F15013_GetDistrictName(int districtId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            exciseTaxRateData = Helper.F15013_GetDistrictName(districtId);
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public string F15013_GetAccountName(int accountId)
        {
            F15013ExciseTaxRateData exciseTaxRateData = new F15013ExciseTaxRateData();
            exciseTaxRateData = Helper.F15013_GetAccountName(accountId);
            return exciseTaxRateData.GetXml();
        }

        #endregion

        #endregion

        #region F15019 Journal Entry

        /// <summary>
        /// Gets the journal entry details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>returns DataSet</returns>
        public string GetJournalEntryDetails(int receiptId)
        {
            F15019JournalEntryData form15019JournalEntryData = new F15019JournalEntryData();
            form15019JournalEntryData = Helper.F15019_GetJournalEntryDetails(receiptId);
            return form15019JournalEntryData.GetXml();
        }

        /// <summary>
        /// Updates the journal entry details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>Integer Value</returns>
        public int UpdateJournalEntryDetails(int statementId, int receiptSourceId, string journalItems)
        {
            return Helper.F15019_InsertJournalEntryDetails(statementId, receiptSourceId, journalItems);
        }

        /// <summary>
        /// F15019_s the check roll year.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>integer value</returns>
        public int F15019_CheckRollYear(int statementId, int receiptSourceId, string journalItems)
        {
            return Helper.F15019_CheckRollYear(statementId, receiptSourceId, journalItems);
        }

        #endregion F15019 Journal Entry

        #region F9013 Next Number Configuration

        #region List NextNumber Configuration

        /// <summary>
        /// List the NextNumber Configuration details
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The dataset containing the list of NextNumber Configuration.
        /// </returns>
        public string F9013_ListNextNumberConfiguration(int rollYear, int userId)
        {
            F9013NextNumberData nextNumberData = new F9013NextNumberData();
            nextNumberData = Helper.F9013_ListNextNumberConfiguration(rollYear, userId);
            return nextNumberData.GetXml();
        }

        #endregion List NextNumber Configuration

        #region Check Next Number

        /// <summary>
        /// Check for valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>The dataset containing the valid Next Number details.</returns>
        public DataSet F9013_CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return Helper.F9013_CheckNextNumber(rollYear, nextNum, formula);
        }

        #endregion Check Next Number

        #region Update NextNumber ConfigDetails

        /// <summary>
        /// Saves Next Number configuration details
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">The user id.</param>
        public void F9013_UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            Helper.F9013_UpdateNextNumberConfigDetails(nextNumId, nextNum, formula, userId);
        }

        #endregion Update NextNumber ConfigDetails

        #region List Roll Year

        /// <summary>
        /// List the NextNumber Roll Year details
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The dataset containing the list of NextNumber Configuration.
        /// </returns>
        public string F9013_ListNextNumberRollYear(int userId)
        {
            F9013NextNumberData nextNumberData = new F9013NextNumberData();
            nextNumberData = Helper.F9013_ListNextNumberRollYear(userId);
            return nextNumberData.GetXml();
        }

        #endregion List Roll Year

        #endregion F9013 Next Number Configuration

        #region F11018 Misc Receipt

        /// <summary>
        /// Gets the Misc Receipt details based on the receiptId
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The string containing the receipt information of the receiptId.
        /// </returns>
        public string F15018_GetMiscReceipt(int receiptId)
        {
            return Helper.F15018_GetMiscReceipt(receiptId).GetXml();
        }

        /// <summary>
        /// gets the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateId">MiscTemplate ID</param>
        /// <returns>
        /// The string containing the MiscReceiptTemplate information of the miscTemplateID.
        /// </returns>
        public string F1021_GetMiscReceiptTemplate(int miscTemplateId)
        {
            return Helper.F1021_GetMiscReceiptTemplate(miscTemplateId).GetXml();
        }

        /// <summary>
        /// saves the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateDetails">The misc template details.</param>
        /// <param name="templateItems">The template items.</param>
        /// <param name="userId">User ID</param>
        /// <returns>
        /// new created templated id - return templatedid if succeed else return negative value
        /// </returns>
        public int F1021_SaveMiscReceiptTemplate(string miscTemplateDetails, string templateItems, int userId)
        {
            return Helper.F1021_SaveMiscReceiptTemplate(miscTemplateDetails, templateItems, userId);
        }

        /// <summary>
        /// List the Misc Receipt template
        /// </summary>
        /// <returns>
        /// The string containing the Misc Receipt Template
        /// </returns>
        public string F1022_ListMiscReceiptTemplate()
        {
            return Helper.F1022_ListMiscReceiptTemplate().GetXml();
        }

        /// <summary>
        /// Deletes the Misc Receipt Template based on the miscTemplateID
        /// </summary>
        /// <param name="miscTemplateId">MiscTemplate ID</param>
        /// <param name="userId">User ID</param>
        public void F1022_DeleteMiscReceiptTemplate(int miscTemplateId, int userId)
        {
            Helper.F1022_DeleteMiscReceiptTemplate(miscTemplateId, userId);
        }

        /// <summary>
        /// Save district details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        public string F1024_SaveDistrictDetails(int levyOption, int districtId, decimal amountValue, int userId, bool IsReplace, string SubFundXML)
        {
            return Helper.F1024_SaveDistrictDetails(levyOption, districtId, amountValue, userId, IsReplace, SubFundXML).GetXml();
        }

        /// <summary>
        /// List district distribution details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <param name="IsReplace">IsReplace</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        public string GetDistrictDistributionData(int LevyOptionId, int districtId, decimal amount, int userId, string subfundsXML, bool isreplace)
        {
            return Helper.GetDistrictDistributionData(LevyOptionId, districtId, amount, userId, subfundsXML, isreplace).GetXml();
        }

        /// <summary>
        /// Gets the district sub fund Items data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public string GetDistrictData(int districtId)
        {
            return Helper.GetDistrictData(districtId).GetXml();
        }

        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        public string F15018_ListAccountDetails(string filterValue, int? rollYear,int? formNo)
        {
            return Helper.F15018_ListAccountDetails(filterValue, rollYear,formNo).GetXml();
        }

        #endregion

        #region F1025 AutoFund Transfer

        #region List RollYear

        /// <summary>
        /// F1025_s the list roll year.
        /// </summary>
        /// <returns>Typed DataSet</returns>
        public string F1025_ListRollYear()
        {
            return Helper.F1025_ListRollYear().GetXml();
        }
        #endregion

        #region F1025_ListAutoFundTransferDetails

        /// <summary>
        /// F1025_s the list auto fund transfer details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Typed Dataset</returns>
        public string F1025_ListAutoFundTransferDetails(int rollYear)
        {
            return Helper.F1025_ListAutoFundTransferDetails(rollYear).GetXml();
        }
        #endregion

        #region Delete AutoFund Transfer

        /// <summary>
        /// F1025_s the delete auto fund transfer details.
        /// </summary>
        /// <param name="autoTransferId">The auto transfer id.</param>
        /// <param name="userId">USer ID</param>
        /// <returns>Integer value</returns>
        public int F1025_DeleteAutoFundTransferDetails(int autoTransferId, int userId)
        {
            return Helper.F1025_DeleteAutoFundTransferDetails(autoTransferId, userId);
        }

        #endregion Delete AutoFund Transfer

        #region Update AutoFund Transfer

        /// <summary>
        /// F1025_s the update auto fund transfer details.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer Value</returns>
        public int F1025_UpdateAutoFundTransferDetails(string autoFundItems, int userId)
        {
            return Helper.F1025_UpdateAutoFundTransferDetails(autoFundItems, userId);
        }
        #endregion Update AutoFund Transfer

        #region Check RollYear

        /// <summary>
        /// F1025_s the check roll year.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <returns>Integer Value</returns>
        public int F1025_CheckRollYear(string autoFundItems)
        {
            return Helper.F1025_CheckRollYear(autoFundItems);
        }

        #endregion Check RollYear

        #endregion F1025 AutoFund Transfer

        #region RDL to Code

        #region Get

        /// <summary>
        /// RdlToCode_Get
        /// </summary>
        /// <param name="getxmlString">The getxml string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        public DataSet RdlToCode_Get(string getxmlString, string formId)
        {
            return Helper.RdlToCode_Get(getxmlString, formId);
        }
        #endregion Get

        #region Fill Combo

        #region OldMethod Commented By Shiva.
        /*
        /// <summary>
        /// Fill method
        /// </summary>
        /// <param name="parameterData">The parameter data.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns>dataset</returns>
        public DataSet RdlToCode_FillCombo(RdlToCodeData.ParameterDataDataTable parameterData, String entityName)
        {
              DataSet rdlToCodeDataSet = new DataSet();
              rdlToCodeDataSet = Helper.RdlToCode_FillCombo(parameterData, entityName);
              return rdlToCodeDataSet.GetXml();
            return Helper.RdlToCode_FillCombo(parameterData, entityName);
        }
       */
        #endregion

        /// <summary>
        /// RDLs to code_ fill combo.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>dataset</returns>
        public DataSet RdlToCode_FillCombo(string storedProcedureName)
        {
            return Helper.RdlToCode_FillCombo(storedProcedureName);
        }

        #endregion Fill Combo

        #region Save

        /// <summary>
        /// RDL Save Method.
        /// </summary>
        /// <param name="saveXMLString">saveXML String.</param>
        /// <param name="formId">form Id.</param>
        /// <returns>primary Key Id.</returns>
        public int RdlToCode_Save(string savexmlString, string formId)
        {
            return Helper.RdlToCode_Save(savexmlString, formId);
        }

        #endregion Save

        #region Delete

        /// <summary>
        /// RdlToCode_Delete
        /// </summary>
        /// <param name="deletexmlString">The deletexml string.</param>
        /// <param name="formId">formId</param>
        public void RdlToCode_Delete(string deletexmlString, string formId)
        {
            Helper.RdlToCode_Delete(deletexmlString, formId);
        }
        #endregion Delete

        #endregion RDL to Code

        #region F27006 Parcel Ownership

        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public string F27006_ListParcelOwnership(int parcelId)
        {
            F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();
            parcelOwnershipData = Helper.F27006_ListParcelOwnership(parcelId);
            return parcelOwnershipData.GetXml();
        }

        #endregion List Parcel Ownership

        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public string F27006_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();
            parcelOwnershipData = Helper.F27006_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
            return parcelOwnershipData.GetXml();
        }

        #endregion List All Owner Details

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">User ID</param>
        public void F27006_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId, bool isfuturePush)
        {
            Helper.F27006_SaveParcelOwnership(parcelOwnership, parcelId, userId, isfuturePush);
        }

        #endregion Save Parcel Ownership

        #region Check Ownership Details

        /// <summary>
        /// To Check Given Ownership Details is valid.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>returns an integer Value whather given details are correct or not</returns>
        public int F27006_CheckOwnershipDetails(string ownershipDetails)
        {
            return Helper.F27006_CheckOwnershipDetails(ownershipDetails);
        }

        #endregion Check Ownership Details

        #region F27006 list MOwnerType Selection

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>string</returns>
        public string ListMOwnerType()
        {
            F27006ParcelOwnershipData getMOwnerTypeData = new F27006ParcelOwnershipData();
            getMOwnerTypeData = Helper.ListMOwnerType();
            return getMOwnerTypeData.GetXml();
        }
        #endregion

        #endregion F27006 Parcel Ownership
        #region F27008 TRParcel Ownership


        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public string F27008_ListParcelOwnership(int parcelId)
        {
            F27008TRParcelOwnershipData parcelOwnershipData = new F27008TRParcelOwnershipData();
            parcelOwnershipData = Helper.F27008_ListParcelOwnership(parcelId);
            return parcelOwnershipData.GetXml();
        }

        #endregion List Parcel Ownership

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">User ID</param>
        public void F27008_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId)
        {
            Helper.F27008_SaveParcelOwnership(parcelOwnership, parcelId, userId);
        }

        #endregion Save Parcel Ownership

        #region Get Owner Details
        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>returns dastaset containing Owner Details</returns>
        public string F27008_GetOwnerDetails(int ownerId, int userId)
        {
            F27008TRParcelOwnershipData ownerDetailData = new F27008TRParcelOwnershipData();
            ownerDetailData = Helper.F27008_GetOwnerDetails(ownerId, userId);
            return ownerDetailData.GetXml();
        }
        #endregion Get Owner Details

        #endregion F27008 TRParcel Ownership

        #region Parcel Header

        /// <summary>
        /// Gets the Materials Footer Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>XML as String</returns>
        /// ///
        public string F25000_GetParcelDetails(int parcelId)
        {
            F25000ParcelHeaderData parcelHeaderData = new F25000ParcelHeaderData();
            parcelHeaderData = Helper.F25000_GetParcelDetails(parcelId);
            return parcelHeaderData.GetXml();
        }

        public F25000FieldUseData F25000_GetCheckOutDetails(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData FieldUseData = new F25000FieldUseData();
            FieldUseData = Helper.F25000_GetCheckOutDetails(snapShotId, snapShotValue);
            return FieldUseData;
        }


        #region F26000ParcelHeaderForm
        /// <summary>
        /// Get Parcel Details
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        public string F26000_GetParcelFormDetails(int parcelId)
        {
            F26000ParcelHeaderFormData parcelHeaderFormData = new F26000ParcelHeaderFormData();
            parcelHeaderFormData = Helper.F26000_GetParcelFormDetails(parcelId);
            return parcelHeaderFormData.GetXml();
        }

        /// <summary>
        /// F26000_s the type of the get apprisal.
        /// </summary>
        /// <returns></returns>
        public string  F26000_GetApprisalType()
        {
            F26000ParcelHeaderFormData parcelHeaderFormData = new F26000ParcelHeaderFormData();
            parcelHeaderFormData = Helper.F26000_GetApprisalType();
            return parcelHeaderFormData.GetXml();
        }

        /// <summary>
        /// F26000_s the exemption details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="exemptionFromAmount">The exemption from amount.</param>
        /// <returns></returns>
        public string F26000_ExemptionDetails(int parcelId, string exemptionCode, decimal? exemptionFromAmount)
        {
            F26000ParcelHeaderFormData parcelHeaderObj=new F26000ParcelHeaderFormData();
            parcelHeaderObj= Helper.F26000_ExemptionDetails(parcelId,exemptionCode,exemptionFromAmount);
            return parcelHeaderObj.GetXml();
        }

        /// <summary>
        /// F26000_s the exempt field details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exmptionId">The exmption id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <returns></returns>
        public string F26000_ExemptFieldDetails(int parcelId, int exmptionId, string exemptionCode)
        {
            F26000ParcelHeaderFormData parcelHeaderObj = new F26000ParcelHeaderFormData();
            parcelHeaderObj = Helper.F26000_ExemptFieldDetails(parcelId, exmptionId,exemptionCode);
            return parcelHeaderObj.GetXml();
        }
        /// <summary>
        /// F26000_s the class code details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public string F26000_ClassCodeDetails(string filterValue)
        {
            F26000ParcelHeaderFormData parcelHeaderObj = new F26000ParcelHeaderFormData();
            parcelHeaderObj = Helper.F26000_ClassCodeDetails(filterValue);
             return parcelHeaderObj.GetXml();
        }
        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public int UpdateParcelHeaderFormDetails(int parcelId, string parcelDetails, int userId, int rollYear)
        {
            return Helper.UpdateParcelHeaderFormDetails(parcelId, parcelDetails, userId, rollYear);
        }

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        public string PrimaryImprovementList()
        {
            F26000ParcelHeaderFormData getPrimaryImprovementData = new F26000ParcelHeaderFormData();
            getPrimaryImprovementData = Helper.PrimaryImprovementList();
            return getPrimaryImprovementData.GetXml();
        }

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        public string PrimaryLandTypeList()
        {
            F26000ParcelHeaderFormData getPrimaryLandTypeData = new F26000ParcelHeaderFormData();
            getPrimaryLandTypeData = Helper.PrimaryLandTypeList();
            return getPrimaryLandTypeData.GetXml();
        }

        /// <summary>
        /// F2101_s the get location selection.
        /// </summary>
        /// <param name="locationCode">The location code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public string f2101_GetLocationSelection(string locationCode, string description)
        {
            F2101LocationSelectionData LocationSelection = new F2101LocationSelectionData();
            LocationSelection = Helper.f2101_GetLocationSelection(locationCode, description);
            return LocationSelection.GetXml();
        }

        /// <summary>
        /// F2102_s the get grouping selection.
        /// </summary>
        /// <param name="groupCode">The group code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public string f2102_GetGroupingSelection(string groupCode, string description)
        {
            F2102GroupingSelectionData GroupingSelection = new F2102GroupingSelectionData();
            GroupingSelection = Helper.f2102_GetGroupingSelection(groupCode, description);
            return GroupingSelection.GetXml();
        }

        /// <summary>
        /// F2103_s the get exemption selection.
        /// </summary>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="description">The description.</param>
        /// <param name="percent">The percent.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public string f2103_GetExemptionSelection(string exemptionCode, string description, decimal? percent, decimal? maximum, int? rollYear)
        {
            F2103ExemptionSelectionData ExemptionSelection = new F2103ExemptionSelectionData();
            ExemptionSelection = Helper.f2103_GetExemptionSelection(exemptionCode, description, percent, maximum, rollYear);
            return ExemptionSelection.GetXml();
        }
        #endregion

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        public string ListPrimaryImprovement()
        {
            F25000ParcelHeaderData getPrimaryImprovementData = new F25000ParcelHeaderData();
            getPrimaryImprovementData = Helper.ListPrimaryImprovement();
            return getPrimaryImprovementData.GetXml();
        }

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        public string ListPrimaryLandType()
        {
            F25000ParcelHeaderData getPrimaryLandTypeData = new F25000ParcelHeaderData();
            getPrimaryLandTypeData = Helper.ListPrimaryLandType();
            return getPrimaryLandTypeData.GetXml();
        }

        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public int UpdateParcelHeaderDetails(int parcelId, string parcelDetails,bool isCopyHeader,int userId)
        {
            return Helper.UpdateParcelHeaderDetails(parcelId, parcelDetails,isCopyHeader,userId);
        }
        #endregion Parcel Header


        #region F25051ParcelHeader

        /// <summary>
        /// Gets the Parcel Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>XML as String</returns>
        /// ///
        public string F25051_GetParcelDetails(int parcelId)
        {
            F25051ParcelHeaderData parcelHeaderData = new F25051ParcelHeaderData();
            parcelHeaderData = Helper.F25051_GetParcelDetails(parcelId);
            return parcelHeaderData.GetXml();
        }

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        public string F25051ParcelClassTypes()
        {
            F25051ParcelHeaderData getParcelClass = new F25051ParcelHeaderData();
            getParcelClass = Helper.F25051ParcelClassTypes();
            return getParcelClass.GetXml();
        }

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        public string F25051OwnerOccupied()
        {
            F25051ParcelHeaderData getOwnerOccupied = new F25051ParcelHeaderData();
            getOwnerOccupied = Helper.F25051OwnerOccupied();
            return getOwnerOccupied.GetXml();
        }

        /// <summary>
        /// Update Parcel Header Details for F25051
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int f25051ParcelHeaderDetails(int parcelId, string parcelDetails, int userId)
        {
            return Helper.f25051ParcelHeaderDetails(parcelId, parcelDetails, userId);
        }

        #endregion F25051ParcelHeader

        #region ParcelType Details

        /// <summary>
        /// GetParcelType Details 
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>XML as String</returns>
        /// ///
        public string GetParcelTypeDetails(int parcelId)
        {
            F2004ParcelCopyData parcelType = new F2004ParcelCopyData();
            parcelType = Helper.GetParcelTypeDetails(parcelId);
            return parcelType.GetXml();
        }

        #endregion ParcelType Details

        #region ParcelType Details

        /// <summary>
        /// GetParcelAttachmentDetails 
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>XML as String</returns>
        public string GetParcelAttachmentDetails(int oldParcelID, int newParcelID, int userID, int moduleID)
        {
            F2004ParcelCopyData parcelType = new F2004ParcelCopyData();
            parcelType = Helper.GetParcelAttachmentDetails(oldParcelID, newParcelID, userID, moduleID);
            return parcelType.GetXml();
        }

        #endregion ParcelType Details

        /// <summary>
        /// Create New Parcel Copy
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelTypeId">The parcel type id.</param>
        /// <param name="copyAllObjects">Copy All Objects</param>
        /// <param name="copyAllSlices">Copy All Slices</param>
        /// <param name="copyAttachments">Copy Attachments</param>
        /// <param name="copyComments">Copy Comments</param>
        /// <param name="parcelElements">Parcel Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int CreateNewParcelCopy(int parcelId, int parcelTypeId, int copyAllObjects, int copyAllSlices, int copyAttachments, int copyComments, string parcelElements, int userId)
        {
            return Helper.CreateNewParcelCopy(parcelId, parcelTypeId, copyAllObjects, copyAllSlices, copyAttachments, copyComments, parcelElements, userId);
        }

        #region Parcel Lock

        /// <summary>
        /// Gets the Materials Footer Details
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <returns>XML as String</returns>        
        public string F2001_getParcelLockingDetails(int keyId)
        {
            F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();
            parcelLockingData = Helper.F2001_getParcelLockingDetails(keyId);
            return parcelLockingData.GetXml();
        }

        /// <summary>
        /// F2001_getParcelLockingDetails
        /// </summary>
        /// <param name="userId">userId </param>
        /// <returns>string</returns>
        public string F2001_getParcelLockingUsername(int userId)
        {
            F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();
            parcelLockingData = Helper.F2001_getParcelLockingUsername(userId);
            return parcelLockingData.GetXml();
        }

        /// <summary>
        /// F2001_UpdateParcelLockingDetails
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="valueLock">valueLock</param>
        /// <param name="adminLock">The admin lock.</param>
        /// <param name="lockAppraisal">The lock appraisal.</param>
        /// <param name="userId">User Id</param>
        /// <returns>Integer</returns>
        public int F2001_UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int userId)
        {
            return Helper.F2001_UpdateParcelLockingDetails(keyId, valueLock, adminLock, lockAppraisal, userId);
        }

        /// <summary>
        /// F2001_s the name of the get valid user.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="formNo">The form no.</param>
        /// <returns>Integer value</returns>
        public int F2001_GetValidUserName(int prcelId, int userId, string formNo)
        {
            return Helper.F2001_GetValidUserName(prcelId, userId, formNo);
        }

        #endregion Parcel Lock

        #region Get ParcelType

        /// <summary>
        /// GetParcelType Details 
        /// </summary>
        /// <returns>XML as String</returns>
        public string GetParcelType()
        {
            F2000ParcelStatusData parcelType = new F2000ParcelStatusData();
            parcelType = Helper.GetParcelType();
            return parcelType.GetXml();
        }

        #endregion Get ParcelType

        #region F15050FeeManagement

        /// <summary>
        /// Get ComboData
        /// </summary>
        /// <returns>string</returns>
        public string F15050_ComboData()
        {
            F15050FeeManagementData feeData = new F15050FeeManagementData();
            feeData = Helper.F15050_ComboData();
            return feeData.GetXml();
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="feeId">Fee ID</param>
        /// <returns>string</returns>
        public string F15050_getDatas(int feeId)
        {
            F15050FeeManagementData feeData = new F15050FeeManagementData();
            feeData = Helper.F15050_getDatas(feeId);
            return feeData.GetXml();
        }

        /// <summary>
        /// F15050_s the save fee management.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="feeTypeId">The fee type id.</param>
        /// <returns>return value</returns>
        public int F15050_SaveFeeManagement(int feeId, string description, decimal amount, int accountId, int userId, byte feeTypeId)
        {
            return Helper.F15050_SaveFeeManagement(feeId, description, amount, accountId, userId, feeTypeId);
        }

        /// <summary>
        /// F15050_ApplyFees
        /// </summary>
        /// <param name="feeXML">Apply feeXML</param>
        /// <param name="amount">amount</param>
        /// <param name="description">description</param>
        /// <param name="accountId">accountId</param>
        /// <param name="userId">UserId</param>
        /// <returns>int</returns>
        public int F15050_ApplyFees(string feeXML, decimal amount, string description, int accountId, int userId)
        {
            return Helper.F15050_ApplyFees(feeXML, amount, description, accountId, userId);
        }

        /// <summary>
        /// F15050_s the list fee types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The fee mgmt dataset xml string</returns>
        public string F15050_ListFeeTypes(int userId)
        {
            F15050FeeManagementData feeData = new F15050FeeManagementData();
            feeData = Helper.F15050_ListFeeTypes(userId);
            return feeData.GetXml();
        }

        /// <summary>
        /// F15050_s the remove template.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="userId">The user id.</param>
        public void F15050_RemoveTemplate(int feeId, int userId)
        {
            Helper.F15050_RemoveTemplate(feeId, userId);
        }

        #endregion

        #region F35001 Value Slice Header/Adjustment

        #region Get Value Slice Header

        /// <summary>
        /// F35001_s the get value slice header.
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <returns>
        /// the XML String Contains the ValueSlice Header Details.
        /// </returns>
        public string F35001_GetValueSliceHeader(int valueSliceId)
        {
            F35001ValueSliceHeaderData valueSliceHeaderData = new F35001ValueSliceHeaderData();
            valueSliceHeaderData = Helper.F35001_GetValueSliceHeader(valueSliceId);
            return valueSliceHeaderData.GetXml();
        }

        #endregion

        #region Get Adjustment Slice Value

        /// <summary>
        /// F35001_s the get adjustment slice value.
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <param name="type">The type.</param>
        /// <param name="isvalue">The is value.</param>
        /// <param name="adjustmentValue">The adjustment value.</param>
        /// <returns>Object Contains the Adjustment Value.</returns>
        public string F35001_GetAdjustmentSliceValue(int valueSliceId, byte type, bool isvalue, decimal adjustmentValue)
        {
            return Helper.F35001_GetAdjustmentSliceValue(valueSliceId, type, isvalue, adjustmentValue);
        }

        #endregion

        #region List Adjustment Types

        /// <summary>
        /// F35002_s the type of the list adjustment.
        /// </summary>
        /// <param name="masterFromNo">The master from no.</param>
        /// <returns>string contains the adjustment types.</returns>
        public string F35002_ListAdjustmentType(int? masterFromNo)
        {
            F35001ValueSliceHeaderData valueSliceHeaderData = new F35001ValueSliceHeaderData();
            valueSliceHeaderData.ListAdjustmentType.Clear();
            valueSliceHeaderData.ListAdjustmentType.Merge(Helper.F35002_ListAdjustmentType(masterFromNo));
            return valueSliceHeaderData.GetXml();
        }

        #endregion List Adjustment Types

        #region Delete Value Slice

        /// <summary>
        /// F35001_s the delete value slice.
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <param name="userId">User ID</param>
        public void F35001_DeleteValueSlice(int valueSliceId, int userId)
        {
            Helper.F35001_DeleteValueSlice(valueSliceId, userId);
        }

        #endregion

        #endregion

        #region F35000 Appraisal Value Summary

        #region Insert/Update Value Slice

        /// <summary>
        /// F35000_s the update value slice.
        /// </summary>
        /// <param name="valueSliceId">value Slice ID</param>
        /// <param name="valueSliceHeaderItems">The value slice header items.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key Id or Error Id.</returns>
        public int F35000_InsertOrUpdateValueSlice(int? valueSliceId, string valueSliceHeaderItems, int userId)
        {
            return Helper.F35000_InsertOrUpdateValueSlice(valueSliceId, valueSliceHeaderItems, userId);
        }

        #endregion

        #region Get Appraisal Summary Objects

        /// <summary>
        /// F35000_s the get appraisal summary objects.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>AppraisalSummary DataSet</returns>
        public string F35000_GetAppraisalSummaryObjects(int parcelId)
        {
            F35000AppraisalSummaryData appraisalSummaryData = new F35000AppraisalSummaryData();
            appraisalSummaryData = Helper.F35000_GetApparaisalSummaryObjecy(parcelId);
            return appraisalSummaryData.GetXml();
        }

        #endregion Get Appraisal Summary Objects

        #region CheckAppraisalSummaryUser

        /// <summary>
        /// F35000_s the get appraisal summary objects.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>AppraisalSummary DataSet</returns>
        public string F35000_CheckAppraisalSummaryUser(int valueSliceId, int objectId, int userId)
        {
            F35000AppraisalSummaryData appraisalSummaryData = new F35000AppraisalSummaryData();
            appraisalSummaryData = Helper.F35000_CheckAppraisalSummaryUser(valueSliceId, objectId, userId);
            return appraisalSummaryData.GetXml();
        }

        #endregion CheckAppraisalSummaryUser

        #region Insert Object

        /// <summary>
        /// F35000_s the insert object.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <param name="objectTypeID">The object type ID.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key Id if Success else Error Id</returns>
        public int F35000_InsertObject(int parcelId, Int16 objectTypeId, string description, int userId)
        {
            return Helper.F35000_InsertObject(parcelId, objectTypeId, description, userId);
        }

        #endregion

        #region UpdateObject
        /// <summary>
        /// F35000_s the Save object.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <param name="properitiesXML">The properitiesXML.</param>
        /// <param name="userId">User ID</param>
        public void F35000_SaveAppraisal(int parcelId, string propertiesXML, int userId)
        {
            Helper.F35000_SaveAppraisal(parcelId, propertiesXML, userId);
        }

        #endregion UpdateObject

        #region List Object Slice Types

        /// <summary>
        /// F35000_s the list object slice types.
        /// </summary>
        /// <returns>string contains the list object and slice types.</returns>
        public string F35000_ListObjectSliceTypes(int? ParcelId)
        {
            F35000AppraisalSummaryData appraisalSummaryData = new F35000AppraisalSummaryData();
            appraisalSummaryData = Helper.F35000_ListObjectSliceTypes(ParcelId);
            return appraisalSummaryData.GetXml();
        }

        #endregion

        #region List  Slice Types

        /// <summary>
        /// F35000_s the list  slice types.
        /// </summary>
        /// <param name="objectID">The object  ID.</param>
        /// <returns>string contains the list slice types.</returns>
        public string F35000_ListSliceTypes(int objectId)
        {
            F35000AppraisalSummaryData appraisalSummaryData = new F35000AppraisalSummaryData();
            appraisalSummaryData = Helper.F35000_ListSliceTypes(objectId);
            return appraisalSummaryData.GetXml();
        }

        #endregion

        #endregion

        public string F35000_ObjectTotal(int parcelId)
        {
            F35000AppraisalSummaryData appraisalValue = new F35000AppraisalSummaryData();
            appraisalValue = Helper.F35000_ObjectTotal(parcelId);
            return appraisalValue.GetXml();
            // return Helper.F35000_ObjectTotal(parcelId);
        }


        #region F35100 NeighborhoodHeader

        ////public string F35100_GetNeighborhoodHeaderDetails(int neighborId)
        ////{
        ////    F35100NeighborhoodHeaderData f35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
        ////    f35100NeighborhoodHeaderData = Helper.F35100_GetNeighborhoodHeaderDetails(neighborId);
        ////    return f35100NeighborhoodHeaderData.GetXml();
        ////}

        /// <summary>
        /// Get Neighborhood Header User Details
        /// </summary>
        /// <param name="applicationId">ApplicationId</param>
        /// <returns>string</returns>
        public string F35100_GetNeighborhoodHeaderUserDetails(int applicationId)
        {
            F35100NeighborhoodHeaderData form35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            form35100NeighborhoodHeaderData = Helper.F35100_GetNeighborhoodHeaderUserDetails(applicationId);
            return form35100NeighborhoodHeaderData.GetXml();
        }

        ////public string F35100_GetNeighborhoodGroupDetails(int rollYear)
        ////{
        ////    F35100NeighborhoodHeaderData f35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
        ////    f35100NeighborhoodHeaderData = Helper.F35100_GetNeighborhoodGroupDetails(rollYear);
        ////    return f35100NeighborhoodHeaderData.GetXml();
        ////}

        /// <summary>
        /// Save Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int F35100_SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId)
        {
            return Helper.F35100_SaveNeighborhoodHeaderDetails(nbhId, nbhDetails, userId);
        }

        /// <summary>
        /// Copy Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int F3511_ExeNeighborhoodDetails(int nbhId, string newnbhdName, int userId)
        {
            return Helper.F3511_ExeNeighborhoodDetails(nbhId, newnbhdName, userId);
        }

        /// <summary>
        /// Duplicate Neighborhood Header Check
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <returns>Integer</returns>
        public int DuplicateNeighborhoodHeaderCheck(int nbhId, string nbhDetails)
        {
            return Helper.DuplicateNeighborhoodHeaderCheck(nbhId, nbhDetails);
        }

        #region Delete Neighborhood  Header

        /// <summary>
        /// To Delete F35100 Neighborhood Header.
        /// </summary>
        /// <param name="nbhdId">Neighborhood ID</param>
        /// <param name="userId">User ID</param>
        public void F35100_DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            Helper.F35100_DeleteNeighborhoodHeader(nbhdId, userId);
        }

        #endregion

        #region Get Neighborhood Details

        /// <summary>
        /// Get Neighborhood HeaderDetails
        /// </summary>
        /// <param name="neighborId">Neighborhood ID</param>
        /// <returns>string</returns>
        public string GetNeighborhoodHeaderDetails(int neighborId)
        {
            F35100NeighborhoodHeaderData form35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            form35100NeighborhoodHeaderData = Helper.GetNeighborhoodHeaderDetails(neighborId);
            return form35100NeighborhoodHeaderData.GetXml();
        }

        #endregion Get Neighborhood Details

        #region ParentDeatails

        /// <summary>
        /// Get Parent Neighborhood HeaderDetails
        /// </summary>
        /// <param name="rollyear">Roll year</param>
        /// <param name="type">Type</param>
        /// <param name="parentNeighborhood">Parent Neighborhood</param>
        /// <returns>string</returns>
        public string GetParentNeighborhoodHeaderDetails(int rollyear, int type, int parentNeighborhood)
        {
            F35100NeighborhoodHeaderData form35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            form35100NeighborhoodHeaderData = Helper.GetParentNeighborhoodHeaderDetails(rollyear, type, parentNeighborhood);
            return form35100NeighborhoodHeaderData.GetXml();
        }
        #endregion ParentDeatails

        #region SaveNeighborhoodDetail
        /// <summary>
        /// Save Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood Header ID</param>
        /// <param name="nbhDetails">Neighborhood Header Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId)
        {
            return Helper.SaveNeighborhoodHeaderDetails(nbhId, nbhDetails, userId);
        }

        #region DeleteRecord
        /// <summary>
        /// To Delete F35100 Neighborhood Header.
        /// </summary>
        /// <param name="nbhdId">NeighborhoodHeader ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public int DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            return Helper.DeleteNeighborhoodHeader(nbhdId, userId);
        }
        #endregion DeleteRecord

        #endregion NeighborhoodDetails

        #region F35102 Neighborhood Configuration

        #region Get Neighborhood Cfg Details

        /// <summary>
        /// Get the Receipt Header
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
        public string GetNeighborhoodCfgDetails(int nbhdId)
        {
            F35102NeighborhoodCfgData neighborhoodCfgData = new F35102NeighborhoodCfgData();
            neighborhoodCfgData = Helper.GetNeighborhoodCfgDetails(nbhdId);
            return neighborhoodCfgData.GetXml();
        }

        #endregion Get Neighborhood Cfg Details

        #region Get Neighborhood Cfg Choice

        /// <summary>
        /// Neighborhood CfgChoice
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="nbhdCfgId">The NBHD CFG id.</param>
        /// <returns>1</returns>
        public string GetNeighborhoodCfgChoice(int nbhdId, int nbhdCfgId)
        {
            F35102NeighborhoodCfgData neighborhoodCfgChoice = new F35102NeighborhoodCfgData();
            neighborhoodCfgChoice = Helper.GetNeighborhoodCfgChoice(nbhdId, nbhdCfgId);
            return neighborhoodCfgChoice.GetXml();
        }

        #endregion Get Neighborhood Cfg Choice

        #region Save Neighborhood Cfg Details

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="neighborhoodConfigId">The Neighborhood Config id.</param>
        /// <param name="neighborhoodConfigDetails">The Neighborhood Config number.</param>
        /// <param name="userId">UserID</param>
        public void F35102_SaveNeighborhoodCfgDetails(int neighborhoodConfigId, string neighborhoodConfigDetails, int userId)
        {
            Helper.F35102_SaveNeighborhoodCfgDetails(neighborhoodConfigId, neighborhoodConfigDetails, userId);
        }

        #endregion Save Neighborhood Cfg Details

        #endregion F35102 Neighborhood Configuration

        #region NeighborhoodParcelLocks

        /// <summary>
        /// Lists the neighborhood parcel locks.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>String</returns>
        public string ListNeighborhoodParcelLocks(int nbhdId)
        {
            F3501NeighborhoodParcelLocksData neighborhoodParcelLocksData = new F3501NeighborhoodParcelLocksData();
            neighborhoodParcelLocksData = Helper.ListNeighborhoodParcelLocks(nbhdId);
            return neighborhoodParcelLocksData.GetXml();
        }

        /// <summary>
        /// F3501_UpdateParcelLockingDetails
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="valueLock">valueLock</param>
        /// <param name="adminLock">adminLock</param>
        /// <param name="lockAppraisal">lockAppraisal</param>
        /// <param name="primaryId">The primary id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string</returns>
        public string UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int primaryId, int userId)
        {
            F3501NeighborhoodParcelLocksData neighborhoodParcelLocksData = new F3501NeighborhoodParcelLocksData();
            neighborhoodParcelLocksData = Helper.UpdateParcelLockingDetails(keyId, valueLock, adminLock, lockAppraisal, primaryId, userId);
            return neighborhoodParcelLocksData.GetXml();
        }

        #endregion NeighborhoodParcelLocks

        #endregion F35100 NeighborhoodHeader

        #region F27000 Misc Assessment

        #region Get Misc Assessment Details

        /// <summary>
        /// Gets the Misc Assessment details based on the Misc Assessment DistrictId
        /// </summary>
        /// <param name="madistrictId">The Misc Assessment District Id.</param>
        /// <returns>
        /// The typed dataset containing the Misc Assessment information of the madistrictId.
        /// </returns>
        public string F27000_GetMiscAssessment(int madistrictId)
        {
            return Helper.F27000_GetMiscAssessment(madistrictId).GetXml();
        }

        #endregion Get Misc Assessment Details

        #region List Misc Assessment District Type

        /// <summary>
        /// To List all the Misc Assessment District Types.
        /// </summary>
        /// <returns>Typed Dataset Containing the Misc Assessment District Types</returns>
        public string F27000_ListMADistrictType()
        {
            return Helper.F27000_ListMADistrictType().GetXml();
        }

        #endregion List Misc Assessment District Type

        #region List Misc Assessment District Item Type

        /// <summary>
        /// To List All Misc Assessment District Item Type
        /// </summary>
        /// <param name="madistrictTypeId">The Misc Assessment District type Id.</param>
        /// <returns>Typed Dataset Containg the All Misc Assessment Misc Assessment Item Types</returns>
        public string F27000_ListMADistrictItemType(int madistrictTypeId)
        {
            return Helper.F27000_ListMADistrictItemType(madistrictTypeId).GetXml();
        }

        #endregion List Misc Assessment District Item Type

        #region Save Misc Assessment Details

        /// <summary>
        /// To Save Misc Assessment Details
        /// </summary>
        /// <param name="distributionItems">distributionItems</param>
        /// <param name="subHeaderItems">subHeaderItems</param>
        /// <param name="userId">User Id</param>
        /// <returns>integer</returns>
        public int F27000_SaveMADetails(string distributionItems, string subHeaderItems, int userId)
        {
            return Helper.F27000_SaveMADetails(distributionItems, subHeaderItems, userId);
        }

        #endregion Save Misc Assessment Details

        #endregion F27000 Misc Assessment

        #region F15015 Statement Ownership

        #region List Statement Ownership

        /// <summary>
        /// To List Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Typed Dataset Containing the Statement Ownership Details</returns>
        public string F15015_ListStatementOwnership(int statementId)
        {
            F15015StatementOwnershipData statementOwnershipData = new F15015StatementOwnershipData();
            statementOwnershipData = Helper.F15015_ListStatementOwnership(statementId);
            return statementOwnershipData.GetXml();
        }

        #endregion List Statement Ownership

        #region F15015 list MOwnerType Selection

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>string</returns>
        public string F15015_ListMOwnerType()
        {
            F15015StatementOwnershipData getMOwnerTypeData = new F15015StatementOwnershipData();
            getMOwnerTypeData = Helper.F15015_ListMOwnerType();
            return getMOwnerTypeData.GetXml();
        }
        #endregion

        #region Save Statement Ownership

        /// <summary>
        /// To Save Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementOwner">The statement owner.</param>
        /// <param name="userId">User ID</param>
        public void F15015_SaveStatementOwnership(int statementId, string statementOwner, int userId)
        {
            Helper.F15015_SaveStatementOwnership(statementId, statementOwner, userId);
        }

        #endregion Save Statement Ownership

        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public string F15015_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            F15015StatementOwnershipData statementOwnershipData = new F15015StatementOwnershipData();
            statementOwnershipData = Helper.F15015_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
            return statementOwnershipData.GetXml();
        }

        #endregion List All Owner Details

        #endregion F15015 Statement Ownership

        #region F95101 Audit Trail

        /// <summary>
        /// To List Audit Trail records
        /// </summary>
        /// <param name="form">Form ID</param>
        /// <param name="keyId">Key ID</param>
        /// <returns>Typed DataSet Containing the Audit Trail Details</returns>
        public string F95101_ListAuditTrail(int form, int keyId)
        {
            F95101AuditTrailData auditTrailData = new F95101AuditTrailData();
            auditTrailData = Helper.F95101_ListAuditTrail(form, keyId);
            return auditTrailData.GetXml();
        }

        #endregion F95101 Audit Trail

        #region F9060 Auditing Configuration

        #region List Auditing Tables

        /// <summary>
        /// To List 9060 Audit Table Configuration
        /// </summary>
        /// <param name="tableType">Table Type</param>
        /// <returns>Data set contains Audit Table List</returns>
        public string F9060_ListAuditingTables(string tableType)
        {
            F9060AuditingConfigurationData auditTableListData = new F9060AuditingConfigurationData();
            auditTableListData = Helper.F9060_ListAuditingTables(tableType);
            return auditTableListData.GetXml();
        }

        #endregion List Auditing Tables

        #region List Auditing Columns

        /// <summary>
        /// To List 9060 Audit Configuration Columns
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <returns>DataSet Contains Audit Configuration Columns</returns>
        public string F9060_ListAuditingColumns(string tableName)
        {
            F9060AuditingConfigurationData auditTableColumnData = new F9060AuditingConfigurationData();
            auditTableColumnData = Helper.F9060_ListAuditingColumns(tableName);
            return auditTableColumnData.GetXml();
        }

        #endregion List Auditing Columns

        #region Save Audit Column Configuration

        /// <summary>
        /// To Save 9060 Audit Column Configuration
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="auditColumns">Audit Columns</param>
        /// <param name="userId">The user id.</param>
        public void F9060_SaveAuditConfiguration(string tableName, string auditColumns, int userId)
        {
            Helper.F9060_SaveAuditConfiguration(tableName, auditColumns, userId);
        }

        #endregion Save Audit Column Configuration

        #region Delete Audit Column Configuration

        /// <summary>
        /// To Delete the Audit Column Configuration
        /// </summary>
        /// <param name="auditTableID">Audit TableID</param>
        /// <param name="userId">The user id.</param>
        public void F9060_DeleteAuditConfiguration(int auditTableId, int userId)
        {
            Helper.F9060_DeleteAuditConfiguration(auditTableId, userId);
        }

        #endregion Delete Audit Column Configuration

        #endregion F9060 Auditing Configuration

        #region F36000 Marshal & Swift

        #region Get House Type Collection

        /// <summary>
        /// To Get Marshal and swift House Type collection.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed DataSet containing Marshal and swift House Type collection details.</returns>
        public string F36000_GetHouseTypeCollection(int valueSliceId)
        {
            F36000MarshalAndSwiftData marshalAndSwiftData = new F36000MarshalAndSwiftData();
            marshalAndSwiftData = Helper.F36000_GetHouseTypeCollection(valueSliceId);
            return marshalAndSwiftData.GetXml();
        }
        #endregion F9060 Auditing Configuration

        #region Depreciation Percentage

        /// <summary>
        /// F36000_s the get depr percentage.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        public string F36000_GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        {
            return Helper.F36000_GetDeprPercentage(age, objectCondition, deprTableId);
        }

        #endregion Depreciation Percentage

        #region Depr Save

        /// <summary>
        /// F36000_s the save depreciation details.
        /// </summary>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F36000_SaveDepreciationDetails(string depreciationXml, int valueSliceId, int userId)
        {
            return Helper.F36000_SaveDepreciationDetails(depreciationXml, valueSliceId, userId);
        }

        #endregion Depr Save

        #region Depr Table Name

        /// <summary>
        /// F36000_s the get depr table name id.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="propertyQuality">The property quality.</param>
        /// <returns>int</returns>
        public int F36000_GetDeprTableNameId(int valueSliceId, int propertyQuality)
        {
            return Helper.F36000_GetDeprTableNameId(valueSliceId, propertyQuality);
        }

        #endregion Depr Table Name

        #endregion F36000 Marshal & Swift

        #region F25011 Street List Management

        #region Get the Master Street Data

        /// <summary>
        /// Get the Master Street List.
        /// </summary>
        /// <param name="streetId">StreetId</param>
        /// <returns>
        /// Typed DataSet Containing the Master Street data.
        /// </returns>
        public string F25011_GetMasterStreetList(int streetId)
        {
            F25011StreetListManagementData streetManagementData = new F25011StreetListManagementData();
            streetManagementData = Helper.F25011_GetMasterStreetList(streetId);
            return streetManagementData.GetXml();
        }

        #endregion Get the Master Street Data

        #region List Master Street List

        /// <summary>
        /// To List Master Street List.
        /// </summary>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed DataSet Containing the Master Street List details.</returns>
        public string F25011_ListMasterStreetList(int streetID, string streetName, string city)
        {
            F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();
            streetListManagementData = Helper.F25011_ListMasterStreetList(streetID, streetName, city);
            return streetListManagementData.GetXml();
        }

        #endregion List Master Street List

        #region List Street City Directional Suffix

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <returns>Typed Dataset conitaining the Street's City, Directional and Suffixs details</returns>
        public string F25011_ListStreetCityDirectionalSuffixDetails()
        {
            F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();
            streetListManagementData = Helper.F25011_ListStreetCityDirectionalSuffixDetails();
            return streetListManagementData.GetXml();
        }

        #endregion List Street City Directional Suffix

        #region Save Street List Management

        /// <summary>
        /// To Save Street List Management Details.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="streetListMgmt">The street list MGMT.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The current Saved streetId</returns>
        public int F25011_SaveStreetListManagement(int streetId, string streetListMgmt, int userId)
        {
            return Helper.F25011_SaveStreetListManagement(streetId, streetListMgmt, userId);
        }

        #endregion Save Street List Management

        #region Delete Street List

        /// <summary>
        /// F25011_s the delete street list.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Deleted Flag</returns>
        public int F25011_DeleteStreetList(int streetId, int userId)
        {
            return Helper.F25011_DeleteStreetList(streetId, userId);
        }

        #endregion Delete Street List

        #endregion F25011 Street List Management

        /// <summary>
        /// Gets the database schema.
        /// </summary>
        /// <returns>Byte value</returns>
        public byte[] GetDatabaseSchema()
        {
            return new byte[100];
        }

        /// <summary>
        /// Checks the installation1.
        /// </summary>
        /// <param name="checkInstall">The check install.</param>
        /// <returns>String or dataset</returns>
        public string CheckInstallation1(string checkInstall)
        {
            return checkInstall;
        }

        #region F2000 Parcel Status

        #region List Parcel Status
        /// <summary>
        /// List the Parcel Status Data.
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>Return String Contains ParcelStatus Data.</returns>
        public string F2000_ListParcelStatus(int parcelId)
        {
            F2000ParcelStatusData parcelStatusData = new F2000ParcelStatusData();
            parcelStatusData = Helper.F2000_ListParcelStatus(parcelId);
            return parcelStatusData.GetXml();
        }

        #endregion

        #region F2000 Delete Form parcelID
        /// <summary>
        /// To Delete Form Parcel ID
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="userId">UserID</param>
        public void F2000_DeleteParcelStatus(int parcelId, int userId)
        {
            Helper.F2000_DeleteParcelStatus(parcelId, userId);
        }

        #endregion

        #region F2000 Update Form ParcelStatus

        /// <summary>
        /// To Update Currrent Form.ParcelID
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="description">description</param>
        /// <param name="parcelType">parcelType</param>
        /// <param name="isexempt">The isexempt.</param>
        /// <param name="isownerPrimary">The isowner primary.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Return Primary KeyID</returns>
        public int F2000_UpdateParcelStatus(int parcelId, string description, string parcelType, int isexempt, int isownerPrimary, int userId)
        {
            return Helper.F2000_UpdateParcelStatus(parcelId, description, parcelType, isexempt, isownerPrimary, userId);
        }

        #endregion


        #region ListRecordLockStatus
        public string ListRecordLockStatus(int formNo, int keyId)
        {
            return Helper.ListRecordLockStatus(formNo, keyId);
        }


        #endregion


        #endregion

        #region F15110 Receipt Actions

        #region Get Receipt Actions Details

        /// <summary>
        /// Gets the Receipt Actions details based on the Receipt Id
        /// </summary>
        /// <param name="receiptId">The Receipt Id.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Actions information of the Receipt Id.
        /// </returns>
        public string F15110_GetReceiptActions(int receiptId)
        {
            F15110ReceiptActionsData receiptActionsData = new F15110ReceiptActionsData();
            receiptActionsData = Helper.F15110_GetReceiptActions(receiptId);
            return receiptActionsData.GetXml();
        }


        /// <summary>
        /// F1557_s the insert refund interest.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        /// <param name="userID">The user ID.</param>
        public void F1557_InsertRefundInterest(int receiptID, int userID)
        {
            Helper.F1557_InsertRefundInterest(receiptID, userID);
        }
        
        #endregion Get Receipt Actions Details

        #endregion F15110 Receipt Actions

        #region F25009 Legal Management

        #region Get Legal Management

        /// <summary>
        /// To Load F25009 Legal Management.
        /// </summary>
        /// <param name="parcelId">The Parcel ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25009_GetLegalManagement(int parcelId, int userId)
        {
            F25009LegalManagementData legalManagementData = new F25009LegalManagementData();
            legalManagementData = Helper.F25009_GetLegalManagement(parcelId, userId);
            return legalManagementData.GetXml();
        }

        #endregion

        #region Get Field Summary

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_FieldSummary(int keyId)
        {
            F25090FieldSummaryData FieldSummaryData = new F25090FieldSummaryData();
            FieldSummaryData = Helper.F25090_FieldSummary(keyId);
            return FieldSummaryData.GetXml();
        }

        #endregion

        #region Get GetAncestryData

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_GetAncestryData(int keyId)
        {
            F25090FieldSummaryData GetAncestryData = new F25090FieldSummaryData();
            GetAncestryData = Helper.F25090_GetAncestryData(keyId);
            return GetAncestryData.GetXml();
        }

        #endregion

        #region Get Correction Summary

        /// <summary>
        /// To Load F25090 Correction Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_GetCorrection(int keyId)
        {
            F25090FieldSummaryData GetCorrectionData = new F25090FieldSummaryData();
            GetCorrectionData = Helper.F25090_GetCorrection(keyId);
            return GetCorrectionData.GetXml();
        }

        #endregion

        #region Get HistoryData

        /// <summary>
        /// To Load F25090 HistoryData Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_GetHistoryData(int keyId)
        {
            F25090FieldSummaryData FieldSummaryData = new F25090FieldSummaryData();
            FieldSummaryData = Helper.F25090_GetHistoryData(keyId);
            return FieldSummaryData.GetXml();
        }

        #endregion

        #region Get ParcelOwnerShip Summary

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_GetParcelOwnerShip(int keyId)
        {
            F25090FieldSummaryData ParcelOwnerShipData = new F25090FieldSummaryData();
            ParcelOwnerShipData = Helper.F25090_GetParcelOwnerShip(keyId);
            return ParcelOwnerShipData.GetXml();
        }

        #endregion

        #region Get GetPhotosData Summary

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_GetPhotos(int keyId, int form)
        {
            F25090FieldSummaryData GetPhotosData = new F25090FieldSummaryData();
            GetPhotosData = Helper.F25090_GetPhotos(keyId, form); ;
            return GetPhotosData.GetXml();
        }

        #endregion

        #region Get ParcelSaleData Summary

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_ParcelSale(int keyId)
        {
            F25090FieldSummaryData ParcelSaleData = new F25090FieldSummaryData();
            ParcelSaleData = Helper.F25090_ParcelSale(keyId);
            return ParcelSaleData.GetXml();
        }

        #endregion

        #region Get BuildingPermits Summary

        /// <summary>
        /// To Load F25090 Field Summary.
        /// </summary>
        /// <param name="KeyId">The Key ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Legal Management Details
        /// </returns>
        public string F25090_BuildingPermits(int keyId)
        {
            F25090FieldSummaryData BuildingPermitsData = new F25090FieldSummaryData();
            BuildingPermitsData = Helper.F25090_BuildingPermits(keyId);
            return BuildingPermitsData.GetXml();
        }

        #endregion

        #region Save Legal Management

        /// <summary>
        /// To Save F25009 Legal Management.
        /// </summary>
        /// <param name="legalId">The Legal ID.</param>
        /// <param name="legalItems">The XML string Containing All values in Legal Management.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The integer value containing Parcel id</returns>
        public int F25009_SaveLegalManagement(int legalId, string legalItems, bool isFuturePush, int userId)
        {
            return Helper.F25009_SaveLegalManagement(legalId, legalItems, isFuturePush, userId);
        }

        #endregion

        #region List Subdivision

        /// <summary>
        ///  To Load F25009 Subdivision.
        /// </summary>        
        /// <returns>Typed DataSet Containing All the Subdivision Details</returns>
        public string F25009_ListSubdivision()
        {
            F25009LegalManagementData legalManagementData = new F25009LegalManagementData();
            legalManagementData = Helper.F25009_ListSubdivision();
            return legalManagementData.GetXml();
        }

        #endregion

        #endregion

        #region F25003 Situs Management

        #region List Situs Management Details

        /// <summary>
        /// To List Situs Mangement Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="situsId">The situs id.</param>
        /// <returns>Typed Dataset containing the Situs Mangement Details</returns>
        public string F25003_ListSitusMangement(int parcelId, int situsId)
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            situsManagementData = Helper.F25003_ListSitusMangement(parcelId, situsId);
            return situsManagementData.GetXml();
        }

        #endregion List Situs Management Details

        #region List Street Details

        /// <summary>
        /// To List Street Details.
        /// </summary>
        /// <returns>Typed Dataset containing the Street Details</returns>
        public string F25003_ListStreet()
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            situsManagementData = Helper.F25003_ListStreet();
            return situsManagementData.GetXml();
        }

        #endregion List Street Details

        #region List Unit Type Details

        /// <summary>
        /// To list Unit Type Details.
        /// </summary>
        /// <returns>Typed DataSet containing the Unit Type Details</returns>
        public string F25003_ListUnitType()
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            situsManagementData = Helper.F25003_ListUnitType();
            return situsManagementData.GetXml();
        }

        #endregion List Unit Type Details

        #region Save Situs Management

        /// <summary>
        /// To Save List Mangement Details.
        /// </summary>
        /// <param name="situsId">The situs id.</param>
        /// <param name="situsItems">The situs items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Intger value containing the new SitusID</returns>
        public int F25003_SaveListMangement(int situsId, string situsItems, bool isFuturePush, int userId)
        {
            return Helper.F25003_SaveListMangement(situsId, situsItems, isFuturePush, userId);
        }

        #endregion Save Situs Management

        #region Delete Situs Management

        /// <summary>
        /// To Delete the Situs management
        /// </summary>
        /// <param name="situsId">situsId</param>
        /// <param name="userId">UserID</param>
        public void F25003_DeleteSitusManagement(int situsId, int userId)
        {
            Helper.F25003_DeleteSitusManagement(situsId, userId);
        }

        #endregion Delete Situs Management

        #endregion F25003 Situs Management

        #region F1555 Receipt Details

        #region Get Receipt Details

        /// <summary>
        /// Gets the Receipt Details based on the Receipt Id
        /// </summary>
        /// <param name="receiptId">The Receipt Id.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Details information of the Receipt Id.
        /// </returns>
        public string F1555_GetReceiptDetails(int receiptId)
        {
            F1555_ReceiptDetailsData receiptDetailsData = new F1555_ReceiptDetailsData();
            receiptDetailsData = Helper.F1555_GetReceiptDetails(receiptId);
            return receiptDetailsData.GetXml();
        }

        #endregion Get Receipt Actions Details

        #region Reverse Receipt Details

        /// <summary>
        /// Reverse receipt details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="sharedPaymentId">Shared Payment Id</param>
        /// <param name="userId">User ID</param>
        /// <returns>Reverse Payment Details</returns>
        public string F1556_ReverseReceiptDetails(int receiptId, int sharedPaymentId, int userId)
        {
            F1555_ReceiptDetailsData receiptDetailsData = new F1555_ReceiptDetailsData();
            receiptDetailsData = Helper.F1556_ReverseReceiptDetails(receiptId, sharedPaymentId, userId);
            return receiptDetailsData.GetXml();
        }

        #endregion Reverse Receipt Details

        #endregion F1555 Receipt Actions

        #region F1060 Suspended Payment Selection

        #region F1060 List Suspended Payment

        /// <summary>
        /// To List Suspended Payment.
        /// </summary>
        /// <param name="SEARCH XML">Search Detail.</param>
        /// <returns>Typed DataSet containing the Suspended Payment Details.</returns>
        public string F1060_ListSuspendedPayment(string searchDetail)
        {
            F1060SudpendedPaymentSelectionData suspendedPaymentSelectionData = new F1060SudpendedPaymentSelectionData();
            suspendedPaymentSelectionData = Helper.F1060_ListSuspendedPayment(searchDetail);
            return suspendedPaymentSelectionData.GetXml();
        }


        #endregion F1060 List Suspended Payment

        #endregion F1060 Suspended Payment Selection

        #region SnapShotUtility

        #region F9040_ListBatchButtonSnapShots

        /// <summary>
        /// To List the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="formsSliceNo">The forms slice no.</param>
        /// <returns>Typed DataSet containg the list of F1440 Batch Button SnapShots for Current form slice</returns>
        public string F9040_ListBatchButtonSnapShots(int formsSliceNo)
        {
            F9040SnapShotUtilityData snapShotUtilityData = new F9040SnapShotUtilityData();
            snapShotUtilityData = Helper.F9040_ListBatchButtonSnapShots(formsSliceNo);
            return snapShotUtilityData.GetXml();
        }

        #endregion F9040_ListBatchButtonSnapShots

        #region F9040_SaveBatchButtonSnapShots

        /// <summary>
        /// To save the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetails">The snap shot details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the snapshot id</returns>
        public int F9040_SaveBatchButtonSnapShots(int snapShotId, string snapShotDetails, int userId)
        {
            return Helper.F9040_SaveBatchButtonSnapShots(snapShotId, snapShotDetails, userId);
        }

        #endregion F9040_SaveBatchButtonSnapShots

        #region ListSnapShots

        /// <summary>
        /// Lists the SnapShots for the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9040SnapShotUtilityData Dataset</returns>
        public string F9040_ListSnapShots(int formId)
        {
            F9040SnapShotUtilityData snapShotUtilityData = new F9040SnapShotUtilityData();
            snapShotUtilityData = Helper.F9040_ListSnapShots(formId);
            return snapShotUtilityData.GetXml();
        }

        #endregion ListSnapShot

        #region SaveSnapShot

        /// <summary>
        /// F9040_s the save snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotxml">The snap shotxml.</param>
        /// <param name="snapshotItemsxml">The snapshot itemsxml.</param>
        /// <param name="filterXML">Filter XML</param>
        /// <param name="pinType">Pinning Type</param>
        /// <param name="userId">UserID</param>
        /// <param name="parentSnapShotID">Parent Snapshot ID</param>
        /// <returns>the saved snapshotid</returns>
        public int F9040_SaveSnapShot(int snapShotId, string snapShotxml, string snapshotItemsxml, string filterXML, string pinType, int userId, int parentSnapShotID)
        {
            return Helper.F9040_SaveSnapShot(snapShotId, snapShotxml, snapshotItemsxml, filterXML, pinType, userId, parentSnapShotID);
        }

        #endregion SaveSnapShot

        #region DeleteSnapShot

        /// <summary>
        /// To Delete F040 Snapshot
        /// </summary>
        /// <param name="snapshotId">The snapshotId</param>
        /// <param name="userId">UserID</param>
        public void F9040_DeleteSnapShot(int snapshotId, int userId)
        {
            Helper.F9040_DeleteSnapShot(snapshotId, userId);
        }

        #endregion DeleteSnapShot

        #endregion SnapShotUtility

        #region F9070ReportListing

        /// <summary>
        /// F9070s the get report listing details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>form9070ReportListingData</returns>
        public string F9070_GetReportListingDetails(int userId)
        {
            F9070ReportListingData form9070ReportListingData = new F9070ReportListingData();
            form9070ReportListingData = Helper.F9070GetReportListingDetails(userId);
            return form9070ReportListingData.GetXml();
        }

        #endregion F9070ReportListing

        #region F25008 Parcel Miscellaneous

        #region Get Parcel Miscellaneous

        /// <summary>
        /// Get ParcelMiscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <returns>ParcelMiscellaneous Data</returns>
        public string F25008_ParcelMiscellaneousData(int parcelId)
        {
            F25008ParcelMiscellaneousData parcelMiscellaneousData = new F25008ParcelMiscellaneousData();
            parcelMiscellaneousData = Helper.F25008_ParcelMiscellaneousData(parcelId);
            return parcelMiscellaneousData.GetXml();
        }

        #endregion Get Parcel Miscellaneous

        #region Get Parcel Miscellaneous Configuration

        /// <summary>
        /// Get the ParcelMiscellaneous Configuration Data
        /// </summary>
        /// <returns>ParcelMiscellaneous Config</returns>
        public string F25008_ParcelMiscellaneousConfigData()
        {
            F25008ParcelMiscellaneousData parcelMiscellaneousConfigData = new F25008ParcelMiscellaneousData();
            parcelMiscellaneousConfigData = Helper.F25008_ParcelMiscellaneousConfigData();
            return parcelMiscellaneousConfigData.GetXml();
        }

        #endregion Get Parcel Miscellaneous Configuration

        #region Save Parcel Miscellaneous

        /// <summary>
        /// Save ParcelMiscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="miscellaneous">miscellaneous</param>
        /// <param name="userId">UserID</param>
        public void F25008_SaveParcelMiscellaneous(int parcelId, string miscellaneous, int userId)
        {
            Helper.F25008_SaveParcelMiscellaneous(parcelId, miscellaneous, userId);
        }

        #endregion Save Parcel Miscellaneous

        #endregion F25008 Parcel Miscellaneous

        #region F35101 Neighborhood Group Header

        #region Get Neighborhood Group Header

        /// <summary>
        ///  To Load F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group ID.</param>
        /// <returns>Typed DataSet Containing All the Neighborhood Group Header Details</returns>
        public string F35101_GetNeighborhoodGroupHeader(int nbhdGroupId)
        {
            F35101NeighborhoodGroupHeaderData neighborhoodGroupHeaderData = new F35101NeighborhoodGroupHeaderData();
            neighborhoodGroupHeaderData = Helper.F35101_GetNeighborhoodGroupHeader(nbhdGroupId);
            return neighborhoodGroupHeaderData.GetXml();
        }

        #endregion

        #region Save Neighborhood Group Header

        /// <summary>
        /// To Save F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header ID.</param>
        /// <param name="neighborhoodGroupHeader">The XML string Containing All values in Neighborhood Group Header.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The integer value containing Neighborhood Group Header id
        /// </returns>
        public int F35101_SaveNeighborhoodGroupHeader(int nbhdGroupId, string neighborhoodGroupHeader, int userId)
        {
            return Helper.F35101_SaveNeighborhoodGroupHeader(nbhdGroupId, neighborhoodGroupHeader, userId);
        }

        #endregion

        #region Delete Neighborhood Group Header

        /// <summary>
        /// To Delete F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group ID.</param>
        /// <param name="userId">The user id.</param>
        public void F35101_DeleteNeighborhoodGroupHeader(int nbhdGroupId, int userId)
        {
            Helper.F35101_DeleteNeighborhoodGroupHeader(nbhdGroupId, userId);
        }

        #endregion

        #endregion

        #region F3040 Zoning

        #region F3040 Get Zoning

        /// <summary>
        /// Used to Get the Zoning Details
        /// </summary>
        /// <returns>Gets Typed DataSet containing the Zoning Details.</returns>
        public string F3040_GetZoningDetails()
        {
            F3040ZoningData zoningDetailsData = new F3040ZoningData();
            zoningDetailsData = Helper.F3040_GetZoningDetails();
            return zoningDetailsData.GetXml();
        }

        #endregion F3040 Get Zoning

        #region F3040 Save Zoning

        /// <summary>
        /// Used to Save the Zoning Details
        /// </summary>
        /// <param name="zoningDetails">The zoning details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// Typed DataSet containing the Zoning Details to Save.
        /// </returns>
        public int F3040_SaveZoningDetails(string zoningDetails, int userId)
        {
            return Helper.F3040_SaveZoningDetails(zoningDetails, userId);
        }

        #endregion F3040 Save Zoning

        #endregion F3040 Zoning

        #region F15035 Suspended Payments

        /// <summary>
        /// F15035s the suspended payments.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// typed dataset containing the suspended payment details
        /// </returns>
        public string F15035SuspendedPayments(int receiptId)
        {
            F15035SuspendedPaymentsData suspendedPaymentsData = new F15035SuspendedPaymentsData();
            suspendedPaymentsData = Helper.F15035SuspendedPayments(receiptId);
            return suspendedPaymentsData.GetXml();
        }

        /// <summary>
        /// F15035_s the delete suspended payment.
        /// </summary>
        /// <param name="receiptId">The receipt ID.</param>
        /// <param name="userId">UserID</param>
        public void F15035_DeleteSuspendedPayment(int receiptId, int userId)
        {
            Helper.F15035_DeleteSuspendedPayment(receiptId, userId);
        }

        /// <summary>
        /// F15035_s the check suspended accounts.
        /// </summary>
        /// <returns>status id</returns>
        public int F15035_CheckSuspendedAccounts()
        {
            return Helper.F15035_CheckSuspendedAccounts();
        }

        #endregion F15035 Suspended Payments

        #region F8062 Components Configuration

        #region List Components Configuration

        /// <summary>
        /// F8062_s the list components configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>
        /// string containing the typed dataset of the component configuration
        /// </returns>
        public string F8062_ListComponentsConfiguration(int applicationId)
        {
            F8062ComponentsConfigData componentsConfigData = new F8062ComponentsConfigData();
            componentsConfigData = Helper.F8062_ListComponentsConfiguration(applicationId);
            return componentsConfigData.GetXml();
        }

        #endregion List Components Configuration

        #region List Feature Class

        /// <summary>
        /// F8062_s the list feature class.
        /// </summary>
        /// <param name="applicationId">applicationID</param>
        /// <returns>
        /// string containing the typed dataset of the feature class
        /// </returns>
        public string F8062_ListFeatureClass(int applicationId)
        {
            F8062ComponentsConfigData componentsConfigData = new F8062ComponentsConfigData();
            componentsConfigData = Helper.F8062_ListFeatureClass(applicationId);
            return componentsConfigData.GetXml();
        }

        #endregion List Feature Class

        #region Save Components Configuration

        /// <summary>
        /// F8062_s the save components configuration.
        /// </summary>
        /// <param name="componentsConfig">The components config.</param>
        /// <param name="userId">UserID</param>
        public void F8062_SaveComponentsConfiguration(string componentsConfig, int userId)
        {
            Helper.F8062_SaveComponentsConfiguration(componentsConfig, userId);
        }

        #endregion Save Components Configuration

        #region Delete Components Configuration

        /// <summary>
        /// Deletes the Components Configuration.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F8062_DeleteComponentsConfiguration(int componentId, int userId)
        {
            return Helper.F8062_DeleteComponentsConfiguration(componentId, userId);
        }
        #endregion

        #endregion F8062 Components Configuration

        #region F8058 Resources Configuration

        #region List Resources Configuration
        /// <summary>
        /// F8058_s the list resources config details.
        /// </summary>
        /// <returns>string</returns>
        public string F8058_ListResourcesConfigDetails()
        {
            F8058ResourcesConfigData resourcesConfigData = new F8058ResourcesConfigData();
            resourcesConfigData = Helper.F8058_ListResourcesConfigDetails();
            return resourcesConfigData.GetXml();
        }
        #endregion List Resources Configuration

        #region Insert Resources Configuration
        /// <summary>
        /// F8058_s the insert reosurces config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="equiptResource">The equipt resource.</param>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F8058_InsertResourcesConfigDetails(int equipmentId, string equiptResource, int applicationId, int userId)
        {
            return Helper.F8058_InsertResourcesConfigDetails(equipmentId, equiptResource, applicationId, userId);
        }
        #endregion Insert Resources Configuration

        #region Delete Resources Configuration
        /// <summary>
        /// F8058_s the delete resources config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F8058_DeleteResourcesConfigDetails(int equipmentId, int userId)
        {
            return Helper.F8058_DeleteResourcesConfigDetails(equipmentId, userId);
        }
        #endregion Delete Resources Configuration

        #endregion F8062 Resources Configuration

        #region F1013 Unpaid Reciepts.

        /// <summary>
        /// List Unpaid Receipts.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>unpaid receipts details xml.</returns>
        public string F1013_ListUnpaidReceipts(int? userId)
        {
            F1013BatchPaymentMgmtData batchPaymentMgmtDataSet = new F1013BatchPaymentMgmtData();
            batchPaymentMgmtDataSet = Helper.F1013_ListUnpaidReceipts(userId);
            return batchPaymentMgmtDataSet.GetXml();
        }

        /// <summary>
        /// F1013_s the save batch payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsXml">The payment items XML.</param>
        /// <param name="receiptItemsXml">The receipt items XML.</param>
        /// <returns>returns the error id.</returns>
        public int F1013_SaveBatchPayment(int ppaymentId, int userId, string paymentItemsXml, string receiptItemsXml, string receiptDate)
        {
            return Helper.F1013_SaveBatchPayment(ppaymentId, userId, paymentItemsXml, receiptItemsXml, receiptDate);
        }

        #region F1013_ListSnapShotItems

        /// <summary>
        /// To list snap shot items collection.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <returns>Typed dataset containing the snap shot items collection</returns>
        public string F1013_ListSnapShotItems(int snapShotId)
        {
            F1013BatchPaymentMgmtData form1013BatchPaymentMgmtData = new F1013BatchPaymentMgmtData();
            form1013BatchPaymentMgmtData = Helper.F1013_ListSnapShotItems(snapShotId);
            return form1013BatchPaymentMgmtData.GetXml();
        }

        #endregion F1013_ListSnapShotItems

        #region F1013_DeleteReceiptItems

        /// <summary>
        /// F1013_s the delete receipt items.
        /// </summary>
        /// <param name="paymentId">The payment id.</param>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F1013_DeleteReceiptItems(int paymentId, string receiptItems, int userId)
        {
            return Helper.F1013_DeleteReceiptItems(paymentId, receiptItems, userId);
        }

        #endregion F1013_DeleteReceiptItems

        #endregion

        #region F8060 Parts Configuration

        #region List Parts Configuration

        /// <summary>
        /// Lists the Parts Configuration details
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <returns>returns dataset contains Parts Configuration details</returns>
        public string F8060_ListPartsConfig(int componentId)
        {
            F8060PartsConfigData partsConfigData = new F8060PartsConfigData();
            partsConfigData = Helper.F8060_ListPartsConfig(componentId);
            return partsConfigData.GetXml();
        }

        #endregion

        #region List Components

        /// <summary>
        /// Lists the Components detail
        /// </summary>
        /// <returns>returns dataset contains Components details</returns>
        public string F8060_ListComponents()
        {
            F8060PartsConfigData partsConfigData = new F8060PartsConfigData();
            partsConfigData = Helper.F8060_ListComponents();
            return partsConfigData.GetXml();
        }

        #endregion

        #region Save Parts Configuration

        /// <summary>
        /// F8062_s the save Parts configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="partsConfig">The parts config.</param>
        /// <param name="userId">UserID</param>
        public void F8060_SavePartsConfiguration(int partId, string partsConfig, int userId)
        {
            Helper.F8060_SavePartsConfiguration(partId, partsConfig, userId);
        }

        #endregion Save Parts Configuration

        #region Delete Parts Configuration

        /// <summary>
        /// Deletes the Parts Configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F8060_DeletePartsConfiguration(int partId, int userId)
        {
            return Helper.F8060_DeletePartsConfiguration(partId, userId);
        }
        #endregion

        #endregion F8060 Parts Configuration

        #region OwnerStatus

        /// <summary>
        /// To List OwnerStatus Details.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// Typed Dataset containing the OwnerStatus Details
        /// </returns>
        public string F9102_GetOwnerStatusDetails(int typeId, int keyId)
        {
            F9102OwnerStatusData ownerStatusData = new F9102OwnerStatusData();
            ownerStatusData = Helper.F9102_GetOwnerStatusDetails(typeId, keyId);
            return ownerStatusData.GetXml();
        }

        #endregion OwnerStatus

        #region F95005 Reference Data

        #region List Refereence Data

        /// <summary>
        /// To List the Reference Data Details
        /// </summary>
        /// <param name="masterFormNo">masterFormNo</param>
        /// <returns>Typed DataSet containg the Reference Data Details</returns>
        public DataSet F95005_ListReferenceData(int masterFormNo)
        {
            return Helper.F95005_ListReferenceData(masterFormNo);
        }

        #endregion List Refereence Data

        #region Save Reference Data

        /// <summary>
        /// To Save the Reference Data Details
        /// </summary>
        /// <param name="referenceData">Xml String containing the Reference Data Details</param>
        /// <param name="deletedData">Xml string containing the deleted data in Reference Data Details.</param>
        /// <param name="tableName">Tabel Name of the Reference Data</param>
        /// <param name="keyColumn">Key Column Name of the Reference Data Table</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// Integer Value containing Whther save is performed or Not
        /// if Saved return = 0
        /// else Unsaved return = -1
        /// </returns>
        public int F95005_SaveReferenceData(string referenceData, string deletedData, string tableName, string keyColumn, int userId)
        {
            return Helper.F95005_SaveReferenceData(referenceData, deletedData, tableName, keyColumn, userId);
        }

        #endregion Save Reference Data

        #endregion F95005 Reference Data

        #region F96000 OwnerManagement

        #region GetOwnerManagementDetails

        /// <summary>
        /// Gets the F96000_GetOwnerDetails
        /// It Returns two table[OwnerDetails,OwnerList]
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>
        /// Type Dataset Returns two table[OwnerDetails,OwnerList]
        /// </returns>
        public string F96000_GetOwnerManagementDetails(int ownerId)
        {
            F96000OwnerManagementData ownerManagementData = new F96000OwnerManagementData();
            ownerManagementData = Helper.F96000_GetOwnerManagementDetails(ownerId);
            return ownerManagementData.GetXml();
        }

        #endregion GetOwnerManagementDetails

        #region ListOwnerStatusType

        /// <summary>
        /// ListOwnerStatusType
        /// </summary>
        /// <returns>String</returns>
        public string F96000_ListOwnerStatusType()
        {
            return Helper.F96000_ListOwnerStatusType().GetXml();
        }

        /// <summary>
        /// F96000_s the country combo details.
        /// </summary>
        /// <returns></returns>
        public string F96000_CountryComboDetails()
        {
            return Helper.F96000_CountryComboDetails().GetXml();
        }

        #endregion ListOwnerStatusType

        #region Insert OwnerManagementDetails
        /// <summary>
        /// Insert the F96000_OwnerManagementDetails
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerDetails">ownerDetails</param>
        /// <param name="ownerStatus">ownerStatus</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F96000_InsertOwnerManagementDetails(int ownerId, string ownerDetails, string ownerStatus, int userId)
        {
            return Helper.F96000_InsertOwnerManagementDetails(ownerId, ownerDetails, ownerStatus, userId);
        }
        #endregion Insert OwnerManagementDetails


        #region DeleteDatas

        public void F96000_DeleteData(int statusId)
        {
            Helper.F96000_DeleteData(statusId);
        }

        #endregion

        #endregion F96000 OwnerManagement

        #region F36011 Misc Improvement Overview

        #region List Depr Table

        /// <summary>
        /// To List the Depr Table details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public string F36011_ListDeprTable(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListDeprTable(valueSliceId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Depr Table

        #region List Misc Code

        /// <summary>
        /// To List Misc Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>
        /// Typed dataset containing the Misc Code Details
        /// </returns>
        public string F36011_ListMiscCode(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListMiscCode(valueSliceId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Misc Code

        #region List Misc Improvements

        /// <summary>
        /// To List Misc Improvements details.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <returns>Typed dataset containing the Misc Improvements details</returns>
        public string F36011_ListMiscImprovements(int miscId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListMiscImprovements(miscId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Misc Improvements

        #region List MICatalog Code

        /// <summary>
        /// To List Catalog Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containg the MICatalog Code Details</returns>
        public string F36011_ListCatalogCode(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListCatalogCode(valueSliceId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List MICatalog Code

        #region Delete MICode

        /// <summary>
        /// To Delete MID in Misc Improvements OverView.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <param name="userId">UserID</param>
        public void F36011_DeleteMICode(int miscId, int userId)
        {
            Helper.F36011_DeleteMICode(miscId, userId);
        }

        #endregion Delete MICode

        #region Save Misc Improvements

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="miscmId">mid</param>
        /// <param name="miscItems">xml string containing the Misc Improvents Overview Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing the key id</returns>
        public int F36011_SaveMiscImprovement(int miscmId, string miscItems, int userId)
        {
            return Helper.F36011_SaveMiscImprovement(miscmId, miscItems, userId);
        }

        #endregion Save Misc Improvements

        #region List Qualit Comm

        /// <summary>
        /// F36011_s the list quality comm.
        /// </summary>
        /// <returns></returns>
        public string F36011_ListQualityComm()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListQualityComm();
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Qualit Comm

        #region List Qualit Res

        /// <summary>
        /// F36011_s the list quality res.
        /// </summary>
        /// <returns></returns>
        public string F36011_ListQualityRes()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListQualityRes();
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Qualit Comm

        #region List Condition

        /// <summary>
        /// F36011_s the list condition.
        /// </summary>
        /// <returns></returns>
        public string F36011_ListCondition()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListCondition();
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List Condition

        #region List DeprFuncCategory

        /// <summary>
        /// F36011_s the list depr func category.
        /// </summary>
        /// <returns></returns>
        public string F36011_ListDeprFuncCategory()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_ListDeprFuncCategory();
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List DeprFuncCategory

        #region List MiscCatalogChoice

        /// <summary>
        /// F36012_s the list misc catalog choice.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <returns></returns>
        public string F36012_ListMiscCatalogChoice(int miscCodeId, int fieldNum)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion List MiscCatalogChoice


        #region RecalcMiscImprovement

        /// <summary>
        /// To List the RecalcMiscImprovement Table details
        /// </summary>
        /// <returns>Typed dataset containing the RecalcMiscImprovement Table details</returns>
        public string F36011_RecalcMiscImprovement(bool withprimary, int? yearIn, string condition, int? economicLife, int? effectiveAge, decimal? physDeprPerc, decimal? funcDeprPerc, decimal? BaseCost, decimal? physDepr, decimal? funcDepr, int valueSliceId, int miscCodeId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            miscImprovementOverviewData = Helper.F36011_RecalcMiscImprovement(withprimary, yearIn, condition, economicLife, effectiveAge, physDeprPerc, funcDeprPerc, BaseCost, physDepr, funcDepr, valueSliceId, miscCodeId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion RecalcMiscImprovement

        #endregion F36011 Misc Improvement Overview

        #region F3602 Copy or Move Misc Improvements.

        /// <summary>
        /// To Get object details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public string GetObjectDetails(int parcelId)
        {
            F3602CopyMoveMiscImprovement miscImprovementOverviewData = new F3602CopyMoveMiscImprovement();
            miscImprovementOverviewData = Helper.GetObjectDetails(parcelId);
            return miscImprovementOverviewData.GetXml();
        }

        /// <summary>
        /// To Get object type details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public string GetObjectTypesList()
        {
            F3602CopyMoveMiscImprovement miscImprovementOverviewData = new F3602CopyMoveMiscImprovement();
            miscImprovementOverviewData = Helper.GetObjectTypesList();
            return miscImprovementOverviewData.GetXml();
        }

        /// <summary>
        /// To Get value slice details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public string GetValueSlicesList(int parcelId, int objectId)
        {
            F3602CopyMoveMiscImprovement miscImprovementOverviewData = new F3602CopyMoveMiscImprovement();
            miscImprovementOverviewData = Helper.GetValueSlicesList(parcelId,objectId);
            return miscImprovementOverviewData.GetXml();
        }

        /// <summary>
        /// To get Misc Improvement Details.
        /// </summary>
        /// <param name="valueSliceID"></param>
        /// <returns></returns>
        public string GetMiscImprovementsList(int valueSliceID)
        {
            F3602CopyMoveMiscImprovement miscImprovementOverviewData = new F3602CopyMoveMiscImprovement();
            miscImprovementOverviewData = Helper.GetMiscImprovementsList(valueSliceID);
            return miscImprovementOverviewData.GetXml();
        }

        /// <summary>
        /// to get Process Misc Improvements.
        /// </summary>
        /// <param name="copyMove"></param>
        /// <param name="parcelId"></param>
        /// <param name="isNewObject"></param>
        /// <param name="existingObjectId"></param>
        /// <param name="newObjectTypeId"></param>
        /// <param name="isNewValueslice"></param>
        /// <param name="existingValueslice"></param>
        /// <param name="miscImprovements"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string F3602_ProcessMiscImprovements(string copyMove, int parcelId, bool isNewObject, int existingObjectId, int newObjectTypeId, bool isNewValueslice, int existingValueslice, string miscImprovements, int userId)
        {
            F3602CopyMoveMiscImprovement miscImprovementOverviewData = new F3602CopyMoveMiscImprovement();
            miscImprovementOverviewData = Helper.F3602_ProcessMiscImprovements(copyMove, parcelId, isNewObject, existingObjectId, newObjectTypeId, isNewValueslice, existingValueslice, miscImprovements, userId);
            return miscImprovementOverviewData.GetXml();
        }

        #endregion Copy or Move Misc Improvements.

        #region F36010 Misc Improvement Catalog

        #region Get Misc Improvement Catalog

        /// <summary>
        /// Get Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>1</returns>
        public string F36010_GetMiscImprovementCatalog(int miscCodeId)
        {
            F36010MiscImprovementCatalog getMiscImprovementData = new F36010MiscImprovementCatalog();
            getMiscImprovementData = Helper.F36010_GetMiscImprovementCatalog(miscCodeId);
            return getMiscImprovementData.GetXml();
        }

        #endregion Get Misc Improvement Catalog

        #region Save the Misc Improvement data

        /// <summary>
        /// To Save 9060 Audit Column Configuration
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCatalogItems">miscCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F36010_SaveMiscImprovementCatalog(int miscCodeId, string miscCatalogItems, int userId, string miscCatalogChoiceItems)
        {
            return Helper.F36010_SaveMiscImprovementCatalog(miscCodeId, miscCatalogItems, userId, miscCatalogChoiceItems);
        }

        #endregion Save Misc Improvement data

        #region Delete Misc Improvement data

        /// <summary>
        /// Delete Misc Improvement data
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="userId">UserID</param>
        public void F36010_DeleteMiscImprovementCatalog(int miscCodeId, int userId)
        {
            Helper.F36010_DeleteMiscImprovementCatalog(miscCodeId, userId);
        }

        #endregion Delete Misc Improvement data

        #region Check Misc Improvement data

        /// <summary>
        /// Check Misc Improvement data
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCode">miscCode</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>Integer</returns>
        public int F36010_CheckMiscImprovementCatalog(int miscCodeId, string miscCode, int rollYear)
        {
            return Helper.F36010_CheckMiscImprovementCatalog(miscCodeId, miscCode, rollYear);
        }
        #endregion Delete Misc Improvement data

        #region List Depr Table

        /// <summary>
        /// F36010_s the list depr table.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>deprTable</returns>
        public string F36010_ListDeprTable(int rollYear)
        {
            F36010MiscImprovementCatalog getMiscImprovementData = new F36010MiscImprovementCatalog();
            getMiscImprovementData = Helper.F36010_ListDeprTable(rollYear);
            return getMiscImprovementData.GetXml();
        }

        #endregion List Depr Table

        #endregion F36010 Misc Improvement Catalog

        #region F36001 Marshal And Swift Commercial

        #region Get Marshal And Swift Commercial

        /// <summary>
        /// To get marshal and swift commercial details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containing the Marshal And Swift Commercial details</returns>
        public string F36001_GetMarshalAndSwiftCommercial(int valueSliceId)
        {
            F36001MarshalAndSwiftCommercialData marshalAndSwiftCommercialData = new F36001MarshalAndSwiftCommercialData();
            marshalAndSwiftCommercialData = Helper.F36001_GetMarshalAndSwiftCommercial(valueSliceId);
            return marshalAndSwiftCommercialData.GetXml();
        }

        #endregion Get Marshal And Swift Commercial

        #region Save Marshal And Swift Commercial

        /// <summary>
        /// To save marshal and swift commercial.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="estimateDetails">The estimate details.</param>
        /// <param name="occupancyDetails">The occupancy details.</param>
        /// <param name="componentDetails">The component details.</param>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer Value</returns>
        public int F36001_SaveMarshalAndSwiftCommercial(int valueSliceId, string estimateDetails, string occupancyDetails, string componentDetails, string depreciationXml, int userId)
        {
            return Helper.F36001_SaveMarshalAndSwiftCommercial(valueSliceId, estimateDetails, occupancyDetails, componentDetails, depreciationXml, userId);
        }

        #endregion Save Marshal And Swift Commercial

        #endregion F36001 Marshal And Swift Commercial

        #region F9080Roll Year Management form

        /// <summary>
        /// F9080_s the list Roll year Management.
        /// </summary>
        /// <param name="UserId">The User ID.</param>
        /// <returns>XML String</returns>
        public string F9080_ListRollYearManagement(int userId)
        {
            F9080RollYearManagementData rollYearData = new F9080RollYearManagementData();
            rollYearData = Helper.F9080_ListRollYearManagement(userId);
            return rollYearData.GetXml();
        }

        /// <summary>
        /// F9080_s the Get Roll year Management.
        /// </summary>
        /// <param name="RollYearId">The RollYear ID.</param>
        /// <param name="UserId">The User ID.</param>
        /// <returns>XML String</returns>
        public string F9080_GetRollYearManagement(short rollYearId, int userId)
        {
            F9080RollYearManagementData StepData = new F9080RollYearManagementData();
            StepData = Helper.F9080_GetRollYearManagement(rollYearId, userId);
            return StepData.GetXml();
        }

        /// <summary>
        /// F9080_s the Exec Roll year Management.
        /// </summary>
        /// <param name="RollOverId">The RollOver id.</param>
        /// <param name="UserId">The User id.</param>
        public string F9080_ExecRollYearStep(short rollOverId, int userId)
        {
            return Helper.F9080_ExecRollYearStep(rollOverId, userId);
        }


        #endregion F9080Roll Year Mangement form


        #region F2550 TaxRollCorrection

        #region List TaxRollCorrection
        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelId">The parcel ID.</param>
        /// <returns>XML String</returns>
        public string F2550_ListParcelDetails(string parcelId, string scheduleId, string stateId, string centralXmlIds)
        {
            F2550TaxRollCorrectionData listTaxRollData = new F2550TaxRollCorrectionData();
            listTaxRollData = Helper.F2550_ListParcelDetails(parcelId, scheduleId, stateId,centralXmlIds);
            return listTaxRollData.GetXml();
        }
        #endregion TaxRollCorrection

        #region ExecTaxRollCorrections

        /// <summary>
        /// F2550_s the exec tax roll corrections.
        /// </summary>
        /// <param name="parcelItems">The parcel items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public int F2550_ExecTaxRollCorrections(string parcelItems, int userId)
        {
            return Helper.F2550_ExecTaxRollCorrections(parcelItems, userId);
        }

        #endregion ExecTaxRollCorrections

        #region List Attachment Details

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public string F2550_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            F2550TaxRollCorrectionData listAttachmentData = new F2550TaxRollCorrectionData();
            listAttachmentData = Helper.F2550_ListAttachmentDetails(formId, keyIds, userId, moduleId);
            return listAttachmentData.GetXml();
        }

        #endregion List Attachment Details

        #region Delete Attachment Details

        /// <summary>
        /// Delete attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public void F2550_DeleteAttachmentDetails(int formId)
        {
            Helper.F2550_DeleteAttachmentDetails(formId);
        }

        #endregion Delete Attachment Details

        #region List Correction Code
        /// <summary>
        /// F2550_s the list correction code.
        /// </summary>
        /// <returns></returns>
        public string F2550_ListCorrectionCode()
        {
            F2550TaxRollCorrectionData correctionCode = new F2550TaxRollCorrectionData();
            correctionCode = Helper.F2550_ListCorrectionCode();
            return correctionCode.GetXml();
        }
        #endregion

        #region Insert CorrectionParcelsTemp Table
        /// <summary>
        /// F2550_s the save correction parcels temp.
        /// </summary>
        /// <param name="correctionId">The correction id.</param>
        /// <param name="correctionTempItems">The correction temp items.</param>
        /// <param name="corrParcelIds">The corr parcel ids.</param>
        /// <param name="statementsIds">The statements ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F2550_SaveCorrectionParcelsTemp(int? correctionId, string correctionTempItems, string corrParcelIds, string statementsIds, int userId)
        {
            return Helper.F2550_SaveCorrectionParcelsTemp(correctionId, correctionTempItems, corrParcelIds, statementsIds, userId);
        }
        #endregion

        #endregion F2550 TaxRollCorrection


        #region EditStatementDetails

        #region ListEditStatementDetails

        /// <summary>
        /// F2551_s the list EditStatement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public string F2551_ListEditStatementDetails(int parcelId, short typeId, int statementId, int ownerId, int userId)
        {
            F2551EditStmtData EditStatementData = new F2551EditStmtData();
            EditStatementData = Helper.F2551_ListEditStatementDetails(parcelId, typeId, statementId, ownerId, userId);
            return EditStatementData.GetXml();
        }


        #endregion

        #region ExecuteLoadGrid

        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="ChangeXML">The Change XML.</param>
        /// <param name="ItemsXML">The ItemsXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public string F2551_LoadStatementGridDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string changeXML, int userId)
        {
            F2551EditStmtData EditStatementData = new F2551EditStmtData();
            EditStatementData = Helper.F2551_LoadStatementGridDetails(parcelId, typeId, statementId, ownerId, itemXML, changeXML, userId);
            return EditStatementData.GetXml();
        }
        #endregion


        #region SaveOperationProcess
        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public int SaveEditStatementtDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string headerXML, int userId)
        {
            return Helper.SaveEditStatementtDetails(parcelId, typeId, statementId, ownerId, itemXML, headerXML, userId);
        }

        #endregion

        #endregion EditStatementDetails

        #region ListStatementSelectionDetails

        /// <summary>
        /// F2552_s the ListStatementSelectionDetails.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public string F2552_ListStatementSelectionDetails(int parcelId, int typeId, int userId)
        {
            F2552StatementSelectionData StatementSelectionData = new F2552StatementSelectionData();
            StatementSelectionData = Helper.F2552_ListStatementSelectionDetails(parcelId, typeId, userId);
            return StatementSelectionData.GetXml();
        }

        #endregion ListStatementSelectionDetails


        #region F1401 ParcelSelection

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public string F1401_GetParcelType(int? parcelId)
        {
            F1401ParcelSearch parcelSearchDataSet = new F1401ParcelSearch();
            parcelSearchDataSet = Helper.F1401_GetParcelType(parcelId);
            return parcelSearchDataSet.GetXml();
        }

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public string F1401_GetSearchResult(string parcelSearchXml)
        {
            F1401ParcelSearch parcelSearchDataSet = new F1401ParcelSearch();
            parcelSearchDataSet = Helper.F1401_GetSearchResult(parcelSearchXml);
            return parcelSearchDataSet.GetXml();
        }

        #endregion F1401 ParcelSelection

        #region F3001 Object Management

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>string</returns>
        public string F3001_GetObjectDetail(int objectId)
        {
            F3001ObjectManagementData objectManagementDataSet = new F3001ObjectManagementData();
            objectManagementDataSet = Helper.F3001_GetObjectDetail(objectId);
            return objectManagementDataSet.GetXml();
        }

        /// <summary>
        /// F3001_s the save object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectItems">The objectItems.</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F3001_SaveObjectManagement(int objectId, string objectItems, int userId)
        {
            return Helper.F3001_SaveObjectManagement(objectId, objectItems, userId);
        }

        /// <summary>
        /// F3001_s the delete object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">UserID</param>
        public void F3001_DeleteObjectManagement(int objectId, int userId)
        {
            Helper.F3001_DeleteObjectManagement(objectId, userId);
        }

        /// <summary>
        /// F3001_s the get parcel description.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        public string F3001_GetParcelDescription(int parcelId)
        {
            return Helper.F3001_GetParcelDescription(parcelId);
        }

        /// <summary>
        /// F3001_s the copy object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectXml">The object XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F3001_CopyObject(int objectId, string objectXml, int userId)
        {
            return Helper.F3001_CopyObject(objectId, objectXml, userId);
        }

        #endregion

        #region F27080ExemptionDefinition

        #region FillExemptionTypeCombo

        /// <summary>
        /// F27080_ListExemptionTypeCombo
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>string</returns>
        public string F27080_ListExemptionTypeCombo(int applicationId)
        {
            F27080ExemptionDefinitionData exemptionDefinitionData = new F27080ExemptionDefinitionData();
            exemptionDefinitionData = Helper.F27080_ListExemptionTypeCombo(applicationId);
            return exemptionDefinitionData.GetXml();
        }

        #endregion

        #region FillExemptionTypeGrid

        /// <summary>
        /// F27080_FillExemptionTypeGrid
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        /// <returns>string</returns>
        public string F27080_FillExemptionTypeGrid(int exemptionId)
        {
            F27080ExemptionDefinitionData exemptionDefinitionDataGrid = new F27080ExemptionDefinitionData();
            exemptionDefinitionDataGrid = Helper.F27080_FillExemptionTypeGrid(exemptionId);
            return exemptionDefinitionDataGrid.GetXml();
        }

        #endregion

        #region GetexemptionError

        /// <summary>
        /// Get Exemption error
        /// </summary>
        /// <param name="exemptionId">Exemption ID</param>
        /// /// <param name="exemptionCode">Exemption Code</param>
        /// <returns>Message</returns>
        public string F27080_GetExemptionError(int exemptionId, string exemptionCode)
        {
            string Message = string.Empty;
            Message = Helper.F27080_GetExemptionError(exemptionId, exemptionCode);
            return Message;
        }
        #endregion

        #region Deleteexemption

        /// <summary>
        /// Delete exemption
        /// </summary>
        /// <param name="UserId">userId</param>
        /// <param name="exemptionId">Exemption ID</param>
        /// /// <param name="exemptionCode">Exemption Code</param>
        /// <returns>NULL</returns>
        public void F27080_DeleteExemption(int userId, int exemptionId, string exemptionCode)
        {
            Helper.F27080_DeleteExemption(userId, exemptionId, exemptionCode);
        }
        #endregion

        #region SaveExemptionType

        /// <summary>
        /// F27080_SaveExemptionType
        /// </summary>
        /// <param name="exemptionId">exemptionID</param>
        /// <param name="seniorExemption">seniorExemption</param>
        /// <param name="exemptionType">exemptionType</param>
        /// <param name="checkError">checkError</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F27080_SaveExemptionType(int exemptionId, string seniorExemption, string exemptionType, int checkError, int userId)
        {
            return Helper.F27080_SaveExemptionType(exemptionId, seniorExemption, exemptionType, checkError, userId);
        }

        #endregion

        #endregion

        #region F29530EventAssociation

        #region FillEventAssociationGrid

        /// <summary>
        /// F29530_FillAssociationEventGrid
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>string</returns>
        public string F29530_FillAssociationEventGrid(int eventId)
        {
            F29530EventAssociationData associationEvent = new F29530EventAssociationData();
            associationEvent = Helper.F29530_FillAssociationEventGrid(eventId);
            return associationEvent.GetXml();
        }

        #endregion

        #endregion

        #region F29500ParcelSplit

        /// <summary>
        /// F29500_s the get base parcel value.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        public string F29500_GetBaseParcelValue(int parcelId)
        {
            F29500ParcelSplitData parcelSplitDataSet = new F29500ParcelSplitData();
            parcelSplitDataSet = Helper.F29500_GetBaseParcelValue(parcelId);
            return parcelSplitDataSet.GetXml();
        }

        /// <summary>
        /// F29500_s the parcel split load.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public string F29500_ParcelSplitLoad(int eventId)
        {
            F29500ParcelSplitData parcelSplitDataSet = new F29500ParcelSplitData();
            parcelSplitDataSet = Helper.F29500_ParcelSplitLoad(eventId);
            return parcelSplitDataSet.GetXml();
        }

        /// <summary>
        /// F29500_s the save parcel split.
        /// </summary>
        /// <param name="splitDefinitionXml">The split definition XML.</param>
        /// <param name="splitHeaderXml">The split header XML.</param>
        /// <param name="parcelSplitXml">The parcel split XML.</param>
        /// <param name="parcelObjectXml">The Parcel object XML.</param>
        /// <param name="cropXml">The Crop XML</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F29500_SaveParcelSplit(string splitDefinitionXml, string splitHeaderXml, string parcelSplitXml, string parcelObjectXml, string cropXml, int userId)
        {
            return Helper.F29500_SaveParcelSplit(splitDefinitionXml, splitHeaderXml, parcelSplitXml, parcelObjectXml, cropXml, userId);
        }

        /// <summary>
        /// F29500_s the create parcel.
        /// </summary>
        /// <param name="splitId">The split id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Return message</returns>
        public string F29500_CreateParcel(int splitId, int userId)
        {
            return Helper.F29500_CreateParcel(splitId, userId);
        }

        #endregion F29500ParcelSplit

        #region SeniorExempt

        /// <summary>
        /// F29600_GetSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>seniorExemptData</returns>
        public string F29600_GetSeniorExemptionDetails(int eventId, int userId)
        {
            F29600SeniorExemptData seniorExemptData = new F29600SeniorExemptData();
            seniorExemptData = Helper.F29600_GetSeniorExemptionDetails(eventId, userId);
            return seniorExemptData.GetXml();
        }

        /// <summary>
        /// F29600_GetSeniorExemptionCode
        /// </summary>
        /// <param name="effectiveDate">Effective Date</param>
        /// <returns>seniorExemptData</returns>
        public string F29600_GetSeniorExemptionCode(string effectiveDate)
        {
            F29600SeniorExemptData seniorExemptData = new F29600SeniorExemptData();
            seniorExemptData = Helper.F29600_GetSeniorExemptionCode(effectiveDate);
            return seniorExemptData.GetXml();
        }

        /// <summary>
        /// F29600_saveSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="seniorExemptDetails">seniorExemptDetails</param>
        /// <param name="userId">User Id</param>
        /// <returns>int</returns>
        public int F29600_saveSeniorExemptionDetails(int eventId, string seniorExemptDetails, int userId)
        {
            return Helper.F29600_saveSeniorExemptionDetails(eventId, seniorExemptDetails, userId);
        }
        #endregion SeniorExempt

        #region F27081 TIFDistrictDetails

        /// <summary>
        /// F27081_GetTIFDistrictDetails
        /// </summary>
        /// <param name="eventId">TIFId</param>
        /// <returns>TIFDistrictData</returns>
        public string F27081_GetTIFDistrictDetails(int TIFIdDistId)
        {
            F27081TIFDistrictData TIFDistrictData = new F27081TIFDistrictData();
            TIFDistrictData = Helper.F27081_GetTIFDistrictDetails(TIFIdDistId);
            return TIFDistrictData.GetXml();
        }


        /// <summary>
        /// F27080_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFId">TIFID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F27081_SaveTIFDistrictDetails(int? TIFIdDistId, string TIFDetails, int userId)
        {
            return Helper.F27081_SaveTIFDistrictDetails(TIFIdDistId, TIFDetails, userId);
        }
        /// <summary>
        /// F27081_s the delete TIF district details.
        /// </summary>
        /// <param name="TIFIdDistId">The TIF id dist id.</param>
        /// <param name="userId">The user id.</param>
        public string F27081_DeleteTIFDistrictDetails(int TIFIdDistId, int userId, bool IsReadyToDelete)
        {
           return Helper.F27081_DeleteTIFDistrictDetails(TIFIdDistId, userId,IsReadyToDelete);
        }
        /// <summary>
        /// F27081_s the get TIF combo box details.
        /// </summary>
        /// <returns></returns>
        public string F27081_GetTIFComboBoxDetails(int RollYear)
        {
            F27081TIFDistrictData TIFDistrictData = new F27081TIFDistrictData();
            TIFDistrictData = Helper.F27081_GetTIFComboBoxDetails(RollYear);
            return TIFDistrictData.GetXml();
        }
        #endregion

        /// <summary>
        /// F34100_GetAglandDetails
        /// </summary>
        /// <param name="eventId">AglandId</param>
        /// <returns>AglandUseData</returns>
        public string F34100_GetAglandDetails(int AglandId)
        {
            F34100AglandUseData AglandData = new F34100AglandUseData();
            AglandData = Helper.F34100_GetAglandDetails(AglandId);
            return AglandData.GetXml();
        }

        /// <summary>
        /// F34100_SaveAglandDetails
        /// </summary>
        /// <param name="AglandId">AglandID</param>
        /// <param name="AglandDetails">AglandDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F34100_SaveAglandDetails(int? AglandID, string AglandDetails, int userId)
        {
            return Helper.F34100_SaveAglandDetails(AglandID, AglandDetails, userId);
        }

        /// <summary>
        /// F391000s the delete Agland details.
        /// </summary>
        /// <param name="AglandId">The Agland id.</param>
        /// <param name="userId">The user id.</param>
        public void F34100_DeleteAglandDetails(int Agland, int userId)
        {
            Helper.F34100_DeleteAglandDetails(Agland, userId);
        }

        /// <summary>
        /// F34110_GetTopDollarDetails
        /// </summary>
        /// <param name="TopDollarId">TopDollarId</param>
        /// <returns>TopDollarData</returns>
        public string F34110_GetTopDollarDetails(int TopDollarId)
        {
            F34110TopDollarData TopDollarData = new F34110TopDollarData();
            TopDollarData = Helper.F34110_GetTopDollarDetails(TopDollarId);
            return TopDollarData.GetXml();
        }

        /// <summary>
        /// F34100_SaveAglandDetails
        /// </summary>
        /// <param name="AglandId">AglandID</param>
        /// <param name="AglandDetails">AglandDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F34110_SaveTopDollarDetails(int? TopDollarID, string TopDollarDetails, int userId)
        {
            return Helper.F34110_SaveTopDollarDetails(TopDollarID, TopDollarDetails, userId);
        }

        /// <summary>
        /// F391000s the delete Agland details.
        /// </summary>
        /// <param name="AglandId">The Agland id.</param>
        /// <param name="userId">The user id.</param>
        public void F34110_DeleteTopDollarDetails(int TopDollarId, int userId)
        {
            Helper.F34110_DeleteTopdDollarDetails(TopDollarId, userId);
        }

        /// <summary>
        /// Calculate Non Crop Dollar
        /// </summary>
        /// <param name="CropDollar">CropDollar</param>
        /// <param name="CountyFactor">CountyFactor</param>
        /// <returns>TopDollarData</returns>
        public string F34110_CropTopDollar(decimal CropDollar, decimal CountyFactor)
        {
            F34110TopDollarData TopDollarData = new F34110TopDollarData();
            TopDollarData = Helper.F34110_CropTopDollar(CropDollar, CountyFactor);
            return TopDollarData.GetXml();
        }


        #region F29660 TIFEventDetails

        /// <summary>
        /// F29660_GetTIFDistrictDetails
        /// </summary>
        /// <param name="eventId">TIFId</param>
        /// <returns>TIFDistrictData</returns>
        public string F29660_GetTIFEventDetails(int EventId, int userId)
        {
            F29660TIFEventData TIFEventData = new F29660TIFEventData();
            TIFEventData = Helper.F29660_GetTIFEventDetails(EventId, userId);
            return TIFEventData.GetXml();
        }



        /// <summary>
        /// F29660_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFId">EventID</param>
        /// <param name="TIFId">TIFID</param>
        /// <param name="BaseValue">BaseValue</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F29660_SaveTIFEventDetails(int? EventId, int TIFId, decimal BaseValue, int userId)
        {
            return Helper.F29660_SaveTIFEventDetails(EventId, TIFId, BaseValue, userId);
        }




        #endregion



        #region F36032 Land Codes

        #region F36032_ListLandItems

        /// <summary>
        /// F36032_s the list land items.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>The landCodeDataSet.</returns>
        public string F36032_ListLandItems(int? rollYear)
        {
            F36032LandCodesData landCodesData = new F36032LandCodesData();
            landCodesData = Helper.F36032_ListLandItems(rollYear);
            return landCodesData.GetXml();
        }

        #endregion F36032_ListLandItems

        #region F36032_ListLandCodeDetails

        /// <summary>
        /// F36032_s the list land code details.
        /// </summary>
        /// <returns>the landCodesDataSet</returns>
        public string F36032_ListLandCodeDetails()
        {
            F36032LandCodesData landCodesData = new F36032LandCodesData();
            landCodesData = Helper.F36032_ListLandCodeDetails();
            return landCodesData.GetXml();
        }

        #endregion F36032_ListLandCodeDetails

        #region F36032_DeleteLandCode

        /// <summary>
        /// F36032_s the delete land code.
        /// </summary>
        /// <param name="landCodeId">The land code ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public int F36032_DeleteLandCode(int landCodeId, int userId)
        {
            return Helper.F36032_DeleteLandCode(landCodeId, userId);
        }

        #endregion F36032_DeleteLandCode

        #region F36032_SaveLandCodeDetails

        /// <summary>
        /// To save the land code deatils
        /// </summary>
        /// <param name="landCodeId">Land Code ID</param>
        /// <param name="landItems">Land Items</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// integer value containing the save land Code Id
        /// </returns>
        public int F36032_SaveLandCodeDetails(int? landCodeId, string landItems, int userId)
        {
            return Helper.F36032_SaveLandCodeDetails(landCodeId, landItems, userId);
        }

        #endregion F36032_SaveLandCodeDetails

        #endregion F36032 Land Codes

        #region F29510ParcelCombine

        /// <summary>
        /// Get Base Parcel value
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns>DataSet</returns>
        public string F29510_GetBaseParcelValue(int eventId)
        {
            F29510ParcelCombineData parcelCombineDataSet = new F29510ParcelCombineData();
            parcelCombineDataSet = Helper.F29510_GetBaseParcelValue(eventId);
            return parcelCombineDataSet.GetXml();
            ////return Helper.F29510_GetBaseParcelValue(eventId);
        }

        /// <summary>
        /// Get Combine Parcel Value
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>DataSet</returns>
        public DataSet F29510_GetCombineParcelDetails(int parcelId)
        {
            return Helper.F29510_GetCombineParcelDetails(parcelId);
        }

        /// <summary>
        /// Save Combined Parcel Details
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="combineItems">CombineItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public int F29510_SaveCombineParcelDetails(int? combineId, string parcelNumber, string combineItems, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction)
        {
            return Helper.F29510_SaveCombineParcelDetails(combineId, parcelNumber, combineItems, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);
        }

        /// <summary>
        /// Create Combine Parcel Value
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="eventId">EventID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="userId">UserID</param>
        /// <returns>F29510ParcelCombineData</returns>
        public string F29510_CreateCombinedParcel(int combineId, string eventId, string parcelNumber, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction)
        {
            F29510ParcelCombineData returnData = new F29510ParcelCombineData();
            returnData = Helper.F29510_CreateCombinedParcel(combineId, eventId, parcelNumber, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);
            return returnData.GetXml();
        }

        #endregion

        #region F36033 Land Code Values

        #region F36033_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public string F36033_ListLandCodeValues()
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            landCodesValuesData = Helper.F36033_ListLandCodeValues();
            return landCodesValuesData.GetXml();
        }


        #endregion F36033_ListLandCodeValues

        /// <summary>
        /// F36065_s the list shape details.
        /// </summary>
        /// <returns></returns>
        public string F36035_ListShapeDetails()
        {
            F36035LandData depreciationData = new F36035LandData();
            depreciationData = Helper.F36035_ListShapeDetails();
            return depreciationData.GetXml();
        }

        #region F36033_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public string F36033_ListIndividualLandCodeValuesItems()
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            landCodesValuesData = Helper.F36033_ListIndividualLandCodeValuesItems();
            return landCodesValuesData.GetXml();
        }

        #endregion F36033_ListIndividualLandCodeValuesItems

        #region F36033_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// </returns>
        public string F36033_ListNeighborhoodType(int rollYear)
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            landCodesValuesData = Helper.F36033_ListNeighborhoodType(rollYear);
            return landCodesValuesData.GetXml();
        }
        #endregion F36033_ListNeighborhood

        #region F36033_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public int F36033_DeleteLandCodevalue(int luvId, int userId)
        {
            return Helper.F36033_DeleteLandCodevalue(luvId, userId);
        }

        #endregion F36033_DeleteLandCodeValue

        #region F36033_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved key id</returns>
        public int F36033_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return Helper.F36033_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F36033_SaveLandCodeValue

        #endregion F36033 Land Code Values


        #region F39133 Land Code Values

        #region F39133_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public string F39133_ListLandCodeValues()
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            landCodesValuesData = Helper.F39133_ListLandCodeValues();
            return landCodesValuesData.GetXml();
        }

        #endregion F39133_ListLandCodeValues

        #region F39133_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public string F39133_ListIndividualLandCodeValuesItems()
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            landCodesValuesData = Helper.F39133_ListIndividualLandCodeValuesItems();
            return landCodesValuesData.GetXml();
        }

        #endregion F39133_ListIndividualLandCodeValuesItems

        #region F39133_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// </returns>
        public string F39133_ListNeighborhoodType(int rollYear)
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            landCodesValuesData = Helper.F39133_ListNeighborhoodType(rollYear);
            return landCodesValuesData.GetXml();
        }
        #endregion F39133_ListNeighborhood

        #region F39133_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public int F39133_DeleteLandCodevalue(int luvId, int userId)
        {
            return Helper.F39133_DeleteLandCodevalue(luvId, userId);
        }

        #endregion F39133_DeleteLandCodeValue

        #region F39133_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved key id</returns>
        public int F39133_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return Helper.F39133_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F39133_SaveLandCodeValue

        #region F39133_Calculat NonCrop Values
        /// <summary>
        /// To Calculate NonCrop Values
        /// </summary>
        /// <returns>Returns typed dataset for Crop, Non Crop Values</returns>

        public string F39133_CalculateNonCropValue(int rollYear, decimal? CropRate, decimal? NonCropRate)
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            landCodesValuesData = Helper.F39133_CalculateNonCropValue(rollYear, CropRate, NonCropRate);
            return landCodesValuesData.GetXml();
        }

        #endregion F39133_Calculat NonCrop Values


        #endregion F39133 Land Code Values


        #region F36035 Land Details

        #region GetLand Details

        /// <summary>
        /// To List LandDetails.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>
        /// Returns typed dataset containing the entire land deatils
        /// </returns>
        public string F36035_ListLandDetails(int valueSliceId)
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_ListLandDetails(valueSliceId);
            return landdetails.GetXml();
        }
        #endregion GetLand Details

        #region GetLandType Details
        /// <summary>
        /// To List LandTypeDetails.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>
        /// Returns typed dataset containing the entire LandType
        /// </returns>
        public string F36035_ListLandTypeDetails(int valueSliceId)
        {
            F36035LandData landTypedetails = new F36035LandData();
            landTypedetails = Helper.F36035_ListLandTypeDetails(valueSliceId);
            return landTypedetails.GetXml();
        }
        #endregion GetLandType Details

        #region Insert LandDetails

        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public int F36035_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return Helper.F36035_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }
        #endregion Insert LandDetails

        #region DeleteLandDetails

        /// <summary>
        /// Delete LandDetails
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="userId">UserID</param>
        public void F36035_DeleteLandDetails(int luid, int userId)
        {
            Helper.F36035_DeleteLandDetails(luid, userId);
        }
        #endregion DeleteLandDetails

        #region GetLandCode
        /// <summary>
        /// To Get LandCode
        /// </summary>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="landType1">Land Type1</param>
        /// <param name="landType2">Land Type2</param>
        /// <param name="landType3">Land Type3</param>
        /// <returns>
        /// Returns typed dataset containing the entire land deatils
        /// </returns>
        public string F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId, int? AglandID)
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_GetLandCode(rollYear, landType1, landType2, landType3, valuesliceId, AglandID);
            return landdetails.GetXml();
        }
        #endregion GetLandCode

        #region GetLandCode BaseValue
        /// <summary>
        /// To Get LandCode
        /// </summary>
        /// <param name="landCode">Land Code</param>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>
        /// Returns typed dataset containing the entire land deatils
        /// </returns>
        public string F36035_GetLandCodeBaseValue(string landCode, int valueSliceId, int? AglandID)
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_GetLandCodeBaseValue(landCode, valueSliceId, AglandID);
            return landdetails.GetXml();
        }
        #endregion GetLandCode BaseValue

        #region List Influence Types

        /// <summary>
        /// F36035_s the type of the list influence.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>The influence type dataset</returns>
        public string F36035_ListInfluenceType(int valueSliceId)
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_ListInfluenceType(valueSliceId);
            return landdetails.GetXml();
        }

        #endregion List Influence Types

        #region List Land Program

        /// <summary>
        /// F36035_s the list land program.
        /// </summary>
        /// <returns>The land program dataset.</returns>
        public string F36035_ListLandProgram()
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_ListLandProgram();
            return landdetails.GetXml();
        }

        #endregion List Land Program

        #region Get UseBaseDollarPerUnit Value

        /// <summary>
        /// F36035_s the get use base dollar per unit.
        /// </summary>
        /// <param name="programId">The program id.</param>
        /// <param name="useAdjustmentType">Type of the use adjustment.</param>
        /// <param name="useAdjustment">The use adjustment.</param>
        /// <param name="useBaseValue">The use base value.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="useMultiplier">The use multiplier.</param>
        /// <param name="units">The units.</param>
        /// <returns>The use base doller per unit dataset.</returns>
        public string F36035_GetUseBaseDollarPerUnit(byte programId, byte useAdjustmentType, string useAdjustment, decimal useBaseValue, int rollYear, decimal useMultiplier, decimal units)
        {
            F36035LandData landdetails = new F36035LandData();
            landdetails = Helper.F36035_GetUseBaseDollarPerUnit(programId, useAdjustmentType, useAdjustment, useBaseValue, rollYear, useMultiplier, units);
            return landdetails.GetXml();
        }

        #endregion Get UseBaseDollarPerUnit Value

        #region Execute VFormula

        /// <summary>
        /// F36035_s the execute V formula.
        /// </summary>
        /// <param name="vformula">The vformula.</param>
        /// <param name="units">The units.</param>
        /// <returns></returns>
        public string F36035_ExecuteVFormula(string vformula, decimal units)
        {
            DataSet resultDataSet = new DataSet();
            resultDataSet = Helper.F36035_ExecuteVFormula(vformula, units);
            return resultDataSet.GetXml();
        }

        #endregion Execute VFormula

        #endregion F36035 Land Details

        #region F39135 Land Details

        #region GETLandTYPEDetails

        /// <summary>
        /// To List LandDetails.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>
        /// Returns typed dataset containing the entire land deatils
        /// </returns>
        public string F39135_Landtypes(int valueSliceId, int rollYear)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_Landtypes(valueSliceId, rollYear);
            return landdetails.GetXml();
        }



        #endregion GETLandTypeDetails

        #region GETLandUse
        /// <summary>
        /// F39135_s the LandUseType.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        public string F39135_LandUseTypes(int valueSliceId)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_LandUseTypes(valueSliceId);
            return landdetails.GetXml();
        }


        #endregion

        #region LandDetails
        ///<summary>
        /// Get the Land Value slice
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landInfoDetails</returns>
        public string F39135_LandDetails(int valueSliceId)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_LandDetails(valueSliceId);
            return landdetails.GetXml();
        }



        #endregion

        #region getWeightedRating
        /// <summary>
        /// F39135_s the get WeightedRating.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="landCode">landCode</param>
        /// <param name="units">units</param>
        /// <param name="landUse">landUse</param>
        /// <returns>the landTypeDetails</returns>
        public string F39135_WeightedRating(string landCode, decimal units, int? landUse, int valueSliceId)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_WeightedRating(landCode, units, landUse, valueSliceId);
            return landdetails.GetXml();
        }



        #endregion

        #region GetCalculatedBasevalue
        /// <summary>
        /// F39135_s the get CalculatedBaseValue.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="adjustmentTypeID">adjustmentTypeID</param>
        /// <param name="units">units</param>
        /// <param name="baseCostUnit">baseCostUnit</param>
        /// <param name="adjustment">adjustment</param>
        /// <returns>the landTypeDetails</returns>
        public string F39135_CalculatedBaseValue(string LandCode, int adjustmentTypeID, decimal units, decimal baseCostUnit, decimal adjustment, int? AglandID, int valueSliceId)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_CalculatedBaseValue(LandCode, adjustmentTypeID, units, baseCostUnit, adjustment, AglandID, valueSliceId);
            return landdetails.GetXml();
        }



        #endregion GetCalculatedBasevalue

        #region F39135_AdjustmentType
        /// <summary>
        /// list adjustmentType.
        /// </summary>
        /// <returns> List Adjustment Type</returns>
        public string F39135_AdjustmentType()
        {
            F39135LandData adjusment = new F39135LandData();
            adjusment = Helper.F39135_AdjustmentType();
            return adjusment.GetXml();
        }
        #endregion

        #region Insert LandDetails

        /// <summary>
        /// F39135_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public int F39135_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return Helper.F39135_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }
        #endregion Insert LandDetails

        #region F39135_GetLandTotals
        /// <summary>
        /// F39135_GetLandTotals.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        public string F39135_GetLandTotals(int valueSliceId)
        {
            F39135LandData landdetails = new F39135LandData();
            landdetails = Helper.F39135_GetLandTotals(valueSliceId);
            return landdetails.GetXml();
        }


        #endregion

        #endregion F39135 Land Details

        #region ParcelSalesTracking

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>parcelSaleTracking XML</returns>
        public string F29550_GetParcelSaleTrackingDetails(int eventId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            parcelSaleTracking = Helper.F29550_GetParcelSaleTrackingDetails(eventId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// F29550_GetPushOwner
        /// </summary>
        /// <param name="saleId">Sale Id</param>
        /// <returns>parcelSaleTracking XML</returns>
        public string F29550_GetPushOwner(int saleId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            parcelSaleTracking = Helper.F29550_GetPushOwner(saleId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <param name="neewParcelId">New ParcelId</param>
        /// <param name="saleId">SaleId</param>
        /// <returns>parcelSaleTracking XML</returns>
        public string F29550_GetParcelDetails(string parcelIdDetails, int neewParcelId, int saleId)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            parcelSaleTracking = Helper.F29550_GetParcelDetails(parcelIdDetails, neewParcelId, saleId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingComboDetails
        /// </summary>
        /// <returns>parcelSaleTracking XML</returns>
        public string F29550_GetParcelSaleTrackingComboDetails()
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            parcelSaleTracking = Helper.F29550_GetParcelSaleTrackingComboDetails();
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <returns>parcelSaleTracking XML</returns>
        public string F29550_GetParcelsOwnerDetails(string parcelIdDetails)
        {
            F29550ParcelSaleTracking parcelSaleTracking = new F29550ParcelSaleTracking();
            parcelSaleTracking = Helper.F29550_GetParcelsOwnerDetails(parcelIdDetails);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// F29600_saveSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="saleItems">Sale Items</param>
        /// <param name="parcelItems">Parcel Items</param>
        /// <param name="ownerItems">Owner Items</param>
        /// <param name="userId">User Id</param>
        /// <returns>int</returns>
        public int F29550_saveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return Helper.F29550_saveParcelSaleDetails(eventId, saleItems, parcelItems, ownerItems, userId);
        }

        #endregion ParcelSalesTracking

        #region F81001 Event Fee Catalog

        #region Get Event Fee Catalog

        /// <summary>
        /// Get Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <returns>String</returns>
        public string F81001_GetEventFeeCatalog(int feeCatId)
        {
            F81001FeeCatalogData getFeeCatalogData = new F81001FeeCatalogData();
            getFeeCatalogData = Helper.F81001_GetEventFeeCatalog(feeCatId);
            return getFeeCatalogData.GetXml();
        }

        #endregion Get Event Fee Catalog

        #region Save Event Fee Catalog

        /// <summary>
        /// Save Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="feeCatalogItems">feeCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public int F81001_SaveEventFeeCatalog(int feeCatId, string feeCatalogItems, int userId)
        {
            return Helper.F81001_SaveEventFeeCatalog(feeCatId, feeCatalogItems, userId);
        }

        #endregion Save Event Fee Catalog

        #region Delete Event Fee Catalog

        /// <summary>
        /// Delete Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="userId">UserID</param>
        public void F81001_DeleteEventFeeCatalog(int feeCatId, int userId)
        {
            Helper.F81001_DeleteEventFeeCatalog(feeCatId, userId);
        }

        #endregion Delete Event Fee Catalog

        #region Check Event Fee Catalog

        /// <summary>
        /// Check Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="formNumber">formNumber</param>
        /// <param name="effectiveDate">effectiveDate</param>
        /// <returns>Integer</returns>
        public int F81001_CheckEventFeeCatalog(int feeCatId, string formNumber, DateTime effectiveDate)
        {
            return Helper.F81001_CheckEventFeeCatalog(feeCatId, formNumber, effectiveDate);
        }
        #endregion Delete Event Fee Catalog

        #endregion F81001 Event Fee Catalog

        #region F36040 Crop Catalog

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>NeighborhoodType String.</returns>
        public string F36040_ListNeighborhoodType()
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            permanentCropData = Helper.F36040_ListNeighborhoodType();
            return permanentCropData.GetXml();
        }

        /// <summary>
        /// F36040_s the list crop catalog.
        /// </summary>
        /// <returns>Crop Catalog String.</returns>
        public string F36040_ListCropCatalog()
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            permanentCropData = Helper.F36040_ListCropCatalog();
            return permanentCropData.GetXml();
        }

        /// <summary>
        /// F36040_s the delete crop catalog.
        /// </summary>
        /// <param name="cropVId">The crop V id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction</returns>
        public int F36040_DeleteCropCatalog(int cropVId, int userId)
        {
            return Helper.F36040_DeleteCropCatalog(cropVId, userId);
        }

        /// <summary>
        /// F36040_s the save crop catalog.
        /// </summary>
        /// <param name="cropUnqiueId">The crop unqiue id.</param>
        /// <param name="cropCatalogItems">The crop catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction</returns>
        public int F36040_SaveCropCatalog(int? cropUnqiueId, string cropCatalogItems, int userId)
        {
            return Helper.F36040_SaveCropCatalog(cropUnqiueId, cropCatalogItems, userId);
        }

        /// <summary>
        /// F36040_s the type of the list crop neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public string F36040_ListCropNeighborhoodType(int rollYear)
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            permanentCropData = Helper.F36040_ListCropNeighborhoodType(rollYear);
            return permanentCropData.GetXml();
        }

        #endregion

        #region 36041 Crop

        #region Get Crop Details
        /// <summary>
        /// Get Crop Details
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>String</returns>
        public string F36041_GetCrop(int valueSliceId)
        {
            F36041CropData getCropData = new F36041CropData();
            getCropData = Helper.F36041_GetCrop(valueSliceId);
            return getCropData.GetXml();
        }
        #endregion Get Crop Details

        #region Get Crop Code Details
        /// <summary>
        /// Get Crop Code Details
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>String</returns>
        public string F36041_GetCropCode(int valueSliceId)
        {
            F36041CropData getCropCodeData = new F36041CropData();
            getCropCodeData = Helper.F36041_GetCropCode(valueSliceId);
            return getCropCodeData.GetXml();
        }

        #endregion Get Crop Code Details

        #region Save Crop Details
        /// <summary>
        /// To Save the Crop Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceID</param>
        /// <param name="cropItems">CropItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing the key id</returns>
        public int F36041_SaveCropCodeDetails(int valueSliceId, string cropItems, int userId)
        {
            return Helper.F36041_SaveCropCodeDetails(valueSliceId, cropItems, userId);
        }
        #endregion Save Crop Details

        #region F36041_DeleteCrop

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public void F36041_DeleteCrop(int cropId, int userId)
        {
            Helper.F36041_DeleteCrop(cropId, userId);
        }

        #endregion F36041_DeleteCrop

        #region F36041_DeleteCropIds

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop ids.</param>
        /// <param name="userId">The user id.</param>
        public void F36041_DeleteCropIds(string cropIds, int userId)
        {
            Helper.F36041_DeleteCropIds(cropIds, userId);
        }

        #endregion F36041_DeleteCrop

        #endregion 36041 Crop

        #region F81002 Event Fee

        /// <summary>
        /// Get the Event Fee data
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>String</returns>
        public string F81002_GetEventFee(int eventId, int form)
        {
            F81002EventFeeData getEventFeeData = new F81002EventFeeData();
            getEventFeeData = Helper.F81002_GetEventFee(eventId, form);
            return getEventFeeData.GetXml();
        }

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="feeItems">The fee items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value containing the key id</returns>
        public int F81002_SaveEventFee(int eventId, string feeItems, int userId)
        {
            return Helper.F81002_SaveEventFee(eventId, feeItems, userId);
        }

        /// <summary>
        /// Delete the Event Fee data
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="userId">UserID</param>
        public void F81002_DeleteEventFee(int eventId, int userId)
        {
            Helper.F81002_DeleteEventFee(eventId, userId);
        }

        #endregion F81002 Event Fee

        #region F3230 Check in

        #region F3230_ChkInTypesXML
        /// <summary>
        /// F3230_ChkInTypesXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInTypesXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInTypesXML();
            return fieldUseData;
        }
        #endregion F3230_ChkInTypesXML

        /// <summary>
        /// F3230_ChkInInsertedFileXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInInsertedFileXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInInsertedFileXML();
            return fieldUseData;
        }

        /// <summary>
        /// F3230_InsertFile
        /// </summary>
        /// <param name="insertxmlContent"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_InsertFile(string insertxmlContent)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_InsertFile(insertxmlContent);
            return fieldUseData;
        }

        public F3230CheckInData F3230_UpdateFile(string updatexmlContent)
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_UpdateFile(updatexmlContent);
            return fieldUseData;
        }

        /// <summary>
        /// F3230_ChkInLandCodeXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInLandCodeXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInLandCodeXML();
            return fieldUseData;
        }

        /// <summary>
        /// F3230_ParcelID
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ParcelID()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ParcelID();
            return fieldUseData;
        }

        /// <summary>
        /// F3230_ChkInInsertXML
        /// </summary>
        /// <returns></returns>
        public string F3230_ChkInInsertXML(out string ChkInInsertXML)
        {
            return Helper.F3230_ChkInInsertXML(out ChkInInsertXML);
        }

        /// <summary>
        /// F3230_ChkInTerraGonInsertXML
        /// </summary>
        /// <returns></returns>
        public string F3230_ChkInTerraGonInsertXML(out string ChkInInsertXML)
        {
            return Helper.F3230_ChkInTerraGonInsertXML(out ChkInInsertXML);
        }


        /// <summary>
        /// F3230_GetChkOutParcelIDs
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_GetChkOutParcelIDs(int SnapShotID)
        {
            return Helper.F3230_GetChkOutParcelIDs(SnapShotID);
        }

        /// <summary>
        /// F3230_GetCheckOutDetails
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_GetCheckOutDetails(int SnapShotID, int UserID)
        {
            return Helper.F3230_GetCheckOutDetails(SnapShotID, UserID);
        }

        /// <summary>
        /// F3230_SaveChkOutParcelIDs
        /// </summary>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public int F3230_SaveChkOutParcelIDs(string ParcelXML)
        {
            return Helper.F3230_SaveChkOutParcelIDs(ParcelXML);
        }

        /// <summary>
        /// F3230_SaveCheckOutDetails
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        public int F3230_SaveCheckOutDetails(string CheckOutXML)
        {
            return Helper.F3230_SaveCheckOutDetails(CheckOutXML);
        }



        public F3230CheckInData F3230_ChkInDeprXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInDeprXML();
            return fieldUseData;
        }


        #region F3230_ChkInEstimateComponentGroupXML
        /// <summary>
        /// F3230_ChkInEstimateComponentGroupXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInEstimateComponentGroupXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInEstimateComponentGroupXML();
            return fieldUseData;
        }
        #endregion

        #region F3230_ChkInNBHDXML
        /// <summary>
        /// F3230_ChkInNBHDXML
        /// </summary>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInNBHDXML()
        {
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInNBHDXML();
            return fieldUseData;
        }
        #endregion F3230_ChkInNBHDXML

        #region F3230_ChkInValueSliceXML
        /// <summary>
        /// F3230_ChkInValueSliceXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInValueSliceXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInValueSliceXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInValueSliceXML

        #region F3230_ChkInCommentXML
        /// <summary>
        /// F3230_ChkInCommentXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInCommentXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInCommentXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInCommentXML

        #region F3230_ChkInEstimateXML
        /// <summary>
        /// F3230_ChkInEstimateXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInEstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInEstimateXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInEstimateXML

        #region F3230_ChkInFileXML
        /// <summary>
        /// F3230_ChkInFileXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInFileXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInFileXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInFileXML

        #region F3230_ChkInLandValuesXML
        /// <summary>
        /// F3230_ChkInLandValuesXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInLandValuesXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInLandValuesXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInLandValuesXML

        #region F3230_ChkInLandXML
        /// <summary>
        /// F3230_ChkInLandXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInLandXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInLandXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInLandXML

        #region F3230_ChkInMiscXML
        /// <summary>
        /// F3230_ChkInMiscXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInMiscXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInMiscXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInMiscXML

        #region F3230_ChkInMSC_EstimateXML
        /// <summary>
        /// F3230_ChkInMSC_EstimateXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInMSC_EstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInMSC_EstimateXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInMSC_EstimateXML

        #region F3230_ChkInObjectXML
        /// <summary>
        /// F3230_ChkInObjectXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInObjectXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInObjectXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInObjectXML

        #region F3230_ChkInParcelValueXML
        /// <summary>
        /// F3230_ChkInParcelValueXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInParcelValueXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInParcelValueXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInParcelValueXML

        #region F3230_ChkInParcelXML
        /// <summary>
        /// F3230_ChkInParcelXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInParcelXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInParcelXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInParcelXML

        #region F3230_ChkInTerraGonXML
        /// <summary>
        /// F3230_ChkInTerraGonXML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInTerraGonXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInTerraGonXML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInTerraGonXML

        #region F3230_ChkInType2XML
        /// <summary>
        /// F3230_ChkInType2XML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInType2XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInType2XML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInType2XML

        #region F3230_ChkInType6XML
        /// <summary>
        /// F3230_ChkInType6XML
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230CheckInData F3230_ChkInType6XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230CheckInData fieldUseData = new F3230CheckInData();
            fieldUseData = Helper.F3230_ChkInType6XML(TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkInType6XML

        #endregion

        #region F3230 FieldUse CheckOut

        #region F3230_AddValues

        /// <summary>
        /// F3230_AddValues
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public int F3230_AddValues(int KeyID, string KeyName, int Form, int? ModuleID, int InsertedBy)
        {
            return Helper.F3230_AddValues(KeyID, KeyName, Form, ModuleID, InsertedBy);
        }

        #endregion

        #region F9065_GetSnapshotDetail
        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public string F9065_GetSnapshotDetail()
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            fieldUseData = Helper.F9065_GetSnapshotDetail();
            return fieldUseData.GetXml();
        }
        #endregion F9065_GetSnapshotDetail

        #region F9065_UpdateApplicationStatus
        /// <summary>
        /// F9065_s the update application status.
        /// </summary>
        /// <param name="isCheckedOut">if set to <c>true</c> [is checked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [is online].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F9065_UpdateApplicationStatus(bool isCheckedOut, bool isOnline, int userId)
        {
            return Helper.F9065_UpdateApplicationStatus(isCheckedOut, isOnline, userId);
        }
        #endregion F9065_UpdateApplicationStatus

        #region F9065_GetAuditCount
        /// <summary>
        /// F9065_s the insert CHK out XML.
        /// </summary>
        /// <returns>int</returns>
        public int F9065_GetAuditCount()
        {
            return Helper.F9065_GetAuditCount();
        }
        #endregion F9065_GetAuditCount

        #region F9065_DeleteCheckOutTable
        /// <summary>
        /// F9065_DeleteCheckOutTable.
        /// </summary>
        /// <returns>int</returns>
        public int F9065_DeleteCheckOutTable()
        {
            return Helper.F9065_DeleteCheckOutTable();
        }
        #endregion F9065_DeleteCheckOutTable

        #region F9065_InsertFieldElement
        /// <summary>
        /// F9065_s the insert CHK out XML.
        /// </summary>
        /// <param name="fieldElement"></param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F9065_InsertFieldElement(string fieldElement, int userId)
        {
            return Helper.F9065_InsertFieldElement(fieldElement, userId);
        }
        #endregion F9065_InsertFieldElement

        #region F9065_GetPreviewDetail
        /// <summary>
        /// F9065_s the get preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>string</returns>
        public string F9065_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            fieldUseData = Helper.F9065_GetPreviewDetail(snapShotId, snapShotDetail);
            return fieldUseData.GetXml();
        }
        #endregion F9065_GetPreviewDetail

        #region F9065_InsertApplicationConfiguration
        /// <summary>
        /// F9065_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F9065_InsertApplicationConfiguration(string configXml, int userId)
        {
            return Helper.F9065_InsertApplicationConfiguration(configXml, userId);
        }
        #endregion F9065_InsertApplicationConfiguration

        #region F9065_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public string F9065_GetcfgConfiguration(string cfgname)
        {
            F9065FieldUseData fieldUseData = new F9065FieldUseData();
            fieldUseData = Helper.F9065_GetcfgConfiguration(cfgname);
            return fieldUseData.GetXml();
        }

        //public F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue)
        //{
        //    F3230FieldUseData fieldUseData = new F3230FieldUseData();
        //    fieldUseData = Helper.F3230_ChkOutConfigXML(snapShotId, snapShotValue);
        //    return fieldUseData;
        //}

        #endregion F9065_GetcfgConfiguration

        #region F3230_GetSnapshotDetail
        /// <summary>
        /// F3230_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public string F3230_GetSnapshotDetail()
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_GetSnapshotDetail();
            return fieldUseData.GetXml();
        }
        #endregion F3230_GetSnapshotDetail

        #region F3230_UpdateApplicationStatus
        /// <summary>
        /// F3230_s the update application status.
        /// </summary>
        /// <param name="isCheckedOut">if set to <c>true</c> [is checked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [is online].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F3230_UpdateApplicationStatus(bool isCheckedOut, bool isOnline, int userId)
        {
            return Helper.F3230_UpdateApplicationStatus(isCheckedOut, isOnline, userId);
        }
        #endregion F3230_UpdateApplicationStatus

        #region F3230_GetAuditCount
        ///// <summary>
        ///// F3230_s the insert CHK out XML.
        ///// </summary>
        ///// <returns>int</returns>
        //public int F3230_GetAuditCount()
        //{
        //    return Helper.F3230_GetAuditCount();
        //}
        #endregion F3230_GetAuditCount

        #region F3230_DeleteCheckOutTable
        /// <summary>
        /// F3230_DeleteCheckOutTable.
        /// </summary>
        /// <returns>int</returns>
        public int F3230_DeleteCheckOutTable()
        {
            return Helper.F3230_DeleteCheckOutTable();
        }
        #endregion F3230_DeleteCheckOutTable

        #region F3230_InsertFieldElement
        /// <summary>
        /// F3230_s the insert CHK out XML.
        /// </summary>
        /// <param name="fieldElement"></param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F3230_InsertFieldElement(string fieldElement, int userId)
        {
            return Helper.F3230_InsertFieldElement(fieldElement, userId);
        }
        #endregion F3230_InsertFieldElement

        #region F3230_GetPreviewDetail
        /// <summary>
        /// F3230_s the get preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>string</returns>
        public string F3230_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_GetPreviewDetail(snapShotId, snapShotDetail);
            return fieldUseData.GetXml();
        }
        #endregion F3230_GetPreviewDetail

        #region F3230_InsertApplicationConfiguration
        /// <summary>
        /// F3230_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F3230_InsertApplicationConfiguration(string configXml, int userId)
        {
            return Helper.F3230_InsertApplicationConfiguration(configXml, userId);
        }
        #endregion F3230_InsertApplicationConfiguration

        #region F3230_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public string F3230_GetcfgConfiguration(string cfgname)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_GetcfgConfiguration(cfgname);
            return fieldUseData.GetXml();
        }

        //public F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue)
        //{
        //    F3230FieldUseData fieldUseData = new F3230FieldUseData();
        //    fieldUseData = Helper.F3230_ChkOutConfigXML(snapShotId, snapShotValue);
        //    return fieldUseData;
        //}

        #endregion F3230_GetcfgConfiguration

        #region F3230_ChkOutConfigXML
        /// <summary>
        /// F3230_ChkOutConfigXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutConfigXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutConfigXML


        #region F3230GetApexFilePath
        /// <summary>
        /// F3230GetApexFilePath
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230GetApexFilePath(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230GetApexFilePath(snapShotId);
            return fieldUseData;
        }
        #endregion F3230GetApexFilePath

        /// <summary>
        /// F3230_ChkOutCommonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutCommonXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutCommonXML(snapShotId, snapShotValue);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutCorrectionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData f3230_ChkOutCorrectionXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.f3230_ChkOutCorrectionXML(snapShotId);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutSaleXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData f3230_ChkOutSaleXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.f3230_ChkOutSaleXML(snapShotId);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutReceiptXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData f3230_ChkOutReceiptXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.f3230_ChkOutReceiptXML(snapShotId);
            return fieldUseData;
        }

        /// <summary>
        /// f3230_ChkOutStatementXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public F3230FieldUseData f3230_ChkOutStatementXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.f3230_ChkOutStatementXML(snapShotId);
            return fieldUseData;
        }


        #region F3230_ChkOutUserXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutUserXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutUserXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion  F3230_ChkOutUserXML

        #region F3230_ChkOutMiscXML
        /// <summary>
        /// F3230_ChkOutMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMiscXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMiscXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMiscXML

        #region F3230_ChkOutFormXML
        /// <summary>
        /// F3230_ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutFormXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutFormXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutFormXML

        #region F3230_ChkOutEventXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutEventXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutEventXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutEventXML

        /// <summary>
        /// F3230_ChkOutParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutParcelXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutParcelXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }

        #region F3230_ChkOutNBHDXML
        /// <summary>
        /// F3230_ChkOutNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutNBHDXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutNBHDXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutNBHDXML

        #region F3230_ChkOutOwnerXML
        /// <summary>
        /// F3230_ChkOutOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutOwnerXML

        #region F3230_ChkOutDistrictXML
        /// <summary>
        /// F3230_ChkOutDistrictXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutDistrictXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutDistrictXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutDistrictXML

        #region F3230_ChkOutLegalXML
        /// <summary>
        /// <summary>
        /// F3230_ChkOutLegalXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutLegalXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutLegalXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutLegalXML

        #region F3230_ChkOutMisc_CatalogXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMisc_CatalogXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMisc_CatalogXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMisc_CatalogXML

        #region F3230_ChkOutMiscTableXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMiscTableXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMiscTableXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMiscTableXML

        #region F3230_ChkOutMOwnerXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMOwnerXML

        #region F3230_ChkOutObjectXML
        /// <summary>
        /// F3230_ChkOutObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutObjectXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutObjectXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutObjectXML

        #region F3230_ChkOutValueSliceXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutValueSliceXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutValueSliceXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutValueSliceXML

        #region F3230ChkOutDeprMiscXML
        /// <summary>
        /// F3230ChkOutDeprMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230ChkOutDeprMiscXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230ChkOutDeprMiscXML(snapShotId, snapShotValue);
            return fieldUseData;
        }

        #endregion F3230ChkOutDeprMiscXML

        /// <summary>
        /// F3230ChkOutDeprXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230ChkOutDeprXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230ChkOutDeprXML(snapShotId, snapShotValue);
            return fieldUseData;
        }

        #region F3230_ChkOutEstimateCompXML
        /// <summary>
        /// F3230_ChkOutEstimateCompXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutEstimateCompXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutEstimateCompXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutEstimateCompXML

        #region F3230_ChkOutVSTGCitemXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutVSTGCitemXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutVSTGCitemXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutVSTGCitemXML

        #region F3230_ChkOutMSCEstimateXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMSCEstimateXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMSCEstimateXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMSCEstimateXML

        #region F3230_ChkOutEstimateResultXML
        /// <summary>
        /// F3230_ChkOutEstimateResultXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutEstimateResultXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutEstimateResultXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutEstimateResultXML

        #region F3230_ChkOutMSCEstimateOccupancyXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutMSCEstimateOccupancyXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutMSCEstimateOccupancyXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutMSCEstimateOccupancyXML

        #region F3230_ChkOutEstimateBuildingXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutEstimateBuildingXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutEstimateBuildingXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutEstimateBuildingXML

        #region F3230_ChkOutLandValuesXML
        /// <summary>
        /// F3230_ChkOutLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutLandValuesXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutLandValuesXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutLandValuesXML

        #region F3230_ChkOutTerraGonXML
        /// <summary>
        /// F3230_ChkOutVSTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutTerraGonXML(int snapShotId)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutTerraGonXML(snapShotId);
            return fieldUseData;
        }
        #endregion F3230_ChkOutVSTerraGonXML

        #region F3230_ChkOutEstimateComponentXML
        /// <summary>
        /// F3230_ChkOutEstimateComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutEstimateComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutEstimateComponentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutEstimateComponentXML

        #region F3230_ChkOutCommentXML
        /// <summary>
        /// F3230_ChkOutCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutCommentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutCommentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutCommentXML

        /// <summary>
        /// F3230_LockParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="LockAdminBy"></param>
        /// <param name="UserID"></param>
        /// <param name="UnlockParcelXML"></param>
        /// <returns></returns>
        public int F3230_LockParcelID(int? SnapShotID, int? LockAdminBy, int? UserID, string UnlockParcelXML)
        {
            return Helper.F3230_LockParcelID(SnapShotID, LockAdminBy, UserID, UnlockParcelXML);
        }

        /// <summary>
        /// F3230_ListLockedParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ListLockedParcelID(int? SnapShotID, out int RowendValue)
        {
            RowendValue = 0;
            return Helper.F3230_ListLockedParcelID(SnapShotID, out  RowendValue);
        }

        #region F3230_ChkOutVSTGComponentXML
        /// <summary>
        /// F3230_ChkOutVSTGComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutVSTGComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutVSTGComponentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutVSTGComponentXML

        #region F3230_ChkOutFileXML
        /// <summary>
        /// F3230_ChkOutFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutFileXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutFileXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutFileXML

        #region F3230_ChkOutVSTGGonBldgXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutVSTGGonBldgXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutVSTGGonBldgXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }

        #endregion F3230_ChkOutVSTGGonBldgXML

        #region F25000_ChkOutLandXML
        /// <summary>
        ///  F25000_ChkOutLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutLandXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutLandXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutLandXML

        #region F25000_ChkOutVersionXML
        /// <summary>
        /// F25000_ChkOutVersionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="RowendValue"></param>
        /// <param name="StartRow"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutVersionXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutVersionXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutVersionXML

        #region F3230_ChkOutSitusXML
        /// <summary>
        /// F3230_ChkOutSitusXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F3230FieldUseData F3230_ChkOutSitusXML(int snapShotId, string snapShotValue)
        {
            F3230FieldUseData fieldUseData = new F3230FieldUseData();
            fieldUseData = Helper.F3230_ChkOutSitusXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F3230_ChkOutSitusXML

        #region F25000_ChkOutType2XML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutType2XML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutType2XML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutType2XML

        //public F3230FieldUseData F3230_ChkOutUserXML(int snapShotId, string snapShotValue)
        //{
        //    F3230FieldUseData fieldUseData = new F3230FieldUseData();
        //    fieldUseData = Helper.F3230_ChkOutUserXML(snapShotId, snapShotValue);
        //    return fieldUseData;
        //}

        #region F25000_ChkOutSeniorExemptionXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutSeniorExemptionXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutSeniorExemptionXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutSeniorExemptionXML

        #region F25000_ChkOutAssessmentTypeXML
        /// <summary>
        /// F25000_ChkOutAssessmentTypeXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutAssessmentTypeXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutAssessmentTypeXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutAssessmentTypeXML

        #region F25000_ChkOutParcelValueXML
        /// <summary>
        /// F25000_ChkOutParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public F25000FieldUseData F25000_ChkOutParcelValueXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            fieldUseData = Helper.F25000_ChkOutParcelValueXML(snapShotId, snapShotValue);
            return fieldUseData;
        }
        #endregion F25000_ChkOutParcelValueXML


        #region F3230_InsertChkOutXML
        /// <summary>
        /// F3230_InsertChkOutXML
        /// </summary>
        /// <param name="xmlInsContent">Content of the XML ins.</param>
        /// <param name="tableXml">The table XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F3230_InsertChkOutXML(string xmlInsContent, string tableXml, int userId, bool IsDelete)
        {
            return Helper.F3230_InsertChkOutXML(xmlInsContent, tableXml, userId, IsDelete);
        }
        #endregion F3230_InsertChkOutXML

        #region F3230_InsertChkInXML
        /// <summary>
        /// F3230_InsertChkOutXML
        /// </summary>
        /// <param name="xmlInsContent">Content of the XML ins.</param>
        /// <param name="tableXml">The table XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int F3230_InsertChkInXML(string xmlInsContent, string tableXml, int userId)
        {
            return Helper.F3230_InsertChkInXML(xmlInsContent, tableXml, userId);
        }
        #endregion F3230_InsertChkInXML

        /// <summary>
        /// F3230_InsertAddedRecordXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int F3230_InsertAddedRecordXML(string xmlInsContent, string tableXml, int userId)
        {
            return Helper.F3230_InsertAddedRecordXML(xmlInsContent, tableXml, userId);
        }


        #endregion

        #region F3200_CamaSketch

        #region F3200_GetSketchData
        /// <summary>
        /// Get the Sketch Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String or dataset</returns>
        public string F3200_GetSketchData(int objectId)
        {
            F3200CamaSketchData getCamaSketchFeeData = new F3200CamaSketchData();
            getCamaSketchFeeData = Helper.F3200_GetSketchData(objectId);
            return getCamaSketchFeeData.GetXml();
        }
        #endregion F3200_GetSketchData

        #region F3200_GetStyleList
        /// <summary>
        /// Get the Style List Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String or dataset</returns>
        public string F3200_GetStyleList(int objectId)
        {
            return Helper.F3200_GetStyleList(objectId);
        }
        #endregion F3200_GetStyleList

        #region F3200_SaveSketch
        /// <summary>
        /// Save the Sketch data
        /// </summary>
        /// <param name="objectId">objectId</param>
        /// <param name="sketchData">sketchData</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public DataSet F3200_SaveSketchData(int objectId, string sketchData, int userId)
        {
            return Helper.F3200_SaveSketchData(objectId, sketchData, userId);
        }
        #endregion F3200_SaveSketch

        #region F3200_CheckSmartPart

        /// <summary>
        /// Check the SmartPart
        /// </summary>
        /// <param name="formId">FormNumber</param>
        /// <returns>integer</returns>
        public int F3200_CheckSmartPart(int formId)
        {
            return Helper.F3200_CheckSmartPart(formId);
        }

        #endregion F3200_CheckSmartPart

        #endregion F3200_CamaSketch

        #region F95010WebFormXML

        #region F95010GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Dataset</returns>
        public string GetWebFormXML(int? keyId, int form, int userId)
        {
            F95010GetWebFormXMLData form95010GetWebFormXMLData = new F95010GetWebFormXMLData();
            form95010GetWebFormXMLData = Helper.GetWebFormXML(keyId, form, userId);
            return form95010GetWebFormXMLData.GetXml();
        }
        #endregion F95010GetWebFormXML

        #endregion F95010WebFormXML

        #region Private Class Used to Convert Collection to a Dataset

        /// <summary>
        /// CollectionToDataSet
        /// </summary>
        private class CollectionToDataSet  ////<T > where T : System.Collections.ICollection
        {
            /// <summary>
            /// _collection
            /// </summary>
            private System.Collections.ICollection _collection;

            /// <summary>
            /// _propertyCollection
            /// </summary>
            private PropertyInfo[] _propertyCollection = null;

            /// <summary>
            /// Initializes a new instance of the <see cref="T:CollectionToDataSet"/> class.
            /// </summary>
            /// <param name="list">The list.</param>
            public CollectionToDataSet(System.Collections.ICollection list)
            {
                this._collection = list;
            }

            /// <summary>
            /// Gets the property collection.
            /// </summary>
            /// <value>The property collection.</value>
            private PropertyInfo[] PropertyCollection
            {
                get
                {
                    if (this._propertyCollection == null)
                    {
                        this._propertyCollection = this.GetPropertyCollection();
                    }

                    return this._propertyCollection;
                }
            }

            /// <summary>
            /// Creates the data set.
            /// </summary>
            /// <returns>Dataset</returns>
            public DataSet CreateDataSet()
            {
                DataSet ds = new DataSet("GridDataSet");

                ds.Tables.Add(this.FillDataTable());

                return ds;
            }

            /// <summary>
            /// Gets the property collection.
            /// </summary>
            /// <returns>Property info</returns>
            private PropertyInfo[] GetPropertyCollection()
            {
                if (this._collection.Count > 0)
                {
                    IEnumerator enumerator = this._collection.GetEnumerator();

                    enumerator.MoveNext();

                    return enumerator.Current.GetType().GetProperties();
                }

                return null;
            }

            /// <summary>
            /// Fills the data table.
            /// </summary>
            /// <returns>Datatable</returns>
            private DataTable FillDataTable()
            {
                IEnumerator enumerator = this._collection.GetEnumerator();

                DataTable dt = this.CreateDataTable();

                while (enumerator.MoveNext())
                {
                    dt.Rows.Add(this.FillDataRow(dt.NewRow(), enumerator.Current));
                }

                return dt;
            }

            /// <summary>
            /// Fills the data row.
            /// </summary>
            /// <param name="dataRow">The data row.</param>
            /// <param name="p">The p.</param>
            /// <returns>Datarow</returns>
            private DataRow FillDataRow(DataRow dataRow, object p)
            {
                foreach (PropertyInfo property in this.PropertyCollection)
                {
                    dataRow[property.Name.ToString()] = property.GetValue(p, null);
                }

                return dataRow;
            }

            /// <summary>
            /// Creates the data table.
            /// </summary>
            /// <param name="nameTable">The name table.</param>
            /// <returns>Datatable</returns>
            private DataTable CreateDataTable(string nameTable)
            {
                DataTable dt = new DataTable("GridDataTable");
                dt.TableName = nameTable;
                foreach (PropertyInfo property in this.PropertyCollection)
                {
                    dt.Columns.Add(property.Name.ToString());
                }

                return dt;
            }

            /// <summary>
            /// Creates the data table.
            /// </summary>
            /// <returns>datatable</returns>
            private DataTable CreateDataTable()
            {
                DataTable dt = new DataTable("GridDataTable");

                foreach (PropertyInfo property in this.PropertyCollection)
                {
                    dt.Columns.Add(property.Name.ToString());
                }

                return dt;
            }
        }

        #endregion

        #region 3510 Neighborhood Selection

        #region List NeighborhoodType

        /// <summary>
        /// F3510_s the type of the list neighborhood.
        /// </summary>
        /// <returns>NeighborhoodType String.</returns>
        /// 
        public string F3510_ListNeighborhoodType()
        {
            F3510NeighborhoodSelectionData neighborhoodType = new F3510NeighborhoodSelectionData();
            neighborhoodType = Helper.F3510_ListNeighborhoodType();
            return neighborhoodType.GetXml();
        }

        #endregion

        #region List Neighborhood Selection

        /// <summary>
        /// Get Neighborhood Selection Details
        /// </summary>
        /// <param name="neighborhood">Neighborhood.</param>
        /// <param name="childof">Childof.</param>
        /// <param name="rollyear">rollYear.</param>
        /// <param name="type">Type.</param>
        /// <param name="Description">Description.</param>
        /// <returns>NeighborhoodSelection String</returns>

        public string F3510_ListNeighborhoodSelection(string neighborhood, string childof, string rollyear, string type, string description)
        {
            F3510NeighborhoodSelectionData neighborhoodSelection = new F3510NeighborhoodSelectionData();
            neighborhoodSelection = Helper.F3510_ListNeighborhoodSelection(neighborhood, childof, rollyear, type, description);
            return neighborhoodSelection.GetXml();
        }

        #endregion

        #endregion

        #region F2010_StateCodeSelection

        #region List StateCodeSelection

        /// <summary>
        /// F2010_s the list state code selection.
        /// </summary>
        /// <returns></returns>
        public string F2010_ListStateCodeSelection()
        {
            return Helper.F2010_ListStateCodeSelection().GetXml();
        }
        #endregion List StateCodeSelection

        #endregion F2010_StateCodeSelection

        #region 9066CheckIn
        //#region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public int F9066_GetAuditCount()
        //{
        //    return Helper.F9066_GetAuditCount();
        //}
        #endregion GetAuditCount

        #region GetCheckInXML
        ///// <summary>
        ///// Get Check In Details
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public string F9066_GetCheckInData()
        //{
        //    F9066CheckInData getCheckInData = new F9066CheckInData();
        //    getCheckInData = Helper.F9066_GetCheckInData();
        //    return getCheckInData.GetXml();
        //}
        #endregion GetCheckInXML

        #region SaveXML
        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="insertValue">insertValue</param>
        /// <param name="updateValue">updateValue</param>
        public void F9066_SaveData(string insertValue, string updateValue)
        {
            Helper.F9066_SaveData(insertValue, updateValue);
        }
        #endregion SaveXML

        #region DeleteData
        ///// <summary>
        ///// Delete the values
        ///// </summary>
        ///// <returns>Integer</returns>
        //public int F9066_DeleteData()
        //{
        //    return Helper.F9066_DeleteData();
        //}
        #endregion DeleteData

        //#endregion 9066CheckIn

        #region F1430 Interest Calculator
        /// <summary>
        /// F1430_GetCalculatorDetails gets the calculator details on load.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String</returns>
        public string F1430_GetCalculatorDetails(int statementId)
        {
            F1430InterestCalculatorData getCalculatorDetails = new F1430InterestCalculatorData();
            getCalculatorDetails = Helper.F1430_GetCalculatorDetails(statementId);
            return getCalculatorDetails.GetXml();
        }

        /// <summary>
        /// F1430_GetInterestDetails get the interest and deliquency details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxAmount">The tax amount.</param>
        /// <returns>String</returns>
        public string F1430_GetInterestDetails(int statementId, DateTime interestDate, decimal taxAmount)
        {
            F1430InterestCalculatorData getInterestDetails = new F1430InterestCalculatorData();
            getInterestDetails = Helper.F1430_GetInterestDetails(statementId, interestDate, taxAmount);
            return getInterestDetails.GetXml();
        }

        #endregion F1430 Interest Calculator

        #region F1440 Batch Button SmartPart

        #region F1440_SaveRecieptinSnapShotBatchButtonControl

        /// <summary>
        /// F1440_SaveRecieptinSnapShotBatchButtonControl        
        /// To Insert or Update the newly created Receipt id  to the particular snapshot id.
        /// </summary>
        /// <param name="snapshotId">the snapshotId</param>
        /// <param name="receiptId">the receiptId</param>
        /// <param name="userId">the userId</param>
        /// <returns>returns the no of items count in snapshot</returns>
        public int F1440_SaveRecieptinSnapShotBatchButtonControl(int snapshotId, int? receiptId, int userId)
        {
            return Helper.F1440_SaveRecieptinSnapShotBatchButtonControl(snapshotId, receiptId, userId);
        }

        #endregion F1440_SaveRecieptinSnapShotBatchButtonControl

        #endregion F1440 Batch Button SmartPart

        #region F82001 BuildingPermit

        #region GetBuildingPermitDetails

        /// <summary>
        /// F82001_s the get building permit details.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns>buildingPermitDetails</returns>
        public string F82001_GetBuildingPermitDetails(int eventID)
        {
            F82001BuildingPermitData buildingPermitDetails = new F82001BuildingPermitData();
            buildingPermitDetails = Helper.F82001_GetBuildingPermitDetails(eventID);
            return buildingPermitDetails.GetXml();
        }

        #endregion GetBuildingPermitDetails

        #region InsertBuildingPermitDetails

        /// <summary>
        /// F82001_s the insert building permit details.
        /// </summary>
        /// <param name="permitId">The permit id.</param>
        /// <param name="buildingPermitItems">The building permit items.</param>
        /// <returns></returns>
        public int F82001_InsertBuildingPermitDetails(int permitId, string buildingPermitItems, int userId)
        {
            return Helper.F82001_InsertBuildingPermitDetails(permitId, buildingPermitItems, userId);
        }

        #endregion InsertBuildingPermitDetails

        #endregion F82001 BuildingPermit

        #region F82002ContractorManagement

        public string F82002_ListContractorManagementData(int? iContractorID, string ContractorXML)
        {
            F82002ContractorManagementData ContractorManagementData = new F82002ContractorManagementData();
            ContractorManagementData = Helper.F82002_ListContractorManagementData(iContractorID, ContractorXML);
            return ContractorManagementData.GetXml();
        }
        public int F82002_InsertBuildingPermitDetails(int? ContractorID, string ContractorItems, int UserID)
        {
            return Helper.F82002_InsertBuildingPermitDetails(ContractorID, ContractorItems, UserID);
        }

        public void F82002_DeleteContractorManagement(int contractorId, int UserID)
        {
            Helper.F82002_DeleteContractorManagement(contractorId, UserID);
        }

        #endregion

        #region F36060DepreciationComp

        #region F36060_GetDepreciationTables

        /// <summary>
        /// To get the Depreciation  tables
        /// </summary>
        /// <param name="deprTableId">Deprtable id</param>
        /// <returns>Typed dataset containing the Deprecition and Deprecition items datatable</returns>
        public string F36060_GetDepreciationTables(int deprTableId)
        {
            F36060DepreciationData form36060DepreciationData = new F36060DepreciationData();
            form36060DepreciationData = Helper.F36060_GetDepreciationTables(deprTableId);
            return form36060DepreciationData.GetXml();
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
        public int F36060_SaveDepreciationTables(int deprTableId, string deprecationItem, string otherDeprItem, int userId)
        {
            return Helper.F36060_SaveDepreciationTables(deprTableId, deprecationItem, otherDeprItem, userId);
        }

        #endregion F36060_SaveDepreciationTables

        #region F36060_DeleteDepreciationTables

        /// <summary>
        /// To delete Depreciation Tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="userId">The user id.</param>
        public void F36060_DeleteDepreciationTables(int deprTableId, int userId)
        {
            Helper.F36060_DeleteDepreciationTables(deprTableId, userId);
        }

        #endregion F36060_DeleteDepreciationTables

        #endregion F36060DepreciationComp

        #region Instrumentheader

        #region GetInstrumentHeaderDetails

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        public string F49910_GetInstrumentHeaderDetails(int instId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();
            instrumentHeaderDataSet = Helper.F49910_GetInstrumentHeaderDetails(instId);
            return instrumentHeaderDataSet.GetXml();
        }
        #endregion GetInstrumentHeaderDetails

        #region ListInstrumentType

        /// <summary>
        /// F49910_GetInstrumentTypeDetails
        /// </summary>
        /// <returns>DataSet</returns>
        public string F49910_GetInstrumentTypeDetails()
        {
            F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();
            instrumentHeaderDataSet = Helper.F49910_GetInstrumentTypeDetails();
            return instrumentHeaderDataSet.GetXml();
        }

        #endregion ListInstrumentType

        #region SaveInstrumentHeaderDetails

        /// <summary>
        /// F49910_SaveInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="paymentItems">paymentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F49910_SaveInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return Helper.F49910_SaveInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion SaveInstrumentHeaderDetails

        #region F49910CheckInstrumentHeader Deatils

        /// <summary>
        /// F49910CheckInstrumentHeaderDetails
        /// Used to validate whether the records can be saved
        /// </summary>        
        /// <param name="instId"></param>
        /// <param name="instrumentItems"></param>
        /// <param name="paymentItems"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// 0 - When the records can be saved
        /// -1 - when Instrument Number already exists in the Database
        /// </returns>
        public int F49910CheckInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return Helper.F49910CheckInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion F49910CheckInstrumentHeader Deatils

        #region DeleteInstrumentHeaderDetails

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F49910_DeleteInstrumentHeader(int instId, int userId)
        {
            return Helper.F49910_DeleteInstrumentHeader(instId, userId);
        }

        #endregion DeleteInstrumentHeaderDetails

        #region CopyInstrumentDetails

        /// <summary>
        /// F49910_CopyInstrumentHeaderDetails
        /// </summary>
        /// <param name="instrumentId">instrumentId</param>
        /// <param name="instrumentValue">instrumentValue</param>
        /// <param name="grantorValue">grantorValue</param>
        /// <param name="granteeValue">granteeValue</param>
        /// <param name="legalValue">legalValue</param>
        /// <returns>dataSet</returns>
        public string F49910_CopyInstrumentHeaderDetails(int instrumentId, int instrumentValue, int grantorValue, int granteeValue, int legalValue)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();
            instrumentHeaderDataSet = Helper.F49910_CopyInstrumentHeaderDetails(instrumentId, instrumentValue, grantorValue, granteeValue, legalValue);
            return instrumentHeaderDataSet.GetXml();
        }

        #endregion CopyInstrumentDetails

        #region F49910_GetGranterAddressDetails

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        public string F49910_GetGranterAddressDetails(int grantId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();
            instrumentHeaderDataSet = Helper.F49910_GetGranterAddressDetails(grantId);
            return instrumentHeaderDataSet.GetXml();
        }
        #endregion F49910_GetGranterAddressDetails

        #region GetFeeDetails

        /// <summary>
        /// F49910_GetFeeDetails
        /// </summary>
        /// <param name="insTypeId">insTypeId</param>
        /// <returns>dataSet</returns>
        public string F49910_GetFeeDetails(int insTypeId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();
            instrumentHeaderDataSet = Helper.F49910_GetFeeDetails(insTypeId);
            return instrumentHeaderDataSet.GetXml();
        }

        #endregion GetFeeDetails

        #endregion Instrumentheader

        #region F2200EditSchedule

        public string F2200_ListEditScheduleDetails(int SheduleID)
        {
            return Helper.F2200_ListEditScheduleDetails(SheduleID).GetXml();
        }

        public string F25050_GetScheduleDetails(int ScheduleID)
        {
            return Helper.F25050_GetScheduleDetails(ScheduleID).GetXml();
        }

        public int F2005_GetValidUser(int ScheduleID,int UserID)
        {
            return Helper.F2005_GetValidUser( ScheduleID, UserID);
        }

        public int F2005_UpdateParcelLockDetails(int ScheduleID, int lockValue, int userId)
        {
            return Helper.F2005_UpdateParcelLockDetails(ScheduleID, lockValue, userId);
        }

        public string F2005_GetScheduleUserName(int ScheduleID)
        {
            return Helper.F2005_GetScheduleUserName(ScheduleID).GetXml();
        }

        public string F2200_InsertEditSchedule(int? ScheduleID, string ScheduleItems, int UserID)
        {
            F2200EditScheduleData insertData = new F2200EditScheduleData();
            insertData = Helper.F2200_InsertEditSchedule(ScheduleID, ScheduleItems, UserID);
            return insertData.GetXml();
        }
        public int F2200_UpdateEditSchedule(int ScheduleID, string ScheduleItems, int UserID)
        {
            return Helper.F2200_UpdateEditSchedule(ScheduleID, ScheduleItems, UserID);
        }
        public int F2200_DeleteEditSchedule(int ScheduleID, int UserID)
        {
            return Helper.F2200_DeleteEditSchedule(ScheduleID, UserID);
        }

        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        public string F2200_GetAssessmentTypeDetails(string assessmentType)
        {
            F2200EditScheduleData getAssessmentTypeData = new F2200EditScheduleData();
            getAssessmentTypeData = Helper.F2200_GetAssessmentTypeDetails(assessmentType);
            return getAssessmentTypeData.GetXml();
        }

        #region Get Penalty Percent
        /// <summary>
        /// Gets the penalty percent.
        /// </summary>
        /// <param name="filingDate">The filing date.</param>
        /// <returns>Penalty Percent</returns>
        public decimal GetPenaltyPercent(string filingDate)
        {
            return Helper.GetPenaltyPercent(filingDate);
        }

        /// <summary>
        /// F2200_s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public decimal F2200_GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear, bool isEx259, decimal ex259Amount)
        {
            return Helper.f2200_GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear, isEx259, ex259Amount);
        }

        /// <summary>
        /// Get Farm ExemptAmount.
        /// </summary>
        /// <param name="scheduleID"></param>
        /// <returns></returns>
        public string F2200_Get259ExemptionAmount(int scheduleID)
        {
            return Helper.F2200_Get259ExemptionAmount(scheduleID).GetXml(); ;
        }
        #endregion Get Penalty Percent

        #endregion

        #region F49920InstrumentSearch Engine

        #region Instrument Search Engine Search

        /// <summary>
        /// </summary>
        /// <param name="instrumentcondition"></param>
        /// <returns></returns>
        public string F49920_ListInstrumentSearch(string instrumentcondition)
        {
            F49920InstrumentSearchEngineData instrumentsearch = new F49920InstrumentSearchEngineData();
            instrumentsearch = Helper.F49920_ListInstrumentSearch(instrumentcondition);
            return instrumentsearch.GetXml();
        }
        #endregion

        #region Instrument Search Engine Load

        /// <summary>
        /// </summary>
        /// <returns>string</returns>
        public string F49920_ListInstrumentLoad()
        {
            F49920InstrumentSearchEngineData instrumentload = new F49920InstrumentSearchEngineData();
            instrumentload = Helper.F49920_ListInstrumentLoad();
            return instrumentload.GetXml();
        }

        #endregion

        #endregion

        #region F49911 PartiesField Listing

        #region  List PartiesField

        /// <summary>
        /// F49911_s the list parties field.
        /// </summary>
        /// <returns></returns>
        public string F49911_ListPartiesField()
        {
            return Helper.F49911_ListPartiesField().GetXml();
        }

        #endregion  List PartiesField

        #region  Insert PartiesField

        /// <summary>
        /// F49911_s the insert parties field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="grantorItems">The grantor items.</param>
        /// <param name="granteeItems">The grantee items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F49911_InsertPartiesFieldDetails(int instid, string grantorItems, string granteeItems, int userId, int isCopy)
        {
            return Helper.F49911_InsertPartiesFieldDetails(instid, grantorItems, granteeItems, userId, isCopy);
        }

        #endregion  Insert PartiesField

        #endregion F49911 PartiesField Listing

        #region F49912 LegalField Listing

        #region  List LegalField

        /// <summary>
        /// F49912_s the list legal field.
        /// </summary>
        /// <returns></returns>
        public string F49912_ListLegalField(int instID)
        {
            return Helper.F49912_ListLegalField(instID).GetXml();
        }

        #endregion  List LegalField

        #region  Insert LegalField

        /// <summary>
        /// F49912_s the insert legal field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F49912_InsertLegalFieldDetails(int instid, string legalItems, int userId, int isCopy)
        {
            return Helper.F49912_InsertLegalFieldDetails(instid, legalItems, userId, isCopy);
        }

        #endregion  Insert LegalField


        /// <summary>
        /// F49912_s the list sub division combo.
        /// </summary>
        /// <returns></returns>
        public string F49912_ListSubDivisionCombo()
        {
            F49912LegalData legalData = new F49912LegalData();
            legalData = Helper.F49912_ListSubDivisionCombo();
            return legalData.GetXml();
        }

        #endregion F49912 LegalField Listing

        #region F36061 Depreciation Control

        #region F36061_ListDepr

        /// <summary>
        /// Used to List the Depr Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depr Details
        /// </returns>
        public string F36061_ListDepr(int nbhdId)
        {
            F36061DepreciationControlData depreciationControlData = new F36061DepreciationControlData();
            depreciationControlData = Helper.F36061_ListDepr(nbhdId);
            return depreciationControlData.GetXml();
        }

        #endregion F36061_ListDepr

        #region F36061_ListDeprControlItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        public string F36061_ListDeprControlItems(int nbhdId)
        {
            F36061DepreciationControlData depreciationControlData = new F36061DepreciationControlData();
            depreciationControlData = Helper.F36061_ListDeprControlItems(nbhdId);
            return depreciationControlData.GetXml();
        }

        #endregion F36061_ListDeprControlItems

        #region F36061_SaveDeprControlItems

        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F36061_SaveDeprControlItems(int? nbhdId, string deprControlItems, int userId)
        {
            return Helper.F36061_SaveDeprControlItems(nbhdId, deprControlItems, userId);
        }

        #endregion F36061_SaveDeprControlItems

        #endregion F36061 Depreciation Control

        #region F36062 LandInfluenceControl

        #region F36062_LandInfluenceItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        public string F36062_LandInfluenceItems(int nbhdId)
        {
            F36062LandInfluenceData LandControlData = new F36062LandInfluenceData();
            LandControlData = Helper.F36062_LandInfluenceItems(nbhdId);
            return LandControlData.GetXml();
        }
        #endregion F36062_LandInfluenceItems

        #region F36062_SaveInfluenceControl

        /// <summary>
        /// Used to save the Influence Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The Influence control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F36062_SaveInfluenceControl(int? nbhdId, string InfluenceItems, int userId)
        {
            return Helper.F36062_SaveInfluenceControl(nbhdId, InfluenceItems, userId);
        }

        #endregion F36062_SaveInfluenceControl

        #endregion F36062 LandInfluenceControl

        #region F35050ScheduleLineItem

        #region F35050_GetScheduleLineItemDetails

        /// <summary>
        /// F35050_GetScheduleLineItemDetails
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <returns>string</returns>
        public string F35050_GetScheduleLineItemDetails(int scheduleId)
        {
            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetScheduleLineItemDetails(scheduleId);
            return scheduleLineItem.GetXml();
        }

        #endregion F35050_GetScheduleLineItemDetails

        #region F35050_GetScheduleItem

        /// <summary>
        /// F35050_GetScheduleItem
        /// </summary>
        /// <returns>string</returns>
        public string F35050_GetScheduleItem()
        {
            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetScheduleItem();
            return scheduleLineItem.GetXml();
        }

        #endregion F35050_GetScheduleItem



        #region ListTableDetails

        public string F35050_GetListTableDetails(int itemcategoryID)
        {

            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetListTableDetails(itemcategoryID);
            return scheduleLineItem.GetXml();
        }

        #endregion ListTableDetails

        #region F35050ListOutTableDetails

        public string F35050_GetListOutTableDetails(int ScheduleID)
        {

            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetListOutTableDetails(ScheduleID);
            return scheduleLineItem.GetXml();
        }


        #endregion F35050ListOutTableDetails



        #region F35050_GetScheduleCategory

        /// <summary>
        /// F35050_GetScheduleCategory
        /// </summary>
        /// <returns>string</returns>
        public string F35050_GetScheduleCategory()
        {
            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetScheduleCategory();
            return scheduleLineItem.GetXml();
        }

        #endregion F35050_GetScheduleCategory

        #region F35050_SaveScheduleLineItem

        /// <summary>
        /// F35050_SaveScheduleLineItem
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="scheduleItem">scheduleItem</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F35050_SaveScheduleLineItem(int scheduleId, string scheduleItem, int userId)
        {
            return Helper.F35050_SaveScheduleLineItem(scheduleId, scheduleItem, userId);
        }

        #endregion F35050_SaveScheduleLineItem



        /// <summary>
        /// F35050_CalculateAmount
        /// </summary>
        /// <param name="ScheduleItemID">ScheduleItemID</param>
        /// <param name="Rollyear">Rollyear</param>
        /// <param name="Year">Year</param>
        /// <param name="Qnty">Qnty</param>
        /// <returns></returns>
        #region  #region F35050_CalculateAmount

        public string F35050_CalculateAmount(int ScheduleItemID, int Rollyear, int Year, int DeprDescription)
        {
            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_CalculateAmount(ScheduleItemID, Rollyear, Year, DeprDescription);
            return scheduleLineItem.GetXml();
        }

        #endregion




        #region F35050_GetDepreciationValue

        /// <summary>
        /// F35050_GetDepreciationValue
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="recv">recv</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>DataSet</returns>
        public string F35050_GetDepreciationValue(int scheduleId, int recv, int rollYear)
        {
            F35050ScheduleLineItemDataSet scheduleLineItem = new F35050ScheduleLineItemDataSet();
            scheduleLineItem = Helper.F35050_GetDepreciationValue(scheduleId, recv, rollYear);
            return scheduleLineItem.GetXml();
        }

        #endregion F35050_GetDepreciationValue

        #region F35050_DeleteScheduleLineItem

        /// <summary>
        /// F35050_DeleteScheduleLineItem
        /// </summary>
        /// <param name="scheduleLineId">scheduleLineId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F35050_DeleteScheduleLineItem(int scheduleLineId, int userId)
        {
            return Helper.F35050_DeleteScheduleLineItem(scheduleLineId, userId);
        }

        #endregion F35050_DeleteScheduleLineItem

        public string F35050_GetDeprPercentage(int rollyear, int deprtableID, int year)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            scheduleLineItemdata = Helper.F35050_GetDeprPercentage(rollyear, deprtableID, year);
            return scheduleLineItemdata.GetXml();
        }

        #region DeleteSchedule

        /// <summary>
        /// F35050_s the delete schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status</returns>
        public int F35050_DeleteSchedule(int scheduleId, int userId)
        {
            return Helper.F35050_DeleteSchedule(scheduleId, userId);
        }

        #endregion

        #endregion F35050ScheduleLineItem

        #region F1402 Schedule Search
        /// <summary>
        /// </summary>
        /// <param name="ScheduleConditionXML"></param>
        /// <returns>DataSet</returns>
        public string F1402_ListScheduleSearch(string ScheduleConditionXML)
        {
            F1402ScheduleSelectionData schedulesearch = new F1402ScheduleSelectionData();
            schedulesearch = Helper.F1402_ListScheduleSearch(ScheduleConditionXML);
            return schedulesearch.GetXml();
        }
        #endregion

        #region CustomGridFunctionality

        /// <summary>
        /// Lists the query engine grid function.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFitler">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Count</param>
        /// <returns></returns>
        public DataSet ListQueryEngineGridFunction(int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, string isFitler, string maxRecord)
        {
            return Helper.ListQueryEngineGridFunction(queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFitler, maxRecord);
        }

        /// <summary>
        /// Lists the query engine grid snapshot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFitler">Flag for load all records</param>
        /// <param name="maxRecord">Max Record count</param>
        /// <returns></returns>
        public DataSet ListQueryEngineGridSnapshot(int snapShotId, int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFitler, string maxRecord)
        {
            return Helper.ListQueryEngineGridSnapshot(snapShotId, queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFitler, maxRecord);
        }

        /// <summary>
        /// Lists the columns.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public DataSet ListColumns(int queryViewId)
        {
            return Helper.ListColumns(queryViewId);
        }

        #endregion CustomGridFunctionality

        #region F27010 MiscAssessment

        #region GetRollYear
        /// <summary>
        /// F27010s the get roll year.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Integer</returns>
        public int F27010GetRollYear(int parcelId)
        {
            return Helper.F27010GetRollYear(parcelId);
        }
        #endregion GetRollYear

        #region Get Assessment Type
        /// <summary>
        /// F27010s the type of the get assessment.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public string F27010GetAssessmentType(int rollYear)
        {
            F27010MiscAssessmentData getAssessmentData = new F27010MiscAssessmentData();
            getAssessmentData = Helper.F27010GetAssessmentType(rollYear);
            return getAssessmentData.GetXml();
        }
        #endregion Get Assessment Type

        #region GetDistrict
        /// <summary>
        /// F27010s the get district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public string F27010GetDistrict(int parcelId, int madTypeId, int rollYear)
        {
            F27010MiscAssessmentData getDistrictData = new F27010MiscAssessmentData();
            getDistrictData = Helper.F27010GetDistrict(parcelId, madTypeId, rollYear);
            return getDistrictData.GetXml();
        }
        #endregion GetDistrict

        #region Check Default District
        /// <summary>
        /// F27010s the check default district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public int F27010CheckDefaultDistrict(int parcelId, int madTypeId, int rollYear)
        {
            return Helper.F27010CheckDefaultDistrict(parcelId, madTypeId, rollYear);
        }
        #endregion Check Default District

        #region Get ToolTip Message
        /// <summary>
        /// F27010s the get message.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <returns>DataSet</returns>
        public string F27010GetMessage(int parcelId, int madTypeId, int madDistrictId)
        {
            F27010MiscAssessmentData getMessageData = new F27010MiscAssessmentData();
            getMessageData = Helper.F27010GetMessage(parcelId, madTypeId, madDistrictId);
            return getMessageData.GetXml();
        }
        #endregion Get ToolTip Message

        #region GetMiscAssessment (MADType1)
        /// <summary>
        /// F27010s the get misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>DataSet</returns>
        public string F27010GetMiscData(int madDistrictId, int parcelId)
        {
            F27010MiscAssessmentData getMiscData = new F27010MiscAssessmentData();
            getMiscData = Helper.F27010GetMiscData(madDistrictId, parcelId);
            return getMiscData.GetXml();
        }
        #endregion GetMiscAssessment (MADType1)

        #region GetMiscAssessment (Other MADType)
        /// <summary>
        /// F27010s the get others misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>DataSet</returns>
        public string F27010GetOthersMiscData(int madDistrictId, int parcelId, string procedureName)
        {
            F27010MiscAssessmentData getMiscData = new F27010MiscAssessmentData();
            getMiscData = Helper.F27010GetOthersMiscData(madDistrictId, parcelId, procedureName);
            return getMiscData.GetXml();
        }
        #endregion GetMiscAssessment (Other MADType)

        #region GetDefaultMiscData

        /// <summary>
        /// F27010s the get default misc data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <returns>DataSet</returns>
        public string F27010GetDefaultMiscData(int parcelId, int madTypeId)
        {
            F27010MiscAssessmentData getMiscData = new F27010MiscAssessmentData();
            getMiscData = Helper.F27010GetDefaultMiscData(parcelId, madTypeId);
            return getMiscData.GetXml();
        }
        #endregion GetDefaultMiscData

        #region SaveMiscAssessment
        /// <summary>
        /// F27010_s the save misc assessment.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="miscType">Type of the misc.</param>
        /// <param name="madItem">The mad item.</param>
        /// <param name="miscItems">The misc items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F27010_SaveMiscAssessment(int parcelId, string miscType, string madItem, string miscItems, int userId)
        {
            return Helper.F27010_SaveMiscAssessment(parcelId, miscType, madItem, miscItems, userId);
        }
        #endregion SaveMiscAssessment

        #endregion F27010 MiscAssessment

        #region F84401 Signs Properties

        #region Get Signs Properties

        /// <summary>
        /// F84401_s the get signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>DataSet contains Signs Property</returns>
        public string F84401_GetSignsProperties(int featureId)
        {
            F84401SignsPropertyData signsPropertyData = new F84401SignsPropertyData();
            signsPropertyData = Helper.F84401_GetSignsProperties(featureId);
            return signsPropertyData.GetXml();
        }

        #endregion Get Signs Properties

        #region Save Signs Properties

        /// <summary>
        /// F84401_s the save signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="signsProperties">The signs properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F84401_SaveSignsProperties(int featureId, string signsProperties, int userId)
        {
            return Helper.F84401_SaveSignsProperties(featureId, signsProperties, userId);
        }

        #endregion Save Signs Properties

        #region Delete Signs Properties

        /// <summary>
        /// F84401_s the delete signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="userId">The user id.</param>
        public void F84401_DeleteSignsProperties(int featureId, int userId)
        {
            Helper.F84401_DeleteSignsProperties(featureId, userId);
        }

        #endregion Delete Signs Properties

        #endregion F84401 Signs Properties

        #region F29531 AssociationLink-LinkType

        /// <summary>
        /// F29531s the type of the association link.
        /// </summary>
        /// <returns></returns>
        public string F29531AssociationLinkType(int userid)
        {
            F29531AssciationLinkData associationlinktype = new F29531AssciationLinkData();
            associationlinktype = Helper.F29531AssociationLinkType(userid);
            return associationlinktype.GetXml();
        }

        /// <summary>
        /// F29530_s the fill association event grid.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        public string F29531_FillAssociationLinkGrid(int keyid, int formId)
        {
            F29531AssciationLinkData associationLink = new F29531AssciationLinkData();
            associationLink = Helper.F29531_FillAssociationLinkGrid(keyid, formId);
            return associationLink.GetXml();
        }

        /// <summary>
        /// Updates the association link details.
        /// </summary>
        /// <param name="associationDetails">The association details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void UpdateAssociationLinkDetails(string associationDetails, int userId)
        {
            Helper.UpdateAssociationLinkDetails(associationDetails, userId);
        }
        /// <summary>
        /// F29531_s the save association link.
        /// </summary>
        /// <param name="associationid">The associationid.</param>
        /// <param name="associationLinkItems">The association link items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29531_SaveAssociationLink(int associationid, string associationLinkItems, int userId)
        {
            return Helper.F29531_SaveAssociationLink(associationid, associationLinkItems, userId);
        }

        /// <summary>
        /// F29531_s the fill association link grid.
        /// </summary>
        /// <param name="cfgid">The cfgid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns></returns>
        public string F29531_GetLinkText(int cfgid, int keyid)
        {
            return Helper.F29531_GetLinkText(cfgid, keyid);
        }

        /// <summary>
        /// F29531_s the delete association link.
        /// </summary>
        /// <param name="associationId">The association id.</param>
        /// <param name="userId">The user id.</param>
        public void F29531_DeleteAssociationLink(int associationId, int userId)
        {
            Helper.F29531_DeleteAssociationLink(associationId, userId);
        }

        #endregion

        #region F29610 HoHExemptionDetails

        /// <summary>
        /// F29610_s the get ho H exemption details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public string F29610_GetHoHExemptionDetails(int eventid)
        {
            F29610HoHExemptionData getHoHExemptionData = new F29610HoHExemptionData();
            getHoHExemptionData = Helper.F29610_GetHoHExemptionDetails(eventid);
            return getHoHExemptionData.GetXml();
        }

        /// <summary>
        /// F29610_s the get calculation of ho H.
        /// </summary>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <param name="exemptionid">The exemptionid.</param>
        /// <returns></returns>
        public string F29610_GetCalculationOfHoH(int scheduleid, int exemptionid)
        {
            F29610HoHExemptionData getCalcHoHExemptionData = new F29610HoHExemptionData();
            getCalcHoHExemptionData = Helper.F29610_GetCalculationOfHoH(scheduleid, exemptionid);
            return getCalcHoHExemptionData.GetXml();
        }

        /// <summary>
        /// F29610_s the get owner percent.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <returns></returns>
        public string F29610_GetOwnerPercent(int ownerId, int scheduleid)
        {
            F29610HoHExemptionData getCalcHoHExemptionData = new F29610HoHExemptionData();
            getCalcHoHExemptionData = Helper.F29610_GetOwnerPercent(ownerId, scheduleid);
            return getCalcHoHExemptionData.GetXml();
        }

        /// <summary>
        /// F29610_s the save ho H exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="HoHItems">The ho H items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29610_SaveHoHExemptionDetails(int eventId, string HoHItems, int userId)
        {
            return Helper.F29610_SaveHoHExemptionDetails(eventId, HoHItems, userId);
        }

        #endregion

        #region F9610 QuickFind

        /// <summary>
        /// F9610s the quick find.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        public string F9610QuickFind(int form, string keyword)
        {
            F9610QuickFind quickfinditem = new F9610QuickFind();
            quickfinditem = Helper.F9610QuickFind(form, keyword);
            return quickfinditem.GetXml();
        }
        #endregion

        #region Testmethod


        public string TestMethod()
        {
            // Add Code here
            return "latha";
        }
        #endregion

        #region F9110MasterNameSearch

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>Returns MasterNameSearchData dataset</returns>
        public string F9110GetMasterNameSearch(string lastName, string firstName, string address)
        {
            F9110MasterNameSearchData masterNameSearch = new F9110MasterNameSearchData();
            masterNameSearch = Helper.F9110GetMasterNameSearch(lastName, firstName, address);
            return masterNameSearch.GetXml();
        }

        #endregion

        #region F1411ParcelStmtSearch

        /// <summary>
        /// Gets the Parcel Statement search.
        /// </summary>
        /// <param name="search Number">search Number.</param>
        /// <returns>Returns ParcelStmtSearchData dataset</returns>
        public string F1411ParcelStatementSearch(string searchNumber)
        {
            F1411ParcelStatementSearchData parcelStmtSearch = new F1411ParcelStatementSearchData();
            parcelStmtSearch = Helper.F1411ParcelStmtSearch(searchNumber);
            return parcelStmtSearch.GetXml();
        }


        #endregion F1411ParcelStmtSearch

        #region F29620Agland Application Details
        /// <summary>
        /// F29620_s the get agland application details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public string F29620_GetAglandApplicationDetails(int eventid)
        {
            F29620AglandApplicationData getaglandapplicationData = new F29620AglandApplicationData();
            getaglandapplicationData = Helper.F29620_GetAglandApplicationDetails(eventid);
            return getaglandapplicationData.GetXml();
        }

        /// <summary>
        /// F29620_s the save agland application details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29620_SaveAglandApplicationDetails(int eventId, int ownerId, int userId)
        {
            return Helper.F29620_SaveAglandApplicationDetails(eventId, ownerId, userId);
        }

        #endregion

        #region StateAssessedOwner
        /// <summary>
        /// F35075_s the get state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        public string F35075_GetStateAssessedOwnerDetails(int stateId)
        {
            F35075StateAssessedData stateAssessedOwnerData = new F35075StateAssessedData();
            stateAssessedOwnerData = Helper.F35075_GetStateAssessedOwnerDetails(stateId);
            return stateAssessedOwnerData.GetXml();
        }

        /// <summary>
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="assessedItems"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int F35075_SaveStateAssessedOwner(int? stateId, string assessedItems, int userId)
        {
            return Helper.F35075_SaveStateAssessed(stateId, assessedItems, userId);
        }

        /// <summary>
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="codeItems"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int F35076_SaveStateAssessedGrid(int? stateId, string codeItems, int userId)
        {
            return Helper.F35076_SaveStateAssessedGrid(stateId, codeItems, userId);
        }

        /// <summary>
        /// F35075_s the delete state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35075_DeleteStateAssessed(int stateId, int userId)
        {
            return Helper.F35075_DeleteStateAssessed(stateId, userId);
        }

        /// <summary>
        /// F35076_s the delete state assessed details.
        /// </summary>
        /// <param name="stateIemId">The state iem id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35076_DeleteStateAssessedDetails(int stateIemId, int userId)
        {
            return Helper.F35076_DeleteStateAssessedDetails(stateIemId, userId);
        }

        #endregion

        #region F2204 CopySchedule

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public string F25050GetParcelTypeDetails()
        {
            F2204CopyScheduleData getParcelData = new F2204CopyScheduleData();
            getParcelData = Helper.F25050GetParcelTypeDetails();
            return getParcelData.GetXml();
        }

        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public string F25050GetScheduleTypeDetails()
        {
            F2204CopyScheduleData getScheduleData = new F2204CopyScheduleData();
            getScheduleData = Helper.F25050GetScheduleTypeDetails();
            return getScheduleData.GetXml();
        }

        /// <summary>
        /// Creates the new parcel copy.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public int F2204CreateNewScheduleCopy(int scheduleId, string scheduleItems, int userId)
        {
            return Helper.F2204CreateNewScheduleCopy(scheduleId, scheduleItems, userId);
        }

        #endregion F2204 CopySchedule

        #region F24630 BoardOfEqualization

        /// <summary>
        /// F29630s the get board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <returns>DataSet</returns>
        public string F29630GetBoardOfEqualizationDetails(int boeId)
        {
            F29630BoardOfEqualizationData getBoardOfEqualizationData = new F29630BoardOfEqualizationData();
            getBoardOfEqualizationData = Helper.F29630GetBoardOfEqualizationDetails(boeId);
            return getBoardOfEqualizationData.GetXml();
        }

        /// <summary>
        /// F29620_s the F29630 save board of equalization details.
        /// </summary>
        /// <param name="boeElements">The boe elements.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        public void F29630SaveBoardOfEqualizationDetails(string boeElements, string boeValues, int userId)
        {
            Helper.F29630SaveBoardOfEqualizationDetails(boeElements, boeValues, userId);
        }

        /// <summary>
        /// F29630s the delete board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F29630DeleteBoardOfEqualizationDetails(int boeId, int userId)
        {
            Helper.F29630DeleteBoardOfEqualizationDetails(boeId, userId);
        }

        /// <summary>
        /// F29630s the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F29630PushBoardOfEqualizationDetails(int boeId, int userId)
        {
            Helper.F29630PushBoardOfEqualizationDetails(boeId, userId);
        }

        #endregion F24630 BoardOfEqualization

        #region F9041 Query View Description

        #region Get QueryDescription

        /// <summary>
        /// F9041s the get query description.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public string F9041GetQueryDescription(int queryViewId)
        {
            F9041QueryViewDescriptionData getQueryViewData = new F9041QueryViewDescriptionData();
            getQueryViewData = Helper.F9041GetQueryDescription(queryViewId);
            return getQueryViewData.GetXml();
        }

        #endregion Get QueryDescription

        #endregion F9041 Query View Description

        #region F82006 Contractor Management

        /// <summary>
        /// F82006_s the get contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <returns>contractManagementData</returns>
        public string F82006_GetContractorList(int contractorId)
        {
            F82006ContractManagementData contractManagementData = new F82006ContractManagementData();
            contractManagementData = Helper.F82006_GetContractorList(contractorId);
            return contractManagementData.GetXml();
        }

        /// <summary>
        /// F82006_s the save contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="contractorXml">The contractor XML.</param>
        /// <param name="contractorEmployeeXml">The contractor employee XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>errorId</returns>
        public int F82006_SaveContractorList(int? contractorId, string contractorXml, string contractorEmployeeXml, int userId)
        {
            return Helper.F82006_SaveContractorList(contractorId, contractorXml, contractorEmployeeXml, userId);
        }

        /// <summary>
        /// F82006_s the delete contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        public void F82006_DeleteContractorList(int contractorId, int userId)
        {
            Helper.F82006_DeleteContractorList(contractorId, userId);
        }

        /// <summary>
        /// F82006_s the delete contractor employee.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="userId">The user id.</param>
        public void F82006_DeleteContractorEmployee(int contractorId, int employeeId, int userId)
        {
            Helper.F82006_DeleteContractorEmployee(contractorId, employeeId, userId);
        }

        #endregion F82006 Contractor Management

        #region F9042 Analytics Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public string F9042_GetTemplate(int templateId)
        {
            F9042ExcelAnalyticsData excelAnalyticsData = new F9042ExcelAnalyticsData();
            excelAnalyticsData = Helper.F9042_GetTemplate(templateId);
            return excelAnalyticsData.GetXml();
        }

        /// <summary>
        /// F9042_s the list template.
        /// </summary>
        /// <param name="queryView">The query view.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public string F9042_ListTemplate(string queryView)
        {
            F9042ExcelAnalyticsData excelAnalyticsData = new F9042ExcelAnalyticsData();
            excelAnalyticsData = Helper.F9042_ListTemplate(queryView);
            return excelAnalyticsData.GetXml();
        }

        #endregion F9042 Analytics Template Selection

        /// <summary>
        /// Gets the snapshot details.
        /// </summary>
        /// <param name="FormNum">The form num.</param>
        /// <param name="UserId">The user id.</param>
        /// <returns></returns>
        public string GetSnapshotDetails(int FormNum, int UserId)
        {
            F9044SnapshotOperations snapshotDetails = new F9044SnapshotOperations();
            snapshotDetails = Helper.GetSnapshotDetails(FormNum, UserId);
            return snapshotDetails.GetXml();

        }

        /// <summary>
        /// F9044_GetSnapshotOperationCount.
        /// </summary>
        /// <param name="OperationId"></param>
        /// <param name="LOSnapshotId"></param>
        /// <param name="ROSnapshotId"></param>
        /// <param name="UserId"></param>
        /// <returns>GetSnapshotOperationCount</returns>
        public string GetSnapshotOperationCount(int OperationId, int LOSnapshotId, int ROSnapshotId, int UserId)
        {
            F9044SnapshotOperations snapshotOperationCount = new F9044SnapshotOperations();
            snapshotOperationCount = Helper.GetSnapshotOperationCount(OperationId, LOSnapshotId, ROSnapshotId, UserId);
            return snapshotOperationCount.GetXml();
        }

        /// <summary>
        /// F9044_insertSnapshotDetailst.
        /// </summary>
        /// <param name="OperationId"></param>
        /// <param name="LOSnapshotId"></param>
        /// <param name="ROSnapshotId"></param>
        /// <param name="RecordCount"></param>
        /// <param name="NewSnapshotName"></param>
        /// <param name="UserId"></param>
        public void insertSnapshotDetails(int OperationId, int LOSnapshotId, int ROSnapshotId, int RecordCount, string NewSnapshotName, int UserId)
        {
            Helper.insertSnapshotDetails(OperationId, LOSnapshotId, ROSnapshotId, RecordCount, NewSnapshotName, UserId);
        }

        #region F81003 Selection Catalog

        /// <summary>
        /// F81003_s the get selection catalog details.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <returns>selection catalog details</returns>
        public string F81003_GetSelectionCatalogDetails(int catalogId)
        {
            F81003SelectionCatalogData selectionCatalogData = new F81003SelectionCatalogData();
            selectionCatalogData = Helper.F81003_GetSelectionCatalogDetails(catalogId);
            return selectionCatalogData.GetXml();
        }

        /// <summary>
        /// F81003_s the list selection category.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>selection category details</returns>
        public string F81003_ListSelectionCategory(int userId)
        {
            F81003SelectionCatalogData selectionCatalogData = new F81003SelectionCatalogData();
            selectionCatalogData = Helper.F81003_ListSelectionCategory(userId);
            return selectionCatalogData.GetXml();
        }

        /// <summary>
        /// F81003_s the save selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <returns>key id.</returns>
        public int F81003_SaveSelectionCatalog(int? catalogId, string selectionItemsXml)
        {
            return Helper.F81003_SaveSelectionCatalog(catalogId, selectionItemsXml);
        }

        /// <summary>
        /// F81003_s the delete selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        public void F81003_DeleteSelectionCatalog(int catalogId)
        {
            Helper.F81003_DeleteSelectionCatalog(catalogId);
        }

        #endregion F81003 Selection Catalog

        #region F9510GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Dataset</returns>
        public string F9510GetWebFormXML(int form, int userId)
        {
            F95010GetWebFormXMLData form9510GetWebFormXMLData = new F95010GetWebFormXMLData();
            form9510GetWebFormXMLData = Helper.F9510GetWebFormXML(form, userId);
            return form9510GetWebFormXMLData.GetXml();
        }
        #endregion F9510GetWebFormXML

        #region F9075 List Template Selection

        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <param name="form"></param>
        /// <param name="userid"></param>
        /// <returns>F9075listtemplateData</returns>
        public string F9075_ListTemplate(int form, int userid)
        {
            F9075CommentTemplate listtemplateData = new F9075CommentTemplate();
            listtemplateData = Helper.F9075_ListTemplate(form, userid);
            return listtemplateData.GetXml();
        }

        #endregion F9075 List Template Selection

        #region F9076New Comment Template

        #region F9076 list Template Selection

        /// <summary>
        /// F9076_gets the template.
        /// </summary>
        /// <param name="templateid">The templateid.</param>
        /// <returns></returns>
        public string F9076_getTemplate(int templateid)
        {
            F9076NewCommentTemplateData gettemplateData = new F9076NewCommentTemplateData();
            gettemplateData = Helper.F9076_getTemplate(templateid);
            return gettemplateData.GetXml();
        }

        #endregion F9076 list Template Selection

        #region F9076 SaveNewCommentTemplate Selection

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="commentItemsXml">The comment items XML.</param>
        /// <param name="isOverwrite">The is overwrite.</param>
        /// <returns></returns>
        public int F9076SaveNewCommentTemplate(int? templateId, string commentItemsXml, int isOverwrite)
        {
            return Helper.F9076SaveNewCommentTemplate(templateId, commentItemsXml, isOverwrite);
        }

        #endregion F9076 SaveNewCommentTemplate Selection

        #region F9076 DeleteNewCommentTemplate Selection

        /// <summary>
        /// F9076_s the delete new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        public void F9076_DeleteNewCommentTemplate(int templateId)
        {
            Helper.F9076_DeleteNewCommentTemplate(templateId);
        }

        #endregion F9076 DeleteNewCommentTemplate Selection

        #endregion New Comment Template

        #region F29505CreateSubdivision

        /// <summary>
        /// F429505_s the list all comoboxes.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public string F429505_ListAllComoboxes(int eventId)
        {
            F29505CreateSubdivisionData listAllComboboxitems = new F29505CreateSubdivisionData();
            listAllComboboxitems = Helper.F429505_ListAllComoboxes(eventId);
            return listAllComboboxitems.GetXml();
        }

        /// <summary>
        /// F429505_s the list all LandCodes.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public string ListLandCodes(int nbhdid, int rollyear)
        {
            F29505CreateSubdivisionData LandCodeitems = new F29505CreateSubdivisionData();
            LandCodeitems = Helper.ListLandCodes(nbhdid, rollyear);
            return LandCodeitems.GetXml();
        }

        /// <summary>
        /// F29505_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public string F29505_GetBaseParcelValue(int eventId)
        {
            F29505CreateSubdivisionData subdivisionSplitDataSet = new F29505CreateSubdivisionData();
            subdivisionSplitDataSet = Helper.F29505_GetBaseParcelValue(eventId);
            return subdivisionSplitDataSet.GetXml();
        }

        /// <summary>
        /// F29505_s the create parcel.
        /// </summary>
        /// <param name="makeSubId">The make sub id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Return message</returns>
        public string F29505_CreateParcel(int makeSubId, int userId)
        {
            return Helper.F29505_CreateParcel(makeSubId, userId);
        }

        /// <summary>
        /// F29505_s the save division parcels.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="makeSubItemsXml">The make sub items XML.</param>
        /// <param name="makeSubParcelsXml">The make sub parcels XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29505_SaveDivisionParcels(int eventId, string makeSubItemsXml, string makeSubParcelsXml, int userId)
        {
            return Helper.F29505_SaveDivisionParcels(eventId, makeSubItemsXml, makeSubParcelsXml, userId);
        }

        /// <summary>
        /// F29505_s the save sub division.
        /// </summary>
        /// <param name="makeSubID">The make sub ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29505_SaveSubDivision(int makeSubID, int userId)
        {
            return Helper.F29505_SaveSubDivision(makeSubID, userId);
        }

        /// <summary>
        /// F29505_s the get land code.
        /// </summary>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="nbhdid">The nbhdid.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public string F29505_GetLandCode(int landType1, int landType2, int landType3, int nbhdid, int rollYear)
        {
            F29505CreateSubdivisionData getlandcodeDataSet = new F29505CreateSubdivisionData();
            getlandcodeDataSet = Helper.F29505_GetLandCode(landType1, landType2, landType3, nbhdid, rollYear);
            return getlandcodeDataSet.GetXml();
        }


        #endregion

        #region F9025ValidationControl

        #region F9025 FormValidationDetails Selection

        /// <summary>
        /// F9025s the form validation details.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>int</returns>
        public int F9025FormValidationDetails(int formid, int userid)
        {
            return Helper.F9025FormValidationDetails(formid, userid);
        }

        #endregion F9025 FormValidationDetails Selection

        #region F9025 SaveValidationDetails Selection

        /// <summary>
        /// F9025s the save validation details.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns>int</returns>
        public int F9025SaveValidationDetails(int formid, int userid, int keyid)
        {
            return Helper.F9025SaveValidationDetails(formid, userid, keyid);
        }

        #endregion F9025 SaveValidationDetails Selection

        #endregion F9025ValidationControl

        #region Selection

        /// <summary>
        /// F81004_s the get selection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>selection details xml string</returns>
        public string F81004_GetSelectionDetails(int eventId, int form)
        {
            return Helper.F81004_GetSelectionDetails(eventId, form).GetXml();
        }

        /// <summary>
        /// F81004_s the get selection catalog details.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>selection catalog xml string</returns>
        public string F81004_GetSelectionCatalogDetails(int categoryId)
        {
            F81004SelectionData selectionData = new F81004SelectionData();
            selectionData.GetSelectionCatalogDetails.Clear();
            selectionData.GetSelectionCatalogDetails.Merge(Helper.F81004_GetSelectionCatalogDetails(categoryId));
            return selectionData.GetXml();
        }

        /// <summary>
        /// F81004_s the save selection items.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>eventId</returns>
        public int F81004_SaveSelectionItems(int eventId, string selectionItemsXml, int userId)
        {
            return Helper.F81004_SaveSelectionItems(eventId, selectionItemsXml, userId);
        }

        #endregion Selection

        #region F24640 Frozen Value

        #region Get Frozen Value

        /// <summary>
        /// Gets the frozen value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Frozen value</returns>
        public string GetFrozenValue(int eventId)
        {
            F29640FrozenValueData frozenValueData = new F29640FrozenValueData();
            frozenValueData = Helper.GetFrozenValue(eventId);
            return frozenValueData.GetXml();
        }

        #endregion Get Frozen Value

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        /// <param name="frozenElements">The frozen elements.</param>
        /// <param name="userId">The user id.</param>
        public void SaveFrozenDetails(string frozenElements, int userId)
        {
            Helper.SaveFrozenDetails(frozenElements, userId);
        }

        #endregion Save Frozen Value

        #region Delete Frozen Value

        /// <summary>
        /// Deletes the frozen details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="frozenId">The frozen id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteFrozenDetails(int eventId, int frozenId, int userId)
        {
            Helper.DeleteFrozenDetails(eventId, frozenId, userId);
        }

        #endregion Delete Frozen Value

        #endregion F24640 Frozen Value

        #region F24650 Exemption

        #region Get Exemption Type

        /// <summary>
        /// Get Exemption Type
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>DataSet contains Exemption Types</returns>
        public string GetExemptionType(int eventId)
        {
            F29650ExemptionData exemptionData = new F29650ExemptionData();
            exemptionData = Helper.GetExemptionType(eventId);
            return exemptionData.GetXml();
        }

        #endregion Get Exemption Type

        #region Get Exemption

        /// <summary>
        /// Get Exemption Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>DataSet contains Exemption Details</returns>
        public string GetExemptionDetails(int eventId)
        {
            F29650ExemptionData exemptionData = new F29650ExemptionData();
            exemptionData = Helper.GetExemptionDetails(eventId);
            return exemptionData.GetXml();
        }

        #endregion Get Exemption

        #region Get Exemption Loss

        /// <summary>
        /// Get Exemption Loss
        /// </summary>
        /// <param name="lossValue">Loss</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns>Decimal</returns>
        public decimal GetExemptionLoss(decimal lossValue, decimal maxValue)
        {
            return Helper.GetExemptionLoss(lossValue, maxValue);
        }

        #endregion Get Exemption Loss

        #region Save Exemption

        /// <summary>
        /// Save Exemption Deatils
        /// </summary>
        /// <param name="exemptionElements">Exemption Details</param>
        /// <param name="userId">User ID</param>
        public void SaveExemptionDetails(string exemptionElements, int userId)
        {
            Helper.SaveExemptionDetails(exemptionElements, userId);
        }

        #endregion Save exemption

        #region Delete Exemption

        /// <summary>
        /// Delete Exemption Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="exemptionEventId">Exemption Event ID</param>
        /// <param name="userId">User ID</param>
        public void DeleteExemptionDetails(int eventId, int exemptionEventId, int userId)
        {
            Helper.DeleteExemptionDetails(eventId, exemptionEventId, userId);
        }

        #endregion Delete Exemption

        #endregion F24650 Exemption

        #region F35060 Schedule Item Code

        #region Get Schedule Item Code

        /// <summary>
        /// Gets the schedule item codes.
        /// </summary>
        /// <returns>DataSet contains Schedule Item Codes</returns>
        public string GetScheduleItemCodes()
        {
            F35060ScheduleItemCodeData scheduleItemCodeData = new F35060ScheduleItemCodeData();
            scheduleItemCodeData = Helper.GetScheduleItemCodes();
            return scheduleItemCodeData.GetXml();
        }

        #endregion Get Schedule Item Code

        #region Save Schedule Item Code

        /// <summary>
        /// Saves the schedule item codes.
        /// </summary>
        /// <param name="scheduleCodeElements">The schedule code elements.</param>
        /// <param name="userId">The user id.</param>
        public void SaveScheduleItemCodes(string scheduleCodeElements, int userId)
        {
            Helper.SaveScheduleItemCodes(scheduleCodeElements, userId);
        }

        #endregion Save Schedule Item Code

        #region Delete Schedule Item Code

        /// <summary>
        /// Deletes the schedule item codes.
        /// </summary>
        /// <param name="itemCodeId">The item code id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteScheduleItemCodes(string itemCodeId, int userId)
        {
            Helper.DeleteScheduleItemCodes(itemCodeId, userId);
        }

        #endregion Delete Schedule Item Code

        #endregion F35060 Schedule Item Code

        #region Calling WCF Trigger Test Method

        /// <summary>
        /// WCFTriggerTestMethod
        /// </summary>
        /// <returns>test string</returns>
        public string WCFTriggerTestMethod()
        {
            return "Test WCF Trigger Method";
        }

        #endregion Calling WCF Trigger Test Method

        #region F2409Review Status

        /// <summary>
        /// F2409_s the type of the reviewstatus inspection.
        /// </summary>
        /// <returns></returns>
        public string F2409_ReviewstatusInspectionType()
        {
            F2409ReviewStatusData inspectionType = new F2409ReviewStatusData();
            inspectionType = Helper.F2409_ReviewstatusInspectionType();
            return inspectionType.GetXml();
        }

        /// <summary>
        /// F2409_s the reviewstatus inspection by user.
        /// </summary>
        /// <returns></returns>
        public string F2409_ReviewstatusInspectionByUser(int applicationId)
        {
            F2409ReviewStatusData inspectionType = new F2409ReviewStatusData();
            inspectionType = Helper.F2409_ReviewstatusInspectionByUser(applicationId);
            return inspectionType.GetXml();
        }

        /// <summary>
        /// F2409_s the list reviewstatus.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public string F2409_ListReviewstatus(int parcelId)
        {
            F2409ReviewStatusData reviewStatus = new F2409ReviewStatusData();
            reviewStatus = Helper.F2409_ListReviewstatus(parcelId);
            return reviewStatus.GetXml();
        }

        /// <summary>
        /// F2409_ReviewStatusData
        /// </summary>
        /// <returns></returns>
        public string F2409_Reviewstatus()
        {
            F2409ReviewStatusData reviewStatuData = new F2409ReviewStatusData();
            reviewStatuData = Helper.F2409_Reviewstatus();
            return reviewStatuData.GetXml();
        }

        /// <summary>
        /// F2409s the update parcel review details.
        /// </summary>
        /// <param name="reviewXML">The review XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void F2409UpdateParcelReviewDetails(string reviewXML, int userId)
        {
            Helper.F2409UpdateParcelReviewDetails(reviewXML, userId);
        }

        #endregion

        #region F2205 Move Schedule

        /// <summary>
        /// F2205s the create schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isNewSchedule">if set to <c>true</c> [is new schedule].</param>
        /// <param name="scheduleHeaderItems">The schedule header items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for created schedule</returns>
        public int F2205CreateSchedule(int? scheduleId, bool isNewSchedule, string scheduleHeaderItems, string scheduleItems, int userId)
        {
            return Helper.F2205CreateSchedule(scheduleId, isNewSchedule, scheduleHeaderItems, scheduleItems, userId);
        }

        #endregion F2205 Move Schedule

        #region F35055 PPLine Items
        /// <summary>
        /// F35055_s the get PP line items details.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns>F35055_GetPPLineItemsDetails</returns>
        public string F35055_GetPPLineItemsDetails(int scheduleID)
        {
            F35055PPLineItemData ppLineItemDetailsData = new F35055PPLineItemData();
            ppLineItemDetailsData = Helper.F35055_GetPPLineItemsDetails(scheduleID);
            return ppLineItemDetailsData.GetXml();
        }

        /// <summary>
        /// F35055_s the get value calculation.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="ppDeprTableId">The pp depr table id.</param>
        /// <param name="originalValue">The original value.</param>
        /// <param name="trend">The trend.</param>
        /// <param name="year">The year.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns F35055PPLineItemData</returns>
        public string F35055_GetValueCalculation(int scheduleId, int ppDeprTableId, Int64 originalValue, int trend, Int16 year, Int16 rollYear)
        {
            F35055PPLineItemData ppLineItemDetailsData = new F35055PPLineItemData();
            ppLineItemDetailsData = Helper.F35055_GetValueCalculation(scheduleId, ppDeprTableId, originalValue, trend, year, rollYear);
            return ppLineItemDetailsData.GetXml();
        }

        /// <summary>
        /// F35055_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Integer</returns>
        public int F35055_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return Helper.F35055_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35055_s the update schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns Integer</returns>
        public int F35055_UpdateScheduleLineItem(int scheduleId, string scheduleItems, int userId, Int16 rollYear)
        {
            return Helper.F35055_UpdateScheduleLineItem(scheduleId, scheduleItems, userId, rollYear);
        }

        /// <summary>
        /// F35055_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Integer</returns>
        public int F35055_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return Helper.F35055_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
        }

        #endregion

        #region F36066 Trend

        #region Check Trend

        /// <summary>
        /// Checks the trend roll year.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Confirmation value</returns>
        public int CheckTrendRollYear(int? trendYearId, int rollYear)
        {
            return Helper.CheckTrendRollYear(trendYearId, rollYear);
        }

        #endregion Check Trend

        #region Get Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <returns>Trend Details</returns>
        public string GetTrendDetails(int trendYearId)
        {
            F36066TrendData getTrendData = new F36066TrendData();
            getTrendData = Helper.GetTrendDetails(trendYearId);
            return getTrendData.GetXml();
        }

        #endregion Check Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendYearItems">The trend year items.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for save</returns>
        public int SaveTrend(int? trendYearId, string trendYearItems, string trendItems, int userId)
        {
            return Helper.SaveTrend(trendYearId, trendYearItems, trendItems, userId);
        }

        #endregion Save Trend

        #region Delete Trend

        /// <summary>
        /// Deletes the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteTrend(int? trendYearId, string trendItems, int userId)
        {
            Helper.DeleteTrend(trendYearId, trendItems, userId);
        }

        #endregion Delete Trend

        #endregion F36066 Trend

        #region F35051 Schedule Line Items

        /// <summary>
        /// F35051_s the get schedule line item details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The schedule line items.</returns>
        public string F35051_GetScheduleLineItemDetails(int scheduleId)
        {
            F35051ScheduleLineItemsData scheduleLineItemData = new F35051ScheduleLineItemsData();
            scheduleLineItemData = Helper.F35051_GetScheduleLineItemDetails(scheduleId);
            return scheduleLineItemData.GetXml();
        }

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public int F35051_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return Helper.F35051_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public int F35051_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return Helper.F35051_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
        }

        /// <summary>
        /// F35051_s the get depr percentage.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="year">The year.</param>
        /// <returns>The schedule line items dataset.</returns>
        public string F35051_GetDeprPercentage(Int16 rollYear, int deprTableId, Int16 year)
        {
            F35051ScheduleLineItemsData scheduleLineItemData = new F35051ScheduleLineItemsData();
            scheduleLineItemData = Helper.F35051_GetDeprPercentage(rollYear, deprTableId, year);
            return scheduleLineItemData.GetXml();
        }

        #endregion F35051 Schedule Line Items

        #region F25055 Personal Property Header

        /// <summary>
        /// Gets the property header details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>Personal property header details</returns>
        public string GetPropertyHeaderDetails(int scheduleId)
        {
            F25055PropertyHeaderData personalPropertyData = new F25055PropertyHeaderData();
            personalPropertyData = Helper.GetPropertyHeaderDetails(scheduleId);
            return personalPropertyData.GetXml();
        }

        #endregion F25055 Personal Property Header

        #region F36065 Personal Property Depreciation

        #region Check Depreciation RollYear

        /// <summary>
        /// F36065_s the check depr roll year.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>confirmation value</returns>
        public int F36065_CheckDeprRollYear(int? deprYearId, int rollYear)
        {
            return Helper.F36065_CheckDeprRollYear(deprYearId, rollYear);
        }

        #endregion Check Depreciation RollYear

        #region Get Depreciation Details

        /// <summary>
        /// F36065_s the get depr details.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <returns>Dataset contains depreciation details</returns>
        public string F36065_GetDeprDetails(int deprYearId)
        {
            F36065PersonalDeprData getDepreciationData = new F36065PersonalDeprData();
            getDepreciationData = Helper.F36065_GetDeprDetails(deprYearId);
            return getDepreciationData.GetXml();
        }

        #endregion Get Depreciation Details

        #region Save Depreciation

        /// <summary>
        /// F36065_s the save depreciation.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="deprYearItems">The depr year items.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>confirmation value for save</returns>
        public int F36065_SaveDepreciation(int? deprYearId, string deprYearItems, string depreciationItems, int userId)
        {
            return Helper.F36065_SaveDepreciation(deprYearId, deprYearItems, depreciationItems, userId);
        }

        #endregion Save Depreciation

        #region Delete Depreciation
        /// <summary>
        /// F36065_s the delete depreciattion.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        public void F36065_DeleteDepreciattion(int? deprYearId, string depreciationItems, int userId)
        {
            Helper.F36065_DeleteDepreciattion(deprYearId, depreciationItems, userId);
        }
        #endregion Delete Depreciation

        #endregion F36065 Personal Property Depreciation

        #region F15020 Receipt Type

        /// <summary>
        /// F15020_s the type of the G get receipt.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Receipt Types</returns>
        public string F15020_GetReceiptTypes(int userId, short formId, int keyId)
        {
            F1070ReceiptTypeData receiptTypeData = new F1070ReceiptTypeData();
            receiptTypeData = Helper.F15020_GetReceiptTypes(userId, formId, keyId);
            return receiptTypeData.GetXml();
        }

        #endregion F15020 Receipt Type

        #region F1504 Copy Account
        /// <summary>
        /// F1504_s the get copy account sub fund.
        /// </summary>
        /// <returns></returns>
        public string F1504_GetCopyAccountSubFund()
        {
            F1504CopyAccountData copyAccountSubFund = new F1504CopyAccountData();
            copyAccountSubFund = Helper.F1504_GetCopyAccountSubFund();
            return copyAccountSubFund.GetXml();
        }

        /// <summary>
        /// F1504_s the get account detail.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        public string F1504_GetAccountDetail(int accountId)
        {
            F1504CopyAccountData accountDetail = new F1504CopyAccountData();
            accountDetail = Helper.F1504_GetAccountDetail(accountId);
            return accountDetail.GetXml();
        }

        /// <summary>
        /// F1504_s the save copy account details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="function">The function.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="accObject">The acc object.</param>
        /// <param name="line">The line.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public string F1504_SaveCopyAccountDetails(int rollYear, string subFund, string description, string function, string bars, string accObject, string line, string userId)
        {
            F1504CopyAccountData copyAccountDataSet = new F1504CopyAccountData();
            copyAccountDataSet = Helper.F1504_SaveCopyAccountDetails(rollYear, subFund, description, function, bars, accObject, line, userId);
            return copyAccountDataSet.GetXml();
        }

        #endregion

        #region F32012 Catalog

        /// <summary>
        /// F32012_s the get catalog data.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Catalog Data</returns>
        public string F32012_GetCatalogData(int valueSliceId)
        {
            F32012CatalogData getCamaSketchFeeData = new F32012CatalogData();
            getCamaSketchFeeData = Helper.F32012_GetCatalogData(valueSliceId);
            return getCamaSketchFeeData.GetXml();
        }

        /// <summary>
        /// F32012_s the save catalog.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="catalogData">The catalog data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation value for save</returns>
        public DataSet F32012_SaveCatalog(int objectId, string catalogData, int userId)
        {
            return Helper.F32012_SaveCatalog(objectId, catalogData, userId);
        }

        #endregion F32012 Caalog

        #region F3205 Aperx sketch

        #region Sketch File Path

        /// <summary>
        /// F3205 pcget Sketch FilePath.
        /// </summary>
        /// <param name="ParcelId">The Parcel id.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>getApexSketch Data</returns>
        public string F3205pcgetSketchFilePath(int parcelId, int userId)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            getApexSketchData = Helper.F3205pcgetSketchFilePath(parcelId, userId);
            return getApexSketchData.GetXml();
        }

        #endregion Sketch File Path

        #region SketchLinkList

        /// <summary>
        ///F3205 pcget SketchLinks Exist.
        /// </summary>
        public string F3205pcgetSketchLinksExist(int parcelId, int userId)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            getApexSketchData = Helper.F3205pcgetSketchLinksExist(parcelId, userId);
            return getApexSketchData.GetXml();
        }

        #endregion SketchLinkList

        #region sketchImagePath

        /// <summary>
        /// Saves the sketch Image Path.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>typed dataset</returns>
        public string F3205pcinsSketchImage(int parcelId, int userId, int pageCount)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            getApexSketchData = Helper.F3205pcinsSketchImage(parcelId, userId, pageCount);
            return getApexSketchData.GetXml();
        }
        #endregion sketchImagePath

        #region insert Apex Sketch

        /// <summary>
        /// insert Apex Sketch
        /// </summary>
        /// <param name="SketchDataXML">The SketchData XML.</param>
        /// <param name="ParcelId">The Parcel Id.</param>
        /// <param name="userId">The userId.</param>
        public void SaveApexSketch(string SketchDataXML, int parcelId, int userId)
        {
            Helper.SaveApexSketch(SketchDataXML, parcelId, userId);
        }

        #endregion Save Apex Sketch

        #region ReCalcValues

        /// <summary>
        ///ReCalculate RCN Values
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public string F3205_pcexeReCalcValues(int userId, int parcelId)
        {
            return Helper.F3205_pcexeReCalcValues(userId, parcelId);
        }


        #endregion ReCalcValues

        #endregion F3205 Apex Sketch


        #region F1403 ParcelSelection

        /// <summary>
        /// F1403_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1403ParcelSearch</returns>
        public string F1403_GetParcelType(int? parcelId)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            parcelSearchDataSet = Helper.F1403_GetParcelType(parcelId);
            return parcelSearchDataSet.GetXml();
        }

        /// <summary>
        /// F1403_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public string F1403_GetSearchResult(string parcelSearchXml)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            parcelSearchDataSet = Helper.F1403_GetSearchResult(parcelSearchXml);
            return parcelSearchDataSet.GetXml();
        }

        /// <summary>
        /// F1403_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public string F1403_GetSaleTrackingRollYear(int eventID)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            parcelSearchDataSet = Helper.F1403_GetSaleTrackingRollYear(eventID);
            return parcelSearchDataSet.GetXml();
        }
        #endregion

        #region F1404 Schedule Search
        /// <summary>
        /// </summary>
        /// <param name="ScheduleConditionXML"></param>
        /// <returns>DataSet</returns>
        public string F1404_ListScheduleSearch(string ScheduleConditionXML)
        {
            F1404ScheduleSelectionData schedulesearch = new F1404ScheduleSelectionData();
            schedulesearch = Helper.F1404_ListScheduleSearch(ScheduleConditionXML);
            return schedulesearch.GetXml();
        }

        /// <summary>
        /// F1404_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public string F1404_GetScheduleType(int? scheduleId)
        {
            F1404ScheduleSelectionData scheduleSearchDataSet = new F1404ScheduleSelectionData();
            scheduleSearchDataSet = Helper.F1404_GetScheduleType(scheduleId);
            return scheduleSearchDataSet.GetXml();
        }

        /// <summary>
        /// F1404_schedule the get schedule tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public string F1404_GetScheduleTrackingRollYear(int eventID)
        {
            F1404ScheduleSelectionData ScheduleDataSet = new F1404ScheduleSelectionData();
            ScheduleDataSet = Helper.F1404_GetScheduleTrackingRollYear(eventID);
            return ScheduleDataSet.GetXml();
        }

        #endregion

        #region F1405 State Search

        /// <summary>
        /// F1405_s the type of the get state.
        /// </summary>
        /// <param name="StateConditionXML"></param>
        /// <returns>DataSet</returns>
        public string F1405_ListStateSearch(string StateConditionXML)
        {
            F1405StateSelectionData statesearch = new F1405StateSelectionData();
            statesearch = Helper.F1405_ListStateSearch(StateConditionXML);
            return statesearch.GetXml();
        }

        /// <summary>
        /// F1405_s the type of the get state.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        //public string F1405_GetStateType(int? stateId)
        //{
        //    F1405StateSelectionData stateSearchDataSet = new F1405StateSelectionData();
        //    stateSearchDataSet = Helper.F1405_GetStateType(stateId);
        //    return stateSearchDataSet.GetXml();
        //}


        #endregion F4105 State Search

        #region F28000 Discretionary Details

        #region Get Discretionary Details

        /// <summary>
        /// Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Discretionary Details</returns>
        public string F28000_GetDiscretionaryDetails(int eventId)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            discretionaryData = Helper.F28000_GetDiscretionaryDetails(eventId);
            return discretionaryData.GetXml();
        }

        #endregion Get Discretionary Details

        #region Class Details

        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        public string F28000_GetClass(int stateId, int eventId)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            discretionaryData = Helper.F28000_GetClass(stateId, eventId);
            return discretionaryData.GetXml();
        }

        #endregion Class Details

        #region Exemption amount

        /// <summary>
        /// Exemption Amount
        /// </summary>
        /// <param name="rollYear">roll Year</param>
        /// <param name="exemptionYear">Exemption Year</param>
        /// <param name="subjectAmount">Subject Amount</param>
        /// <returns>Exemption Amount</returns>
        public string F28000_GetExemptionAmount(int rollYear, int exemptionYear, decimal subjectAmount)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            discretionaryData = Helper.F28000_GetExemptionAmount(rollYear, exemptionYear, subjectAmount);
            return discretionaryData.GetXml();
        }

        #endregion Exemption amount

        #region Save Discretionary Details

        /// <summary>
        /// Save Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML string</param>
        /// <param name="userId">User ID</param>
        /// <returns>Confirmation Value</returns>
        public int F28000_SaveDiscretionaryDetail(int eventId, int? discretionaryId, string discretionaryItems, int userId)
        {
            return Helper.F28000_SaveDiscretionaryDetail(eventId, discretionaryId, discretionaryItems, userId);
        }

        #endregion Save Discretionary Details

        #region Delete Discretionary Details

        /// <summary>
        /// Delete Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML String</param>
        /// <param name="userId">USer ID</param>
        public void F28000_DeletediscretionaryDetails(int? discretionaryId, string discretionaryItems, int userId)
        {
            Helper.F28000_DeletediscretionaryDetails(discretionaryId, discretionaryItems, userId);
        }

        #endregion Delete Discretionary Details

        #endregion F28000 Discretionary Details

        #region F28100 BOE

        #region Get BOE Details

        /// <summary>
        /// Get BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>BOE Details</returns>
        public string F28100_GetBOEDetails(int eventId)
        {
            F28100BOEData boeData = new F28100BOEData();
            boeData = Helper.F28100_GetBOEDetails(eventId);
            return boeData.GetXml();
        }

        #endregion Get BOE Details

        #region Get Total Amount

        /// <summary>
        /// Get Total amounts
        /// </summary>
        /// <param name="boeId">boe ID</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Total values</returns>
        public string F28100_GetTotalAmount(int boeId, int eventId, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            boeData = Helper.F28100_GetTotalAmount(boeId, eventId, assessedValues);
            return boeData.GetXml();
        }

        #endregion Get Total Amount

        #region Save BOE Details

        /// <summary>
        /// Save BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="boeItems">BOE Items</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key</returns>
        public int F28100_SaveBOEDetails(int eventId, string boeItems, string assessedValues, int userId)
        {
            return Helper.F28100_SaveBOEDetails(eventId, boeItems, assessedValues, userId);
        }

        #endregion Save BOE Details

        #region Delete BOE Details

        /// <summary>
        /// Delete BOE
        /// </summary>
        /// <param name="boeId">BOE ID</param>
        /// <param name="userId">The User ID</param>
        public void F28100_DeleteBOEDetails(int? boeId, int userId)
        {
            Helper.F28100_DeleteBOEDetails(boeId, userId);
        }

        #endregion Delete BOE Details

        #region Push Value
        /// <summary>
        /// F28100 the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F28100_PushBOEDetails(int boeId, int userId)
        {
            Helper.F28100_PushBOEDetails(boeId, userId);
        }
        #endregion Push Value

        #region Local Values

        /// <summary>
        /// Get Local Values
        /// </summary>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assesed Value</returns>
        public string F28100_GetLocalValues(string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            boeData = Helper.F28100_GetLocalValues(assessedValues);
            return boeData.GetXml();
        }

        #endregion Local Values

        #region County Values

        /// <summary>
        /// Get County Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Assessed Value</returns>
        public string F28100_GetCountyValues(bool isLocal, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            boeData = Helper.F28100_GetCountyValues(isLocal, assessedValues);
            return boeData.GetXml();
        }


        #endregion County Values

        #region State Values

        /// <summary>
        /// Get State Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="isCounty">Is Couny</param>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assessed Value</returns>
        public string F28100_GetStateValues(bool isLocal, bool isCounty, string assessedValues)
        {
            F28100BOEData boeData = new F28100BOEData();
            boeData = Helper.F28100_GetStateValues(isLocal, isCounty, assessedValues);
            return boeData.GetXml();
        }

        #endregion State Values

        #endregion F28100 BOE

        #region F29551 Parcel Sale Tracking

        /// <summary>
        /// DataSet to populate combo values
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>DataSet to populate combos</returns>
        public string F29551_GetParcelSaleComboDetails(int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            parcelSaleTracking = Helper.F29551_GetParcelSaleComboDetails(userId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// DataSet to Populate Grid and other controls
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User ID</param>
        /// <returns>DataSet to populate Controls</returns>
        public string F29551_GetParcelSaleTrackingDetails(int eventId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            parcelSaleTracking = Helper.F29551_GetParcelSaleTrackingDetails(eventId, userId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// Data to populate Owner Grid
        /// </summary>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="ownerId">The Owner Id</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Owner Details DataSet</returns>
        public string F29551_GetOwnerDetails(int? saleId, int? ownerId, int? parcelId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            parcelSaleTracking = Helper.F29551_GetOwnerDetails(saleId, ownerId, parcelId, userId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// Save ParcelSale Details
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="saleItems">saleItems</param>
        /// <param name="parcelItems">parcelItems</param>
        /// <param name="ownerItems">ownerItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value</returns>
        public int F29551_SaveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return Helper.F29551_SaveParcelSaleDetails(eventId, saleItems, parcelItems, ownerItems, userId);
        }


        /// <summary>
        /// Parcel and Owner details
        /// </summary>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="parcelCollection">Parcel Collections</param>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Parcel and Owner details</returns>
        public string F29551_GetParcelOwnerDetails(int? parcelId, string parcelCollection, int? saleId, int userId)
        {
            F29551ParcelSaleTrackingData parcelSaleTracking = new F29551ParcelSaleTrackingData();
            parcelSaleTracking = Helper.F29551_GetParcelOwnerDetails(parcelId, parcelCollection, saleId, userId);
            return parcelSaleTracking.GetXml();
        }

        /// <summary>
        /// Create Sale Versions
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="checkedParcels">Checked Parcels List</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_CreateSaleVersions(int eventId, int userId, string checkedParcels)
        {
            return Helper.F29551_CreateSaleVersions(eventId, userId, checkedParcels);
        }

        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_TransferOwnership(int eventId, int userId)
        {
            return Helper.F29551_TransferOwnership(eventId, userId);
        }

        /// <summary>
        /// F29551_s the update sale parcel.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message returned from SP</returns>
        public string F29551_UpdateSaleParcel(int eventId, int userId)
        {
            return Helper.F29551_UpdateSaleParcel(eventId, userId);
        }

        #endregion F29551 Parcel Sale Tracking

        #region F9045 Generic Search

        /// <summary>
        /// F9045s the get configuration.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <returns>Configuration Details</returns>
        public string F9045GetConfiguration(int genericSearchId)
        {
            F9045GenericSearchData configurationDetails = new F9045GenericSearchData();
            configurationDetails = Helper.F9045GetConfiguration(genericSearchId);
            return configurationDetails.GetXml();
        }

        /// <summary>
        /// F9045s the get search results.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Search Results</returns>
        public string F9045GetSearchResults(int genericSearchId, string searchString, int userId)
        {
            F9045GenericSearchData searchResult = new F9045GenericSearchData();
            searchResult = Helper.F9045GetSearchResults(genericSearchId, searchString, userId);
            return searchResult.GetXml();
        }

        #endregion F9045 Generic Search

        #region F3201 Sketch Link

        /// <summary>
        /// F3201_s the get sketch link data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Sketch Link Data</returns>
        public string F3201_GetSketchLinkData(int parcelId, int userId)
        {
            F3201SketchLinkData getSketchData = new F3201SketchLinkData();
            getSketchData = Helper.F3201_GetSketchLinkData(parcelId, userId);
            return getSketchData.GetXml();
        }

        /// <summary>
        /// F3201_s the save sketch link data.
        /// </summary>
        /// <param name="linkXML">The link XML.</param>
        /// <param name="parcelId">The parcel Id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Message</returns>
        public string F3201_SaveSketchLinkData(string linkXML, int parcelId, int userId)
        {
            return Helper.F3201_SaveSketchLinkData(linkXML, parcelId, userId);
        }

        #endregion F3201 Sketch Link

        /// <summary>
        /// F1500_s the get sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <returns></returns>
        public string F1500_GetSampleFormDetails(int FormID)
        {
            F1500SampleForm objSampleForm = new F1500SampleForm();
            objSampleForm = Helper.F1500_GetSampleFormDetails(FormID);
            return objSampleForm.GetXml();
        }


        /// <summary>
        /// Inserts the sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="SampleFormDetails">The sample form details.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public int InsertSampleFormDetails(int FormID, string SampleFormDetails, int UserID)
        {
            return Helper.InsertSampleFormDetails(FormID, SampleFormDetails, UserID);
        }

        /// <summary>
        /// Gets the application id.
        /// </summary>
        /// <returns></returns>
        public string GetApplicationId()
        {
            F1500SampleForm objSampleFormAppId = new F1500SampleForm();
            objSampleFormAppId = Helper.GetApplicationId();
            return objSampleFormAppId.GetXml();
        }

        /// <summary>
        /// Gets the menu id details.
        /// </summary>
        /// <returns></returns>
        public string GetMenuIdDetails()
        {
            F1500SampleForm objSampleFormMenuidDetails = new F1500SampleForm();
            objSampleFormMenuidDetails = Helper.GetMenuIdDetails();
            return objSampleFormMenuidDetails.GetXml();
        }


        /// <summary>
        /// F1500_s the delete form ID details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="GroupID">The group ID.</param>
        public void F1500_DeleteFormIDDetails(int FormID, int GroupID)
        {
            Helper.F1500_DeleteFomIDDetails(FormID, GroupID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int InsertFieldUseDetails(int KeyID, string KeyField, int UserID)
        {
            return Helper.InsertFieldUseDetails(KeyID, KeyField, UserID);
        }

        public DataSet ClassCode_RGB(string storedProcedureName)
        {
            return Helper.ClassCode_RGB(storedProcedureName);
        }

        /// <summary>
        /// F2200s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public string f2200GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear)
        {
            F2200EditScheduleData scheduleData = new F2200EditScheduleData();
           scheduleData=   Helper.f2200GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear);
           return scheduleData.GetXml();
          
        }

        #region F35080
        /// <summary>
        /// F35080_s the central assessed owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <returns></returns>
        public string F35080_CentralAssessedOwnerDetails(int centralId)
        {
            F35080CentralAssessedOwner OwnerObj = new F35080CentralAssessedOwner();
            OwnerObj = Helper.F35080_CentralAssessedOwnerDetails(centralId);
            return OwnerObj.GetXml();
        }

        /// <summary>
        /// F35080_s the property class combo.
        /// </summary>
        /// <returns></returns>
        public string F35080_PropertyClassCombo()
        {
            F35080CentralAssessedOwner ownerObj = new F35080CentralAssessedOwner();
            ownerObj = Helper.F35080_PropertyClassCombo();
            return ownerObj.GetXml();
        }

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void F35080_DeleteOwnerDetails(int centralId, int userId)
        {
            Helper.F35080_DeleteOwnerDetails(centralId, userId);
        }

        /// <summary>
        /// F35080_s the insert owner central details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="centralXML">The central XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35080_InsertOwnerCentralDetails(int? centralId, string centralXML, int userId)
        {
            return Helper.F35080_InsertOwnerCentralDetails(centralId, centralXML, userId);
        }
        /// <summary>
        /// F35080_s the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        public string F35080_OwnerDetails(int ownerId)
        {
            F35080CentralAssessedOwner OwnerObj = new F35080CentralAssessedOwner();
            OwnerObj = Helper.F35080_CentralAssessedOwnerDetails(ownerId);
            return OwnerObj.GetXml();
        } 
        #endregion

        #region F35081
        /// <summary>
        /// F35081_s the central assessed grid details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        public string F35081_CentralAssessedGridDetails(int CentralId)
        {
            F35081CentralAssessedGridData OwnerObj = new F35081CentralAssessedGridData();
            OwnerObj = Helper.F35081_CentralAssessedGridDetails(CentralId);
            return OwnerObj.GetXml();
        }

        /// <summary>
        /// F35081_s the central assessed rate details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="personalProperty">The personal property.</param>
        /// <param name="realProperty">The real property.</param>
        /// <returns></returns>
        public string F35081_CentralAssessedRateDetails(int subFundId, decimal personalProperty, decimal realProperty, string description, string centralXMLList)
        {
            F35081CentralAssessedGridData OwnerObj = new F35081CentralAssessedGridData();
            OwnerObj = Helper.F35081_CentralAssessedRateDetails(subFundId, personalProperty, realProperty,description,centralXMLList);
            return OwnerObj.GetXml();
        }
        /// <summary>
        /// F35081_s the insert owner assessed grid.
        /// </summary>
        /// <param name="centralXMLItems">The central XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void F35081_InsertOwnerAssessedGrid(string centralXMLItems, int centralId, int userId)
        {
           Helper.F35081_InsertOwnerAssessedGrid(centralXMLItems, centralId, userId);
        }
        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="removeXMLItems">The remove XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        public void F35081_DeleteOwnerGridDetails(string removeXMLItems, int centralId, int userId)
        {
            Helper.F35081_DeleteOwnerGridDetails(removeXMLItems, centralId, userId);
        } 
        #endregion

        #region F35085

        /// <summary>
        /// F35085_s the import type combo.
        /// </summary>
        /// <returns></returns>
        public string  F35085_ImportTypeCombo()
        {
            F35085CentrallyAssessedImportData ownerObj = new F35085CentrallyAssessedImportData();
            ownerObj = Helper.F35085_ImportTypeCombo();
            return ownerObj.GetXml();
        }
        /// <summary>
        /// F35085_s the central assessed owner details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public string F35085_CentralAssessedImportDetails(int importId)
        {
            F35085CentrallyAssessedImportData ownerObj = new F35085CentrallyAssessedImportData();
            ownerObj = Helper.F35085_CentralAssessedImportDetails(importId);
            return ownerObj.GetXml();
        }

        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F35085_DeletetemplateDetails(int importId, int userId)
        {
            Helper.F35085_DeletetemplateDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public DataSet F35085_InsertCentralTemplateDetails(int? importId, string importXML, int userId)
        {
           return Helper.F35085_InsertCentralTemplateDetails(importId, importXML, userId);
        }
        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F35085_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return Helper.F35085_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F35085_ExecuteCheckForErrors(int importId, int userId)
        {
            Helper.F35085_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F35085_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return Helper.F35085_CreateImportRecords(importId, userId, isProcess);
        }
        #endregion

        #region F16072

        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <returns></returns>
        public string F16072_GetMiscteplateDetails(int misctemplateId)
        {
            F16072MiscReceiptTemplate MiscObj = new F16072MiscReceiptTemplate();
            MiscObj = Helper.F16072_GetMiscteplateDetails(misctemplateId);
            return MiscObj.GetXml();            
        }

        /// <summary>
        /// F16072_s the save misc receipt template.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscHeaderDetails">The misc header details.</param>
        /// <param name="accountDetails">The account details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F16072_SaveMiscReceiptTemplate(int? misctemplateId, string miscHeaderDetails, string accountDetails, int userId)
        {
            return Helper.F16072_SaveMiscReceiptTemplate(misctemplateId, miscHeaderDetails, accountDetails, userId);
        }

        /// <summary>
        /// F16072_s the delete misctemplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="userId">The user id.</param>
        public void F16072_DeleteMisctemplateDetails(int misctemplateId, int userId)
        {
            Helper.F16072_DeleteMisctemplateDetails(misctemplateId, userId);
        }

        /// <summary>
        /// F16072_s the delete misc gridtemplate.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscIds">The misc ids.</param>
        /// <param name="userId">The user id.</param>
        public void F16072_DeleteMiscGridtemplate(int misctemplateId, string miscIds, int userId)
        {
            Helper. F16072_DeleteMiscGridtemplate(misctemplateId, miscIds, userId);
        }
        #endregion


        #region F16071


        /// <summary>
        /// F16071_s the get journal teplate details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns></returns>
         public string F16071_GetJournalTeplateDetails(int templateId)
         {
            F16071JournalEntryTemplateData TempObj = new F16071JournalEntryTemplateData();
            TempObj = Helper.F16071_GetJournalTeplateDetails(templateId);
            return TempObj.GetXml();   
         }

         /// <summary>
         /// F16071_s the save header template details.
         /// </summary>
         /// <param name="templateId">The template id.</param>
         /// <param name="rollYear">The roll year.</param>
         /// <param name="description">The description.</param>
         /// <param name="userId">The user id.</param>
         /// <returns></returns>
         public int F16071_SaveHeaderTemplateDetails(int? templateId, int rollYear, string description, int userId)
         {
             return Helper.F16071_SaveHeaderTemplateDetails(templateId, rollYear, description, userId);
         }

         /// <summary>
         /// F16071_s the save grid template details.
         /// </summary>
         /// <param name="templateId">The template id.</param>
         /// <param name="gridDetails">The grid details.</param>
         /// <param name="userId">The user id.</param>
         public void F16071_SaveGridTemplateDetails(int? templateId, string gridDetails, int userId)
         {
             Helper.F16071_SaveGridTemplateDetails(templateId, gridDetails, userId);
         }

         /// <summary>
         /// F16071_s the delete journal header details.
         /// </summary>
         /// <param name="templateId">The template id.</param>
         /// <param name="userId">The user id.</param>
         public void F16071_DeleteJournalHeaderDetails(int templateId, int userId)
         {
             Helper.F16071_DeleteJournalHeaderDetails(templateId, userId);
         }

         /// <summary>
         /// F16071_s the delete journal grid details.
         /// </summary>
         /// <param name="templateId">The template id.</param>
         /// <param name="gridDetails">The grid details.</param>
         /// <param name="userId">The user id.</param>
         public void F16071_DeleteJournalGridDetails(int templateId, string gridDetails, int userId)
         {
             Helper.F16071_DeleteJournalGridDetails(templateId, gridDetails, userId);
         }
        #endregion

        #region F19062

        public string  F14062_GridResultDetails(string ownerIds,string statementIds,string parcelIds,string scheduleIds,string stateIds,int userId)
        {
            F14062StatementPullListData TempObj = new F14062StatementPullListData();
            TempObj = Helper.F14062_GridResultDetails(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, userId);
            return TempObj.GetXml();
		}

        public string F14062_GetStatementPullListDetails()
        {
            F14062StatementPullListData TempObj = new F14062StatementPullListData();
            TempObj = Helper.F14062_GetStatementPullListDetails();
            return TempObj.GetXml();
        }
	   
	    public string F1407_GetPullListStatus()
        {
            F14062StatementPullListData TempObj = new F14062StatementPullListData();
            TempObj = Helper.F1407_GetPullListStatus();
            return TempObj.GetXml();

        }

        public string F1407_GetPullListType()
        {
            F14062StatementPullListData TempObj = new F14062StatementPullListData();
            TempObj = Helper.F1407_GetPullListType();
            return TempObj.GetXml();
        }
	   public void F14062_SaveGridDetails(string pullListItems, int userId)
       {
           Helper.F14062_SaveGridDetails(pullListItems, userId);
	   }
       public string F14062_DeleteStatementPullList(string pullListItems, int userId, bool isProcess)
       {
           return Helper.F14062_DeleteStatementPullList(pullListItems, userId, isProcess);
       }
        #endregion


        #region F11024

       /// <summary>
       /// F11024_s the get multiple journal template details.
       /// </summary>
       /// <param name="jetTemplateID">The jet template ID.</param>
       /// <returns></returns>
       public string F11024_GetMultipleJournalTemplateDetails(int jetTemplateID)
       {
           F11024MultiplejournalEntryData TempObj = new F11024MultiplejournalEntryData();
           TempObj = Helper.F11024_GetMultipleJournalTemplateDetails(jetTemplateID);
           return TempObj.GetXml();
       }

       /// <summary>
       /// F11024_s the save multiple journal template.
       /// </summary>
       /// <param name="transferDate">The transfer date.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="description">The description.</param>
       /// <param name="journalTemplateDetails">The journal template details.</param>
       public void F11024_SaveMultipleJournalTemplate(string transferDate, int userId, string description, string journalTemplateDetails)
       {
           Helper.F11024_SaveMultipleJournalTemplate(transferDate,userId,description,journalTemplateDetails);
       }

       /// <summary>
       /// F11024_s the search template details.
       /// </summary>
       /// <returns></returns>
       public string F11024_SearchTemplateDetails()
       {
           F11024MultiplejournalEntryData TempObj = new F11024MultiplejournalEntryData();
           TempObj = Helper.F11024_SearchTemplateDetails();
           return TempObj.GetXml();
       }

        #endregion

        #region F29555
       /// <summary>
       /// F29555_s the deedtype combo box.
       /// </summary>
       /// <returns></returns>
       public string F29555_DeedtypeComboBox()
       {
           F29555PersonalPropertySaleData TempObj = new F29555PersonalPropertySaleData();
           TempObj = Helper.F29555_DeedtypeComboBox();
           return TempObj.GetXml();
       }

       public string F29555_SaveTransferOwnership(int eventId, int userId)
       {
           return Helper.F29555_SaveTransferOwnership(eventId, userId);
       }
       /// <summary>
       /// F29555_s the get personal sales owner.
       /// </summary>
       /// <param name="pSsaleId">The p ssale id.</param>
       /// <param name="ownerId">The owner id.</param>
       /// <param name="scheduleId">The schedule id.</param>
       /// <param name="userid">The userid.</param>
       /// <param name="scheduleString">The schedule string.</param>
       /// <returns></returns>
       public string F29555_GetPersonalSalesOwner(int? pSsaleId, int? ownerId, int? scheduleId, int userid, string scheduleString)
       {
           F29555PersonalPropertySaleData TempObj = new F29555PersonalPropertySaleData();
           TempObj = Helper.F29555_GetPersonalSalesOwner(pSsaleId,ownerId,scheduleId,userid,scheduleString);
           return TempObj.GetXml();
       }
       /// <summary>
       /// F29555_s the get sales scheduleand owners.
       /// </summary>
       /// <param name="scheduleId">The schedule id.</param>
       /// <param name="scheduleIds">The schedule ids.</param>
       /// <param name="pSsaleId">The p ssale id.</param>
       /// <param name="userid">The userid.</param>
       /// <returns></returns>
        public string F29555_GetSalesScheduleandOwners(int? scheduleId, string scheduleIds, int? pSsaleId, int userid)
        {
            F29555PersonalPropertySaleData TempObj = new F29555PersonalPropertySaleData();
            TempObj = Helper.F29555_GetSalesScheduleandOwners(scheduleId, scheduleIds, pSsaleId, userid);
            return TempObj.GetXml();
        }

        /// <summary>
        /// F29555_s the schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public string F29555_ScheduleSaleTracking(int eventId, int userid)
        {
            F29555PersonalPropertySaleData TempObj = new F29555PersonalPropertySaleData();
            TempObj = Helper.F29555_ScheduleSaleTracking(eventId,userid);
            return TempObj.GetXml();
        }

        /// <summary>
        /// F29555_s the save sales owner.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        public void F29555_SaveSalesOwner(int pSaleId, string ownerDetails, int userId)
        {
            Helper.F29555_SaveSalesOwner(pSaleId, ownerDetails, userId);
        }

        /// <summary>
        /// F29555_s the save sales schedule.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        public void F29555_SaveSalesSchedule(int pSaleId, string scheduleItems, int userId)
        {
            Helper.F29555_SaveSalesSchedule(pSaleId, scheduleItems,userId);
        }

        /// <summary>
        /// F29555_s the save schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="pSaleItems">The p sale items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29555_SaveScheduleSaleTracking(int eventId, string pSaleItems, string scheduleItems, string ownerDetails, int userId)
        {
            return Helper.F29555_SaveScheduleSaleTracking(eventId, pSaleItems, scheduleItems, ownerDetails, userId);
        }
        #endregion


        #region F2201

        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public string F2201_GetPersonalPropertyDescription(string code)
        {
            F2201CentrallyAssessedSearchData TempObj = new F2201CentrallyAssessedSearchData();
            TempObj = Helper.F2201_GetPersonalPropertyDescription(code);
            return TempObj.GetXml();
        }

        /// <summary>
        /// F2201_s the get personal property search.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public string F2201_GetPersonalPropertySearch(string code, string description)
        {
            F2201CentrallyAssessedSearchData TempObj = new F2201CentrallyAssessedSearchData();
            TempObj = Helper.F2201_GetPersonalPropertySearch(code,description);
            return TempObj.GetXml();
        }
        #endregion

        #region F1406

        /// <summary>
        /// F2550_s the state of the get configured.
        /// </summary>
        /// <returns></returns>
        public  string F2550_GetConfiguredState()
        {
            F2550TaxRollCorrectionData TempObj = new F2550TaxRollCorrectionData();
            TempObj = Helper.F2550_GetConfiguredState();
            return TempObj.GetXml();
        }

        /// <summary>
        /// F1406_s the central assessed grid details.
        /// </summary>
        /// <param name="centralSearchXML">The central search XML.</param>
        /// <returns></returns>
        public string F1406_CentralAssessedGridDetails(string centralSearchXML)
        {
           
            F1406CentralAssessedSearchData TempObj = new F1406CentralAssessedSearchData();
            TempObj = Helper.F1406_CentralAssessedGridDetails(centralSearchXML);
            return TempObj.GetXml();
        }

        /// <summary>
        /// F1406_s the load propert class combo.
        /// </summary>
        /// <returns></returns>
        public string F1406_LoadPropertClassCombo()
        {
            F1406CentralAssessedSearchData TempObj = new F1406CentralAssessedSearchData();
            TempObj = Helper.F1406_LoadPropertClassCombo();
            return TempObj.GetXml();
        }
        #endregion

        #region F1203


        /// <summary>
        /// F1203s the load due date management.
        /// </summary>
        /// <returns></returns>
        public string F1203LoadDueDateManagement()
        {
            F1203DueDateManagementData TempObj = new F1203DueDateManagementData();
            TempObj = Helper.F1203LoadDueDateManagement();
            return TempObj.GetXml();
        }

        /// <summary>
        /// F1203_s the save due date management.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dueDateXML">The due date XML.</param>
        public void F1203_SaveDueDateManagement(int userId, string dueDateXML)
        {
            Helper.F1203_SaveDueDateManagement(userId, dueDateXML);
        }

        #endregion

        #region F29636

        /// <summary>
        /// F29636_s the get BOE details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public string F29636_GetBOEDetails(int eventId)
        {
            F29636BOEData TempObj = new F29636BOEData();
            TempObj = Helper.F29636_GetBOEDetails(eventId);
            return TempObj.GetXml();
        }

        /// <summary>
        /// F29636_s the BOE type details.
        /// </summary>
        /// <returns></returns>
        public string F29636_BOETypeDetails()
        {
            F29636BOEData TempObj = new F29636BOEData();
            TempObj = Helper.F29636_BOETypeDetails();
            return TempObj.GetXml();
        }


        /// <summary>
        /// F29636_s the save BOE details.
        /// </summary>
        /// <param name="boeElemenets">The boe elemenets.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        public void F29636_SaveBOEDetails(string boeElemenets, string boeValues, int userId)
        {
            Helper.F29636_SaveBOEDetails(boeElemenets, boeValues, userId);
        }


        /// <summary>
        /// F29636_s the push BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public string F29636_PushBOEDetails(int boeId, int userId)
        {
            return Helper.F29636_PushBOEDetails(boeId, userId);
        }

        /// <summary>
        /// F29636_s the delete BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public void F29636_DeleteBOEDetails(int boeId, int userId)
        {
            Helper.F29636_DeleteBOEDetails(boeId, userId);
        }
        #endregion

        #region F9105

        /// <summary>
        /// F9105_s the name of the execute copy.
        /// </summary>
        /// <param name="copyData">The copy data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  int F9105_ExecuteCopyName(string copyData, int userId)
        {
            return Helper.F9105_ExecuteCopyName(copyData, userId);
        }
        #endregion

        #region Permit Import Template

        #region Get

        /// <summary>
        /// Gets the Permit Import Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Permit Import Template Details</returns>
        public string GetPermitImportTemplate(int templateId)
        {
            F23200PermitImportTemplate permitImpotTemplateData = new F23200PermitImportTemplate();
            permitImpotTemplateData = Helper.GetPermitImportTemplate(templateId);
            return permitImpotTemplateData.GetXml();
        }

        #endregion

        //#region List permit Import Template

        ///// <summary>
        ///// Lists the permit import template.
        ///// </summary>
        ///// <returns>permit import template list</returns>
        //public string ListMortgageTemplate()
        //{
        //    MortgageImpotTemplateData mortgageImpotTemplateData = new MortgageImpotTemplateData();
        //    mortgageImpotTemplateData = Helper.ListMortgageTemplate();
        //    return mortgageImpotTemplateData.GetXml();
        //}

        //#endregion

        #region List PermitImportFileType

        /// <summary>
        /// Lists the type of the Permit import file.
        /// </summary>
        /// <returns>returns the dataset containing the Permit Import FileType</returns>
        public string ListPermitImportFileType()
        {
            F23200PermitImportTemplate permitImportData = new F23200PermitImportTemplate();
            permitImportData = Helper.ListPermitImportFileType();
            return permitImportData.GetXml();
        }

        #endregion

        #region Save Permit Import Template

        /// <summary>
        /// Saves the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// 
        public int SavePermitImportTemplate(int? templateId, string permitImportXML, int userId)
        {
           return Helper.SavePermitImportTemplate(templateId, permitImportXML, userId);
        }

        #endregion

        #region Delete Permit Template

        /// <summary>
        /// Deletes the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// returns dataset containing templateId and overrideStatus.
        /// </returns>
        public string DeletePermiTemplate(int templateId, int userId)
        {
            return Helper.DeletePermitTemplate(templateId, userId);
        }

        #endregion

        #endregion


        #region Income Source Details

        #region Get

        /// <summary>
        /// Gets the Income Source Details
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <returns>Permit Import Template Details</returns>
        public string GetIncomeSourceDetail(int IncomeSourceID)
        {
            F36090IncomeSourceData incomesourceData = new F36090IncomeSourceData();
            incomesourceData = Helper.GetIncomeSourceDetail(IncomeSourceID);
            return incomesourceData.GetXml();
        }

        #endregion

        #region List UnitTerms

        /// <summary>
        /// Lists the type of the unit terms.
        /// </summary>
        /// <returns>returns the dataset containing the unit terms</returns>
        public string ListUnitTerms()
        {
            F36090IncomeSourceData incomesourceImportData = new F36090IncomeSourceData();
            incomesourceImportData = Helper.ListUnitTerms();
            return incomesourceImportData.GetXml();
        }

        #endregion

        #region Save income source details

        /// <summary>
        /// Saves the income source detail.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSourceID id.</param>
        /// <param name="IncomeSourceItems">IncomeSource Items.</param>
        /// <param name="userId">The user id.</param>
        /// 
        public int SaveIncomeSourceDetails(int? IncomeSourceID, string IncomeSourceItems, int userId)
        {
            return Helper.SaveIncomeSourceDetails(IncomeSourceID, IncomeSourceItems, userId);
        }

        #endregion

        #region Delete Income Source Details

        /// <summary>
        /// Deletes the Income Source.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// returns dataset containing IncomeSourceID.
        /// </returns>
        public string DeleteIncomeSource(int IncomeSourceID, int userId)
        {
            return Helper.DeleteIncomeSource(IncomeSourceID, userId);
        }

        #endregion

        #endregion

        #region MAD Import Template

        #region Get

        /// <summary>
        /// Gets the MAD Import Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>MAD Import Template Details</returns>
        public string GetMADImportTemplate(int templateId)
        {
            F23300MADImportTemplate MADImpotTemplateData = new F23300MADImportTemplate();
            MADImpotTemplateData = Helper.GetMADImportTemplate(templateId);
            return MADImpotTemplateData.GetXml();
        }

        #endregion

        #region List MADImportFileType

        /// <summary>
        /// Lists the type of the MAD import file.
        /// </summary>
        /// <returns>returns the dataset containing the Permit Import FileType</returns>
        public string ListMADImportFileType()
        {
            F23300MADImportTemplate MADImportData = new F23300MADImportTemplate();
            MADImportData = Helper.ListMADImportFileType();
            return MADImportData.GetXml();
        }

        #endregion

        #region Save MAD Import Template

        /// <summary>
        /// Saves the MAD import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// 
        public int SaveMADImportTemplate(int? templateId, string madImportXML, int userId)
        {
            return Helper.SaveMADImportTemplate(templateId, madImportXML, userId);
        }

        #endregion

        #region Delete MAD Import Template

         //<summary>
         //Deletes the MAD Import template.
         //</summary>
         //<param name="templateId">The template id.</param>
         //<param name="userId">UserID</param>
         //<returns>
         //returns string containing message.
         //</returns>
        public string DeleteMADTemplate(int templateId, int userId)
        {
            return Helper.DeleteMADTemplate(templateId, userId);
        }

        #endregion

        #endregion


        #region F28210

        
        /// <summary>
        /// F28210_s the permit Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public string F28210_PermitImportDetails(int importId)
        {
            F28210PermitImport permitObj = new F28210PermitImport();
            permitObj = Helper.F28210_PermitImportDetails(importId);
            return permitObj.GetXml();
        }

        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28210_DeletetemplateDetails(int importId, int userId)
        {
            Helper.F28210_DeletePermitImportDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28210_InsertImportPermitDetails(int? importId, string importXML, int userId)
        {
            return Helper.F28210_InsertImportPermitDetails(importId, importXML, userId);
        }
        /// <summary>
        /// F28210_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28210_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return Helper.F28210_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28210_ExecuteCheckForErrors(int importId, int userId)
        {
            Helper.F28210_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28210_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return Helper.F28210_CreateImportRecords(importId, userId, isProcess);
        }
        #endregion

        #region F28310


        /// <summary>
        /// F28310_s the MAD Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public string F28310_MADImportDetails(int importId)
        {
            F28310MADImport MADObj = new F28310MADImport();
            MADObj = Helper.F28310_MADImportDetails(importId);
            return MADObj.GetXml();
        }

        #region List DistrictType

        /// <summary>
        /// Lists the type of the District Type file.
        /// </summary>
        /// <returns>returns the dataset containing the District FileType</returns>
        public string ListDistrictType()
        {
            F28310MADImport MADImportData = new F28310MADImport();
            MADImportData = Helper.ListDistrictType();
            return MADImportData.GetXml();
        }

        #endregion

        /// <summary>
        /// F28310_s the deleteMADImport details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28310_DeleteMADImportDetails(int importId, int userId)
        {
            Helper.F28310_DeleteMADImportDetails(importId, userId);
        }
        /// <summary>
        /// F28310_s the insert MAD Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28310_InsertImportMADDetails(int? importId, string importXML, int userId)
        {
            return Helper.F28310_InsertImportMADDetails(importId, importXML, userId);
        }
        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28310_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return Helper.F28310_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28310_ExecuteCheckForErrors(int importId, int userId)
        {
            Helper.F28310_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F28310_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28310_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return Helper.F28310_CreateImportRecords(importId, userId, isProcess);
        }
        #endregion

        #region F36091

        /// <summary>
        /// F36091_GetIncomeSources the execute import.
        /// </summary>
        /// <param name="valueSliceId">The valueSliceId id.</param>
        /// <returns></returns>
        public string F36091_GetIncomeSources(int valueSliceId)
        {
            F36091IncomeApproachData getIncomeSourceData = new F36091IncomeApproachData();
            getIncomeSourceData = Helper.F36091_GetIncomeSources(valueSliceId);
            return getIncomeSourceData.GetXml();
        }

        /// <summary>
        /// F36091_GetIncomeSources the execute import.
        /// </summary>
        /// <param name="valueSliceId">The valueSliceId id.</param>
        /// <returns></returns>
        public string F36091_GetIncomeApproachItemDetails(string IncomeApproachDetails)
        {
            F36091IncomeApproachData getIncomeApproachData = new F36091IncomeApproachData();
            getIncomeApproachData = Helper.F36091_GetIncomeApproachItemDetails(IncomeApproachDetails);
            return getIncomeApproachData.GetXml();
        }

        /// <summary>
        /// F36091_SaveIncomeSourceDetails the insert income Source details.
        /// </summary>
        /// <param name="valueSliceId">The valueSliceId id.</param>
        /// <param name="SourceGridDetails">The import XML.</param>
        /// /// <param name="IncomeApproachDetails">The IncomeApproachDetails XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void F36091_SaveIncomeSourceDetails(int valueSliceId, string SourceGridDetails, string IncomeApproachDetails, int userId)
        {
             Helper.F36091_SaveIncomeSourceDetails(valueSliceId, SourceGridDetails, IncomeApproachDetails, userId);
        }

        #region Get Source Code Details
        /// <summary>
        /// Get Source Code Details
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>String</returns>
        public string F36091_ListSourceDetails(int valueSliceId)
        {
            F36091IncomeApproachData getSourceCodeData = new F36091IncomeApproachData();
            getSourceCodeData = Helper.F36091_ListSourceDetails(valueSliceId);
            return getSourceCodeData.GetXml();
        }

        #endregion Get Source Code Details

        #region Get Approach Code Details
        /// <summary>
        /// Get Approach Code Details
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>String</returns>
        public string F36091_ListApproachValues(int incomeSourceID, decimal Units, decimal ContractPerUnit, out decimal contract, out decimal marketperunit, out decimal market)
        {
            F36091IncomeApproachData getApproachCodeData = new F36091IncomeApproachData();
            getApproachCodeData = Helper.F36091_ListApproachValues(incomeSourceID, Units, ContractPerUnit, out contract,out  marketperunit,out  market);
            return getApproachCodeData.GetXml();
        }

        #endregion Get Source Code Details


        #region F36091_DeleteIncomeSource

        /// <summary>
        /// F36091_s the delete income source.
        /// </summary>
        /// <param name="incomesourceIds">The incomesource Ids.</param>
        /// <param name="userId">The user id.</param>
        public void F36091_DeleteIncomeSource(string incomesourceIds, int userId)
        {
            Helper.F36091_DeleteIncomeSource(incomesourceIds, userId);
        }

        #endregion F36091_DeleteIncomeSource
        #endregion

        #region F1557

        /// <summary>
        /// F36091_GetIncomeSources the execute import.
        /// </summary>
        /// <param name="valueSliceId">The valueSliceId id.</param>
        /// <returns></returns>
        public string GetPaymentManagement(int valueSliceId)
        {
            F1557PayamentManagementData getPaymentManageData = new F1557PayamentManagementData();
            getPaymentManageData = Helper.GetPaymentManagement(valueSliceId);
            return getPaymentManageData.GetXml();
        }

        /// <summary>
        /// F1557 the Insert payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">User ID</param>
        public void F1557_InsertPayment(string receiptPayment, int userId)
        {
            Helper.F1557_InsertPayment(receiptPayment, userId);
        }

        /// <summary>
        /// F1557 the Update payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">User ID</param>
        public void F1557_UpdatePayment(string receiptPayment, int userId)
        {
            Helper.F1557_UpdatePayment(receiptPayment, userId);
        }

        /// <summary>
        /// F1557 the Delete payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">User ID</param>
        public void F1557_DeletePayment(string PaymentIDs, int userId)
        {
            Helper.F1557_DeletePaymentIds(PaymentIDs, userId);
        }

        #endregion

        #region Snapshot Import Template

        #region Get

        /// <summary>
        /// Gets the Snapshot Import Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Snapshot Import Template Details</returns>
        public string GetSnapshotImportTemplate(int templateId)
        {
            F23500SnapshotTemplate SnapshotImpotTemplateData = new F23500SnapshotTemplate();
            SnapshotImpotTemplateData = Helper.GetSnapshotImportTemplate(templateId);
            return SnapshotImpotTemplateData.GetXml();
        }

        #endregion

        #region List SnapshotImportFileType

        /// <summary>
        /// Lists the type of the Snapshot import file.
        /// </summary>
        /// <returns>returns the dataset containing the Snapshot Import FileType</returns>
        public string ListSnapshotImportFileType()
        {
            F23500SnapshotTemplate SnapshotImportData = new F23500SnapshotTemplate();
            SnapshotImportData = Helper.ListSnapshotImportFileType();
            return SnapshotImportData.GetXml();
        }

        #endregion

        #region Save Snapshot Import Template

        /// <summary>
        /// Saves the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// 
        public int SaveSnapshotImportTemplate(int? templateId, string SnapshotImportXML, int userId)
        {
            return Helper.SaveSnapshotImportTemplate(templateId, SnapshotImportXML, userId);
        }

        #endregion

        #region Delete Snapshot Import Template

        //<summary>
        //Deletes the Snapshot Import template.
        //</summary>
        //<param name="templateId">The template id.</param>
        //<param name="userId">UserID</param>
        //<returns>
        //returns string containing message.
        //</returns>
        public string DeleteSnapshotTemplate(int templateId, int userId)
        {
            return Helper.DeleteSnapshotTemplate(templateId, userId);
        }

        #endregion

        #endregion Snapshot Import Template

        #region F28510


        /// <summary>
        /// F28510_s the Snapshot Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public string F28510_SnapshotImportDetails(int importId)
        {
            F23510SnapshotImport snapshotObj = new F23510SnapshotImport();
            snapshotObj = Helper.F28510_SnapshotImportDetails(importId);
            return snapshotObj.GetXml();
        }

        /// <summary>
        /// F28510_s the delete import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28510_DeleteSnapshotImportDetails(int importId, int userId)
        {
            Helper.F28510_DeleteSnapshotImportDetails(importId, userId);
        }
        /// <summary>
        /// F28510_s the insert Snapshot import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F28510_InsertImportSnapshotDetails(int? importId, string importXML, int userId)
        {
            return Helper.F28510_InsertImportSnapshotDetails(importId, importXML, userId);
        }
        /// <summary>
        /// F28510_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28510_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return Helper.F28510_ExecuteImport(importId, importXML, userId, isProcess);
        }
        /// <summary>
        /// F28510_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public void F28510_ExecuteCheckForErrors(int importId, int userId)
        {
            Helper.F28510_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F28510 the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public string F28510_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return Helper.F28510_CreateImportRecords(importId, userId, isProcess);
        }


        #endregion

        #region Snapshot Import Template Selection

        /// <summary>
        /// Gets the Snapshot Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Snapshot Import Template Details.</returns>
        public string GetSnapshotImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            ListSnapshotImportTemplateData snapshotImportTemplateSelectData = new ListSnapshotImportTemplateData();
            snapshotImportTemplateSelectData = Helper.GetSnapshotImportTemplateDetails(TemplateName, Description, FileType);
            return snapshotImportTemplateSelectData.GetXml();
        }

        #endregion

    }
}