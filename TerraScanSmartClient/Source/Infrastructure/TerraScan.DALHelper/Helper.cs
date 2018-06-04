// -------------------------------------------------------------------------------------------
// <copyright file="Helper.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access database</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
[assembly: System.CLSCompliant(false)]
namespace TerraScan.DalHelper
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.Dal;
    using System.Data;
    using TerraScan.BusinessEntities;
    #endregion Namespace

    /// <summary>
    /// Helper class for Data Access Layer
    /// </summary>
    public class Helper
    {
        #region Real Estate

        #region Query

        #region List Query

        /// <summary>
        /// Lists the queries.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <returns> The dataset containing the list of queries.</returns>
        public static DataSet ListQuery(int formId, int userId)
        {
            return QueryComp.ListQuery(formId, userId);
        }

        #endregion List Query

        #region List Sort Query

        /// <summary>
        /// Lists the Sorted query.
        /// </summary>
        /// <param name="savedQueryId"> The query Id of the query saved.</param>
        /// <param name="orderByCondition"> The order by condition.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The dataset containing sorted order of the query result.</returns>
        public static DataSet ListSortQuery(int savedQueryId, string orderByCondition, int formId)
        {
            return QueryComp.ListSortQuery(savedQueryId, orderByCondition, formId);
        }

        #endregion List Sort Query

        #region Execute Query

        /// <summary>
        /// Method to exceute a sql query
        /// </summary>
        /// <param name="whereCondition"> The whereCondition to be applied on query.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The Typed datatset having the exceuted query results.</returns>
        public static QueryData ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            return QueryComp.ExecuteQuery(whereCondition, orderByCondition, formId);
        }

        #endregion  Execute Query

        #region Save Query

        /// <summary>
        /// Method to save query
        /// </summary>
        /// <param name="savedQueryName"> Name of the saved query.</param>
        /// <param name="formId">  The form id  of the form being used.</param>
        /// <param name="savedQueryNote"> Note of the saved query.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <param name="savedQueryDate"> Date of the saved query.</param>
        /// <param name="savedQuery"> The query to be saved.</param>
        /// <param name="whereCondn"> The where condition in the query.</param>
        /// <param name="canOverride"> Flag to specify override or not.</param>
        /// <returns> The datset containing the saved query details.</returns>
        public static DataSet SaveQuery(string savedQueryName, int formId, string savedQueryNote, int userId, DateTime savedQueryDate, string savedQuery, string whereCondn, bool canOverride)
        {
            return RealEstateComp.SaveQuery(savedQueryName, formId, savedQueryNote, userId, savedQueryDate, savedQuery, whereCondn, canOverride);
        }

        #endregion Save Query

        #region Get Query Result

        /// <summary>
        /// Fetches the query result for the query id passed.
        /// </summary>
        /// <param name="queryId"> The query Id of the query to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The typed dataset containing the query results.</returns>
        public static QueryData GetQueryResult(int queryId, string orderByCondn)
        {
            return QueryComp.GetQueryResult(queryId, orderByCondn);
        }

        #endregion Get Query Result

        #region Check Query Exist

        /// <summary>
        /// Checks the query exist.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="savedQueryName"> Name of the saved query.</param>
        /// <returns> The status indicating the presence of the query.</returns>
        public static int CheckQueryExist(int formId, string savedQueryName)
        {
            return QueryComp.CheckQueryExist(formId, savedQueryName);
        }

        #endregion Check Query Exist

        #region Delete Query

        /// <summary>
        /// Deletes the query with the specified query id.
        /// </summary>
        /// <param name="queryId"> The query Id of the query to be deleted.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteQuery(int queryId, int userId)
        {
            RealEstateComp.DeleteQuery(queryId, userId);
        }

        #endregion Delete Query

        #endregion Query

        #region SnapShot

        #region Check SnapShot Exist

        /// <summary>
        /// Checks the snap shot exist.
        /// </summary>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedSnapShotName">Name of the saved snap shot.</param>
        /// <returns>True if snapshot name exist and False if not exist.</returns>
        public static int CheckSnapShotExist(int formId, string savedSnapShotName)
        {
            return QueryComp.CheckSnapShotExist(formId, savedSnapShotName);
        }

        #endregion Check SnapShot Exist

        #region Get SnapShot Result

        /// <summary>
        /// Get the result of the snapshot.
        /// </summary>
        /// <param name="snapShotId"> The snapshot Id of the snapshot to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The Typed datset containing the snapshot results.</returns>
        public static QueryData GetSnapShotResult(int snapShotId, string orderByCondn)
        {
            return QueryComp.GetSnapShotResult(snapShotId, orderByCondn);
        }

        #endregion Get SnapShot Result

        #region Save SnapShot

        /// <summary>
        /// Method to save the snapshot
        /// </summary>
        /// <param name="snapshotName"> Name of the snapshot to be named.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="snapshotNote"> Note of the snapshot.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <param name="snapshotDate"> Date of the saved snapshot.</param>
        /// <param name="snapshotQuery"> Query of the snapshot.</param>
        /// <param name="whereCondn"> The where condition of the query.</param>
        /// <param name="keyIds"> Reciept or statement information in xml format.</param>
        /// <param name="canOverride"> Flag to specify override or not.</param>
        /// <returns> The datset containing the saved snapshot details.</returns>
        public static DataSet SaveSnapShot(string snapshotName, int formId, string snapshotNote, int userId, DateTime snapshotDate, string snapshotQuery, string whereCondn, string keyIds, bool canOverride)
        {
            return RealEstateComp.SaveSnapShot(snapshotName, formId, snapshotNote, userId, snapshotDate, snapshotQuery, whereCondn, keyIds, canOverride);
        }

        #endregion Save SnapShot

        #region List SnapShot

        /// <summary>
        /// Lists the snapshots.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The datset containing the list of the snapshots.</returns>
        public static DataSet ListSnapShot(int formId)
        {
            return RealEstateComp.ListSnapShot(formId);
        }

        #endregion List SnapShot

        #region List Sort SnapShot

        /// <summary>
        /// Lists the Sorted SnapShot.
        /// </summary>
        /// <param name="snapShotId"> The snapshot Id of the snapshot executed.</param>
        /// <param name="orderByCondition"> The order by condition.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The dataset containing sorted order of the snapshot result.</returns>
        public static DataSet ListSortSnapShot(int snapShotId, string orderByCondition, int formId)
        {
            return QueryComp.ListSortSnapShot(snapShotId, orderByCondition, formId);
        }

        #endregion List Sort SnapShot

        #region Delete SnapShot

        /// <summary>
        /// Deletes the snapshot with the specified snapshot id.
        /// </summary>
        /// <param name="snapshotId"> The snapshot Id of the snapshot to be deleted.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteSnapShot(int snapshotId, int userId)
        {
            RealEstateComp.DeleteSnapSnot(snapshotId, userId);
        }

        #endregion Delete SnapShot

        #region Execute Snapshot

        /// <summary>
        /// Method to apply filter on snapshot
        /// </summary>
        /// <param name="snapshotId"> The id used to retrieve snapshotitems to which filter applied</param>
        /// <param name="whereCondition">wherecondition used to query snapshotitems</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>The Typed Dataset</returns>
        public static QueryData ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            return QueryComp.ExecuteSnapshot(snapshotId, whereCondition, orderByCondition, formId);
        }

        #endregion

        #endregion SnapShot

        #region Real Estate Statements

        #region Get Real Estate Statement Count

        /// <summary>
        /// Gets the real estate statementcount
        /// </summary>
        /// <returns> The count of statements.</returns>
        public static int GetRealEstateStatementCount()
        {
            return RealEstateComp.GetRealEstateStatementCount();
        }

        #endregion Get Real Estate Statement Count

        #region Get Real Estate Statement Ids

        /// <summary>
        /// Gets the real estate statement Id's
        /// </summary>
        /// <param name="orderField"> The orderbycolumn name.</param>
        /// <param name="orderBy"> The orderby direction.</param>
        /// <returns> The typed dataset containing the list of real estate statementids.</returns>
        public static RealEstateData GetRealEstateStatementIds(string orderField, string orderBy)
        {
            return RealEstateComp.GetRealEstateStatementIds(orderField, orderBy);
        }

        #endregion Get Real Estate Statement Ids

        #region Get Real Estate Statement

        /// <summary>
        /// Gets the real estate statement based on the statement id
        /// </summary>
        /// <param name="statementId"> The statement id of the statement to be fetched.</param>
        /// <returns> The typed dataset containing the statement information of the statementid.</returns>
        public static RealEstateData GetRealEstateStatement(int statementId)
        {
            return RealEstateComp.GetRealEstateStatement(statementId);
        }

        #endregion

        #endregion Real Estate Statements

        #endregion Real Estate

        #region Comments

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="userId"> The user id of the user logged in.</param>
        /// <returns> The Typed dataset containing the comments.</returns>
        public static CommentsData GetComments(int keyId, int formId, int userId)
        {
            ////return CommentsComp.GetComments(keyId, formId, userId);
            return CommentsComp.GetComments(keyId, formId, userId);
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>The dataset containing the comment and priority.</returns>
        public static CommentsData F9075_GetComment(int keyId, int formId)
        {
            return CommentsComp.F9075_GetComment(keyId, formId);
        }

        /// <summary>
        /// Saves the comments.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentDate">The comment date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="printOnReceipt">The print on receipt.</param>
        /// <param name="publicComment">The public comment.</param>
        /// <param name="priorityStatus">The priority status.</param>
        /// <param name="isroll">Roll</param>
        /// <param name="commentPriorityId">commentPriorityId</param>
        public static void SaveComments(int commentId, int formId, int keyId, DateTime commentDate, int userId, string comments, int printOnReceipt, int publicComment, int priorityStatus, int isroll, int commentPriorityId)
        {
            CommentsComp.SaveComments(commentId, formId, keyId, commentDate, userId, comments, printOnReceipt, publicComment, priorityStatus, isroll, commentPriorityId);
        }

        /// <summary>
        /// Delete the comment based on the commentid, formid and keyid.
        /// </summary>
        /// <param name="keyId"> Reciept or statement information in xml format.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <param name="commentId"> The commentid of the comment to be deletd.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteComments(int keyId, int formId, int commentId, int userId)
        {
            CommentsComp.DeleteComments(keyId, formId, commentId, userId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The Typed Dataset having count of comments.</returns>
        public static CommentsData GetCommentsCount(int formId, int keyId, int userId)
        {
            return CommentsComp.GetCommentsCount(formId, keyId, userId);
        }

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the comments.</returns>
        public static CommentsData GetConfigDetails(string configName)
        {
            return CommentsComp.GetConfigDetails(configName);
        }

        /// <summary>
        /// For Testing Purpose added this method. later stage it should be removed.
        /// </summary>
        /// <param name="msversionId">The msversion id.</param>
        /// <returns>Connection string.</returns>
        public static CommonData GetConnectionString(int msversionId)
        {
            return CommentsComp.GetConnectionString(msversionId);
        }

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
        public static int GetAttachmentCount(int formId, int receiptId, int userId)
        {
            return AttachmentsComp.GetAttachmentCount(formId, receiptId, userId);
        }

        #endregion GetAttachmentCount

        #region GetAttachmentItems

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The typed dataset containing the attachment items.</returns>
        public static AttachmentsData GetAttachmentItems(int formId, int keyId, int userId)
        {
            return AttachmentsComp.GetAttachmentItems(formId, keyId, userId);
        }

        #endregion GetAttachmentItems

        #region GetAttachementFunctionName

        /// <summary>
        /// Get the attachment function name.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>
        /// The typed dataset containing the attachment function name.
        /// </returns>
        public static AttachmentsData GetAttachementFunctionName(int formId)
        {
            return AttachmentsComp.GetAttachementFunctionName(formId);
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
        /// <param name="sourceConfig">The config path</param>
        /// <returns>File path details</returns>
        public static AttachmentsData SaveAttachments(int? fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isroll, int linktype, string aurl, int pfileid, string sourceConfig)
        {
            return AttachmentsComp.SaveAttachments(fileId, extension, formId, keyId, fileTypeId, source, primary, description, eventDate, userId, publicValue, isroll, linktype, aurl, pfileid, sourceConfig);
        }

        #endregion SaveAttachments

        #region DeleteAttachments

        /// <summary>
        /// Deletes the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteAttachments(int fileId, int userId)
        {
            AttachmentsComp.DeleteAttachments(fileId, userId);
        }

        #endregion DeleteAttachments

        #region GetProgramId

        /// <summary>
        /// GetProgramId
        /// </summary>
        /// <param name="fileTypeId">The file type id.</param>
        /// <returns>
        /// The dataset containing the attachment file type.
        /// </returns>
        public static AttachmentsData GetProgramId(int fileTypeId)
        {
            return AttachmentsComp.GetProgramId(fileTypeId);
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
        /// <returns> The dataset containing the path of the file.</returns>
        public static AttachmentsData GetFilePath(string source, int formId, int keyId, string extension, int userId)
        {
            return AttachmentsComp.GetFilePath(source, formId, keyId, extension, userId);
        }

        /// <summary>
        /// Gets the original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <returns>FilePath</returns>
        public static string F9005_GetOriginalFilePath(int fileId, int userId)
        {
            return AttachmentsComp.F9005_GetOriginalFilePath(fileId, userId);
        }

        #endregion GetFilePath

        #region Generate Thumbnail

        /// <summary>
        /// Create Thumbnails 
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">UserID</param>
        public static void GenerateThumbnail(int? fileId, int userId, string fileIdXml)
        {
            AttachmentsComp.GenerateThumbnail(fileId, userId, fileIdXml);
        }

        #endregion Generate Thumbnail

        #endregion Attachments

        #region Receipt Engine

        #region ListHistoryGrid

        /// <summary>
        /// Lists the history information of the statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>
        /// The Typed dataset containing the history information of the statementid.
        /// </returns>
        public static ReceiptEngineData ListHistoryGrid(int statementId)
        {
            return ReceiptEngineComp.ListHistoryGrid(statementId);
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// Get a cecipt detials.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the reciept.
        /// </returns>
        public static ReceiptEngineData GetReceiptDetails(int receiptId)
        {
            return ReceiptEngineComp.GetReceiptDetails(receiptId);
        }

        #endregion GetReceiptDetails

        #region ListTenderType

        /// <summary>
        /// Lists the tender type.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>
        /// The Typed dataset containing the types of tender.
        /// </returns>
        public static ReceiptEngineData ListTenderType(bool allowOverUnder)
        {
            return ReceiptEngineComp.ListTenderType(allowOverUnder);
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
        public static string GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return ReceiptEngineComp.GetValidReceiptTest(statementId, receiptDate);
        }

        #endregion GetValidReceiptTest

        #region SaveReceipt

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// The return value specifying status of the save action.
        /// </returns>
        public static ReceiptEngineData SaveReceipt(string receiptItems, string paymentItems, int userId)
        {
            return ReceiptEngineComp.SaveReceipt(receiptItems, paymentItems, userId);
        }

        #endregion SaveReceipt

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
        public static decimal GetMinTaxDue(int statmentId, string interestDate)
        {
            return ReceiptEngineComp.GetMinTaxDue(statmentId, interestDate);
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
        public static decimal GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            return ReceiptEngineComp.GetInterestAmount(statmentId, interestDate, taxDueAmount);
        }

        #endregion GetInterestAmount

        #endregion Tax CalCulation for Receipt Engine

        #endregion Receipt Engine

        #region Receipt Management

        #region GetReceiptHeaderDetails

        /// <summary>
        /// Gets the receipt header details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Returns ReceiptHeader dataset</returns>
        public static F15100ReceiptHeaderData GetReceiptHeaderDetails(int receiptId)
        {
            return F15100ReceiptHeaderComp.GetReceiptHeaderDetails(receiptId);
        }

        /// <summary>
        /// Get a cecipt detials.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the reciept.
        /// </returns>
        public static F15100ReceiptHeaderData GetReceiptListDetails(int receiptId)
        {
            return F15100ReceiptHeaderComp.GetReceiptListDetails(receiptId);
        }

        #endregion GetReceiptHeaderDetails

        #region Save Receipt Header

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="userId">UserID</param>
        public static void F15100_SaveReceiptHeaderreceiptNumber(int receiptId, string receiptNumber, int userId)
        {
            F15100ReceiptHeaderComp.F15100_SaveReceiptHeaderreceiptNumber(receiptId, receiptNumber, userId);
        }

        #endregion Save Receipt Header

        #region ListReceiptItems

        /// <summary>
        /// Lists the receiptitems details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Returns ReceiptItems dataset</returns>
        public static F15101ReceiptItemsData ListReceiptItems(int receiptId)
        {
            return F15101ReceiptItemsComp.ListReceiptItems(receiptId);
        }
        #endregion ListReceiptItems

        #region Update Transaction Items

        /// <summary>
        /// F15101_s the update transaction items.
        /// </summary>
        /// <param name="transactionItems">The transaction items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>the status.</returns>
        public static int F15101_UpdateTransactionItems(string transactionItems, int userId)
        {
            return F15101ReceiptItemsComp.F15101_UpdateTransactionItems(transactionItems, userId);
        }

        #endregion Update Transaction Items

        #region GetStatementReceiptHeaderDetails

        /// <summary>
        /// Gets the receipt statement header details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Returns ReceiptstatementHeader dataset</returns>
        public static F15102ReceiptStatementHeaderData GetReceiptStatementHeaderDetails(int receiptId)
        {
            return F15102ReceiptStatementHeaderComp.GetReceiptStatementHeaderDetails(receiptId);
        }

        #endregion GetStatementReceiptHeaderDetails

        #region ListReceiptOwners
        /// <summary>
        /// Lists the ReceiptOwners details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>eturns ReceiptItems dataset</returns>
        public static F15103ReceiptOwnersData ListReceiptOwners(int receiptId)
        {
            return F15103ReceiptOwnersComp.ListReceiptOwners(receiptId);
        }
        #endregion ListReceiptOwners

        #endregion Receipt Management

        #region PayeeDetails

        /// <summary>
        /// Get Payee detail based of OwnerId
        /// </summary>
        /// <param name="PayeeID">The PayeeID.</param>
        /// <returns>The typed dataset containing the Payments.</returns>
        public static PaymentEngineData F1019_GetPayeeDetails(int PayeeID)
        {
            return PaymentEngineComp.F1019_GetPayeeDetails(PayeeID);
        }

        #endregion

        #region Payment Engine

        #region Get Payment

        /// <summary>
        /// Get Payment detail based of ppaymentId
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>The typed dataset containing the Payments.</returns>
        public static PaymentEngineData GetPayment(int ppaymentId)
        {
            return PaymentEngineComp.GetPayment(ppaymentId);
        }

        /// <summary>
        /// Get Payment detail based of ppaymentId
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>The typed dataset containing the Payments.</returns>
        public static PaymentEngineData GetMultiplePayment(string ppaymentId)
        {
            return PaymentEngineComp.GetMultiplePayment(ppaymentId);
        }
        #endregion Get Payment



        #region Save Payment
        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Return PPayment ID</returns>
        public static int SavePayment(int ppaymentId, string paymentItems, int userId, int ownerId)
        {
            return PaymentEngineComp.SavePayment(ppaymentId, paymentItems, userId, ownerId);
        }
        #endregion Save Payment

        #region Get Tender Type Configuration

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <returns>Tender type configuartion information</returns>
        public static PaymentEngineData GetTenderTypeConfiguration()
        {
            return PaymentEngineComp.GetTenderTypeConfiguration();
        }

        #endregion Get Tender Type Configuration

        #endregion Payment Engine

        #region Reports
        /// <summary>
        /// Get  the report details based on the formid and userid.
        /// </summary>
        /// <param name="reportId">The report id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns DataSet With full Report Details</returns>
        public static DataSet GetReportDetails(int reportId, int userId)
        {
            return Report.GetReportDetails(reportId, userId);
        }

        /// <summary>
        /// F9008s the get report details.
        /// </summary>        
        /// <param name="userId">The user id.</param>
        /// <returns>F9008ReportDetailsData</returns>
        public static F9008ReportDetailsData F9008GetReportDetails(int userId)
        {
            return F9008ReportDetailsComp.F9008GetReportDetails(userId);
        }

        #region SaveReportDetails

        /// <summary>
        /// To Save Report Details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="printerConf">The printer conf.</param>
        public static void F9008_SaveReportDetails(int userId, string printerConf)
        {
            F9008ReportDetailsComp.F9008_SaveReportDetails(userId, printerConf);
        }

        #endregion SaveReportDetails

        #region GetAutoPrintStatus

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns true/false</returns>
        public static int GetAutoPrintStatus(int formId, int userId)
        {
            return Report.GetAutoPrintStatus(formId, userId);
        }

        #endregion GetAutoPrintStatus

        #region SaveAutoPrint

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [auto print].</param>
        public static void SaveAutoPrint(int formId, int userId, bool autoPrint)
        {
            Report.SaveAutoPrint(formId, userId, autoPrint);
        }

        #endregion SaveAutoPrint

        #endregion

        #region GetMenuItems

        /// <summary>
        /// used to get the menuitems depends on userid
        /// </summary>
        /// <param name="userId">UserID To getMenuItems </param>
        /// <param name="applicationId">applicationID To getMenuItems</param>
        /// <returns> MenuItems Details DataSet</returns>
        public static DataSet GetMenuItems(int userId, int applicationId)
        {
            return General.GetMenuItems(userId, applicationId);
        }
        #endregion

        #region GetFormItem

        /// <summary>
        /// Gets the form items.
        /// </summary>
        /// <returns>return Dataset</returns>
        public static DataSet GetFormItems()
        {
            return General.GetFormItems();
        }

        #endregion

        #region GetFormTitle

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String with Title</returns>
        public static string GetFormTitle(int formId)
        {
            return General.GetFormTitle(formId);
        }

        #endregion

        #region GetFormPermissions

        /// <summary>
        /// used to get the Form Permissions depends on userid
        /// </summary>
        /// <param name="userId">UserID To Form Permissions </param>
        /// <param name="applicationId">applicationID To Form Permissions</param>
        /// <returns> MenuItems Details DataSet</returns>
        public static DataSet GetFormPermissions(int userId, int applicationId)
        {
            return General.GetFormPermissions(userId, applicationId);
        }
        #endregion

        #region UserManagement

        #region User Tab
        #region GetUserGroupDetails
        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <returns>DataSet With User and Group </returns>
        public static UserManagementData GetUserGroupDetails(int applicationId)
        {
            return UserManagementComp.GetUserGroupDetails(applicationId);
        }
        #endregion

        #region InsertUSer
        /// <summary>
        /// save the  the user group details changes and add new user details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="nameDisplay">The name display.</param>
        /// <param name="nameFull">The name full.</param>
        /// <param name="nameNet">The name net.</param>
        /// <param name="email">The email.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="administrator">if set to <c>true</c> [administrator].</param>
        /// <param name="applicationId">Application ID</param>
        /// <param name="loginUserId">UserID</param>
        /// <returns>DataSet with  Details of Proper insert or not</returns>
        public static UserManagementData SaveUserDetails(int userId, string nameDisplay, string nameFull, string nameNet, string email, int active, int administrator, int appraiser, int applicationId, int loginUserId)
        {
            return UserManagementComp.SaveUserDetails(userId, nameDisplay, nameFull, nameNet, email, active, administrator, appraiser, applicationId, loginUserId);
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="loginUseId">The login use id.</param>
        public static void DeleteUserDetails(int userId, int loginUseId)
        {
            UserManagementComp.DeleteUserDetails(userId, loginUseId);
        }
        #endregion

        #endregion

        #region GroupTab

        #region GetUserGroupDetails
        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>DataSet With User and Group</returns>
        public static UserManagementData GetGroupDetails(int userId)
        {
            return UserManagementComp.GetGroupDetails(userId);
        }
        #endregion

        #region InsertUserGroupDetails
        /// <summary>
        /// save the  the user group details changes and add new user details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="userGroup">The user group.</param>
        /// <param name="userId">UserID</param>
        /// <returns>DataSet 1 for success insert 0 for error</returns>
        public static UserManagementData InsertGroupDetails(int groupId, string groupName, string description, string userGroup, int userId)
        {
            return UserManagementComp.InsertGroupDetails(groupId, groupName, description, userGroup, userId);
        }
        #endregion

        #region DeleteUserGroupDetails
        /// <summary>
        /// save the  the user group details changes and add new user details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteGroupDetails(int groupId, int userId)
        {
            UserManagementComp.DeleteGroupDetails(groupId, userId);
        }
        #endregion

        #endregion

        #region coding For PermissionsTab

        #region GetUserGroupDetails
        /// <summary>
        /// Gets the user group permissions details.
        /// </summary>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// DataSet With Forms Name and relative Permissions
        /// </returns>
        public static UserManagementData GetGroupPermissionDetails(int userId)
        {
            return UserManagementComp.GetGroupPermissionDetails(userId);
        }

        #endregion

        #region SaveUserGroupDetails

        /// <summary>
        /// Saves the grou permission details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="formPermissions">The form permissions.</param>
        /// <param name="userId">UserID</param>
        public static void SaveGrouPermissionDetails(int groupId, string formPermissions, int userId)
        {
            UserManagementComp.SaveGrouPermissionDetails(groupId, formPermissions, userId);
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
        public static void LogException(int eventId, int priority, string severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processId, string processName, string managedThreadName, string win32ThreadId, string message, string formattedMessage)
        {
            ExceptionComp.LogException(eventId, priority, severity, title, timeStamp, machineName, appDomainName, processId, processName, managedThreadName, win32ThreadId, message, formattedMessage);
        }
        #endregion

        #region Query Utility

        /// <summary>
        /// Gets the query utility list.
        /// </summary>
        /// <param name="formId">The form ID.</param>
        /// <returns>return dataset</returns>
        public static QueryUtilityData GetQueryUtilityList(int formId)
        {
            return QueryUtilityComp.GetQueryUtilityList(formId);
        }

        /// <summary>
        /// Deletes the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteQueryUtility(int queryId, int userId)
        {
            QueryUtilityComp.DeleteQueryUtility(queryId, userId);
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
        public static int InsertQueryUtility(int queryId, string queryName, int formId, string description, int userId, string whereCondition, string userWhereCondition, int overrideValue)
        {
            return QueryUtilityComp.InsertQueryUtility(queryId, queryName, formId, description, userId, whereCondition, userWhereCondition, overrideValue);
        }

        #endregion

        #region Snapshot Utility

        /// <summary>
        /// Gets the snapshot utility list.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>return dataset</returns>
        public static SnapshotUtilityData GetSnapshotUtilityList(int formId)
        {
            return SnapshotUtilityComp.GetSnapshotUtilityList(formId);
        }

        /// <summary>
        /// Deletes the snapshot utility.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteSnapshotUtility(int snapshotId, int userId)
        {
            SnapshotUtilityComp.DeleteSnapshotUtility(snapshotId, userId);
        }

        /// <summary>
        /// Inserts the query utility.
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
        public static int InsertSnapshotUtility(int snapshotId, string snapshotName, int snapshotFormId, string description, int recordCount, int userId, int overrideValue, string keyIds)
        {
            return SnapshotUtilityComp.InsertSnapshotUtility(snapshotId, snapshotName, snapshotFormId, description, recordCount, userId, overrideValue, keyIds);
        }

        #endregion

        #region Login User Information

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>return dataset</returns>
        public static DataSet GetUserInformation(string userName, int applicationId)
        {
            return LoginComp.GetUserInformation(userName, applicationId);
        }

        /// <summary>
        /// Used To get the Net_Name for a particular User
        /// </summary>
        /// <param name="userFullName">User FullName</param>
        /// <returns>NetName for a particular fullname</returns>
        public static DataSet GetUserNetName(string userFullName)
        {
            return LoginComp.GetUserNetName(userFullName);
        }

        /// <summary>
        /// Gets the config information.
        /// </summary>
        /// <returns>return dataset</returns>
        public static DataSet GetConfigInformation()
        {
            return LoginComp.GetConfigInformation();
        }

        /// <summary>
        /// Gets the state of the authentication.
        /// </summary>
        /// <returns>returns AuthenticationState dataset</returns>
        public static DataSet GetAuthenticationState()
        {
            return LoginComp.GetAuthenticationState();
        }
        #endregion

        #region SQL Support

        #region GetQueryResult

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>returns dataset</returns>
        public static DataSet GetSQLQueryResult(string sqlQuery)
        {
            return SQLSupportComp.GetSQLQueryResult(sqlQuery);
        }

        #endregion

        #region ListSqlDescription
        /// <summary>
        /// Gets the SQLDescription
        /// </summary>
        /// <param name="categoryId">The SQL category.</param>
        /// <returns>SQLSupportData Dataset</returns>
        public static SQLSupportData GetSQLDescription(int categoryId)
        {
            return SQLSupportComp.GetSQLDescription(categoryId);
        }
        #endregion

        #region ListSqlCategory
        /// <summary>
        /// Gets the SQLCategory
        /// </summary>
        /// <returns>SQLSupportData Dataset</returns>
        public static SQLSupportData GetSQLCategory()
        {
            return SQLSupportComp.GetSQLCategory();
        }
        #endregion

        #region GetSqlString

        /// <summary>
        /// Gets the SQLString
        /// </summary>
        /// <param name="categoryId">Category</param>
        /// <param name="sqlId">Description</param>
        /// <returns>SQLSupportData Dataset</returns>
        public static SQLSupportData GetSQLString(int categoryId, int sqlId)
        {
            return SQLSupportComp.GetSqlString(categoryId, sqlId);
        }

        #endregion

        #region SaveSQLQuery

        /// <summary>
        /// Saves the SQL query.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="description">The description.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>SaveSQLQuery</returns>
        public static int SaveSQLQuery(int categoryId, string description, string statement, int moduleId, int userId, int sqlId)
        {
            return SQLSupportComp.SaveSQLQuery(categoryId, description, statement, moduleId, userId, sqlId);
        }

        #endregion

        #region Delete Query

        /// <summary>
        /// F9015_s the delete query.
        /// </summary>
        /// <param name="sqlId">The SQL id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>QueryId</returns>
        public static int F9015_DeleteQuery(int sqlId, int userId)
        {
            return SQLSupportComp.F9015_DeleteQuery(sqlId, userId);
        }

        #endregion

        #endregion

        #region County Configuration

        #region GETConfig
        /// <summary>
        /// Gets the county configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">The User id</param>
        /// <returns>County Config Details</returns>
        public static DataSet GetCountyConfiguration(int applicationId, int userId)
        {
            return CountyConfigurationComp.GetCountyConfiguration(applicationId, userId);
        }
        #endregion

        #region Save
        /// <summary>
        /// Gets the county configuration.
        /// </summary>
        /// <param name="configId">The config id.</param>
        /// <param name="configDescription">The config description.</param>
        /// <param name="userId">UserID</param>
        public static void UpdateCountyConfigDetails(int configId, string configDescription, int userId)
        {
            CountyConfigurationComp.UpdateCountyConfigDetails(configId, configDescription, userId);
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
        public static MortgageImpotTemplateData GetMortgageTemplate(int templateId)
        {
            return MortgageImportTemplateComp.GetMortgageTemplate(templateId);
        }
        #endregion

        #region List Mortgage Import Template
        /// <summary>
        /// Lists the mortgage template.
        /// </summary>
        /// <returns>Mortgage template list</returns>
        public static MortgageImpotTemplateData ListMortgageTemplate()
        {
            return MortgageImportTemplateComp.ListMortgageTemplate();
        }
        #endregion

        #region List MortgageImportFileType
        /// <summary>
        /// Lists the type of the mortgage import file.
        /// </summary>
        /// <returns>returns the dataset containing the Mortgage Import FileType</returns>
        public static MortageImportData ListMortgageImportFileType()
        {
            return MortgageImportTemplateComp.ListMortgageImportFileType();
        }
        #endregion

        #region Save Mortgage Import Template


        public static void SaveMortgageImportTemplate(int templateId, string templateName, int typeId, int userId, string description, string filePath, int statementIdPos, int statementIdWid, int statementNumPos, int statementNumWid, int amountPos, int amountWid, int commentPos, int commentWid, int bankCodePos, int bankCodeWid, int loanNumPos, int loanNumWid, int taxPayNamePos, int taxPayNameWid, int ParcelNumPos, int ParcelNumWid, int PostTypePos, int PostTypeWid, int OwnerIDPos, int OwnerIDWid, int CartIdPos, int CartidWid)
        {
            MortgageImportTemplateComp.SaveMortgageImportTemplate(templateId, templateName, typeId, userId, description, filePath, statementIdPos, statementIdWid, statementNumPos, statementNumWid, amountPos, amountWid, commentPos, commentWid, bankCodePos, bankCodeWid, loanNumPos, loanNumWid, taxPayNamePos, taxPayNameWid, ParcelNumPos, ParcelNumWid, PostTypePos, PostTypeWid, OwnerIDPos, OwnerIDWid,CartIdPos,CartidWid);
        }
        #endregion

        #region Delete Mortgage Template

        /// <summary>
        /// Deletes the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteMortgageTemplate(int templateId, bool overrideStatus, int userId)
        {
            return MortgageImportTemplateComp.DeleteMortgageTemplate(templateId, overrideStatus, userId);
        }
        #endregion

        #endregion

        #region Mortgage Import

        #region Mortgage Import Statement Ids

        /// <summary>
        /// Gets the Mortgage Import statement Id's
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import statementids.</returns>
        public static DataSet GetMortgageImportStatementIds()
        {
            return MortgageImportComp.GetMortgageImportStatementIds();
        }

        #endregion

        #region MortgageImport Statement

        /// <summary>
        /// Gets the Mortgage Import statement based on the import id
        /// </summary>
        /// <param name="importId"> The importEd of the statement to be fetched.</param>
        /// <param name="nextAvailableRecord">true fetch next available record if current record deleted,false previoud record</param>
        /// <returns> The dataset containing the statement information of the importId.</returns>
        public static MortageImportData GetMortgageImportStatement(int importId, bool nextAvailableRecord)
        {
            return MortgageImportComp.GetMortgageImportStatement(importId, nextAvailableRecord);
        }

        #endregion

        #region Mortgage Import Check Valid Receipt

        /// <summary>
        /// Check For Valid Receipt 
        /// </summary>
        /// <param name="importId">The import id</param>       
        /// <param name="receiptDate">The receipt date</param>       
        /// <returns>The DataSet containg valid receipt details</returns>
        public static MortageImportData CheckMortgageImportValidReceipt(int importId, DateTime receiptDate)
        {
            return MortgageImportComp.CheckMortgageImportValidReceipt(importId, receiptDate);
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
        /// <returns>The DataSet containg inserted entries details</returns>  
        public static MortageImportData SaveMortgageImportEntries(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfpaycode, string mortgageImportEntries)
        {
            return MortgageImportComp.SaveMortgageImportEntries(importId, templateId, templateName, typeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId,firstHalfpaycode, mortgageImportEntries);
        }

        #endregion

        #region Mortgage Import Template Selection

        /// <summary>
        /// Gets the Mortgage Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Mortgage Import Template Details.</returns>
        public static MortgageImportTemplateSelectData GetMortgageImportTemplateDetails()
        {
            return MortgageImportComp.GetMortgageImportTemplateDetails();
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
        /// <returns>the DataSet Containing the Error Records Information</returns>
        public static MortageImportData MortgageImportCheckErrors(int importId, int templateId, string templateName, int typeId, string filePath, DateTime recieptDate, DateTime interestDate, bool payCode, int userId, int rollYear,int firstHalfpaycode, int ppaymentId)
        {
            return MortgageImportComp.MortgageImportCheckErrors(importId, templateId, templateName, typeId, filePath, recieptDate, interestDate, payCode, userId, rollYear,firstHalfpaycode, ppaymentId);
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
        /// <returns>The DataSet containg inserted entries import id</returns>
        public static MortageImportData SaveMortgageImport(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, bool resetErrorCheck)
        {
            return MortgageImportComp.SaveMortgageImport(importId, templateId, templateName, typeId, filePath, receiptDate, interestDate, payCode, userId, rollYear, ppaymentId, firstHalfPayCode, resetErrorCheck);
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
        public static MortageImportData CreateReceipt(int importId, int templateId, string templateName, string filePath, int typeId, DateTime receiptDate, DateTime interestDate, bool payCode,int firsthalfpayCode, int userId, int rollYear, int? ppaymentId, bool resetErrorCheck)
        {
            return MortgageImportComp.CreateReceipt(importId, templateId, templateName, filePath, typeId, receiptDate, interestDate, payCode,firsthalfpayCode, userId, rollYear, ppaymentId, resetErrorCheck);
        }
       
        #endregion  Create Receipt

        #endregion

        #region Delete Mortgage Import

        /// <summary>
        /// Delete Mortgage import record
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">UserID</param>
        /// <returns>The DataSet</returns>
        public static MortageImportData DeleteMortgageImport(int importId, int userId)
        {
            return MortgageImportComp.DeleteMortgageImport(importId, userId);
        }

        /// <summary>
        /// Delete Mortgage import file entries
        /// </summary>
        /// <param name="importId">The import id</param>
        /// <param name="userId">UserID</param>
        /// <returns>The DataSet</returns>
        public static MortageImportData DeleteMortgageImportFileEntries(int importId, int userId)
        {
            return MortgageImportComp.DeleteMortgageImportFileEntries(importId, userId);
        }

        #endregion

        #region Error Engine

        /// <summary>
        /// Gets the error engine.
        /// </summary>
        /// <param name="errorTypeId">The error type id.</param>
        /// <returns>return Error engine data</returns>
        public static ErrorEngineData GetErrorEngine(int errorTypeId)
        {
            return ErrorEngineComp.GetErrorEngine(errorTypeId);
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
        public static void InsertErrorEngine(string errorDate, int userId, string userIP, int errorTypeId, string parameter, string comment)
        {
            ErrorEngineComp.InsertErrorEngine(errorDate, userId, userIP, errorTypeId, parameter, comment);
        }

        #endregion

        #region NextNumber Configuration

        #region List NextNumber Configuration

        /// <summary>
        /// List the NextNumber Configuration details
        /// </summary>
        /// <returns>The dataset containing the list of NextNumber Configuration.</returns>
        public static NextNumberData ListNextNumberConfiguration()
        {
            return NextNumberConfigurationComp.ListNextNumberConfiguration();
        }

        #endregion

        #region Check Next Number

        /// <summary>
        /// Check for valid Next Number
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>The dataset containing the valid Next Number details.</returns>
        public static DataSet CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return NextNumberConfigurationComp.CheckNextNumber(rollYear, nextNum, formula);
        }

        #endregion

        #region Update NextNumber ConfigDetails

        /// <summary>
        /// Saves Next Number configuration details
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">UserID</param>
        public static void UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            NextNumberConfigurationComp.UpdateNextNumberConfigDetails(nextNumId, nextNum, formula, userId);
        }

        #endregion

        #endregion

        #region Excise Tax Affidavit

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static ExciseIndividualType GetExciseIndividualType()
        {
            return ExciseTaxAffidavitComp.GetExciseIndividualType();
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static ExciseTaxAffidavitData GetExciseTaxAffidavitDetails(int statementId)
        {
            return ExciseTaxAffidavitComp.GetExciseTaxAffidavitDetails(statementId);
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static ExciseTaxAffidavitAmountDueData GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            return ExciseTaxAffidavitComp.GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateId, taxCode, taxableSaleAmount);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>returns dataset containing AffiDavit Details</returns>
        public static int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, int userId)
        {
            return ExciseTaxAffidavitComp.SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, userId);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public static ExciseTaxAffidavitData GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            return ExciseTaxAffidavitComp.GetAffidavitStatementId(formId, orderField, orderBy);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public static PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return ExciseTaxAffidavitComp.GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public static AffidavitDistrictSelectionData GetDistrictSelection(int exciseRateId)
        {
            return ExciseTaxAffidavitComp.GetDistrictSelection(exciseRateId);
        }

        /// <summary>
        /// Delete The PArticular StatmentID Detials
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteAffidavitDetails(int statementId, int userId)
        {
            ExciseTaxAffidavitComp.DeleteAffidavitDetails(statementId, userId);
        }

        /// <summary>
        /// Executes the affdvt query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>Returns ExecuteAffdvtQuery Dataset</returns>
        public static QueryByFormData ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn)
        {
            return ExciseTaxAffidavitComp.ExecuteAffdvtQuery(formId, whereCondnSql, orderByCondn);
        }

        #endregion

        #region ExciseTax Affidavit validation

        /// <summary>
        /// Gets the excise tax affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <returns>The dataset containing statementID status</returns>
        public static ExciseAffidavitValidationData GetExciseTaxAffidavitStatus(int statementId, int treasurerStatus)
        {
            return ExciseAffidavitValidationComp.GetExciseTaxAffidavitStatus(statementId, treasurerStatus);
        }

        /// <summary>
        /// Updates the excise affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="userId">UserID</param>
        public static void UpdateExciseAffidavitStatus(int statementId, int treasurerStatus, int statusId, int userId)
        {
            ExciseAffidavitValidationComp.UpdateExciseAffidavitStatus(statementId, treasurerStatus, statusId, userId);
        }

        #endregion

        #region Excise Work Queue

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
        /// <returns>Return AffidavitWorkQueueData Dataset</returns>
        public static AffidavitWorkQueueData F1107_ExciseWorkQueue_GetWorkQueueSearchResult(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, string statementNumber)
        {
            return AffidavitWorkQueueComp.F1107_ExciseWorkQueue_GetWorkQueueSearchResult(parcelNumber, name, receiptDate, address, taxCode, treasurer, assessor, statementNumber);
        }

        #endregion

        #region Management Work Queue

        /// <summary>
        /// F1109_s the list management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="treasurer">The treasurer.</param>
        /// <param name="assessor">The assessor.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>Returns ManagementWorkQueue DataSet</returns>
        public static AffidavitManagementQueue F1109_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, int rollYear, string statementNumber)
        {
            return ManagementWorkQueueComp.F1109_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, treasurer, assessor, rollYear, statementNumber);
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
            return ManagementWorkQueueComp.F1109_ManagementQueueFilterResult(statusFilterId, rollYear, filterFromDate, filterToDate);
        }

        /// <summary>
        /// F1109_s the filter search affidavit.
        /// </summary>
        /// <param name="filterXml">The filter XML.</param>
        /// <returns>Returns ManagementWorkQueue Filter Result</returns>
        public static AffidavitManagementQueue F1109_FilterSearchAffidavit(string filterXml)
        {
            return ManagementWorkQueueComp.F1109_FilterSearchAffidavit(filterXml);
        }

        /// <summary>
        /// F1109_s the list roll year.
        /// </summary>
        /// <returns>Returns Rollyear DataSet</returns>
        public static AffidavitManagementQueue F1109_ListRollYear()
        {
            return ManagementWorkQueueComp.F1109_ListRollYear();
        }

        #endregion

        #region Submittal Queue

        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public static REETA F1108_GetSubmitAffidavit(string statementId)
        {
            return SubmittalQueueComp.F1108_GetSubmitAffidavit(statementId);
        }

        /// <summary>
        /// F1108_s the list management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>Returns SubmittalQueue dataset</returns>
        public static SubmittalQueueData F1108_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string receiptNumber, string statementNumber)
        {
            return SubmittalQueueComp.F1108_ListManagementQueue(parcelNumber, name, receiptDate, address, taxCode, receiptNumber, statementNumber);
        }

        /// <summary>
        /// F1108_s the list configuration detail.
        /// </summary>
        /// <returns>Returns ConfigurationDetail</returns>
        public static SubmittalQueueData F1108_ListConfigurationDetail()
        {
            return SubmittalQueueComp.F1108_ListConfigurationDetail();
        }

        /// <summary>
        /// F1108_s the get submit affidavit reply.
        /// </summary>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Returns SubmitAffidavit reply datatSet</returns>
        public static REETA F1108_GetSubmitAffidavitReply(string reetReplyXml, int userId)
        {
            return SubmittalQueueComp.F1108_GetSubmitAffidavitReply(reetReplyXml, userId);
        }

        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public static int F1108_SaveReplyReetXml(string reetXml, string reetReplyXml, int userId)
        {
            return SubmittalQueueComp.F1108_SaveReplyReetXml(reetXml, reetReplyXml, userId);
        }

        #endregion

        #endregion

        #region Excise Tax Statement

        #region Get Excise Tax Statement

        /// <summary>
        /// Gets the excise tax statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Statement Details</returns>
        public static ExciseTaxStatementData GetExciseTaxStatement(int statementId)
        {
            return ExciseTaxStatementComp.GetExciseTaxStatement(statementId);
        }

        #endregion

        #region Get Excise Tax Receipt

        /// <summary>
        /// Gets the Excise Tax Receipt
        /// </summary>
        /// <param name="statementId">The statement Id.</param>
        /// <returns>The typed dataset containing the ExciseTaxReceipt information based on statementId</returns>
        public static ExciseTaxReceiptData GetExciseTaxReceipt(int statementId)
        {
            return ExciseTaxReceiptComp.GetExciseTaxReceipt(statementId);
        }

        #endregion

        #region List Excise Tax Statement

        /// <summary>
        /// List the Excise Tax Statement ID
        /// </summary>
        /// <returns>Excise Tax Statement ID Details</returns>
        public static ExciseTaxStatementData ListExciseTaxStatement()
        {
            return ExciseTaxStatementComp.ListExciseTaxStatement();
        }

        #endregion List Excise Tax Statement

        #region Save Tax Receipt

        /// <summary>
        /// Saves the excise tax receipt.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The typed dataset</returns> 
        public static ExciseTaxStatementData SaveExciseTaxReceipt(string statementItems, int userId)
        {
            return ExciseTaxStatementComp.SaveExciseTaxReceipt(statementItems, userId);
        }

        #endregion Save Tax Receipt

        #endregion

        #region Excise Rate District Selection

        #region List Excise Rate District

        /// <summary>
        /// Lists the excise rate district.
        /// </summary>
        /// <param name="district">The district.</param>
        /// <param name="year">The year.</param>
        /// <param name="description">The description.</param>
        /// <returns>The typed dataset containing the ExciseRateDistrict information based on district, year and description</returns>
        public static ExciseRateDistrictSelectionData ListExciseRateDistrict(string district, int year, string description)
        {
            return ExciseRateDistrictSelectionComp.ListExciseRateDistrict(district, year, description);
        }

        #endregion

        #endregion

        #region Excise District Copy

        #region Get Excise District Copy

        /// <summary>
        /// Gets the District And Roll Year(Base Year)
        /// </summary>
        /// <param name="exciserateId">ExciseRateID</param>
        /// <returns>The typed dataset containing the information about district and base year</returns>
        public static ExciseDistrictCopyData GetExciseDistrictCopy(int exciserateId)
        {
            return ExciseDistrictCopyComp.GetExciseDistrictCopy(exciserateId);
        }

        #endregion Get Excise District Copy

        #region Save Excise District Copy

        /// <summary>
        /// Save the Excise district Copy
        /// The returns values from Database 
        /// 0 = When The record is successfully saved.
        /// 1 = When Invalid Source Record
        /// 2 = When Invalid Destination Record
        /// </summary>
        /// <param name="district">The district</param>
        /// <param name="basedOnYear">The Based On year</param>
        /// <param name="newDistrictYear">The New District year</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// The returns values from Database 
        /// 0 = When The record is successfully saved.
        /// 1 = When Invalid Source Record
        /// 2 = When Invalid Destination Record
        /// </returns>
        public static int SaveExciseDistrcitCopy(int district, int basedOnYear, int newDistrictYear, int userId)
        {
            return ExciseDistrictCopyComp.SaveExciseDistrictCopy(district, basedOnYear, newDistrictYear, userId);
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
        public static MasterNameSearchData GetMasterNameSearch(string lastName, string firstName, string address)
        {
            return MasterNameSearchComp.GetMasterNameSearch(lastName, firstName, address);
        }

        #endregion

        #region Excise Tax Rate

        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public static ExciseTaxRateData GetExciseTaxRate(int exciseRateId)
        {
            return ExciseTaxRateComp.GetExciseTaxRate(exciseRateId);
        }

        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public static ExciseTaxRateData ListExciseTaxRate()
        {
            return ExciseTaxRateComp.ListExciseTaxRate();
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// Save the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">UserID</param>
        public static void SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            ExciseTaxRateComp.SaveExciseTaxRate(exciseRateId, exciseTaxDetails, userId);
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">The excise rate Id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            return ExciseTaxRateComp.DeleteExciseTaxRate(exciseRateId, userId);
        }
        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public static ExciseTaxRateData GetDistrictName(int districtId)
        {
            return ExciseTaxRateComp.GetDistrictName(districtId);
        }
        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public static ExciseTaxRateData GetAccountName(int accountId)
        {
            return ExciseTaxRateComp.GetAccountName(accountId);
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
        /// <returns>The account selection data.</returns>
        public static AccountSelectionData GetAccountSelectionData(string subFund, string bars, string functionName, string objectname, string line, int rollYear, string desciption, int iscash)
        {
            return AccountSelectionComp.GetAccountSelectionData(subFund, bars, functionName, objectname, line, rollYear, desciption, iscash);
        }

        #endregion Get Account Slection data

        #endregion Account Slection

        #region F1512 District Slection

        #region Get District Slection Data

        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DistrictSelectionComp dataset</returns>
        public static F1512DistrictSelectionData F1512_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            return F1512DistrictSelectionComp.F1512_GetDistrictSelectionData(districtId, district, description, rollYear);
        }

        #endregion Get District Slection data

        #endregion F1512 District Slection

        #region Help Engine

        #region List Help Engine

        /// <summary>
        /// Lists the Help Documents
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns>returns dataset contains Help Form details</returns>
        public static HelpEngineData ListHelpDocumentForm(string formFile)
        {
            return HelpEngineComp.ListHelpDocumentForm(formFile);
        }

        #endregion

        #endregion

        #region GDocEventEngine

        #region Get TypeStatus Data

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>
        /// returns Value For Event Type Datas and Event Status Data
        /// </returns>
        public static GDocEventEngineTypeStatusData ListEventTypeStatusDetails(int featureClassId)
        {
            return GDocEventEngineComp.ListEventTypeStatusDetails(featureClassId);
        }

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>
        /// returns dataset contains EnvetEngine Datas
        /// </returns>
        public static GDocEventEngineData LoadEventEngineData(int featureClassId, int featureId)
        {
            return GDocEventEngineComp.LoadEventEngineData(featureClassId, featureId);
        }

        #endregion

        #region GetEventEngineHeader

        /// <summary>
        /// Loads the event engine data header.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>Event Engine Header Data</returns>
        public static GDocEventEngineData GetEventEngineDataHeader(int featureClassId, int featureId)
        {
            return GDocEventEngineComp.GetEventEngineDataHeader(featureClassId, featureId);
        }
        #endregion

        /// <summary>
        /// Insert The Data which is  passed as XML
        /// </summary>
        /// <param name="featureId">GetGDocEventEngineFeatureClassId</param>
        /// <returns>Inserted EventID</returns>
        public static int GetGDocEventEngineFeatureClassId(int featureId)
        {
            return GDocEventEngineComp.GetGDocEventEngineFeatureClassId(featureId);
        }

        #region insertEventEnigne
        /// <summary>
        /// Insert The Data which is  passed as XML
        /// </summary>
        /// <param name="eventEngineInsertData">The event engine insert data.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Inserted EventID</returns>
        public static int InsertGDocEventEngineData(string eventEngineInsertData, int userId)
        {
            return GDocEventEngineComp.InsertGDocEventEngineData(eventEngineInsertData, userId);
        }
        #endregion

        #region Active Work Records

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureClassId">The featureClass id.</param>
        /// <returns>typed dataset containing the WOID,Date,Type and Comments</returns>
        public static GDocWorkOrderData GetWorkOrderDetails(int featureClassId)
        {
            return GDocEventEngineComp.GetWorkOrderDetails(featureClassId);
        }

        #endregion Active Work Records

        #endregion

        #region GDoc Comment

        #region Get GDoc Comment

        /// <summary>
        /// Gets the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed Dataset containing the GDoc comment</returns>
        public static GDocCommentData GetGDocComment(int eventId)
        {
            return GDocCommentComp.GetGDocComment(eventId);
        }

        #endregion Get GDoc Comment

        #region Save GDoc Comment

        /// <summary>
        /// Saves the GDoc Comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="comment">The GDoc comment.</param>
        /// <param name="userId">featureId</param>
        public static void SaveGDocComment(int eventId, string comment, int userId)
        {
            GDocCommentComp.SaveGDocComment(eventId, comment, userId);
        }

        #endregion Save GDoc Comment

        #endregion GDoc Comment

        #region GDoc Event Header

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public static GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return GDocEventHeaderComp.GetGDocEventHeader(eventId);
        }

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>Typed status containing Event Engine status.</returns>
        public static GDocEventHeaderData ListGDocEventHeaderStatus(int eventId)
        {
            return GDocEventHeaderComp.ListGDocEventHeaderStatus(eventId);
        }

        #endregion ListGDocEventHeaderStatus

        #region DeleteGDocEventHeader

        /// <summary>
        /// Deletes the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="childFlag">The child flag.</param>
        /// <param name="userId">UserID</param>
        public static void DeleteGDocEventHeader(int eventId, int childFlag, int userId)
        {
            GDocEventHeaderComp.DeleteGDocEventHeader(eventId, childFlag, userId);
        }

        #endregion DeleteGDocEventHeader

        #region SaveGDocEventHeader

        /// <summary>
        /// Saves the GDoc event header.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed dataset</returns>
        public static GDocEventHeaderData SaveGDocEventHeader(string eventItems, int userId)
        {
            return GDocEventHeaderComp.SaveGDocEventHeader(eventItems, userId);
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
        /// <returns></returns>
        public static int GetSystemId(int userId, int formNumber)
        {
            return GDocWorkOrderEngineComp.GetSystemId(userId, formNumber);
        }

        #endregion GetSystemID

        #region GetWorkOrderEngine

        /// <summary>
        /// Gets the work order engine.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <param name="isopen">The is open.</param>
        /// <returns>Typed Dataset containing the Work Order Engine Values</returns>
        public static GDocWorkOrderEngineData F8901_GetWorkOrderEngine(int systemId, int isopen)
        {
            return GDocWorkOrderEngineComp.F8901_GetWorkOrderEngine(systemId, isopen);
        }

        #endregion GetWorkOrderEngine

        #region GetWorkOrderType

        /// <summary>
        /// Gets the type of the work order.
        /// </summary>
        /// <param name="systemId">The system id.</param>        
        /// <returns>Typed Dataset containing the Work Order Type Values</returns>
        public static GDocWorkOrderEngineData F8901_GetWorkOrderType(int systemId)
        {
            return GDocWorkOrderEngineComp.F8901_GetWorkOrderType(systemId);
        }

        #endregion GetWorkOrderType

        #region SaveWorkOrderEngine

        /// <summary>
        /// Saves the work order engine.
        /// </summary>
        /// <param name="workOrderItems">The work order items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed Dataset containing the Work Order Engine Values</returns>
        public static GDocWorkOrderEngineData F8901_SaveWorkOrderEngine(string workOrderItems, int userId)
        {
            return GDocWorkOrderEngineComp.F8901_SaveWorkOrderEngine(workOrderItems, userId);
        }

        #endregion SaveWorkOrderEngine

        #endregion GDoc Work Order Engine

        #region GDoc Work order CallIn

        #region Get GDoc Work order CallIn

        /// <summary>
        /// Get work order call In values  for F8912.
        /// </summary>
        /// <param name="workorderId">The work order id.</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public static GDocWorkorderCallInData F8912_GetWorkOrderCallIn(int workorderId)
        {
            return GDocWorkorderCallInComp.F8912_GetWorkOrderCallIn(workorderId);
        }

        #endregion Get GDoc Work order CallIn

        #region Get GDoc Addresses

        /// <summary>
        /// To Get Addresses for GDOC Form Slices.
        /// </summary>        
        /// <returns>Typed DataSet Containing the Gdoc Address</returns>
        public static GDocWorkorderCallInData wListAddresses()
        {
            return GDocWorkorderCallInComp.wListAddresses();
        }

        #endregion Get GDoc Addresses

        #region Save GDoc Work order CallIn

        /// <summary>
        /// Save GDoc work order call In Values.
        /// </summary>
        /// <param name="workOrderCall">The work order call.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed DataSet Containing the Gdoc Work Order CallIn Values</returns>
        public static GDocWorkorderCallInData F8912_SaveWorkOrderCallIn(string workOrderCall, int userId)
        {
            return GDocWorkorderCallInComp.F8912_SaveWorkOrderCallIn(workOrderCall, userId);
        }

        #endregion Save GDoc Work order CallIn

        #endregion GDoc Work order CallIn

        #region GDoc Work order General

        #region Get GDoc Work order General

        /// <summary>
        /// Get work order general values for F8912.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public static GDocWorkOrderGeneralData F8910_GetWorkOrderGeneral(int workorderId)
        {
            return GDocWorkOrderGeneralComp.F8910_GetWorkOrderGeneral(workorderId);
        }

        #endregion Get GDoc Work order General

        #region Save GDoc Work order General

        /// <summary>
        /// Save work order general values for F8910.
        /// </summary>
        /// <param name="workOrderGeneral">The work order general.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed DataSet containing the GDoc Work order General Values</returns>
        public static GDocWorkOrderGeneralData F8910_SaveWorkOrderGeneral(string workOrderGeneral, int userId)
        {
            return GDocWorkOrderGeneralComp.F8910_SaveWorkOrderGeneral(workOrderGeneral, userId);
        }

        #endregion Save GDoc Work order General

        #endregion GDoc Work order General

        #region MakeDeposits

        #region GetPaymentItemsDetails

        /// <summary>
        /// Gets the payment items details.
        /// </summary>
        /// <returns>the DataSet which Contains the DepositItemDetails</returns>
        public static MakeDepositsData GetPaymentItemsDetails()
        {
            return MakeDepositsComp.GetPaymentItemsDetails();
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
        public static void SavePaymentItemsDetails(int registerId, decimal amount, int userId, string paymentItemsDetails)
        {
            MakeDepositsComp.SavePaymentItemsDetails(registerId, amount, userId, paymentItemsDetails);
        }

        #endregion

        #endregion

        #region PostingHistory

        #region ListPostTypes

        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>PostingHistoryComp.ListPostTypesData</returns>
        public static PostingHistoryData.ListPostTypeDataTable ListPostTypes(int form)
        {
            return PostingHistoryComp.ListPostTypesData(form);
        }

        #endregion

        #region ListPostingHistory

        /// <summary>
        /// Lists the posting history.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="postTypeId">The post type id.</param>
        /// <returns>
        /// PostingHistoryComp.ListPostinghistoryData
        /// </returns>
        public static PostingHistoryData.ListPostingHistoryDataTable ListPostingHistory(int count, int postTypeId)
        {
            return PostingHistoryComp.ListPostinghistoryData(count, postTypeId);
        }

        #endregion

        #endregion

        #region PostingErrors

        #region ListPostingErrors

        /// <summary>
        /// Lists the post errors.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>ListPostingErrors(</returns>
        public static PostingErrorsData ListPostErrors(int userId)
        {
            return PostingErrorsComp.ListPostingErrors(userId);
        }

        #endregion

        #region InsertAccount

        /// <summary>
        /// Inserts the account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="errorTypeId">The error type id.</param>
        public static void InsertAccount(int userId, int errorTypeId)
        {
            PostingErrorsComp.InsertAccount(userId, errorTypeId);
        }

        #endregion

        #endregion

        #region Posting

        #region List PostTypes

        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <returns>PostingHistoryComp.ListPostTypesData</returns>
        public static PostingData ListPostTypes()
        {
            return PostingComp.ListPostTypes();
        }

        #endregion

        #region List PreviewPosting

        /// <summary>
        /// Lists the preview posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>List Preview Posting</returns>
        public static PostingData ListPreviewPosting(DateTime postDate)
        {
            return PostingComp.ListPreviewPosting(postDate);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Clears the temporary records.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public static void ClearTemporaryRecords(int userId)
        {
            PostingComp.ClearTemporaryRecords(userId);
        }

        #endregion Delete

        #region Save

        /// <summary>
        /// Compiles the posting record set.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Posting Error DataTable</returns>
        public static PostingData CompilePostingRecordSet(DateTime postDate, string selectedPostTypes, int userId)
        {
            return PostingComp.CompilePostingRecordSet(postDate, selectedPostTypes, userId);
        }

        /// <summary>
        /// Performs the posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Post Lock DataTable</returns>
        public static PostingData PerformPosting(DateTime postDate, string selectedPostTypes, int userId)
        {
            return PostingComp.PerformPosting(postDate, selectedPostTypes, userId);
        }

        #endregion Save

        #endregion

        #region Reverse GL Post

        #region Get PostId Details

        /// <summary>
        /// Gets the post id data.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>GetPostIdDetails</returns>
        public static PostIdDetailsData GetPostIdDetails(int postId)
        {
            return ReverseGLPostComp.GetPostIdDetails(postId);
        }

        #endregion

        #region Inset Reverse GL Post

        /// <summary>
        /// Inserts the reverse GL post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        public static void InsertReverseGLPost(int postId, int userId)
        {
            ReverseGLPostComp.InsertReverseGLPost(postId, userId);
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
        public static SanitaryPipeInspectionData GetEventEngineEventProperties(int eventId)
        {
            return SanitaryPipeInspectionComp.GetEventEngineEventProperties(eventId);
        }

        #endregion

        #region Save Event Properties

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void SaveEventEngineEventProperties(string eventItems, int userId)
        {
            SanitaryPipeInspectionComp.SaveEventEngineEventProperties(eventItems, userId);
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
        public static SanitaryPipeInspectionDetailsData ListEventEngineTVDetails(int eventId)
        {
            return SanitaryPipeInspectionDetailsComp.ListEventEngineTVDetails(eventId);
        }

        #endregion

        #region List EventEngine Detail Types

        /// <summary>
        /// Lists the EventEngine DetailTypes
        /// </summary>
        /// <returns>returns dataset contains DetailTypes</returns>
        public static SanitaryPipeInspectionDetailsData ListEventEngineDetailTypes()
        {
            return SanitaryPipeInspectionDetailsComp.ListEventEngineDetailTypes();
        }

        #endregion

        #region Save EventEngine TV Details

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void SaveEventEngineTVDetails(string eventItems, int userId)
        {
            SanitaryPipeInspectionDetailsComp.SaveEventEngineTVDetails(eventItems, userId);
        }

        #endregion

        #region Update EventEngine TV Details

        /// <summary>
        /// Updates the Event Engine TV Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void UpdateEventEngineTVDetails(string eventItems, int userId)
        {
            SanitaryPipeInspectionDetailsComp.UpdateEventEngineTVDetails(eventItems, userId);
        }

        #endregion

        #region Delete EventEngine TV Details

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="detailId">The detail id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteEventEngineTVDetails(int detailId, int userId)
        {
            return SanitaryPipeInspectionDetailsComp.DeleteEventEngineTVDetails(detailId, userId);
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
        /// returns Sandwich And ItsSlice Information details
        /// </returns>
        public static FormMasterData GetSandwichAndItsSliceInformation(int form, int keyId, int userId)
        {
            return FormMasterComp.GetSandwichAndItsSliceInformation(form, keyId, userId);
        }

        #endregion GetSandwichAndItsSliceInformation

        #region GetSandwichSubTitleInformation

        /// <summary>
        /// Gets the sandwich sub title information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed DataSet</returns>
        public static FormMasterData GetSandwichSubTitleInformation(int form, int keyId, int userId)
        {
            return FormMasterComp.GetSandwichSubTitleInformation(form, keyId, userId);
        }

        #endregion GetSandwichSubTitleInformation

        #endregion FormMaster

        #region SupportForm

        #region GetFormDetails

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData GetFormDetails(int form, int userId)
        {
            return SupportFormCallComp.GetFormDetails(form, userId);
        }

        /// <summary>
        /// Gets the translated form details.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData GetTranslatedFormDetails(int formNo, string keyValue)
        {
            return SupportFormCallComp.GetTranslatedFormDetails(formNo, keyValue);
        }

        #endregion

        #region ListUserNames

        /// <summary>
        /// List UserNames
        /// </summary>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData ListUserNames()
        {
            return SupportFormCallComp.ListUserNames();
        }

        #endregion

        #region GetFormManagementDetails

        /// <summary>
        /// F9002_s the get form management details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public static SupportFormData F9002_GetFormManagementDetails(int form, int userId)
        {
            return SupportFormCallComp.F9002_GetFormManagementDetails(form, userId);
        }

        #endregion

        #endregion

        #region Deposit History

        #region List DepositHistroy Details

        /// <summary>
        /// Lists the deposit history details.
        /// </summary>
        /// <returns>the DataSet Which Holds the DepositHistoryDetails</returns>
        public static DepositHistoryData ListDepositHistoryDetails()
        {
            return DepositHistoryComp.ListDepositHistoryDetails();
        }

        #endregion

        #region Get DepositHistory Serach Results

        /// <summary>
        /// Gets the deposit history search result.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>DepositHistoryDataSet contains the resulted Search</returns>
        public static DepositHistoryData GetDepositHistorySearchResult(int form, string whereCondnSql)
        {
            return DepositHistoryComp.GetDepositHistorySearchResult(form, whereCondnSql);
        }

        #endregion

        #region Update Deposit History

        /// <summary>
        /// Updates the deposit history.
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        public static void UpdateDepositHistory(int clid, int userId)
        {
            DepositHistoryComp.UpdateDepositHistory(clid, userId);
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataset containing the AccountNames.</returns>
        public static DepositHistoryData.ListAccountNameDataTable ListAccountNames()
        {
            return DepositHistoryComp.ListAccountNames();
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
        public static LinearEventData GetLinearEventType(int eventId)
        {
            return LinearEventComp.GetLinearEventType(eventId);
        }

        #endregion

        #region Save Linear Event Type

        /// <summary>
        /// Save the Linear Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void SaveLinearEventType(string eventItems, int userId)
        {
            LinearEventComp.SaveLinearEventType(eventItems, userId);
        }

        #endregion

        #endregion

        #region Point Event Type

        #region Get Point Event Type

        /// <summary>
        /// Gets the Point Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Point Event Properties</returns>
        public static PointEventData GetPointEventType(int eventId)
        {
            return PointEventComp.GetPointEventType(eventId);
        }

        #endregion

        #region Save Point Event Type

        /// <summary>
        /// Save the Point Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void SavePointEventType(string eventItems, int userId)
        {
            PointEventComp.SavePointEventType(eventItems, userId);
        }

        #endregion

        #endregion

        #region 1211 DisbursementCheckStaging

        /// <summary>
        /// Gets the disbursement check list.
        /// </summary>
        /// <returns>DisbursementCheckStagingData Dataset</returns>
        public static DisbursementCheckStagingData F1211_GetDisbursementCheckList()
        {
            return DisbursementCheckComp.F1211_GetDisbursementCheckList();
        }

        /// <summary>
        /// Updates the check staging.
        /// </summary>
        /// <param name="tclId">The TCL id.</param>
        /// <param name="disbursementCheck">The disbursement check.</param>
        /// <param name="checkItems">The check items.</param>
        /// <param name="userId">UserID</param>
        public static void F1211_UpdateCheckStaging(int tclId, string disbursementCheck, string checkItems, int userId)
        {
            DisbursementCheckComp.F1211_UpdateCheckStaging(tclId, disbursementCheck, checkItems, userId);
        }

        /// <summary>
        /// Updates the agency is valid.
        /// </summary>
        /// <param name="tclIds">The TCL ids.</param>
        /// <param name="validStatus">The valid status.</param>
        /// <param name="userId">UserID</param>
        public static void F1211_UpdateAgencyValidStatus(string tclIds, int validStatus, int userId)
        {
            DisbursementCheckComp.F1211_UpdateAgencyValidStatus(tclIds, validStatus, userId);
        }

        /// <summary>
        /// F1211_s the delete check staging.
        /// </summary>
        /// <param name="tclIdDelete">The TCL id delete.</param>
        /// <param name="userId">UserID</param>
        public static void F1211_DeleteCheckStaging(string tclIdDelete, int userId)
        {
            DisbursementCheckComp.F1211_DeleteCheckStaging(tclIdDelete, userId);
        }

        /// <summary>
        /// F1211_s the create checks.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="createChecksTclId">The create checks TCL id.</param>
        /// <returns>Returns Count</returns>
        public static int F1211_CreateChecks(int userId, string createChecksTclId)
        {
            return DisbursementCheckComp.F1211_CreateChecks(userId, createChecksTclId);
        }

        #endregion

        #region Inspection Event

        #region List Inspection Details

        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Inspection Details</returns>
        public static InspectionEventData F8056_ListInspectionDetails(int eventId)
        {
            return InspectionEventComp.F8056_ListInspectionDetails(eventId);
        }

        #endregion

        #region Save Inspection Details

        /// <summary>
        /// Save the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void F8056_SaveInspectionDetails(string eventItems, int userId)
        {
            InspectionEventComp.F8056_SaveInspectionDetails(eventItems, userId);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// Updates the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">UserID</param>
        public static void F8056_UpdateInspectionDetails(string eventItems, int userId)
        {
            InspectionEventComp.F8056_UpdateInspectionDetails(eventItems, userId);
        }

        #endregion

        #region Delete a Inspection Item

        /// <summary>
        /// Deletes the Inspection Item
        /// </summary>
        /// <param name="inspectionId">The inspection id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F8056_DeleteInspectionDetails(int inspectionId, int userId)
        {
            return InspectionEventComp.F8056_DeleteInspectionDetails(inspectionId, userId);
        }
        #endregion

        #endregion

        #region Stoppage Event

        #region List Stoppage Event Details
        /// <summary>
        /// Get the Stoppage Event Record
        /// </summary>
        /// <param name="eventId">Event Id to be passed</param>
        /// <returns>StoppageEventData</returns>
        public static StoppageEventData F8016_GetStoppageEventDetails(int eventId)
        {
            return StoppageEventComp.F8016_GetStoppageEventDetails(eventId);
        }
        #endregion List Stoppage Event Details

        #region Save Stoppage Event Details
        /// <summary>
        /// Saves the details of Stoppage Event 
        /// </summary>
        /// <param name="eventItems">Dataset As string</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed dataset</returns>
        public static StoppageEventData F8016_SaveStoppageEventDetails(string eventItems, int userId)
        {
            return StoppageEventComp.F8016_SaveStoppageEventDetails(eventItems, userId);
        }
        #endregion Save Stoppage Event Details

        #endregion Stoppage Event

        #region Time Footer

        #region Get Time Footer Details
        /// <summary>
        /// Get the timer footer Details record
        /// </summary>
        /// <param name="eventId">Event Id to be passed</param>
        /// <param name="formId">The form id.</param>
        /// <returns>TimeFooterData - typed DataSet</returns>
        public static TimeFooterData F8042_GetTimeFooterDetails(int eventId, int formId)
        {
            return TimeFooterComp.F8042_GetTimeFooterDetails(eventId, formId);
        }
        #endregion Get Time Footer Details

        #endregion Time Footer

        #region Materials Footer

        #region Get Materials Footer Details
        /// <summary>
        /// Get the Materials footer Details record
        /// </summary>
        /// <param name="eventId">Event Id to be passed</param>
        /// <param name="formId">The form id.</param>
        /// <returns>Materials FooterData - typed DataSet</returns>
        public static MaterialsFooterData F8046_GetMaterialsFooterDetails(int eventId, int formId)
        {
            return MaterialsFooterComp.F8046_GetMaterialsFooterDetails(eventId, formId);
        }
        #endregion Get Materials Footer Details

        #endregion Materials  Footer

        #region F25000FieldUse

        public static F25000FieldUseData F25000_GetCheckOutDetails(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_GetCheckOutDetails(snapShotId, snapShotValue);
        }

        #endregion

        #region Get Parcel Header Details


        /// <summary>
        /// Get Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Parcel Header - typed DataSet</returns>
        public static F25000ParcelHeaderData F25000_GetParcelDetails(int parcelId)
        {
            return F25000ParcelHeaderComp.F25000_GetParcelDetails(parcelId);
        }

        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">UserID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public static int UpdateParcelHeaderDetails(int parcelId, string parcelDetails,bool isCopyHeader, int userId)
        {
            return F25000ParcelHeaderComp.UpdateParcelHeaderDetails(parcelId, parcelDetails,isCopyHeader,userId);
        }

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        public static F25000ParcelHeaderData ListPrimaryImprovement()
        {
            return F25000ParcelHeaderComp.ListPrimaryImprovement();
        }

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        public static F25000ParcelHeaderData ListPrimaryLandType()
        {
            return F25000ParcelHeaderComp.ListPrimaryLandType();
        }

        #endregion Get Parcel Header Details

        #region F26000ParcelheaderForm
        /// <summary>
        /// Get Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Parcel Header - typed DataSet</returns>
        public static F26000ParcelHeaderFormData F26000_GetParcelFormDetails(int parcelId)
        {
            return F26000ParcelHeaderFormComp.F26000_GetParcelFormDetails(parcelId);
        }

        /// <summary>
        /// F26000_s the exemption details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="exemptionFromAmount">The exemption from amount.</param>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_ExemptionDetails(int parcelId, string exemptionCode, decimal? exemptionFromAmount)
        {
            return F26000ParcelHeaderFormComp.F26000_ExemptionDetails(parcelId, exemptionCode, exemptionFromAmount);
        }
        
        /// <summary>
        /// F26000_s the exempt field details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exmptionId">The exmption id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_ExemptFieldDetails(int parcelId, int exmptionId, string exemptionCode)
        {
            return F26000ParcelHeaderFormComp.F26000_ExemptFieldDetails(parcelId, exmptionId, exemptionCode);
        }

        /// <summary>
        /// F26000_s the class code details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_ClassCodeDetails(string filterValue)
        {
            return F26000ParcelHeaderFormComp.F26000_ClassCodeDetails(filterValue);
        }
        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">UserID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        public static int UpdateParcelHeaderFormDetails(int parcelId, string parcelDetails, int userId, int rollYear)
        {
            return F26000ParcelHeaderFormComp.UpdateParcelHeaderFormDetails(parcelId, parcelDetails, userId, rollYear);
        }

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData PrimaryImprovementList()
        {
            return F26000ParcelHeaderFormComp.PrimaryImprovementList();
        }

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData PrimaryLandTypeList()
        {
            return F26000ParcelHeaderFormComp.PrimaryLandTypeList();
        }

        /// <summary>
        /// F26000_s the type of the get apprisal.
        /// </summary>
        /// <returns></returns>
        public static F26000ParcelHeaderFormData F26000_GetApprisalType()
        {
            return F26000ParcelHeaderFormComp.F26000_GetApprisalType();
        }

        /// <summary>
        /// F2101_s the get location selection.
        /// </summary>
        /// <param name="locationCode">The location code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static F2101LocationSelectionData f2101_GetLocationSelection(string locationCode, string description)
        {
            return F2101LocationSelectionComp.f2101_GetLocationSelection(locationCode, description);
        }

        /// <summary>
        /// F2102_s the get grouping selection.
        /// </summary>
        /// <param name="groupCode">The group code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static F2102GroupingSelectionData f2102_GetGroupingSelection(string groupCode, string description)
        {
            return F2102GroupingSelectionComp.f2102_GetGroupingSelection(groupCode, description);
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
        public static F2103ExemptionSelectionData f2103_GetExemptionSelection(string exemptionCode, string description, decimal? percent, decimal? maximum, int? rollYear)
        {
            return F2103ExemptionSelectionComp.f2103_GetExemptionSelection(exemptionCode, description, percent, maximum, rollYear);
        }

        public static DataSet ClassCode_RGB(string storedProcedureName)
        {
            return F26000ParcelHeaderFormComp.ClassCode_RGB(storedProcedureName);
        }
        #endregion

        #region F25051 Parcel Header Details

        /// <summary>
        /// Get Parcel Header Details F25051
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Parcel Header - typed DataSet</returns>
        public static F25051ParcelHeaderData F25051_GetParcelDetails(int parcelId)
        {
            return F25051ParcelHeaderComp.F25051_GetParcelDetails(parcelId);
        }

        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int f25051ParcelHeaderDetails(int parcelId, string parcelDetails, int userId)
        {
            return F25051ParcelHeaderComp.F25051ParcelHeaderDetails(parcelId, parcelDetails, userId);
        }

        /// <summary>
        /// Lists the OwnerOccupied Type.
        /// </summary>
        /// <returns></returns>
        public static F25051ParcelHeaderData F25051OwnerOccupied()
        {
            return F25051ParcelHeaderComp.F25051OwnerOccupied();
        }

        /// <summary>
        /// Lists the type of Parcel Class.
        /// </summary>
        /// <returns></returns>
        public static F25051ParcelHeaderData F25051ParcelClassTypes()
        {
            return F25051ParcelHeaderComp.F25051ParcelClassTypes();
        }

        #endregion F25051 Parcel Header Details

        #region Get ParcelType Details

        /// <summary>
        /// Get ParcelType Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>ParcelType - typed DataSet</returns>
        public static F2004ParcelCopyData GetParcelTypeDetails(int parcelId)
        {
            return F2004ParcelCopy.GetParcelTypeDetails(parcelId);
        }
        #endregion Get ParcelType Details

        #region Get Attachment Details

        /// <summary>
        /// Get ParcelType Details
        /// </summary>
        /// <param name="oldParcelID">The old parcel ID.</param>
        /// <param name="newParcelID">The new parcel ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="moduleID">The module ID.</param>
        /// <returns>ParcelType - typed DataSet</returns>
        public static F2004ParcelCopyData GetParcelAttachmentDetails(int oldParcelID, int newParcelID, int userID, int moduleID)
        {
            return F2004ParcelCopy.GetParcelAttachmentDetails(oldParcelID, newParcelID, userID, moduleID);
        }
        #endregion Get Attachment Details

        #region CreateNewParcelCopy

        /// <summary>
        /// Create Parcel Copy
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelTypeId">The parcel type id.</param>
        /// <param name="copyAllObjects">Copy Objects</param>
        /// <param name="copyAllSlices">Copy Slices</param>
        /// <param name="copyAttachments">Copy Attachments</param>
        /// <param name="copyComments">Copy Comments</param>
        /// <param name="parcelElements">Parcel Elements</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int CreateNewParcelCopy(int parcelId, int parcelTypeId, int copyAllObjects, int copyAllSlices, int copyAttachments, int copyComments, string parcelElements, int userId)
        {
            return F2004ParcelCopy.CreateNewParcelCopy(parcelId, parcelTypeId, copyAllObjects, copyAllSlices, copyAttachments, copyComments, parcelElements, userId);
        }

        #endregion CreateNewParcelCopy

        #region Get Parcel Locking Details

        /// <summary>
        /// Get Parcel Get Parcel Locking Details Details
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Parcel Header - typed DataSet</returns>
        public static F2001ParcelLockingData F2001_getParcelLockingUsername(int userId)
        {
            return F2001parcelLockingComp.F2001_getParcelLockingUsername(userId);
        }

        /// <summary>
        /// F2001_gets the parcel locking details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>Typed Dataset</returns>
        public static F2001ParcelLockingData F2001_getParcelLockingDetails(int keyId)
        {
            return F2001parcelLockingComp.F2001_getParcelLockingDetails(keyId);
        }

        /// <summary>
        /// F2001_s the update parcel locking details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="valueLock">The value lock.</param>
        /// <param name="adminLock">The admin lock.</param>
        /// <param name="lockAppraisal">The lock appraisal.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// Typed Dataset containing the Parcel Lock Details to Save
        /// </returns>
        public static int F2001_UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int userId)
        {
            return F2001parcelLockingComp.F2001_UpdateParcelLockingDetails(keyId, valueLock, adminLock, lockAppraisal, userId);
        }

        /// <summary>
        /// F2001_GetValidUserName
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="userId">userId</param>
        /// <param name="formNo">Form Number</param>
        /// <returns>F2001parcelLockingComp</returns>
        public static int F2001_GetValidUserName(int keyId, int userId, string formNo)
        {
            return F2001parcelLockingComp.F2001_GetValidUserName(keyId, userId, formNo);
        }

        #endregion Get Parcel Locking Details

        #region F15050FeeManageMent

        /// <summary>
        /// F15050FeeManagementData
        /// </summary>
        /// <returns>F15050ComboData DataSet</returns>
        public static F15050FeeManagementData F15050_ComboData()
        {
            return F15050FeeManagementComp.F15050_ComboData();
        }

        /// <summary>
        /// F15050FeeManagementData
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <returns>Typed Dataset containg the Fee Management details.</returns>
        public static F15050FeeManagementData F15050_getDatas(int feeId)
        {
            return F15050FeeManagementComp.F15050_getDatas(feeId);
        }

        /// <summary>
        /// F15050_s the save fee management.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed dataset</returns>
        public static int F15050_SaveFeeManagement(int feeId, string description, decimal amount, int accountId, int userId, byte feeTypeId)
        {
            return F15050FeeManagementComp.F15050_SaveFeeManagement(feeId, description, amount, accountId, userId, feeTypeId);
        }

        /// <summary>
        /// F15050_ApplyFees
        /// </summary>
        /// <param name="sysSnapshotId">sysSnapshotID</param>
        /// <param name="amount">amount</param>
        /// <param name="description">description</param>
        /// <param name="accountId">accountId</param>
        /// <param name="userId">UserID</param>
        /// <param name="feeTypeId">The fee type id.</param>
        /// <returns>int</returns>
        public static int F15050_ApplyFees(string feeXML, decimal amount, string description, int accountId, int userId)
        {
            return F15050FeeManagementComp.F15050_ApplyFees(feeXML, amount, description, accountId, userId);
        }

        /// <summary>
        /// F15050_s the list fee types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The fee mgmt dataset</returns>
        public static F15050FeeManagementData F15050_ListFeeTypes(int userId)
        {
            return F15050FeeManagementComp.F15050_ListFeeTypes(userId);
        }

        /// <summary>
        /// F15050_s the remove template.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="userId">The user id.</param>
        public static void F15050_RemoveTemplate(int feeId, int userId)
        {
            F15050FeeManagementComp.F15050_RemoveTemplate(feeId, userId);
        }

        #endregion

        #region Get Parcel Header(slim) Details

        /// <summary>
        /// Get Parcel Header(slim) Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Parcel Header slim - typed DataSet</returns>
        public static F27007ParcelHeaderSlimData F27007_GetParcelHeaderSlimDetails(int parcelId)
        {
            return F27007ParcelheaderSlimComp.F27007_GetParcelHeaderSlimDetails(parcelId);
        }

        #endregion Get Parcel Header(slim) Details

        #region Get statement Header(slim) Details

        /// <summary>
        /// Get Parcel Header(slim) Details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Parcel Header slim - typed DataSet</returns>
        public static F15016StatementHeaderData F15016_GetstatementHeaderSlimDetails(int statementId)
        {
            return F15016StatementheaderSlimComp.F15016_GetstatementHeaderSlimDetails(statementId);
        }
        #endregion Get statement Header(slim) Details

        #region Check Detail

        #region Get and List Check Detail

        /// <summary>
        /// Gets the Cash Ledger ID
        /// </summary>
        /// <returns>Cash Ledger ID</returns>
        public static CheckDetailData F1226_ListCashLedger()
        {
            return CheckDetailComp.F1226_ListCashLedger();
        }

        /// <summary>
        /// Gets the Cash Ledger(check) Detail
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public static CheckDetailData F1226_GetCashLedger(int clid)
        {
            return CheckDetailComp.F1226_GetCashLedger(clid);
        }

        #endregion

        #region Update Check Detail And Status

        /// <summary>
        /// F1221_s the update cash ledger.
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="overRide">The over ride.</param>
        /// <param name="checkDetails">The check details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>1 if checkno already exist else 0</returns>
        public static int F1221_UpdateCashLedger(int clid, int overRide, string checkDetails, int userId)
        {
            return CheckDetailComp.F1221_UpdateCashLedger(clid, overRide, checkDetails, userId);
        }

        /// <summary>
        /// Updates the Cash Ledger Status
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="functionDate">The function date.</param>
        /// <param name="functionId">The function id.</param>
        /// <param name="loginUserId">UserID</param>
        public static void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId)
        {
            CheckDetailComp.F1226_UpdateCashLedgerStatus(clid, userId, functionDate, functionId, loginUserId);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">UserID</param>
        public static void F1226_DeleteCashLedger(int clid, int userId)
        {
            CheckDetailComp.F1226_DeleteCashLedger(clid, userId);
        }

        #endregion

        #endregion Check Detail

        #region 1210 Disbursement

        #region List Disbursement Agency/SubFund Details

        /// <summary>
        /// Gets the disbursement details.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>Disbursement DataSet</returns>
        public static DisbursementData F1210_GetDisbursementDetails(DateTime postDate)
        {
            return DisbursementComp.F1210_GetDisbursementDetails(postDate);
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataTable containing the AccountNames.</returns>
        public static DisbursementData.ListAccountNameDataTable F1210_DisbursementAccountNames()
        {
            return DisbursementComp.F1210_DisbursementAccountNames();
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
        public static int F1210_SaveDisbursement(int registerId, int userId, DateTime postDate, string agencies, int overrideStatus)
        {
            return DisbursementComp.F1210_SaveDisbursement(registerId, userId, postDate, agencies, overrideStatus);
        }

        #endregion

        #endregion

        #region 1214 Refund Management

        #region List AccontNames

        /// <summary>
        /// F1214 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static RefundManagementData.ListAccountNamesDataTable F1214_AccountNames()
        {
            return RefundManagementComp.F1214_AccountNames();
        }

        #endregion

        #region List RefundPayments

        /// <summary>
        /// Lists the refund payments data.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>RefundManagementtsDataSet</returns>
        public static RefundManagementData.ListRefundPaymentsDataTable ListRefundPayments(int form, string whereCondnSql)
        {
            return RefundManagementComp.ListRefundPaymentsData(form, whereCondnSql);
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
        /// <returns>ErrorID</returns>
        public static int F1214_PrepareChecks(int registerId, int ownerId, DateTime interestDate, int userId, string paymentItems)
        {
            return RefundManagementComp.F1214_PrepareChecks(registerId, ownerId, interestDate, userId, paymentItems);
        }

        #endregion

        #endregion

        #region 8040 Time

        #region List Time

        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>List TimeDataTable </returns>
        public static F8040TimeData F8040_ListTimeInformation(int formId, int keyId)
        {
            return F8040TimeComp.F8040_ListTimeInformation(formId, keyId);
        }
        #endregion List Time

        #region List Time Resource
        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="isactive">The active value</param>
        /// <returns>ListTimeDataTable</returns>
        public static F8040TimeData F8040_ListTimeResourceInformation(int isactive)
        {
            return F8040TimeComp.F8040_ListTimeResourceInformation(isactive);
        }
        #endregion List Time Resource

        #region Save
        /// <summary>
        /// F8040_s the save time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">UserID</param>
        public static void F8040_SaveTime(string timeDetails, int userId)
        {
            F8040TimeComp.F8040_SaveTime(timeDetails, userId);
        }
        #endregion Save

        #region Update
        /// <summary>
        /// F8040_s the update time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">UserID</param>
        public static void F8040_UpdateTime(string timeDetails, int userId)
        {
            F8040TimeComp.F8040_UpdateTime(timeDetails, userId);
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// F8040_s the delete time.
        /// </summary>
        /// <param name="timeId">The time id.</param>
        /// <param name="userId">UserID</param>
        public static void F8040_DeleteTime(int timeId, int userId)
        {
            F8040TimeComp.F8040_DeleteTime(timeId, userId);
        }
        #endregion Delete

        #region CheckEventId

        /// <summary>
        /// F8040_s the check event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Integer</returns>
        public static int F8040_CheckEventId(int formId, int keyId)
        {
            return F8040TimeComp.F8040_CheckEventId(formId, keyId);
        }

        #endregion CheckEventId

        #endregion 8040 Time

        #region 8044 Materials

        #region List Material Details

        /// <summary>
        /// Lists the EventEngineTVDetails
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>returns dataset contains EventEngine TVDetails</returns>
        public static F8044MaterialsData F8044_ListMaterialDetails(int formId, int keyId)
        {
            return F8044MaterialsComp.F8044_ListMaterialDetails(formId, keyId);
        }

        #endregion

        #region List Materials Resource Types

        /// <summary>
        /// Lists the Materials Resource Type
        /// </summary>
        /// <param name="flagActiveAndAll">The flag active and all.</param>
        /// <param name="eventId">The event id.</param>
        /// <returns>
        /// returns dataset contains Materials Resource Type
        /// </returns>
        public static F8044MaterialsData F8044_ListMaterialsResourceType(int flagActiveAndAll, int eventId)
        {
            return F8044MaterialsComp.F8044_ListMaterialsResourceType(flagActiveAndAll, eventId);
        }

        #endregion

        #region Save Material Details

        /// <summary>
        /// Save the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">UserID</param>
        public static void F8044_SaveMaterialDetails(string materialItems, int userId)
        {
            F8044MaterialsComp.F8044_SaveMaterialDetails(materialItems, userId);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// Updates the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">UserID</param>
        public static void F8044_UpdateMaterialDetails(string materialItems, int userId)
        {
            F8044MaterialsComp.F8044_UpdateMaterialDetails(materialItems, userId);
        }

        #endregion

        #region Delete a Material Item

        /// <summary>
        /// Deletes the Material Item
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F8044_DeleteMaterialItem(int materialId, int userId)
        {
            return F8044MaterialsComp.F8044_DeleteMaterialItem(materialId, userId);
        }
        #endregion

        #endregion

        #region 8092 EventHeader

        #region Get

        /// <summary>
        /// F8902 GetHeader
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>Datatable</returns>
        public static F8902HeaderData F8902_GetHeader(int workId)
        {
            return F8092HeaderComp.F8902_GetWorkOrderHeader(workId);
        }

        #endregion

        #region Save

        /// <summary>
        /// F8902 SaveHeader
        /// </summary>
        /// <param name="headerDetails">headerDetails</param>
        /// <param name="userId">UserID</param>
        public static void F8902_SaveHeader(string headerDetails, int userId)
        {
            F8092HeaderComp.F8902_SaveWorkOrderHeader(headerDetails, userId);
        }

        #endregion

        #region Delete

        /// <summary>
        /// F8902 DeleteHeader
        /// </summary>
        /// <param name="workId">workId</param>
        /// <param name="userId">UserID</param>
        public static void F8902_DeleteHeader(int workId, int userId)
        {
            F8092HeaderComp.F8902_DeleteWorkOrderHeader(workId, userId);
        }

        #endregion

        #endregion

        #region 1220 Account Register

        #region List AccontNames

        /// <summary>
        /// F1220_s the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static F1220AccountRegisterData.ListAccountNamesDataTable F1220_AccountNames()
        {
            return F1220AccountRegisterComp.F1220_AccountNames();
        }

        #endregion

        #region Get ReconciledDetails

        /// <summary>
        /// F1220_s the get reconciled details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>datatable holds the Reconciled Details</returns>
        public static F1220AccountRegisterData.ReconciledDetailsDataTable F1220_GetReconciledDetails(int registerId)
        {
            return F1220AccountRegisterComp.F1220_GetReconciledDetails(registerId);
        }

        #endregion

        #region List AccountRegister

        /// <summary>
        /// F1220_s the list account register.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>datatable contains the Account Register Details</returns>
        public static F1220AccountRegisterData.ListAccountRegisterDataTable F1220_ListAccountRegister(int registerId, DateTime beginningDate)
        {
            return F1220AccountRegisterComp.F1220_ListAccountRegister(registerId, beginningDate);
        }

        #endregion

        #region GetAccountRegisterDetails

        /// <summary>
        /// F1220_s the get account register details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>accountRegister DataSet</returns>
        public static F1220AccountRegisterData F1220_GetAccountRegisterDetails(int registerId, DateTime beginningDate)
        {
            return F1220AccountRegisterComp.F1220_GetAccountRegisterDetails(registerId, beginningDate);
        }

        #endregion

        #endregion

        #region F8904 Event Grid

        #region Get

        /// <summary>
        /// F8904_s the list Event Grid Details
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>Dataset Containing Grid Details</returns>
        public static F8904EventGridData F8904_GetEventGridDetails(int workId)
        {
            return F8904EventGridComp.F8904_GetEventGrid(workId);
        }

        #endregion

        #endregion

        #region 9002 GetUserDetails

        /// <summary>
        /// F9001_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns> User Details </returns>
        public static UserManagementData F9002_GetUserDetails(int applicationId)
        {
            return UserManagementComp.F9002_GetUserDetails(applicationId);
        }

        #endregion

        #region 1224 Check Print Queue

        #region List AccontNames

        /// <summary>
        /// F1224 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static F1224CheckPrintQueueData.ListAccountNamesDataTable F1224_AccountNames()
        {
            return F1224CheckPrintQueueComp.F1224_AccountNames();
        }

        #endregion

        #region List Get Check Number

        /// <summary>
        /// F1224_s the get check number.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <returns>Check Numbers</returns>
        public static F1224CheckPrintQueueData F1224_GetCheckNumber(int registerId)
        {
            return F1224CheckPrintQueueComp.F1224_GetCheckNumber(registerId);
        }

        #endregion

        #region List UnPrinted Checks

        /// <summary>
        /// F1224_s the list un printed checks.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <returns>Un Printed Checks</returns>
        public static F1224CheckPrintQueueData F1224_ListUnPrintedChecks(int registerId)
        {
            return F1224CheckPrintQueueComp.F1224_ListUnPrintedChecks(registerId);
        }

        #endregion

        #region Print Checks

        /// <summary>
        /// F1224_s the create checks.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="startingCheckNumber">The starting check number.</param>
        /// <param name="checkItems">The check items.</param>
        /// <returns>printed Check Numbers</returns>
        public static F1224CheckPrintQueueData F1224_CreateChecks(int registerId, int userId, Int64 startingCheckNumber, string checkItems)
        {
            return F1224CheckPrintQueueComp.F1224_CreateChecks(registerId, userId, startingCheckNumber, checkItems);
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
        public static F1502AccountManagementData F1502_GetAccountElementMgmt(string function, string description, int type)
        {
            return F1502AccountManagementComp.F1502_GetAccountElementMgmt(function, description, type);
        }

        #endregion GetAccountElementMgmt

        #region SaveAccountElementMgmt

        /// <summary>
        /// To Save Account Element Management details
        /// </summary>
        /// <param name="functionElemnts">The xml string which contains the Account elements mgmt Grid values</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public static int F1502_SaveAccountElementMgmt(string functionElemnts, int userId)
        {
            return F1502AccountManagementComp.F1502_SaveAccountElementMgmt(functionElemnts, userId);
        }

        #endregion SaveAccountElementMgmt

        #region DeleteAccountElementMgmt

        /// <summary>
        /// To Delete Account Element Management details
        /// </summary>
        /// <param name="functionId">The Functional Id</param>
        /// <param name="userId">UserID</param>
        public static void F1502_DeleteAccountElementMgmt(string functionId, int userId)
        {
            F1502AccountManagementComp.F1502_DeleteAccountElementMgmt(functionId, userId);
        }

        #endregion DeleteAccountElementMgmt

        #endregion F1502 Account Element Management

        #region F9600 Search Engine

        /// <summary>
        /// F9600s the list sort result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <param name="searchOrder">if set to <c>true</c> [search order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        /// <returns>F9600SearchData DataSet</returns>
        public static F9600SearchData F9600ListSortResult(string searchValue, int appId, bool searchOrder, bool groupOrder)
        {
            return F9600SearchEngineComp.F9600_ListSortResult(searchValue, appId, searchOrder, groupOrder);
        }

        /// <summary>
        /// F9600s the list Sort search result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <returns>the datatable contains the Search Value and ApplicationID</returns>
        public static F9600SearchData F9600ListSearchResult(string searchValue, int appId)
        {
            return F9600SearchEngineComp.F9600_ListSearchResult(searchValue, appId);
        }

        #endregion

        #region 1530 Cash Account Management

        #region Get and List Institution

        /// <summary>
        /// Gets the institution list, institution detail, cash account list and institution contact list
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <returns>F1530CashAccountManagementData with institution Detail</returns>
        public static F1530CashAccountManagementData F1530_GetInstitutionDetail(int institutionId)
        {
            return F1530CashAcctMgmtComp.F1530_GetInstitutionDetail(institutionId);
        }

        #endregion

        #region Save Institution

        /// <summary>
        /// saves the institution
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="institutionElements">The institution elements.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved institution id</returns>
        public static int F1530_SaveInstitution(int institutionId, string institutionElements, int userId)
        {
            return F1530CashAcctMgmtComp.F1530_SaveInstitution(institutionId, institutionElements, userId);
        }

        #endregion

        #endregion

        #region 1531 Cash Account

        #region Get Cash Account

        /// <summary>
        /// Gets the cash account detail
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with cash account Detail
        /// </returns>
        public static F1530CashAccountManagementData F1531_GetCashAccountDetail(int registerId)
        {
            return F1530CashAcctMgmtComp.F1531_GetCashAccountDetail(registerId);
        }

        #endregion

        #region Save Cash Account

        /// <summary>
        /// saves cash account.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="registerItems">The register items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>subfund validated result,-1- validation failed else registerId</returns>
        public static int F1531_SaveCashAccount(int registerId, string registerItems, int userId)
        {
            return F1530CashAcctMgmtComp.F1531_SaveCashAccount(registerId, registerItems, userId);
        }

        #endregion

        #endregion

        #region 1532 Institution Contact

        #region Get Institution Contact

        /// <summary>
        /// Gets the institution contact detail
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with institution contact Detail
        /// </returns>
        public static F1530CashAccountManagementData F1532_GetInstitutionContactDetail(int contactId)
        {
            return F1530CashAcctMgmtComp.F1532_GetInstitutionContactDetail(contactId);
        }

        #endregion

        #region Save Institution Contact

        /// <summary>
        /// saves the Institution Contact.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved contact id</returns>
        public static int F1532_SaveInstitutionContact(int contactId, string acctEmelemts, int userId)
        {
            return F1530CashAcctMgmtComp.F1532_SaveInstitutionContact(contactId, acctEmelemts, userId);
        }

        #endregion

        #endregion

        #region D1030SpecialDistrict

        #region F1030SpecialDistrictDefinition

        #region ListMethods

        /// <summary>
        /// F1030_s the type of the list district definition.
        /// </summary>
        /// <returns>SpecialDistrict dataset</returns>
        public static F1030SpecialDistrictDefinitionData F1030_ListDistrictDefinitionType()
        {
            return F1030SpecialDistrictDefinitionComp.F1030_ListDistrictDefinitionType();
        }

        #endregion ListMethods

        #region GetMethods

        /// <summary>
        /// F1030_s the get district definition details.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <returns>returns SpecialDistrictDefinition Dataset</returns>
        public static F1030SpecialDistrictDefinitionData F1030_GetDistrictDefinitionDetails(int districtNo)
        {
            return F1030SpecialDistrictDefinitionComp.F1030_GetDistrictDefinitionDetails(districtNo);
        }

        #endregion GetMethods

        #region DeleteMethods

        /// <summary>
        /// F1030_s the delete district definition.
        /// </summary>
        /// <param name="specialDistrictId">The special district ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Returns DistrictId</returns>
        public static int F1030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            return F1030SpecialDistrictDefinitionComp.F1030_DeleteDistrictDefinition(specialDistrictId, userId);
        }

        /// <summary>
        /// F1030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item ID.</param>
        /// <param name="userId">UserID</param>
        public static void F1030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            F1030SpecialDistrictDefinitionComp.F1030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
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
        /// <param name="userId">UserID</param>
        /// <returns>Returns SaveDistrict dataset</returns>
        public static string F1030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, int userId)
        {
            return F1030SpecialDistrictDefinitionComp.F1030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, userId);
        }

        #endregion SaveMethods

        #endregion F1030SpecialDistrictDefinition

        #region F16030SpecialDistrictDefinition

        #region ListMethods

        /// <summary>
        /// F16030_s the type of the list district definition.
        /// </summary>
        /// <returns>SpecialDistrict dataset</returns>
        public static F1030SpecialDistrictDefinitionData F16030_ListDistrictDefinitionType()
        {
            return F16030SpecialDistrictDefinitionComp.F16030_ListDistrictDefinitionType();
        }

        #endregion ListMethods

        #region GetMethods

        /// <summary>
        /// F16030_s the get district definition details.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <returns>returns SpecialDistrictDefinition Dataset</returns>
        public static F1030SpecialDistrictDefinitionData F16030_GetDistrictDefinitionDetails(int districtNo)
        {
            return F16030SpecialDistrictDefinitionComp.F16030_GetDistrictDefinitionDetails(districtNo);
        }

        #endregion GetMethods

        #region DeleteMethods

        /// <summary>
        /// F16030_s the delete district definition.
        /// </summary>
        /// <param name="specialDistrictId">The special district ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Returns DistrictId</returns>
        public static int F16030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            return F16030SpecialDistrictDefinitionComp.F16030_DeleteDistrictDefinition(specialDistrictId, userId);
        }

        /// <summary>
        /// F16030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item ID.</param>
        /// <param name="userId">UserID</param>
        public static void F16030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            F16030SpecialDistrictDefinitionComp.F16030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
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
        /// <param name="flagOverride">if set to <c>true</c> [flag override].</param>
        /// <param name="flagValidation">if set to <c>true</c> [flag validation].</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// KeyId</returns>
        public static string F16030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, bool flagValidation, int userId)
        {
            return F16030SpecialDistrictDefinitionComp.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, flagValidation, userId);
        }

        #endregion SaveMethods

        #endregion F16030SpecialDistrictDefinition

        #region F1033SpecialDistrictSelection

        #region ListPostTypes

        /// <summary>
        /// F1033_s the list post types.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// Typed Dataset Contains the List Of PostTypes
        /// </returns>
        public static F1033SpecialDistrictSelectionData F1033_ListPostTypes(int? form)
        {
            return F1033SpecialDistrictSelectionComp.ListPostTypes(form);
        }

        #endregion ListPostTypes

        #region ListSpecialDistricts
        /// <summary>
        /// List the Special Districts
        /// </summary>
        /// <param name="district">district</param>
        /// <param name="rollYear">rollYear</param>
        /// <param name="description">description</param>
        /// <param name="postTypeId">The post type id.</param>
        /// <returns>
        /// Typed Dataset Contains List of Special Districts
        /// </returns>
        public static F1033SpecialDistrictSelectionData F1033_ListSpecialDistricts(int? district, int? rollYear, string description, int? postTypeId)
        {
            return F1033SpecialDistrictSelectionComp.ListSpecialDistricts(district, rollYear, description, postTypeId);
        }
        #endregion ListSpecialDistricts
        #endregion F1033SpecialDistrictSelection

        #endregion D1030SpecialDistrict

        #region F1500AccountManagement

        #region Getdescription

        /// <summary>
        /// F1500_s the get description.
        /// </summary>
        /// <param name="keyId">The key ID.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>description</returns>
        public static AccountManagementData F1500_GetDescription(string keyId, string elementName)
        {
            return F1500AccountManagementComp.F1500_GetDescription(keyId, elementName);
        }

        #endregion.

        #region Get SubFund Items

        /// <summary>
        /// F1500_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Subfund Items</returns>
        public static AccountManagementData F1500_GetSubFundItems(string subFund, short rollYear)
        {
            return F1500AccountManagementComp.F1500_GetSubFundItems(subFund, rollYear);
        }

        #endregion

        #region Get Function Items

        /// <summary>
        /// F1500_s the get function items.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>Function Items</returns>
        public static AccountManagementData F1500_GetFunctionItems(string function)
        {
            return F1500AccountManagementComp.F1500_GetFunctionItems(function);
        }

        #endregion

        #region Get AccountIDs and Details

        /// <summary>
        /// F1500_s the list account details.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <returns>AcountIDs and Details</returns>
        public static AccountManagementData F1500_ListAccountDetails(int accountId)
        {
            return F1500AccountManagementComp.F1500_ListAccountDetails(accountId);
        }

        #endregion

        #region Save and Edit the Account Details

        /// <summary>
        /// F1500_s the create or edit account.
        /// </summary>
        /// <param name="accountId">The account ID.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">UserID</param>
        /// <returns>accountID</returns>
        public static int F1500_CreateOrEditAccount(int accountId, string acctEmelemts, int userId)
        {
            return F1500AccountManagementComp.F1500_CreateOrEditAccount(accountId, acctEmelemts, userId);
        }

        #endregion

        #region List Register type

        /// <summary>
        /// List the register types.
        /// </summary>
        /// <returns>AccountManagementData with register type</returns>
        public static AccountManagementData F1500_ListRegisterType()
        {
            return F1500AccountManagementComp.F1500_ListRegisterType();
        }

        #endregion

        #region Get Configuration Value

        /// <summary>
        /// F1500_s the get configuration value.
        /// </summary>
        /// <param name="cfgName">Name of the CFG.</param>
        /// <returns>Config Value</returns>
        public static AccountManagementData F1500_GetConfigurationValue(string cfgName)
        {
            return F1500AccountManagementComp.F1500_GetConfigurationValue(cfgName);
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
        public static F1503GenericManagementData F1503_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            return F1503GenericManagementComp.F1503_GetGenericElementMgmt(keyValue, description, formName);
        }

        #endregion GetGenericElementMgmt

        #region SaveGenericElementMgmt

        /// <summary>
        /// To Save the Generic Element Management details
        /// </summary>
        /// <param name="functionElemnts">The Xml string containing Element ID and Description Value</param>
        /// <param name="formName">The Form name</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public static int F1503_SaveGenericElementMgmt(string functionElemnts, string formName, int userId)
        {
            return F1503GenericManagementComp.F1503_SaveGenericElementMgmt(functionElemnts, formName, userId);
        }

        #endregion SaveGenericElementMgmt

        #region DeleteGenericElementMgmt

        /// <summary>
        /// To Delete the Generic Element Management details
        /// </summary>
        /// <param name="elementId">The Particular Element ID</param>
        /// <param name="formName">The Form name</param>
        /// <param name="userId">UserID</param>
        public static void F1503_DeleteGenericElementMgmt(string elementId, string formName, int userId)
        {
            F1503GenericManagementComp.F1503_DeleteGenericElementMgmt(elementId, formName, userId);
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
        public static F1515SubFundSelectionData F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            return F1515SubFundSelectionComp.F1515_GetSubFundSelection(subFund, description, rollYear, iscash);
        }

        #endregion F1515_GetSubFundSelection

        #endregion F1515 Sub Fund Selection

        #region Improvement District Definition.
        /// <summary>
        /// Lists interest method.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition ListInterestMethod()
        {
            return F16040ImproveDistrictDefinitionComp.ListInterestMethod();
        }

        /// <summary>
        /// Get District Details.
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition GetDistrictDetails(int districtId)
        {
            return F16040ImproveDistrictDefinitionComp.GetDistrictDetails(districtId);
        }

        /// <summary>
        /// Lists Delq interest details.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition ListInterestDelqDetails()
        {
            return F16040ImproveDistrictDefinitionComp.ListInterestDelqDetails();
        }

        /// <summary>
        /// Improvement District Type list.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition ImprovementDistrictTypelist(string districtType)
        {
            return F16040ImproveDistrictDefinitionComp.ImprovementDistrictTypelist(districtType);
        }

        /// <summary>
        /// District Distribution Type list.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition GetDistributionDetails()
        {
            return F16040ImproveDistrictDefinitionComp.GetDistributionDetails();
        }

        /// <summary>
        /// District Definition Details list.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition GetDistrictDefinitionDetails(int districtID)
        {
            return F16040ImproveDistrictDefinitionComp.GetDistrictDefinitionDetails(districtID);
        }

        /// <summary>
        /// Improvement District Type list.
        /// </summary>
        /// <returns></returns>
        public static F16040ImprovementDistrictDefinition RollOver_ImprovementDistrict(int districtId,int userId)
        {
            return F16040ImproveDistrictDefinitionComp.RollOver_ImprovementDistrict(districtId,userId);
        }

        /// <summary>
        /// Improvement District Type list.
        /// </summary>
        /// <returns></returns>
        public static string F16040_SaveImproveDistrictDefinition(string districtItem,string distributionItem, int userId)
        {
            return F16040ImproveDistrictDefinitionComp.F16040_SaveImproveDistrictDefinition(districtItem,distributionItem, userId);
        }

        /// <summary>
        /// Update Improvement District Details.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="districtItem"></param>
        /// <param name="distributionItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string F16040_UpdateImproveDistrictDefinition(int districtNo, string districtItem, string distributionItem, int userId)
        {
            return F16040ImproveDistrictDefinitionComp.F16040_UpdateImproveDistrictDefinition(districtNo, districtItem, distributionItem, userId);
        }

        #endregion Improvement District Definition.

        #region Improvement District Parcels.

        /// <summary>
        /// District Definition Details list.
        /// </summary>
        /// <returns></returns>
        public static F16041ImprovementDistrictParcels GetDistrictParcels(int districtID)
        {
            return F16041ImprovementDistrictParcelsComp.GetDistrictParcels(districtID);
        }

        /// <summary>
        /// List selected parcel details.
        /// </summary>
        /// <returns></returns>
        public static F16041ImprovementDistrictParcels ListDistrictParcelsDetails(string parcelval, int? parcelId, int? rollYear)
        {
            return F16041ImprovementDistrictParcelsComp.ListDistrictParcelsDetails(parcelval, parcelId, rollYear);
        }

        /// <summary>
        /// Save Improvement District Parcels.
        /// </summary>
        /// <returns></returns>
        public static string F16041_SaveDistrictParcels(string districtProperty, int userId)
        {
            return F16041ImprovementDistrictParcelsComp.F16041_SaveDistrictParcels(districtProperty,userId);
        }

        /// <summary>
        /// F16030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item ID.</param>
        /// <param name="userId">UserID</param>
        public static string F16041_DeleteDistrictParcels(int workingFileItemId, int userId)
        {
            return F16041ImprovementDistrictParcelsComp.F16041_DeleteDistrictParcels(workingFileItemId, userId);
        }

        /// <summary>
        /// Check Parcel Details.
        /// </summary>
        /// <returns></returns>
        public static string CheckParcelDetails(string districtProperty)
        {
            return F16041ImprovementDistrictParcelsComp.CheckParcelDetails(districtProperty);
        }

        #endregion

        #region F1513 Fund Selection

        #region F1513_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        public static F1513FundSelectionData F1513_GetFundSelection(string fund, string description)
        {
            return F1513FundSelectionComp.F1513_GetFundSelection(fund, description);
        }

        /// <summary>
        /// F1513_CentralFundItemValidation
        /// </summary>
        /// <param name="fundId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        public static int F1513_CentralFundItemValidation(int fundId, int rollYear)
        {
            return F1513FundSelectionComp.F1513_CentralFundItemValidation(fundId, rollYear);
        }
        #endregion F1513_GetFundSelection

        #endregion F1513 Fund Selection

        #region 16031 Special District Assessment for working FileID


        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public static F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessmentDetails(int workingfileId)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_ListDistrictAssessmentDetails(workingfileId);
        }

        public static F1031SpecialDistrictAssessmentData F16031_GetSpecialAssessmentParcel(string parcelNumber, int? parcelId, int? rollYear)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_GetSpecialAssessmentParcel(parcelNumber, parcelId, rollYear);
        }
        public static F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessment(int districtId)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_ListDistrictAssessment(districtId);
        }
        public static int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, int userId)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_SaveDistrictAssessmentDetails(districtProperty, districtRates, userId);
        }
        public static F1031SpecialDistrictAssessmentData F16031_DeleteDistrictAssessment(int statementId, int userId)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_DeleteDistrictAssessment(statementId, userId);
        }

        public static F1031SpecialDistrictAssessmentData F16031_CheckSpecialAssessment(string districtProperty)
        {
            return F1031SpecialDistrictAssessmentComp.F16031_CheckSpecialAssessment(districtProperty);
        }

        public static void F16031_ExeWriteTaxStatement(int workingfileId, int userId, bool isCancel)
        {
            F1031SpecialDistrictAssessmentComp.F16031_ExeWriteTaxStatement(workingfileId, userId, isCancel);
        }
        #endregion 16031 Special District Assessment for working FileID

        #region 1031 Special District Assessment

        #region List Special District Assessment Details

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentDetails(int statementId)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_ListDistrictAssessmentDetails(statementId);
        }

        #endregion

        #region List Special District Assessment IDs

        /// <summary>
        /// F1031_s the list district assessment I ds.
        /// </summary>
        /// <returns>
        /// returns dataset containing District Assessment IDs
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentIDs()
        {
            return F1031SpecialDistrictAssessmentComp.F1031_ListDistrictAssessmentIDs();
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
        public static F1031SpecialDistrictAssessmentData F1031_GetDistrictAssessmentParcelID(string parcelNumber, int? parcelId, int? rollYear)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_GetDistrictAssessmentParcelID(parcelNumber, parcelId, rollYear);
        }

        #endregion

        #region List Special District

        /// <summary>
        /// F1031_s the list district assessment.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>
        /// returns dataset containing specialDistrict Details
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessment(int districtId)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_ListDistrictAssessment(districtId);
        }

        #endregion

        #region Save District Assessment Details

        /// <summary>
        /// F1031_s the save district assessment details.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="overrideStatus">if set to <c>true</c> [override status].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">UserID</param>
        /// <returns>Key ID</returns>
        public static int F1031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool overrideStatus, bool ownerRide, int userId)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_SaveDistrictAssessmentDetails(districtProperty, districtRates, overrideStatus, ownerRide, userId);
        }

        #endregion

        #region Delete District Assessment

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F1031_DeleteDistrictAssessment(int statementId, int userId)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_DeleteDistrictAssessment(statementId, userId);
        }
        #endregion

        #region Check Duplicate Statement/Owner

        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public static int F1031_CheckSpecialDistrictStatementOrOwner(string districtProperty, bool statementFlag)
        {
            return F1031SpecialDistrictAssessmentComp.F1031_CheckSpecialDistrictStatementOrOwner(districtProperty, statementFlag);
        }

        #endregion

        #endregion

        #region 9503 SubFund Management

        #region List SubFund Details

        /// <summary>
        /// F9503_s the get sub fund management details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <returns>DataSet F9503SubFungMgmtData</returns>
        public static F9503SubFundManagementData F9503_GetSubFundManagementDetails(int? subFundId)
        {
            return F9503SubFundMgmtComp.F9503_GetSubFundManagementDetails(subFundId);
        }

        #endregion

        #region Get SubFund Items

        /// <summary>
        /// F9503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Subfund Items</returns>
        public static F9503SubFundManagementData F9503_GetSubFundItems(string subFund, short rollYear)
        {
            return F9503SubFundMgmtComp.F9503_GetSubFundItems(subFund, rollYear);
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
        public static int F15005_CheckSubFund(int? subFundId, string subFund, int rollYear)
        {
            return F9503SubFundMgmtComp.F15005_CheckSubFund(subFundId, subFund, rollYear);
        }

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <param name="userId">UserID</param>
        /// <returns>returns primaryId</returns>
        public static int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments, int userId)
        {
            return F9503SubFundMgmtComp.F9503_CreateOrEditSubFund(subFundId, subFundElments, userId);
        }

        #endregion

        #endregion

        #region F1501 General Ledger Configuration

        #region List RollYear

        /// <summary>
        /// F1501_s the list roll year.
        /// </summary>
        /// <returns>GLConfiguration dataset</returns>
        public static F1501GLConfigurationData F1501_ListRollYear()
        {
            return F1501GLConfigurationComp.F1501_ListRollYear();
        }

        #endregion

        #region List GL Config Details

        /// <summary>
        /// F1501_s the list GL config details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns GLConfiguration dataset</returns>
        public static F1501GLConfigurationData F1501_ListGLConfigDetails(int rollYear)
        {
            return F1501GLConfigurationComp.F1501_ListGLConfigDetails(rollYear);
        }

        #endregion

        #region Get GL config Details

        /// <summary>
        /// F1501_s the get GL config details.
        /// </summary>
        /// <param name="configId">The config ID.</param>
        /// <returns>returns GLConfiguration dataset</returns>
        public static F1501GLConfigurationData F1501_GetGLConfigDetails(int configId)
        {
            return F1501GLConfigurationComp.F1501_GetGLConfigDetails(configId);
        }

        #endregion

        #region Save and Edit the Account Details

        /// <summary>
        /// F1501_s the create or edit GL config details.
        /// </summary>
        /// <param name="configId">The config ID.</param>
        /// <param name="configElements">The config elements.</param>
        /// <param name="userId">UserID</param>
        /// <returns>GL config Details</returns>
        public static int F1501_CreateOrEditGLConfigDetails(int configId, string configElements, int userId)
        {
            return F1501GLConfigurationComp.F1501_CreateOrEditGLConfigDetails(configId, configElements, userId);
        }

        #endregion

        #endregion

        #region F9080 Roll year Management form

        /// <summary>
        /// F9080_s the get RollYear Management Data.
        /// </summary>
        /// <param name="RollYear">The Roll Year.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>Returns Roll Year DataSet</returns>
        public static F9080RollYearManagementData F9080_GetRollYearManagement(short rollYear, int userId)
        {
            return F9080RollYearManagementComp.F9080_GetRollYearManagement(rollYear, userId);
        }

        /// <summary>
        /// F9080_s the list Roll Year Management.
        /// </summary>
        /// <param name="User ID">The User Id.</param>
        /// <returns>Returns OwnerReceipting DataSet</returns>
        public static F9080RollYearManagementData F9080_ListRollYearManagement(int userId)
        {
            return F9080RollYearManagementComp.F9080_ListRollYearManagement(userId);
        }

        /// <summary>
        /// F9080_s the execute Roll Year Steps.
        /// </summary>
        /// <param name="ownerId">The RollOver id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Returns Steps Dataset</returns>
        public static string F9080_ExecRollYearStep(short rollOverId, int userId)
        {
            return F9080RollYearManagementComp.F9080_ExecRollYearStep(rollOverId, userId);
        }

        #endregion F9080 Roll year Mangement form

        #region F1410 Owner Receipting

        /// <summary>
        /// F1410_s the get owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns Owner Reeipting DataSet</returns>
        public static F1410OwnerReceiptingData F1410_GetOwnerReceipting(string interestDate, string ownerId, string parcelIDs)
        {
            return F1410OwnerReceiptingComp.F1410_GetOwnerReceipting(interestDate, ownerId, parcelIDs);
        }

        /// <summary>
        /// F1410_s the list owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="formBackColor">Form Backcolor</param>
        /// <returns>Returns OwnerReceipting DataSet</returns>
        public static F1410OwnerReceiptingData F1410_ListOwnerReceipting(string interestDate, string statementXml, string formBackColor)
        {
            return F1410OwnerReceiptingComp.F1410_ListOwnerReceipting(interestDate, statementXml, formBackColor);
        }

        /// <summary>
        /// F1410_s the delete owner receipting.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerXml">The owner XML.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="userId">UserID</param>
        /// <param name="formBackColor">Form Backcolor</param>
        /// <returns>Returns OwnerReceipting Dataset</returns>
        public static F1410OwnerReceiptingData F1410_DeleteOwnerReceipting(int ownerId, string ownerXml, string statementXml, int userId, string formBackColor)
        {
            return F1410OwnerReceiptingComp.F1410_DeleteOwnerReceipting(ownerId, ownerXml, statementXml, userId, formBackColor);
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
            return F1410OwnerReceiptingComp.F1410_SaveOwnerReceipting(userId, receiptDate, interestDate, ppaymentId, paymentOption, statementXml);
        }

        public static int F1410_SaveOwnerReceiptPreview(int userId, string statementDetails)
        {
            return F1410OwnerReceiptingComp.F1410_SaveOwnerReceiptPreview(userId, statementDetails);
        }

        #region Get Attachment Details

        /// <summary>
        /// List the attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public static F1410OwnerReceiptingData F1410_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            return F1410OwnerReceiptingComp.F1410_ListAttachmentDetails(formId, keyIds, userId, moduleId);
        }

        #endregion Get Attachment Details

        #endregion

        #region F8000 GDoc Commons

        #region Get GDocBusiness

        /// <summary>
        /// To Load GDoc Business ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocBusiness()
        {
            return GDocCommonComp.F8000_GetGDocBusiness();
        }

        #endregion Get GDocBusiness

        #region Get GDocDiameter

        /// <summary>
        /// To Load GDoc Diameter ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocDiameter(int featureClassId)
        {
            return GDocCommonComp.F8000_GetGDocDiameter(featureClassId);
        }

        #endregion Get GDocDiameter

        #region Get GDocPropertyReference

        /// <summary>
        /// To Load GDoc PropertyReference ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <param name="refField">The Ref Field</param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocPropertyReference(int featureClassId, string refField)
        {
            return GDocCommonComp.F8000_GetGDocPropertyReference(featureClassId, refField);
        }

        #endregion Get GDocPropertyReference

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData wListStreets()
        {
            return GDocCommonComp.wListStreets();
        }

        #endregion Get GDocStreet

        #region Get GDocUser

        /// <summary>
        /// To Load GDoc User ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocUser()
        {
            return GDocCommonComp.F8000_GetGDocUser();
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
        public static F84121SanitaryManholePropertiesData F84121_GetSanitaryManholeProperties(int manholeId)
        {
            return F84121SanitaryManholePropertiesComp.F84121_GetSanitaryManholeProperties(manholeId);
        }

        #endregion

        #region Save Sanitary Manhole Properties

        /// <summary>
        /// To Save F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The Manhole ID.</param>
        /// <param name="sanitaryManholeProperties">The XML string Containing All values in Sanitary Manhole properties.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing manhole id</returns>
        public static int F84121_SaveSanitaryManholeProperties(int manholeId, string sanitaryManholeProperties, int userId)
        {
            return F84121SanitaryManholePropertiesComp.F84121_SaveSanitaryManholeProperties(manholeId, sanitaryManholeProperties, userId);
        }

        #endregion

        #region Delete Sanitary Manhole Properties

        /// <summary>
        /// To Delete F84121 Sanitary Manhole properties
        /// </summary>
        /// <param name="manholeId">The Manhole Id</param>
        /// <param name="userId">UserID</param>
        public static void F84121_DeleteSanitaryManholeProperties(int manholeId, int userId)
        {
            F84121SanitaryManholePropertiesComp.F84121_DeleteSanitaryManholeProperties(manholeId, userId);
        }

        #endregion

        #endregion

        #region F84122 Sanitary Manhole Location

        #region Get Sanitary Manhole Location

        /// <summary>
        /// To Load F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Sanitary Manhole Loaction Details
        /// </returns>
        public static F84122SanitaryManholeLocationData F84122_GetSanitaryManholeLocation(int keyId)
        {
            return F84122SanitaryManholeLocationComp.F84122_GetSanitaryManholeLocation(keyId);
        }

        #endregion

        #region Save Sanitary Manhole Location

        /// <summary>
        /// To Save F84122 Sanitary Manhole Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryManholeLocation">The Sanitary Manhole location.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing key id</returns>
        public static int F84122_SaveSanitaryManholeLocation(int keyId, string sanitaryManholeLocation, int userId)
        {
            return F84122SanitaryManholeLocationComp.F84122_SaveSanitaryManholeLocation(keyId, sanitaryManholeLocation, userId);
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
        public static F84721WaterValvePropertiesData F84721_GetWaterValveProperties(int valveId)
        {
            return F84721WaterValvePropertiesComp.F84721_GetWaterValveProperties(valveId);
        }

        #endregion Get Water Valve Properties

        #region Save Water Valve Properties

        /// <summary>
        /// To Save F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <param name="waterValveProperties">The XML string Containing All values in Water valve properties.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing valve id</returns>
        public static int F84721_SaveWaterValveProperties(int valveId, string waterValveProperties, int userId)
        {
            return F84721WaterValvePropertiesComp.F84721_SaveWaterValveProperties(valveId, waterValveProperties, userId);
        }

        #endregion Save Water Valve Properties

        #region Delete Water Valve Properties

        /// <summary>
        /// To Delete F84721 Water valve properties
        /// </summary>
        /// <param name="valveId">The ValveId</param>
        /// <param name="userId">UserID</param>
        public static void F84721_DeleteWaterValveProperties(int valveId, int userId)
        {
            F84721WaterValvePropertiesComp.F84721_DeleteWaterValveProperties(valveId, userId);
        }

        #endregion Delete Water Valve Properties

        #endregion F84721 Water Valve Properties

        #region F9033 QueryEngine

        #region ListQueryView

        /// <summary>
        /// F9033s the list query view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9033QueryEngineData</returns>
        public static F9033QueryEngineData F9033ListQueryView(int formId)
        {
            return F9033QueryEngineComp.ListQueryView(formId);
        }

        #endregion

        #region ListQueryEngine

        /// <summary>
        /// F9033s the list query engine.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static DataSet F9033ListQueryEngine(int queryViewId)
        {
            return F9033QueryEngineComp.ListQueryEngine(queryViewId);
        }

        #endregion

        #region GetSnapShotRecordSet

        /// <summary>
        /// F9033_s the get snap shot record set.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">Query View ID</param>
        /// <returns>DataSet</returns>
        public static DataSet F9033_GetSnapShotRecordSet(int snapShotId, int queryViewId)
        {
            return F9033QueryEngineComp.F9033_GetSnapShotRecordSet(snapShotId, queryViewId);
        }

        #endregion GetSnapShotRecordSet

        #region GetDefaultLayoutXml

        /// <summary>
        /// F9033s the get default layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>QueryEngine dataset</returns>
        public static F9033QueryEngineData F9033GetDefaultLayout(int queryViewId)
        {
            return F9033QueryEngineComp.GetDefaultLayout(queryViewId);
        }

        #endregion

        #region ListQuerySnapShot

        /// <summary>
        /// F9033_s the list query snap shot.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static F9033QueryEngineData F9033_ListQuerySnapShot(int queryViewId)
        {
            return F9033QueryEngineComp.F9033_ListQuerySnapShot(queryViewId);
        }

        #endregion ListQuerySnapShot

        #region ListQueryLayout

        /// <summary>
        /// F9033_s the list query layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public static F9033QueryEngineData F9033_ListQueryLayout(int queryViewId, int userId)
        {
            return F9033QueryEngineComp.F9033_ListQueryLayout(queryViewId, userId);
        }

        #endregion ListQueryLayout

        #region InsertSnapShotItems

        /// <summary>
        /// F9030_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotXML">The system snap shot XML.</param>
        /// <returns>Integer</returns>
        public static int F9033_InsertSnapShotItems(int? userId, string systemSnapShotXML)
        {
            return F9033QueryEngineComp.F9033_InsertSnapShotItems(userId, systemSnapShotXML);
        }

        #endregion InsertSnapShotItems

        #region GetSystemSnapshotCount

        /// <summary>
        /// F9033_s the Get SystemSnapshot Count
        /// </summary>
        /// <param name="systemSnapshotId">The system Snapshot Id</param>
        /// <returns>DataSet</returns>
        public static F9033QueryEngineData F9033_GetSystemSnapshotCount(int systemSnapshotId)
        {
            return F9033QueryEngineComp.F9033_GetSystemSnapShotCount(systemSnapshotId);
        }

        #endregion GetSystemSnapshotCount

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
        /// <param name="isFilter">The is filter.</param>
        /// <returns>DataSet</returns>
        public static DataSet F9033_GetSystemSnapShotRecordSet(int systemSnapShotId, int masterFormNO, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter)
        {
            return F9033QueryEngineComp.F9033_GetSystemSnapShotRecordSet(systemSnapShotId, masterFormNO, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter);
        }

        #endregion GetSystemSnapShotRecordSet

        ////Added by Latha

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
        /// <returns>DataSet</returns>
        public static DataSet ListQueryEngineGridFunction(int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, string isFilter, string maxRecord)
        {
            return F9033QueryEngineComp.ListQueryEngineGridFunction(queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter, maxRecord);
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
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Count</param>
        /// <returns>DataSet</returns>
        public static DataSet ListQueryEngineGridSnapshot(int snapShotId, int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter, string maxRecord)
        {
            return F9033QueryEngineComp.ListQueryEngineGridSnapshot(snapShotId, queryViewId, filterValue, sortOrder, summaryValue, columnValue, keyIdCollection, isFilter, maxRecord);
        }

        /// <summary>
        /// Lists the columns.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static DataSet ListColumns(int queryViewId)
        {
            return F9033QueryEngineComp.ListColumns(queryViewId);
        }

        #endregion CustomGridFunctionality

        #endregion

        #region F9039QueryUpdate

        #region ListQueryViewColumn

        /// <summary>
        /// Lists the query view column.
        /// </summary>
        /// <param name="queryViewId">The query view ID.</param>
        /// <returns>DataSet</returns>
        public static F9039QueryUpdate F9039ListQueryViewColumn(int queryViewId)
        {
            return F9039QueryUpdateComp.F9039ListQueryViewColumn(queryViewId);
        }

        #endregion ListQueryViewColumn

        #region GetCommandResult

        /// <summary>
        /// F9039s the get command result.
        /// </summary>
        /// <param name="replaceId">The replace ID.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns>DataSet</returns>
        public static DataSet F9039GetCommandResult(int replaceId, string commandResult)
        {
            return F9039QueryUpdateComp.F9039GetCommandResult(replaceId, commandResult);
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
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        public static string F9039UpdateQueryData(int queryViewId, string keyField, string keyId, string updateField, int doprocessValue, int userId)
        {
            return F9039QueryUpdateComp.F9039UpdateQueryData(queryViewId, keyField, keyId, updateField, doprocessValue, userId);
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
        public static F84722WaterValveLocationData F84722_GetWaterValveLocation(int keyId, int formId)
        {
            return F84722WaterValveLocationComp.F84722_GetWaterValveLocation(keyId, formId);
        }

        #endregion Get Water Valve Location

        #region Save Water Valve Location

        /// <summary>
        /// To Save F84722 Water valve Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="waterValveLocation">The water valve location.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing key id</returns>
        public static int F84722_SaveWaterValveLocation(int keyId, string waterValveLocation, int formId, int userId)
        {
            return F84722WaterValveLocationComp.F84722_SaveWaterValveLocation(keyId, waterValveLocation, formId, userId);
        }

        #endregion Save Water Valve Location

        #endregion F84722 Water Valve Location

        #region F84723 Water Hydrant Properties

        #region Get Water Hydrant Properties

        /// <summary>
        /// To Load Water Hydrant Properties
        /// </summary>
        /// <param name="hydrantId">The hydrantId.</param>
        /// <returns>Typed DataSet Containing the Water Hydrant Properties Details.</returns>
        public static F84723WaterHydrantPropertiesData F84723_GetWaterHydrantProperties(int hydrantId)
        {
            return F84723WaterHydrantPropertiesComp.F84723_GetWaterHydrantProperties(hydrantId);
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
        public static int F84723_CheckMainValveId(int mainValveId)
        {
            return F84723WaterHydrantPropertiesComp.F84723_CheckMainValveId(mainValveId);
        }

        #endregion Check Main Valve ID

        #region Save Water Hydrant Properties

        /// <summary>
        /// To Save Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="waterHydrantPropties">The XML String containing the Water Hydrant Properties Details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer valu containing the hydrantId</returns>
        public static int F84723_SaveWaterHydrantProperties(int hydrantId, string waterHydrantPropties, int userId)
        {
            return F84723WaterHydrantPropertiesComp.F84723_SaveWaterHydrantProperties(hydrantId, waterHydrantPropties, userId);
        }

        #endregion Save Water Hydrant Properties

        #region Delete Water Hydrant Properties

        /// <summary>
        /// To Delete Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">hydrantId</param>
        /// <param name="userId">UserID</param>
        public static void F84723_DeleteWaterHydrantProperties(int hydrantId, int userId)
        {
            F84723WaterHydrantPropertiesComp.F84723_DeleteWaterHydrantProperties(hydrantId, userId);
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
        public static F84725WaterPipePropertiesData F84725_GetWaterPipeProperties(int pipeId)
        {
            return F84725WaterPipePropertiesComp.F84725_GetWaterPipeProperties(pipeId);
        }

        #endregion Get Water Pipe Properties

        #region Save Water Pipe Properties

        /// <summary>
        /// To Save water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeProperties">The XML String Containing the Water Pipe Properties details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>the integer value containing the pipeid</returns>
        public static int F84725_SaveWaterPipeProperties(int pipeId, string waterPipeProperties, int userId)
        {
            return F84725WaterPipePropertiesComp.F84725_SaveWaterPipeProperties(pipeId, waterPipeProperties, userId);
        }

        #endregion Save Water Pipe Properties

        #region Delete Water Pipe Properties

        /// <summary>
        /// To Delete water pipe properties.
        /// </summary>
        /// <param name="pipeId">the pipe Id</param>
        /// <param name="userId">UserID</param>
        public static void F84725_DeleteWaterPipeProperties(int pipeId, int userId)
        {
            F84725WaterPipePropertiesComp.F84725_DeleteWaterPipeProperties(pipeId, userId);
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
        public static F84726WaterPipeLocationData F84726_GetWaterPipeLocation(int pipeId)
        {
            return F84726WaterPipeLocationComp.F84726_GetWaterPipeLocation(pipeId);
        }

        #endregion Get Water Pipe Location

        #region Save Water Pipe Location

        /// <summary>
        /// To Save Water Pipe Location.
        /// </summary>
        /// <param name="pipeId">The Pipe Id.</param>
        /// <param name="waterPipeLocation">The Xml String containing the Water Pipe Location details</param>
        /// <param name="userId">UserID</param>
        /// <returns>The Integer value containing pipe Id value</returns>
        public static int F84726_SaveWaterPipeLocation(int pipeId, string waterPipeLocation, int userId)
        {
            return F84726WaterPipeLocationComp.F84726_SaveWaterPipeLocation(pipeId, waterPipeLocation, userId);
        }

        #endregion Save Water Pipe Location

        #endregion F84726 Water Pipe Location

        #region F1505 District Copy Form


        /// <summary>
        /// F1505_s the District Copy Form
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="activeStatus">The is active.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Error Statement or PrimaryKey Id</returns>
        public static string F1505ExecuteCopyDistrict(int districtId, string districtText, int rollyear, string description, bool isactive, int districtTypeId, int ExciseId, int userId)
        {
            return F1505DistrictCopyComp.F1505ExecuteCopyDistrict(districtId, districtText, rollyear, description, isactive, districtTypeId, ExciseId, userId);       
        }


        #endregion F1505 District Copy Form

        #region F15002 District Fund Management

        #region Get Distict Fund Details

        /// <summary>
        /// F15002_s the get distirct fund details.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>DataSet Contains the District Fund Deatails</returns>
        public static F15002DistMgmtData F15002_GetDistirctFundDetails(int? districtId)
        {
            return F15002DistrictMgmtComp.F15002_GetDistirctFundDetails(districtId);
        }

        /// <summary>
        /// F15002_s the list all funds.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet Contains the All Funds Deatails</returns>
        public static F15002DistMgmtData F15002_ListAllFunds(int? fundId, string fund, int? rollYear)
        {
            return F15002DistrictMgmtComp.F15002_ListAllFunds(fundId, fund, rollYear);
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
        public static int F15002_CheckDistrict(int? districtId, string district, int rollYear)
        {
            return F15002DistrictMgmtComp.F15002_CheckDistrict(districtId, district, rollYear);
        }

        /// <summary>
        /// F15002_s the create or edit district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtDetails">The district details.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F15002_CreateOrEditDistrictMgmt(int? districtId, string districtDetails, string districtFundItems, int userId)
        {
            return F15002DistrictMgmtComp.F15002_CreateOrEditDistrictMgmt(districtId, districtDetails, districtFundItems, userId);
        }

        #endregion

        #region Get District Type

        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static F15002DistMgmtData F15002_GetDistrictType(int userId)
        {
            return F15002DistrictMgmtComp.F15002_GetDistrictType(userId);
        }

        #endregion

        #endregion

        #region F84123 Sanitary Pipe Properties

        #region Get Sanitary Pipe Properties

        /// <summary>
        ///  To Load F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe Properties Details</returns>
        public static F84123SanitaryPipePropertiesData F84123_GetSanitaryPipeProperties(int pipeId)
        {
            return F84123SanitaryPipePropertiesComp.F84123_GetSanitaryPipeProperties(pipeId);
        }

        #endregion Get WSanitary Pipe Properties

        #region Save Sanitary Pipe Properties

        /// <summary>
        /// To Save F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <param name="sanitaryPipeProperties">The XML string Containing All values in Sanitary Pipe properties.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing pipe id</returns>
        public static int F84123_SaveSanitaryPipeProperties(int pipeId, string sanitaryPipeProperties, int userId)
        {
            return F84123SanitaryPipePropertiesComp.F84123_SaveSanitaryPipeProperties(pipeId, sanitaryPipeProperties, userId);
        }

        #endregion Save Sanitary Pipe Properties

        #region Delete Sanitary Pipe Properties

        /// <summary>
        /// To Delete F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The PipeId</param>
        /// <param name="userId">UserID</param>
        public static void F84123_DeleteSanitaryPipeProperties(int pipeId, int userId)
        {
            F84123SanitaryPipePropertiesComp.F84123_DeleteSanitaryPipeProperties(pipeId, userId);
        }

        #endregion Delete Sanitary Pipe Properties

        #endregion F84123 Sanitary Pipe Location

        #region F84124 Sanitary Pipe Location

        #region Get Sanitary Pipe Location

        /// <summary>
        ///  To Load F84124 Sanitary Pipe Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe Location Details</returns>
        public static F84124SanitaryPipeLocationData F84124_GetSanitaryPipeLocation(int keyId, int formId)
        {
            return F84124SanitaryPipeLocationComp.F84124_GetSanitaryPipeLocation(keyId, formId);
        }

        #endregion Get Sanitary Pipe Location

        #region Save Sanitary Pipe Location

        /// <summary>
        /// To Save F84124 Sanitary Pipe Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryPipeLocation">The Sanitary Pipe Location.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing key id</returns>
        public static int F84124_SaveSanitaryPipeLocation(int keyId, string sanitaryPipeLocation, int userId)
        {
            return F84124SanitaryPipeLocationComp.F84124_SaveSanitaryPipeLocation(keyId, sanitaryPipeLocation, userId);
        }

        #endregion Save Sanitary Pipe Location

        #endregion F84124 Sanitary Pipe Location

        #region 11020 Real Property
        #endregion

        #region Get Real Property Statement

        /// <summary>
        /// Gets the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id of the statement to be fetched.</param>
        /// <returns>
        /// The typed dataset containing the statement information of the statementid.
        /// </returns>
        public static F11020RealPropertyData F11020_GetRealPropertyStatement(int statementId)
        {
            return F11020RealPropertyComp.F11020_GetRealPropertyStatement(statementId);
        }

        #endregion Get Real Property Statement

        #region 11030 Real Property

        #region Get Real Property Statement

        /// <summary>
        /// Gets the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id of the statement to be fetched.</param>
        /// <returns>
        /// The typed dataset containing the statement information of the statementid.
        /// </returns>
        public static F11020RealPropertyData F15030_GetRealPropertyStatements(int statementId)
        {
            return F11020RealPropertyComp.F15030_GetRealPropertyStatements(statementId);
        }

        #endregion Get Real Property Statement

        #region update Real Property Statement

        /// <summary>
        /// update the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">UserID</param>
        public static void F1423_UpdateRealPropertyStatement(int statementId, string statementItems, int userId)
        {
            F11020RealPropertyComp.F1423_UpdateRealPropertyStatement(statementId, statementItems, userId);
        }

        #endregion update Real Property Statement

        #region List Mortgage Name

        /// <summary>
        /// list the mortgage name.
        /// </summary>
        /// <returns>F11020RealPropertyData with morgage name list</returns>
        public static F11020RealPropertyData F1423_ListMortgageName()
        {
            return F11020RealPropertyComp.F1423_ListMortgageName();
        }

        #endregion List Mortgage Name

        #endregion 11020 Real Property

        #region 15020 receipt engine

        #region ListHistoryGrid

        /// <summary>
        /// list history grid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>F15020ReceiptEngineData with receipt history and Detail</returns>
        public static F15020ReceiptEngineData F15020_ListHistoryGrid(int statementId)
        {
            return F15020ReceiptEngineComp.F15020_ListHistoryGrid(statementId);
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// get receipt details and payment items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>F15020ReceiptEngineData with receipt detail</returns>
        public static F15020ReceiptEngineData F15020_GetReceiptDetails(int receiptId)
        {
            return F15020ReceiptEngineComp.F15020_GetReceiptDetails(receiptId);
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
        public static decimal F1003_GetMinTaxDue(int statmentId, string interestDate)
        {
            return F15020ReceiptEngineComp.F1003_GetMinTaxDue(statmentId, interestDate);
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
        public static decimal F1004_GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            return F15020ReceiptEngineComp.F1004_GetInterestAmount(statmentId, interestDate, taxDueAmount);
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
        public static string F1009_GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return F15020ReceiptEngineComp.F1009_GetValidReceiptTest(statementId, receiptDate);
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
        public static int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId)
        {
            return F1405MasterReceiptingComp.F1405_SaveMasterReceipt(statementId, receiptSourceId, otherParameterInfo, sharedPaymentId);
        }

        #endregion SaveReceipt

        #endregion

        #region 15104 ReceiptPayament

        /// <summary>
        /// F15104_s the get receipt payment.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ReceiptPayamentDataSet</returns>
        public static F15104ReceiptPayamentData F15104_GetReceiptPayment(int receiptId)
        {
            return F15104ReceiptPaymentComp.F15104_GetReceiptPayment(receiptId);
        }

        /// <summary>
        /// F1557_s the insert payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">UserID</param>
        public static void F15104_UpdateReceiptPayment(string receiptPayment, int userId)
        {
            F15104ReceiptPaymentComp.F15104_UpdateReceiptPayment(receiptPayment, userId);
        }


 

        #endregion

        #region 15004 Agency Details

        #region Get AgencyDetails

        /// <summary>
        /// F15004_s the get agency details.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <returns>F15004AgencyManagementData</returns>
        public static F15004AgencyManagementData F15004_GetAgencyDetails(int agencyId)
        {
            return F15004AgencyManagmentComp.F15004_GetAgencyDetails(agencyId);
        }

        #endregion

        #region Check for Agency Dupilcate Record

        /// <summary>
        /// F15004_s the check duplicate agency.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <param name="agencyName">Name of the agency.</param>
        /// <returns>ErrorID</returns>
        public static int F15004_CheckDuplicateAgency(int agencyId, string agencyName)
        {
            return F15004AgencyManagmentComp.F15004_CheckDuplicateAgency(agencyId, agencyName);
        }

        #endregion

        #region Create and  Edit the Agency Details

        /// <summary>
        /// F15004_s the create or edit agency details.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public static int F15004_CreateOrEditAgencyDetails(int agencyId, string acctEmelemts, int userId)
        {
            return F15004AgencyManagmentComp.F15004_CreateOrEditAgencyDetails(agencyId, acctEmelemts, userId);
        }

        #endregion

        #endregion

        #region 15007 Account Management Slice

        /// <summary>
        /// F15007_s the check duplicate account.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>ErrorID</returns>
        public static int F15007_CheckDuplicateAccount(int accountId, string acctEmelemts)
        {
            return F15007AccountMgmtComp.F15007_CheckDuplicateAccount(accountId, acctEmelemts);
        }

        #endregion

        #region Get ParcelType

        /// <summary>
        /// Get ParcelType Details
        /// </summary>
        /// <returns>ParcelType - typed DataSet</returns>
        public static F2000ParcelStatusData GetParcelType()
        {
            return F2000ParcelStatusComp.GetParcelType();
        }

        #endregion Get ParcelType

        #region F9038 Layout Management

        #region  Load Layout ManagementGrid

        /// <summary>
        /// F9038_s the layout management grid.
        /// </summary>
        /// <param name="queryViewId">The query view ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>
        /// LayoutManagement Data For particular querviewID
        /// </returns>
        public static F9038LayoutManagementData F9038_LoadLayoutInformation(int queryViewId, int userId)
        {
            return F9038LayoutManagementComp.F9038_LoadLayoutInformation(queryViewId, userId);
        }

        #endregion

        #region Save LoadLayoutManagement

        /// <summary>
        /// F9038_s the save layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout ID.</param>
        /// <param name="layoutManagement">The layout management.</param>
        /// <param name="layoutxmlValue">The layoutxml value.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public static int F9038_SaveLayoutInformation(int queryLayoutId, string layoutManagement, string layoutxmlValue, int userId)
        {
            return F9038LayoutManagementComp.F9038_SaveLayoutInformation(queryLayoutId, layoutManagement, layoutxmlValue, userId);
        }

        #endregion Save LoadLayoutManagement

        #region Delete LoadLayoutManagement

        /// <summary>
        /// F9038_s the delete water pipe properties.
        /// </summary>
        /// <param name="queryLayoutId">The query layout ID.</param>
        /// <param name="userId">UserID</param>
        public static void F9038_DeleteLayoutInformation(int queryLayoutId, int userId)
        {
            F9038LayoutManagementComp.F9038_DeleteLayoutInformation(queryLayoutId, userId);
        }

        #endregion Delete LoadLayoutManagement

        #endregion

        #region Get Working Day

        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public static DateTime F9001_GetNextWorkingDay()
        {
            return General.F9001_GetNextWorkingDay();
        }

        #endregion

        #region F15003 Fund Management

        #region Get Fund and SubFund Details

        /// <summary>
        /// F15003_s the get fund sub fund details.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <returns>dataset which contains Fund Details</returns>
        public static F15003FundMgmtData F15003_GetFundSubFundDetails(int? fundId)
        {
            return F15003FundMgmtComp.F15003_GetFundSubFundDetails(fundId);
        }

        /// <summary>
        /// F15003_s the list available sub funds.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="fundId">The fund id.</param>
        /// <returns>DataSet Contains the Available Funds Details</returns>
        public static F15003FundMgmtData F15003_ListAvailableSubFunds(string subFund, string description, int? rollYear, int? fundId)
        {
            return F15003FundMgmtComp.F15003_ListAvailableSubFunds(subFund, description, rollYear, fundId);
        }

        /// <summary>
        /// Lists the type of the fund.
        /// </summary>
        /// <returns>dataTable Contains the FundGroup Types</returns>
        public static F15003FundMgmtData.ListFundTypeDataTable F15003_ListFundType()
        {
            return F15003FundMgmtComp.F15003_ListFundType();
        }

        #endregion

        #region Save and Edit Fund Details

        /// <summary>
        /// F15003_s the check fund.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns the fund valid status</returns>
        public static int F15003_CheckFund(int? fundId, string fund, int rollYear)
        {
            return F15003FundMgmtComp.F15003_CheckFund(fundId, fund, rollYear);
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
        /// <param name="userId">UserID</param>
        /// <returns>returns the save process status</returns>
        public static int F15003_CreateOrEditFundMgmt(int? fundId, string fund, int rollYear, string description, int? fundGroupId, string fundItems, int userId)
        {
            return F15003FundMgmtComp.F15003_CreateOrEditFundMgmt(fundId, fund, rollYear, description, fundGroupId, fundItems, userId);
        }

        #endregion

        #endregion

        #region F11011 Excise Statement

        #region Get Excise Receipt

        /// <summary>
        /// Gets the Excise Receipt details 
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Receipt Details</returns>
        public static F11011ExciseStatementData F15012_GetExciseReceipt(int statementId)
        {
            return F11011ExciseStatementComp.F15012_GetExciseReceipt(statementId);
        }

        #endregion

        #region Excise Statement Summary

        /// <summary>
        /// Gets the Excise Statement Summary
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Statement Summary Details</returns>
        public static F11011ExciseStatementData F15011_GetExciseStatement(int statementId)
        {
            return F11011ExciseStatementComp.F15011_GetExciseStatement(statementId);
        }

        /// <summary>
        /// update the Excise Statement - receipt and interest date
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="userId">UserID</param>
        public static void F15011_SaveExciseStatement(int statementId, DateTime interestDate, DateTime receiptDate, int userId)
        {
            F11011ExciseStatementComp.F15011_SaveExciseStatement(statementId, interestDate, receiptDate, userId);
        }

        #endregion

        #endregion

        #region F15010 Excise Affidavit

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetExciseIndividualType()
        {
            return F15010ExciseAffidavitComp.F15010_GetExciseIndividualType();
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitDetails(int statementId)
        {
            return F15010ExciseAffidavitComp.F15010_GetExciseTaxAffidavitDetails(statementId);
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate ID.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            return F15010ExciseAffidavitComp.F15010_GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateId, taxCode, taxableSaleAmount);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="mobileHomeDetails">The mobile home details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// returns dataset containing AffiDavit Details
        /// </returns>
        public static int F15010_SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, string mobileHomeDetails, int userId)
        {
            return F15010ExciseAffidavitComp.F15010_SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, mobileHomeDetails, userId);
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public static F15010ExciseAffidavitData F15010_GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            return F15010ExciseAffidavitComp.F15010_GetAffidavitStatementId(formId, orderField, orderBy);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return F15010ExciseAffidavitComp.F15010_GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// Get Owner Status.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_GetOwnerStatus(int ownerId)
        {
            return F15010ExciseAffidavitComp.F15010_GetOwnerStatus(ownerId);
        }
        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public static F15010ExciseAffidavitData F15010_GetDistrictSelection(int exciseRateId)
        {
            return F15010ExciseAffidavitComp.F15010_GetDistrictSelection(exciseRateId);
        }

        /// <summary>
        /// Delete The PArticular StatmentID Detials
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public static void F15010_DeleteAffidavitDetails(int statementId, int userId)
        {
            F15010ExciseAffidavitComp.F15010_DeleteAffidavitDetails(statementId, userId);
        }


        /// <summary>
        /// F15010_s the get parcel detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <returns>Returns Dataset foe parcel Selection</returns>
        public static F15010ExciseAffidavitData F15010_GetParcelDetail(int? parcelId, string parcelNumber)
        {
            return F15010ExciseAffidavitComp.F15010_GetParcelDetail(parcelId, parcelNumber);
        }

        /// <summary>
        /// F15010_s the list excise WAC.
        /// </summary>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListExciseWAC()
        {
            return F15010ExciseAffidavitComp.F15010_ListExciseWAC();
        }

        /// <summary>
        /// F5010_s the list excise individual.
        /// </summary>
        /// <param name="ExciseIndividualElements">The excise individual elements.</param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListExciseIndividual(string ExciseIndividualElements)
        {
            return F15010ExciseAffidavitComp.F15010_ListExciseIndividual(ExciseIndividualElements);
        }

        /// <summary>
        /// F15010_s the list open space field.
        /// </summary>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListOpenSpaceField(string parcelIds)
        {
            return F15010ExciseAffidavitComp.F15010_ListOpenSpaceField(parcelIds);
        }

        #endregion F15010 Excise Affidavit

        #region 15013 Excise Tax Rate

        #region Get Excise Tax Rate

        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>returns dataset contains Excise Tax Rate Details</returns>
        public static F15013ExciseTaxRateData F15013_GetExciseTaxRate(int exciseRateId)
        {
            return F15013ExciseTaxRateComp.F15013_GetExciseTaxRate(exciseRateId);
        }

        #endregion

        #region List Excise Tax Rate

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>returns dataset contains ExciseRateID</returns>
        public static F15013ExciseTaxRateData F15013_ListExciseTaxRate()
        {
            return F15013ExciseTaxRateComp.F15013_ListExciseTaxRate();
        }

        #endregion

        #region Save Excise Tax Rate

        /// <summary>
        /// F15013_s the save excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>errorId/Primary KeyId</returns>
        public static int F15013_SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId)
        {
            return F15013ExciseTaxRateComp.F15013_SaveExciseTaxRate(exciseRateId, exciseTaxDetails, userId);
        }

        #endregion

        #region Delete Excise Tax Rate

        /// <summary>
        /// Deletes the Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">The excise rate Id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F15013_DeleteExciseTaxRate(int exciseRateId, int userId)
        {
            return F15013ExciseTaxRateComp.F15013_DeleteExciseTaxRate(exciseRateId, userId);
        }
        #endregion

        #region Get District Name

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>returns dataset contains District Name</returns>
        public static F15013ExciseTaxRateData F15013_GetDistrictName(int districtId)
        {
            return F15013ExciseTaxRateComp.F15013_GetDistrictName(districtId);
        }
        #endregion

        #region Get Account Name

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public static F15013ExciseTaxRateData F15013_GetAccountName(int accountId)
        {
            return F15013ExciseTaxRateComp.F15013_GetAccountName(accountId);
        }
        #endregion

        #endregion

        #region F15019 Journal Entry

        #region Get JournalEntry
        /// <summary>
        /// F15019_s the get journal entry details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>Typed Dataset</returns>
        public static F15019JournalEntryData F15019_GetJournalEntryDetails(int receiptId)
        {
            return F15019JournalEntryComp.F15019GetJournalEntryDetails(receiptId);
        }
        #endregion Get JournalEntry

        #region Save JournalEntry

        /// <summary>
        /// F15019_s the insert journal entry details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>integer value</returns>
        public static int F15019_InsertJournalEntryDetails(int statementId, int receiptSourceId, string journalItems)
        {
            return F15019JournalEntryComp.F15019InsertJournalEntryDetails(statementId, receiptSourceId, journalItems);
        }

        /// <summary>
        /// F15019_s the check roll year.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>success of dboperation</returns>
        public static int F15019_CheckRollYear(int statementId, int receiptSourceId, string journalItems)
        {
            return F15019JournalEntryComp.F15019_CheckRollYear(statementId, receiptSourceId, journalItems);
        }
        #endregion Save JournalEntry

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
        public static F9013NextNumberData F9013_ListNextNumberConfiguration(int rollYear, int userId)
        {
            return F9013NextNumberComp.F9013_ListNextNumberConfiguration(rollYear, userId);
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
        public static DataSet F9013_CheckNextNumber(int rollYear, int nextNum, string formula)
        {
            return F9013NextNumberComp.F9013_CheckNextNumber(rollYear, nextNum, formula);
        }

        #endregion Check Next Number

        #region Update NextNumber ConfigDetails

        /// <summary>
        /// Saves Next Number configuration details
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">UserID</param>
        public static void F9013_UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId)
        {
            F9013NextNumberComp.F9013_UpdateNextNumberConfigDetails(nextNumId, nextNum, formula, userId);
        }

        #endregion Update NextNumber ConfigDetails

        #region List Roll Year

        /// <summary>
        /// To List next number roll year.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// The dataset containing the list of Next Number RollYear.
        /// </returns>
        public static F9013NextNumberData F9013_ListNextNumberRollYear(int userId)
        {
            return F9013NextNumberComp.F9013_ListNextNumberRollYear(userId);
        }

        #endregion List Roll Year

        #endregion F9013 Next Number Configuration

        #region F11018 Misc Receipt

        /// <summary>
        /// Gets the Misc Receipt details based on the receiptId
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The typed dataset containing the receipt information of the receiptId.
        /// </returns>
        public static F11018MiscReceiptData F15018_GetMiscReceipt(int receiptId)
        {
            return F11018MiscReceiptComp.F15018_GetMiscReceipt(receiptId);
        }

        /// <summary>
        /// gets the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateId">The misc template ID.</param>
        /// <returns>The typed dataset containing the receipt Template information of the miscTemplateID.</returns>
        public static F11018MiscReceiptData F1021_GetMiscReceiptTemplate(int miscTemplateId)
        {
            return F11018MiscReceiptComp.F1021_GetMiscReceiptTemplate(miscTemplateId);
        }

        /// <summary>
        /// saves the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateDetails">The misc template details.</param>
        /// <param name="templateItems">The template items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// new created templated id - return templatedid if succeed else return negative value
        /// </returns>
        public static int F1021_SaveMiscReceiptTemplate(string miscTemplateDetails, string templateItems, int userId)
        {
            return F11018MiscReceiptComp.F1021_SaveMiscReceiptTemplate(miscTemplateDetails, templateItems, userId);
        }

        /// <summary>
        /// List the Misc Receipt template
        /// </summary>
        /// <returns>
        /// The typed dataset containing the Misc Receipt Template
        /// </returns>
        public static F11018MiscReceiptData F1022_ListMiscReceiptTemplate()
        {
            return F11018MiscReceiptComp.F1022_ListMiscReceiptTemplate();
        }

        /// <summary>
        /// Deletes the Misc Receipt Template based on the miscTemplateID
        /// </summary>
        /// <param name="miscTemplateId">The misc template ID.</param>
        /// <param name="userId">UserID</param>
        public static void F1022_DeleteMiscReceiptTemplate(int miscTemplateId, int userId)
        {
            F11018MiscReceiptComp.F1022_DeleteMiscReceiptTemplate(miscTemplateId, userId);
        }

        /// <summary>
        /// Save district details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        public static F11018MiscReceiptData F1024_SaveDistrictDetails(int levyOption, int districtId, decimal amountValue, int userId, bool IsReplace, string SubFundXML)
        {
            return F11018MiscReceiptComp.F1024_SaveDistrictDetails(levyOption, districtId, amountValue, userId, IsReplace, SubFundXML);
        }

        /// <summary>
        /// List district Distribution details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <param name="isreplace">IsReplace</param>
        /// <returns>District Selection dataset</returns>
        public static DistrictSelectionData GetDistrictDistributionData(int LevyOptionId, int districtId, decimal amount, int userId, string subfundsXML, bool isreplace)
        {
            return DistrictSelectionComp.GetDistrictDistributionData(LevyOptionId, districtId, amount, userId, subfundsXML, isreplace);
        }


        /// <summary>
        /// Gets the district sub fund Items data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public static DistrictSelectionData GetDistrictData(int districtId)
        {
            return DistrictSelectionComp.GetDistrictData(districtId);
        }

        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        public static F11018MiscReceiptData F15018_ListAccountDetails(string filterValue, int? rollYear, int? formNo)
        {
            return F11018MiscReceiptComp.F15018_ListAccountDetails(filterValue, rollYear,formNo);
        }

        #endregion

        #region F1025 AutoFund Transfer

        #region List RollYear

        /// <summary>
        /// F1025_s the list roll year.
        /// </summary>
        /// <returns>Typed DataSet</returns>
        public static F1025AutoFundTransferData F1025_ListRollYear()
        {
            return F1025AutoFundTransferComp.F1025_ListRollYear();
        }

        #endregion

        #region List AutoFund Transfer Details

        /// <summary>
        /// F1025_s the list auto fund transfer details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Typed DataSet</returns>
        public static F1025AutoFundTransferData F1025_ListAutoFundTransferDetails(int rollYear)
        {
            return F1025AutoFundTransferComp.F1025_ListAutoFundTransferDetails(rollYear);
        }

        #endregion

        #region Delete AutoFund Transfer

        /// <summary>
        /// F1025_s the delete auto fund transfer details.
        /// </summary>
        /// <param name="autoTransferId">The auto transfer ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer value</returns>
        public static int F1025_DeleteAutoFundTransferDetails(int autoTransferId, int userId)
        {
            return F1025AutoFundTransferComp.F1025_DeleteAutoFundTransferDetails(autoTransferId, userId);
        }
        #endregion Delete AutoFund Transfer

        #region Update AutoFund Transfer

        /// <summary>
        /// F1025_s the update auto fund transfer details.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>IntegerValue</returns>
        public static int F1025_UpdateAutoFundTransferDetails(string autoFundItems, int userId)
        {
            return F1025AutoFundTransferComp.F1025_UpdateAutoFundTransferDetails(autoFundItems, userId);
        }

        #endregion Update AutoFund Transfer

        #region Check RollYear

        /// <summary>
        /// F1025_s the check roll year.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <returns>IntgerValue</returns>
        public static int F1025_CheckRollYear(string autoFundItems)
        {
            return F1025AutoFundTransferComp.F1025_CheckRollYear(autoFundItems);
        }
        #endregion Check RollYear
        #endregion F1025 AutoFund Transfer

        #region RDL to Code

        #region Get

        /// <summary>
        /// RDLs to code_ get.
        /// </summary>
        /// <param name="getxmlString">The getxml string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset.</returns>
        public static DataSet RdlToCode_Get(string getxmlString, string formId)
        {
            return RdlToCodeComp.RdlToCode_Get(getxmlString, formId);
        }

        #endregion Get

        #region Fill Combo

        #region Old Method Commented By Shiva
        /*
         * 
        /// <summary>
        /// Fill method
        /// </summary>
        /// <param name="parameterData">parameterData</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns>dataset</returns>
        public static DataSet RdlToCode_FillCombo(RdlToCodeData.ParameterDataDataTable parameterData, String entityName)
        {
            return RdlToCodeComp.RdlToCode_FillCombo(parameterData, entityName);
        }
        */
        #endregion

        /// <summary>
        /// RDLs to code_ fill combo.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>dataset</returns>
        public static DataSet RdlToCode_FillCombo(string storedProcedureName)
        {
            return RdlToCodeComp.RdlToCode_FillCombo(storedProcedureName);
        }

        #endregion Fill Combo

        #region Save

        /// <summary>
        /// RDLs to code_ save.
        /// </summary>
        /// <param name="savexmlString">The savexml string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>primary key id.</returns>
        public static int RdlToCode_Save(string savexmlString, string formId)
        {
            return RdlToCodeComp.RdlToCode_Save(savexmlString, formId);
        }

        #endregion Save

        #region Delete

        /// <summary>
        /// RDLs to code_ delete.
        /// </summary>
        /// <param name="deletexmlString">The deletexml string.</param>
        /// <param name="formId">The form id.</param>
        public static void RdlToCode_Delete(string deletexmlString, string formId)
        {
            RdlToCodeComp.RdlToCode_Delete(deletexmlString, formId);
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
        public static F27006ParcelOwnershipData F27006_ListParcelOwnership(int parcelId)
        {
            return F27006ParcelOwnershipComp.F27006_ListParcelOwnership(parcelId);
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
        public static F27006ParcelOwnershipData F27006_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            return F27006ParcelOwnershipComp.F27006_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
        }

        #endregion List All Owner Details

        #region F27006 list MOwnerType Selection

        public static F27006ParcelOwnershipData ListMOwnerType()
        {
            return F27006ParcelOwnershipComp.ListMOwnerType();
        }

        #endregion

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">UserID</param>
        public static void F27006_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId, bool isfuturePush)
        {
            F27006ParcelOwnershipComp.F27006_SaveParcelOwnership(parcelOwnership, parcelId, userId, isfuturePush);
        }

        #endregion Save Parcel Ownership

        #region Check Ownership Details

        /// <summary>
        /// To Check Given Ownership Details is valid.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>returns an integer Value whather given details are correct or not</returns>
        public static int F27006_CheckOwnershipDetails(string ownershipDetails)
        {
            return F27006ParcelOwnershipComp.F27006_CheckOwnershipDetails(ownershipDetails);
        }

        #endregion Check Ownership Details

        #endregion F27006 Parcel Ownership

        #region F27008 TRParcel Ownership

        #region List TRParcel Ownership
        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public static F27008TRParcelOwnershipData F27008_ListParcelOwnership(int parcelId)
        {
            return F27008TRParcelOwnershipDataComp.F27008_ListParcelOwnership(parcelId);
        }
        #endregion  List TRParcel Ownership

        # region SaveParcelOwnership
        public static void F27008_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId)
        {
            F27008TRParcelOwnershipDataComp.F27008_SaveParcelOwnership(parcelOwnership, parcelId, userId);

        }
        #endregion SaveParcelOwnership

        #region OwnerDetails

        /// <summary>
        /// To List Owner Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public static F27008TRParcelOwnershipData F27008_GetOwnerDetails(int extraownerId, int UserId)
        {
            return F27008TRParcelOwnershipDataComp.F27008_GetOwnerDetails(extraownerId, UserId);
        }

        #endregion OwnerDetails

        #endregion F27008 TRParcel Ownership


        #region F35001 Value Slice Header/Adjustment

        #region Get Value Slice Header

        /// <summary>
        /// F35001_s the get value slice header.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <returns>the DataSet with the Header and Adjustment Values.</returns>
        public static F35001ValueSliceHeaderData F35001_GetValueSliceHeader(int valueSliceId)
        {
            return F35001ValueSliceHeaderComp.F35001_GetValueSliceHeader(valueSliceId);
        }

        #endregion

        #region Get Adjustment Slice Value

        /// <summary>
        /// F35001_s the get adjustment slice value.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <param name="type">The type.</param>
        /// <param name="isvalue">The is value.</param>
        /// <param name="adjustmentValue">The adjustment value.</param>
        /// <returns>Object Contains the Adjustment Value.</returns>
        public static string F35001_GetAdjustmentSliceValue(int valueSliceId, byte type, bool isvalue, decimal adjustmentValue)
        {
            return F35001ValueSliceHeaderComp.F35001_GetAdjustmentSliceValue(valueSliceId, type, isvalue, adjustmentValue);
        }

        #endregion

        #region List Adjustment Types

        /// <summary>
        /// F35002_s the type of the list adjustment.
        /// </summary>
        /// <param name="masterFromNo">The master from no.</param>
        /// <returns>Adjustment Types dataTable</returns>
        public static F35001ValueSliceHeaderData.ListAdjustmentTypeDataTable F35002_ListAdjustmentType(int? masterFromNo)
        {
            return F35001ValueSliceHeaderComp.F35002_ListAdjustmentType(masterFromNo);
        }

        #endregion

        #region Delete Value Slice

        /// <summary>
        /// F35001_s the delete value slice.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <param name="userId">UserID</param>
        public static void F35001_DeleteValueSlice(int valueSliceId, int userId)
        {
            F35001ValueSliceHeaderComp.F35001_DeleteValueSlice(valueSliceId, userId);
        }

        #endregion

        #endregion

        #region F35000 Appraisal Value Summary

        #region Insert/Update Value Slice

        /// <summary>
        /// F35000_s the update value slice.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="valueSliceHeaderItems">The value slice header items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Primary Key Id or Error Id.</returns>
        public static int F35000_InsertOrUpdateValueSlice(int? valueSliceId, string valueSliceHeaderItems, int userId)
        {
            return F35000ApprisalSummaryComp.F35000_InsertOrUpdateValueSlice(valueSliceId, valueSliceHeaderItems, userId);
        }

        #endregion

        #region Get Object/Value Slice Details

        /// <summary>
        /// F35000_s the get apparaisal summary objecy.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>AppraisalSummary DataSet</returns>
        public static F35000AppraisalSummaryData F35000_GetApparaisalSummaryObjecy(int parcelId)
        {
            return F35000ApprisalSummaryComp.F35000_GetAppraisalSummaryObjects(parcelId);
        }

        #endregion

        #region Insert Object

        /// <summary>
        /// F35000_s the insert object.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="objectTypeId">The object type id.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Primary Key Id if Success else Error Id</returns>
        public static int F35000_InsertObject(int parcelId, Int16 objectTypeId, string description, int userId)
        {
            return F35000ApprisalSummaryComp.F35000_InsertObject(parcelId, objectTypeId, description, userId);
        }

        #endregion

        #region Save Appraisal

        public static void F35000_SaveAppraisal(int parcelId, string propertiesXML, int userId)
        {
            F35000ApprisalSummaryComp.F35000_SaveAppraisal(parcelId, propertiesXML, userId);
        }

        #endregion Save Appraisal
        #region List Object Slice Types

        /// <summary>
        /// F35000_s the list object slice types.
        /// </summary>
        /// <returns>DataSet Contains the List Object and Slice Types</returns>
        public static F35000AppraisalSummaryData F35000_ListObjectSliceTypes(int? parcelId)
        {
            return F35000ApprisalSummaryComp.F35000_ListObjectSliceTypes(parcelId);
        }

        #endregion

        #region List  Slice Types

        /// <summary>
        /// F35000_s the list  slice types.
        /// </summary>
        /// <param name="ObjectId">The value ObjectId.</param>
        /// <returns>DataSet Contains the List  Slice Types</returns>
        public static F35000AppraisalSummaryData F35000_ListSliceTypes(int objectId)
        {
            return F35000ApprisalSummaryComp.F35000_ListSliceTypes(objectId);
        }

        #endregion

        #region Object Total value
        /// <summary>
        /// F35000_s the object total.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F35000AppraisalSummaryData F35000_ObjectTotal(int parcelId)
        {
            return F35000ApprisalSummaryComp.F35000_ObjectTotal(parcelId);
        }
        #endregion

        #region UserCheck

        /// <summary>
        /// F35000_s the list object slice types.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// DataSet Contains the List Object and Slice Types
        /// </returns>
        public static F35000AppraisalSummaryData F35000_CheckAppraisalSummaryUser(int valueSliceId, int objectId, int userId)
        {
            return F35000ApprisalSummaryComp.F35000_CheckAppraisalSummaryUser(valueSliceId, objectId, userId);
        }

        #endregion

        #endregion

        #region F27000 Misc Assessment

        #region Get Misc Assessment Details

        /// <summary>
        /// Gets the Misc Assessment details based on the Misc Assessment DistrictId
        /// </summary>
        /// <param name="madistrictId">The Misc Assessment District Id.</param>
        /// <returns>
        /// The typed dataset containing the Misc Assessment information of the madistrictId.
        /// </returns>
        public static F22000MiscAssessmentData F27000_GetMiscAssessment(int madistrictId )
        {
            return F27000MiscAssessmentComp.F27000_GetMiscAssessment(madistrictId);
        }

        #endregion Get Misc Assessment Details

        #region List Misc Assessment District Type

        /// <summary>
        /// To List all the Misc Assessment District Types.
        /// </summary>
        /// <returns>Typed Dataset Containing the Misc Assessment District Types</returns>
        public static CommonData F27000_ListMADistrictType()
        {
            return F27000MiscAssessmentComp.F27000_ListMADistrictType();
        }

        #endregion List Misc Assessment District Type

        #region List Misc Assessment District Item Type

        /// <summary>
        /// To List All Misc Assessment District Item Type
        /// </summary>
        /// <param name="madistrictTypeId">The Misc Assessment District type Id.</param>
        /// <returns>Typed Dataset Containg the All Misc Assessment Misc Assessment Item Types</returns>
        public static CommonData F27000_ListMADistrictItemType(int madistrictTypeId)
        {
            return F27000MiscAssessmentComp.F27000_ListMADistrictItemType(madistrictTypeId);
        }

        #endregion List Misc Assessment District Item Type

        #region Save Misc Assessment Details

        /// <summary>
        /// To Save Misc Assessment Details
        /// </summary>
        /// <param name="distributionItems">distributionItems</param>
        /// <param name="subHeaderItems">subHeaderItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public static int F27000_SaveMADetails(string distributionItems, string subHeaderItems, int userId)
        {
            return F27000MiscAssessmentComp.F27000_SaveMADetails(distributionItems, subHeaderItems, userId);
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
        public static F15015StatementOwnershipData F15015_ListStatementOwnership(int statementId)
        {
            return F15015StatementOwnershipComp.F15015_ListStatementOwnership(statementId);
        }

        #endregion List Statement Ownership

        #region F15015 list MOwnerType Selection

        /// <summary>
        /// Lists the type of the Mowner.
        /// </summary>
        /// <returns>Mowner Type List</returns>
        public static F15015StatementOwnershipData F15015_ListMOwnerType()
        {
            return F15015StatementOwnershipComp.F15015_ListMOwnerType();
        }

        #endregion

        #region Save Statement Ownership

        /// <summary>
        /// To Save Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementOwner">The statement owner.</param>
        /// <param name="userId">UserID</param>
        public static void F15015_SaveStatementOwnership(int statementId, string statementOwner, int userId)
        {
            F15015StatementOwnershipComp.F15015_SaveStatementOwnership(statementId, statementOwner, userId);
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
        public static F15015StatementOwnershipData F15015_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            return F15015StatementOwnershipComp.F15015_ListALLOwnerDetails(firstName, lastName, address1, address2, city);
        }

        #endregion List All Owner Details

        #endregion F15015 Statement Ownership

        #region F95101 Audit Trail

        /// <summary>
        /// To List Audit Trail records
        /// </summary>
        /// <param name="form">Form No</param>
        /// <param name="keyId">Key ID</param>
        /// <returns>Typed DataSet Containing the Audit Trail Details</returns>
        public static F95101AuditTrailData F95101_ListAuditTrail(int form, int keyId)
        {
            return F95101AuditTrailComp.F95101_ListAuditTrail(form, keyId);
        }

        #endregion F95101 Audit Trail

        #region F9060 Auditing Configuration

        #region List Auditing Tables

        /// <summary>
        /// To List Audit Table Details
        /// </summary>
        /// <param name="tableType">Table Type</param>
        /// <returns>Typed Dataset Containing the Audit Table Details</returns>
        public static F9060AuditingConfigurationData F9060_ListAuditingTables(string tableType)
        {
            return F9060AuditingConfigurationComp.F9060_ListAuditingTables(tableType);
        }

        #endregion List Auditing Tables

        #region List Auditing Columns

        /// <summary>
        /// To List Audit Column Details
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <returns>Typed Dataset Containing the Audit Column Details</returns>
        public static F9060AuditingConfigurationData F9060_ListAuditingColumns(string tableName)
        {
            return F9060AuditingConfigurationComp.F9060_ListAuditingColumns(tableName);
        }

        #endregion List Auditing Columns

        #region Save Audit Column Configuration

        /// <summary>
        /// To Save the Audit Configuration
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="auditColumns">Audit Columns</param>
        /// <param name="userId">UserID</param>
        public static void F9060_SaveAuditConfiguration(string tableName, string auditColumns, int userId)
        {
            F9060AuditingConfigurationComp.F9060_SaveAuditConfiguration(tableName, auditColumns, userId);
        }

        #endregion Save Audit Column Configuration

        #region Delete Audit Column Configuration

        /// <summary>
        /// To Save the Audit Configuration
        /// </summary>
        /// <param name="auditTableId">The audit table id.</param>
        /// <param name="userId">UserID</param>
        public static void F9060_DeleteAuditConfiguration(int auditTableId, int userId)
        {
            F9060AuditingConfigurationComp.F9060_DeleteAuditConfiguration(auditTableId, userId);
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
        public static F36000MarshalAndSwiftData F36000_GetHouseTypeCollection(int valueSliceId)
        {
            return F36000MarshalAndSwiftComp.F36000_GetHouseTypeCollection(valueSliceId);
        }

        #endregion Get House Type Collection

        #region Depreciation Percentage

        /// <summary>
        /// F36000_s the get depr percentage.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        public static string F36000_GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        {
            return F36000MarshalAndSwiftComp.F36000_GetDeprPercentage(age, objectCondition, deprTableId);
        }

        #endregion Depreciation Percentage

        #region Depr Table Name

        /// <summary>
        /// F36000_s the name of the get depr table.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="propertyQuality">The property quality.</param>
        /// <returns>int</returns>
        public static int F36000_GetDeprTableNameId(int valueSliceId, int propertyQuality)
        {
            return F36000MarshalAndSwiftComp.F36000_GetDeprTableNameId(valueSliceId, propertyQuality);
        }

        #endregion Depr Table Name

        #region Depr Save

        /// <summary>
        /// F36000_s the save depreciation details.
        /// </summary>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F36000_SaveDepreciationDetails(string depreciationXml, int valueSliceId, int userId)
        {
            return F36000MarshalAndSwiftComp.F36000_SaveDepreciationDetails(depreciationXml, valueSliceId, userId);
        }

        #endregion Depr Save

        #endregion F36000 Marshal & Swift

        #region F25011 Street List Management

        #region Get the Master Street Data

        /// <summary>
        /// To Get Master Street Data.
        /// </summary>
        /// <param name="streetId">Street ID</param>
        /// <returns>Typed DataSet Containing the Master Street data.</returns>
        public static F25011StreetListManagementData F25011_GetMasterStreetList(int streetId)
        {
            return F25011StreetListManagementComp.F25011_GetMasterStreetList(streetId);
        }

        #endregion Get the Master Street Data

        #region List Master Street List

        /// <summary>
        /// F25011_s the list master street list.
        /// </summary>
        /// <param name="streetID">The street ID.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public static F25011StreetListManagementData F25011_ListMasterStreetList(int streetID, string streetName, string city)
        {
            return F25011StreetListManagementComp.F25011_ListMasterStreetList(streetID, streetName, city);
        }

        #endregion List Master Street List

        #region List Street City Directional Suffix

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <returns>Typed Dataset conitaining the Street's City, Directional and Suffixs details</returns>
        public static F25011StreetListManagementData F25011_ListStreetCityDirectionalSuffixDetails()
        {
            return F25011StreetListManagementComp.F25011_ListStreetCityDirectionalSuffixDetails();
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
        public static int F25011_SaveStreetListManagement(int streetId, string streetListMgmt, int userId)
        {
            return F25011StreetListManagementComp.F25011_SaveStreetListManagement(streetId, streetListMgmt, userId);
        }

        #endregion Save Street List Management

        #region Delete Street List

        /// <summary>
        /// F25011_s the delete street list.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Deleted Flag</returns>
        public static int F25011_DeleteStreetList(int streetId, int userId)
        {
            return F25011StreetListManagementComp.F25011_DeleteStreetList(streetId, userId);
        }

        #endregion Delete Street List

        #endregion F25011 Street List Management

        #region F2000 ParcelStatus

        #region F2000 ListParcelStatus
        /// <summary>
        /// To List the Parcel Status.
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>Typed DataSet Containing the List of Parcel Status .</returns>
        public static F2000ParcelStatusData F2000_ListParcelStatus(int parcelId)
        {
            return F2000ParcelStatusComp.F2000_ListParcelStatus(parcelId);
        }
        #endregion

        #region F2000 Delete Parcel

        /// <summary>
        /// To Dealte ParcelId
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="userId">UserID</param>
        public static void F2000_DeleteParcelStatus(int parcelId, int userId)
        {
            F2000ParcelStatusComp.F2000_DeleteParcelStatus(parcelId, userId);
        }

        #endregion

        #region F2000 Update parcel

        /// <summary>
        /// Update ParcelDetails For a ParcelID
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="description">description</param>
        /// <param name="parcelType">Parcel Type</param>
        /// <param name="isexempt">The isexempt.</param>
        /// <param name="isownerPrimary">The isowner primary.</param>
        /// <param name="userId">UserID</param>
        /// <returns>It Returns a Updated ParcelID</returns>
        public static int F2000_UpdateParcelStatus(int parcelId, string description, string parcelType, int isexempt, int isownerPrimary, int userId)
        {
            return F2000ParcelStatusComp.F2000_UpdateParcelStatus(parcelId, description, parcelType, isexempt, isownerPrimary, userId);
        }

        #endregion

        #region ListRecordLockStatus
        public static string ListRecordLockStatus(int formNo, int keyId)
        {
            return F2000ParcelStatusComp.ListRecordLockStatus(formNo, keyId);
        }
        #endregion


        #endregion

        #region F25009 Legal Management

        #region Get Legal Management Details

        /// <summary>
        ///  To Load F25009 Legal Management.
        /// </summary>
        /// <param name="parcelId">The Parcel ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25009LegalManagementData F25009_GetLegalManagement(int parcelId, int userId)
        {
            return F25009LegalManagementComp.F25009_GetLegalManagement(parcelId, userId);
        }

        #endregion

        #region Save Legal Management

        /// <summary>
        /// To Save F25009 Legal Management.
        /// </summary>
        /// <param name="legalId">The Legal ID.</param>
        /// <param name="legalItems">The XML string Containing All values in Legal Management.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing Parcel Id</returns>
        public static int F25009_SaveLegalManagement(int legalId, string legalItems, bool isFuturePush, int userId)
        {
            return F25009LegalManagementComp.F25009_SaveLegalManagement(legalId, legalItems, isFuturePush, userId);
        }

        #endregion

        #region List Subdivision

        /// <summary>
        ///  To Load Subdivision.
        /// </summary>        
        /// <returns>Typed DataSet Containing All the Subdivision Details</returns>
        public static F25009LegalManagementData F25009_ListSubdivision()
        {
            return F25009LegalManagementComp.F25009_ListSubdivision();
        }

        #endregion

        #endregion

        #region F25090 Field Summary


        /// <summary>
        ///  To Load Quick Value Summary
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_FieldSummary(int keyId)
        {
            return F25090FieldSummaryComp.F25090_FieldSummary(keyId);
        }


        /// <summary>
        ///  To Load Building Permits
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_BuildingPermits(int keyId)
        {
            return F25090FieldSummaryComp.F25090_BuildingPermits(keyId);
        }

        /// <summary>
        ///  To Load Ancestry Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_GetAncestryData(int keyId)
        {
            return F25090FieldSummaryComp.F25090_GetAncestryData(keyId);
        }

        /// <summary>
        ///  To Load Correction Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_GetCorrection(int keyId)
        {
            return F25090FieldSummaryComp.F25090_GetCorrection(keyId);
        }

        /// <summary>
        ///  To Load History Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_GetHistoryData(int keyId)
        {
            return F25090FieldSummaryComp.F25090_GetHistoryData(keyId);
        }

        /// <summary>
        ///  To Load Parcel Ownership Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_GetParcelOwnerShip(int keyId)
        {
            return F25090FieldSummaryComp.F25090_GetParcelOwnerShip(keyId);
        }

        /// <summary>
        ///  To Load Parcel Ownership Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_GetPhotos(int keyId, int form)
        {
            return F25090FieldSummaryComp.F25090_GetPhotos(keyId, form);
        }

        /// <summary>
        ///  To Load Parcel Sale Data
        /// </summary>
        /// <param name="keyId">The keyId ID.</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25090FieldSummaryData F25090_ParcelSale(int keyId)
        {
            return F25090FieldSummaryComp.F25090_ParcelSale(keyId);
        }

        #endregion F25090 Field Summary

        #region F15110 Receipt Actions

        #region Get Receipt Actions Details

        /// <summary>
        /// Gets the Receipt Actions details based on the ReceiptId
        /// </summary>
        /// <param name="receiptId">The Receipt Id.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Actions information of the receiptId.
        /// </returns>
        public static F15110ReceiptActionsData F15110_GetReceiptActions(int receiptId)
        {
            return F15110ReceiptActionsComp.F15110_GetReceiptActions(receiptId);
        }

        /// <summary>
        /// F1557_s the insert refund interest.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        /// <param name="userID">The user ID.</param>
        public static void F1557_InsertRefundInterest(int receiptID, int userID)
        {
            F15110ReceiptActionsComp.F1557_InsertRefundInterest(receiptID, userID);
        }

        #endregion Get Receipt Details

        #endregion F15110 Receipt Actions

        #region F25003 Situs Management

        #region List Situs Management Details

        /// <summary>
        /// To List Situs Mangement Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="situsId">The situs id.</param>
        /// <returns>Typed Dataset containing the Situs Mangement Details</returns>
        public static F25003SitusManagementData F25003_ListSitusMangement(int parcelId, int situsId)
        {
            return F25003SitusManagementComp.F25003_ListSitusMangement(parcelId, situsId);
        }

        #endregion List Situs Management Details

        #region List Street Details

        /// <summary>
        /// To List Street Details.
        /// </summary>
        /// <returns>Typed Dataset containing the Street Details</returns>
        public static F25003SitusManagementData F25003_ListStreet()
        {
            return F25003SitusManagementComp.F25003_ListStreet();
        }

        #endregion List Street Details

        #region List Unit Type Details

        /// <summary>
        /// To list Unit Type Details.
        /// </summary>
        /// <returns>Typed DataSet containing the Unit Type Details</returns>
        public static F25003SitusManagementData F25003_ListUnitType()
        {
            return F25003SitusManagementComp.F25003_ListUnitType();
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
        public static int F25003_SaveListMangement(int situsId, string situsItems, bool isFuturePush, int userId)
        {
            return F25003SitusManagementComp.F25003_SaveListMangement(situsId, situsItems, isFuturePush, userId);
        }

        #endregion Save Situs Management

        #region Delete Situs Management

        /// <summary>
        /// To Delete the Situs management
        /// </summary>
        /// <param name="situsId">situsId</param>
        /// <param name="userId">UserID</param>
        public static void F25003_DeleteSitusManagement(int situsId, int userId)
        {
            F25003SitusManagementComp.F25003_DeleteSitusManagement(situsId, userId);
        }

        #endregion Delete Situs Management

        #endregion F25003 Situs Management

        #region F1555 Receipt Details

        #region Get Receipt Details

        /// <summary>
        /// Gets the Receipt details based on the ReceiptId
        /// </summary>
        /// <param name="receiptId">The Receipt Id.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Details information of the receiptId.
        /// </returns>
        public static F1555_ReceiptDetailsData F1555_GetReceiptDetails(int receiptId)
        {
            return F1555_ReceiptDetailsComp.F1555_GetReceiptDetails(receiptId);
        }

        #endregion Get Receipt Details

        #region Reverse Receipt Details

        /// <summary>
        /// Reverse receipt details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="sharedPaymentId">Shared Payment Id</param>
        /// <param name="userId">User ID</param>
        /// <returns>Reverse Payment Details</returns>
        public static F1555_ReceiptDetailsData F1556_ReverseReceiptDetails(int receiptId, int sharedPaymentId, int userId)
        {
            return F1555_ReceiptDetailsComp.F1556_ReverseReceiptDetails(receiptId, sharedPaymentId, userId);
        }

        #endregion Reverse Receipt Details

        #endregion F1555 Receipt Details

        #region SnapShotUtility

        #region F9040_ListBatchButtonSnapShots

        /// <summary>
        /// To List the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="formsSliceNo">The forms slice no.</param>
        /// <returns>Typed DataSet containg the list of F1440 Batch Button SnapShots for Current form slice</returns>
        public static F9040SnapShotUtilityData F9040_ListBatchButtonSnapShots(int formsSliceNo)
        {
            return F9040SnapShotUtilityComp.F9040_ListBatchButtonSnapShots(formsSliceNo);
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
        public static int F9040_SaveBatchButtonSnapShots(int snapShotId, string snapShotDetails, int userId)
        {
            return F9040SnapShotUtilityComp.F9040_SaveBatchButtonSnapShots(snapShotId, snapShotDetails, userId);
        }

        #endregion F9040_SaveBatchButtonSnapShots

        #region ListSnapShots

        /// <summary>
        /// Lists the SnapShots for the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9040SnapShotUtilityData Dataset</returns>
        public static F9040SnapShotUtilityData F9040_ListSnapShots(int formId)
        {
            return F9040SnapShotUtilityComp.F9040_ListSnapShots(formId);
        }

        #endregion ListSnapShot

        #region SaveSnapShot

        /// <summary>
        /// F9040_s the save snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotXML">The snap shot XML.</param>
        /// <param name="snapshotItemsXML">The snapshot items XML.</param>
        /// <param name="filterXML">Filter XML</param>
        /// <param name="pinType">Pinning Type </param>
        /// <param name="userId" >UserID</param>
        /// <param name="parentSnapShotID">Parent Snapshot ID</param>
        /// <returns>the saved snapshotid</returns>
        public static int F9040_SaveSnapShot(int snapShotId, string snapShotXML, string snapshotItemsXML, string filterXML, string pinType, int userId, int parentSnapShotID)
        {
            return F9040SnapShotUtilityComp.F9040_SaveSnapShot(snapShotId, snapShotXML, snapshotItemsXML, filterXML, pinType, userId, parentSnapShotID);
        }

        #endregion SaveSnapShot

        #region DeleteSnapShot

        /// <summary>
        /// To Delete F040 Snapshot
        /// </summary>
        /// <param name="snapshotId">The snapshotId</param>
        /// <param name="userId">UserID</param>
        public static void F9040_DeleteSnapShot(int snapshotId, int userId)
        {
            F9040SnapShotUtilityComp.F9040_DeleteSnapShot(snapshotId, userId);
        }

        #endregion DeleteSnapShot

        #endregion SnapShotUtility

        #region F1060 Suspended Payment Selection

        #region F1060 List Suspended Payment

        /// <summary>
        /// F1060_s the list suspended payment.
        /// </summary>
        /// <param name="SEARCH XML">Search Detail.</param>
        /// <returns>Typed DataSet containing the Suspended Payment Details.</returns>
        public static F1060SudpendedPaymentSelectionData F1060_ListSuspendedPayment(string searchDetail)
        {
            return F1060SudpendedPaymentSelectionComp.F1060_ListSuspendedPayment(searchDetail);
        }

        #endregion F1060 List Suspended Payment

        #endregion F1060 Suspended Payment Selection

        #region F9070Report Listing

        /// <summary>
        /// F9070s the get report listing details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed Dataset</returns>
        public static F9070ReportListingData F9070GetReportListingDetails(int userId)
        {
            return F9070ReportListingComp.F9070GetReportListingDetails(userId);
        }
        #endregion F9070Report Listing

        #region F25008 Parcel Miscellaneous

        #region Get Parcel Miscellaneous

        /// <summary>
        /// Get ParcelMiscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <returns>ParcelMiscellaneous Data</returns>
        public static F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousData(int parcelId)
        {
            return F25008ParcelMiscellaneousComp.F25008_ParcelMiscellaneousData(parcelId);
        }

        #endregion Get Parcel Miscellaneous

        #region Get Parcel Miscellaneous Configuration

        /// <summary>
        /// Get the ParcelMiscellaneous Configuration Data
        /// </summary>
        /// <returns>ParcelMiscellaneous Config Data</returns>
        public static F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousConfigData()
        {
            return F25008ParcelMiscellaneousComp.F25008_ParcelMiscellaneousConfigData();
        }

        #endregion Get Parcel Miscellaneous Configuration

        #region Save Parcel Miscellaneous

        /// <summary>
        /// Save ParcelMiscellaneous
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="miscellaneous">miscellaneous</param>
        /// <param name="userId">UserID</param>
        public static void F25008_SaveParcelMiscellaneous(int parcelId, string miscellaneous, int userId)
        {
            F25008ParcelMiscellaneousComp.F25008_SaveParcelMiscellaneous(parcelId, miscellaneous, userId);
        }

        #endregion Save Parcel Miscellaneous

        #endregion F25008 Parcel Miscellaneous

        #region F35101 Neighborhood Group Header

        #region Get Neighborhood Group Header

        /// <summary>
        /// To Load F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Neighborhood Group Header Details
        /// </returns>
        public static F35101NeighborhoodGroupHeaderData F35101_GetNeighborhoodGroupHeader(int nbhdGroupId)
        {
            return F35101NeighborhoodGroupHeaderComp.F35101_GetNeighborhoodGroupHeader(nbhdGroupId);
        }

        #endregion

        #region Save Neighborhood Group Header

        /// <summary>
        /// To Save F35101 Neighborhood Group Header.
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group id.</param>
        /// <param name="neighborhoodGroupHeader">The Neighborhood Group Header Details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The integer value containing the Neighborhood Group Header id</returns>
        public static int F35101_SaveNeighborhoodGroupHeader(int nbhdGroupId, string neighborhoodGroupHeader, int userId)
        {
            return F35101NeighborhoodGroupHeaderComp.F35101_SaveNeighborhoodGroupHeader(nbhdGroupId, neighborhoodGroupHeader, userId);
        }

        #endregion

        #region Delete Neighborhood Group Header

        /// <summary>
        /// To Delete F35101 Neighborhood Group Header
        /// </summary>
        /// <param name="nbhdGroupId">The Neighborhood Group Header Id</param>
        /// <param name="userId">UserID</param>
        public static void F35101_DeleteNeighborhoodGroupHeader(int nbhdGroupId, int userId)
        {
            F35101NeighborhoodGroupHeaderComp.F35101_DeleteNeighborhoodGroupHeader(nbhdGroupId, userId);
        }

        #endregion

        #region SeniorExemption

        /// <summary>
        /// F29600_GetSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>Dataset</returns>
        public static F29600SeniorExemptData F29600_GetSeniorExemptionDetails(int eventId, int userId)
        {
            return F29600SeniorExemptComp.F29600_GetSeniorExemptionDetails(eventId, userId);
        }

        /// <summary>
        /// F29600_GetSeniorExemptionCode
        /// </summary>
        /// <param name="effectiveDate">Effective Date</param>
        /// <returns>DataSet</returns>
        public static F29600SeniorExemptData F29600_GetSeniorExemptionCode(string effectiveDate)
        {
            return F29600SeniorExemptComp.F29600_GetSeniorExemptionCode(effectiveDate);
        }

        /// <summary>
        /// F29600_saveSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="seniorExemptDetails">seniorExemptDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public static int F29600_saveSeniorExemptionDetails(int eventId, string seniorExemptDetails, int userId)
        {
            return F29600SeniorExemptComp.F29600_saveSeniorExemptionDetails(eventId, seniorExemptDetails, userId);
        }
        #endregion SeniorExemption

        #region ParcelSaleTracking

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>Dataset</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelSaleTrackingDetails(int eventId)
        {
            return F29550ParcelSaleTrackingComp.F29550_GetParcelSaleTrackingDetails(eventId);
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="saleId">Sale ID</param>
        /// <returns>Dataset</returns>
        public static F29550ParcelSaleTracking F29550_GetPushOwner(int saleId)
        {
            return F29550ParcelSaleTrackingComp.F29550_GetPushOwner(saleId);
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingDetails
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <param name="neewParcelId">New Parcel ID</param>
        /// <param name="saleId">SaleID</param>
        /// <returns>DataSet</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelDetails(string parcelIdDetails, int neewParcelId, int saleId)
        {
            return F29550ParcelSaleTrackingComp.F29550_GetParcelDetails(parcelIdDetails, neewParcelId, saleId);
        }

        /// <summary>
        /// F29550_GetParcelSaleTrackingComboDetails
        /// </summary>
        /// <returns>Dataset</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelSaleTrackingComboDetails()
        {
            return F29550ParcelSaleTrackingComp.F29550_GetParcelSaleTrackingComboDetails();
        }

        /// <summary>
        /// F29550_GetParcelsOwnerDetails
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <returns>Dataset</returns>
        public static F29550ParcelSaleTracking F29550_GetParcelsOwnerDetails(string parcelIdDetails)
        {
            return F29550ParcelSaleTrackingComp.F29550_GetParcelsOwnerDetails(parcelIdDetails);
        }

        /// <summary>
        /// Save Parcel Sale Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="saleItems">Sale ITems</param>
        /// <param name="parcelItems">Parcel Items</param>
        /// <param name="ownerItems">Owner Items</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F29550_saveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return F29550ParcelSaleTrackingComp.F29550_saveParcelSaleDetails(eventId, saleItems, parcelItems, ownerItems, userId);
        }
        #endregion ParcelSaleTracking

        #region Neighborhood

        ///// <summary>
        ///// F35100_GetNeighborhoodHeaderDetails
        ///// </summary>
        ///// <param name="neighborId">neighborId</param>
        ///// <returns>F35100NeighborhoodComp</returns>
        ////public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodHeaderDetails(int neighborId)
        ////{
        ////    return F35100NeighborhoodComp.F35100_GetNeighborhoodHeaderDetails(neighborId);
        ////}

        /// <summary>
        /// F35100_GetNeighborhoodHeaderUserDetails
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>F35100NeighborhoodComp</returns>
        public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodHeaderUserDetails(int applicationId)
        {
            return F35100NeighborhoodComp.F35100_GetNeighborhoodHeaderUserDetails(applicationId);
        }

        ///// <summary>
        ///// F35100_GetNeighborhoodHeaderUserDetails
        ///// </summary>
        ///// <param name="rollYear">rollYear</param>
        ///// <returns>F35100_GetNeighborhoodGroupDetails</returns>
        ////public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodGroupDetails(int rollYear)
        ////{
        ////    return F35100NeighborhoodComp.F35100_GetNeighborhoodGroupDetails(rollYear);
        ////}

        /// <summary>
        /// Copy Neighborhood Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F3511_ExeNeighborhoodDetails(int nbhId, string newnbhdName, int userId)
        {
            return F35100NeighborhoodComp.F35100_SaveNeighborhoodHeaderDetails(nbhId, newnbhdName, userId);
        }

        /// <summary>
        /// Save Neighborhood Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F35100_SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId)
        {
            return F35100NeighborhoodComp.F35100_SaveNeighborhoodHeaderDetails(nbhId, nbhDetails, userId);
        }

        /// <summary>
        /// Check Duplicate Neighborhood Header
        /// </summary>
        /// <param name="nbhId">Neighborhood Header ID</param>
        /// <param name="nbhDetails">Neighborhood Header Details</param>
        /// <returns>Integer</returns>
        public static int DuplicateNeighborhoodHeaderCheck(int nbhId, string nbhDetails)
        {
            return F35100NeighborhoodComp.DuplicateNeighborhoodHeaderCheck(nbhId, nbhDetails);
        }

        /// <summary>
        /// F35101_DeleteNeighborhoodGroupHeader
        /// </summary>
        /// <param name="nbhdId">Neighborhood ID</param>
        /// <param name="userId">UserID</param>
        public static void F35100_DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            F35100NeighborhoodComp.F35100_DeleteNeighborhoodHeader(nbhdId, userId);
        }
        #region Neighborhood Details

        /// <summary>
        /// GetNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="neighborId">neighborId</param>
        /// <returns>F35100NeighborhoodComp</returns>
        public static F35100NeighborhoodHeaderData GetNeighborhoodHeaderDetails(int neighborId)
        {
            return F35100NeighborhoodComp.GetNeighborhoodHeaderDetails(neighborId);
        }

        #endregion Neighborhood Details

        #region ParentDetails
        /// <summary>
        /// GetNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="rollyear">Roll Year</param>
        /// <param name="type">Type</param>
        /// <param name="parentNeighborhood">Parent Neighborhood</param>
        /// <returns>F35100NeighborhoodComp</returns>
        public static F35100NeighborhoodHeaderData GetParentNeighborhoodHeaderDetails(int rollyear, int type, int parentNeighborhood)
        {
            return F35100NeighborhoodComp.GetParentNeighborhoodHeaderDetails(rollyear, type, parentNeighborhood);
        }
        #endregion ParentDetails

        #region saveNeighborhood

        /// <summary>
        /// Save Neighborhood Header
        /// </summary>
        /// <param name="nbhId">Neighborhood Header ID</param>
        /// <param name="nbhDetails">Neighborhood Header Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId)
        {
            return F35100NeighborhoodComp.SaveNeighborhoodHeaderDetails(nbhId, nbhDetails, userId);
        }

        #endregion saveNeighborhood

        #region DeleteNeighborhoodRecord

        /// <summary>
        /// Delete Neighborhood Header
        /// </summary>
        /// <param name="nbhdId">Neighborhood Header ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            return F35100NeighborhoodComp.DeleteNeighborhoodHeader(nbhdId, userId);
        }

        #endregion DeleteNeighborhoodRecord

        #region NeighborhoodParcelLock

        /// <summary>
        /// GetNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="neighborId">neighborId</param>
        /// <returns>F35100NeighborhoodComp</returns>
        public static F3501NeighborhoodParcelLocksData ListNeighborhoodParcelLocks(int neighborId)
        {
            return F3501NeighborhoodParcelLocks.ListNeighborhoodParcelLocks(neighborId);
        }

        /// <summary>
        /// F3501_s the update parcel locking details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="valueLock">The value lock.</param>
        /// <param name="adminLock">The admin lock.</param>
        /// <param name="lockAppraisal">The lock appraisal.</param>
        /// <param name="primaryId">The primary id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// Typed Dataset containing the Parcel Lock Details to Save
        /// </returns>
        public static F3501NeighborhoodParcelLocksData UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int primaryId, int userId)
        {
            return F3501NeighborhoodParcelLocks.UpdateParcelLockingDetails(keyId, valueLock, adminLock, lockAppraisal, primaryId, userId);
        }

        #endregion NeighborhoodParcelLock

        #region F35102 Neighborhood Configuration

        #region Get Neighborhood Cfg Details

        /// <summary>
        /// Get the Receipt Header
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
        public static F35102NeighborhoodCfgData GetNeighborhoodCfgDetails(int nbhdId)
        {
            return F35102NeighborhoodCfgComp.GetNeighborhoodCfgDetails(nbhdId);
        }

        #endregion Get Neighborhood Cfg Details

        #region Get Neighborhood Cfg Choice

        /// <summary>
        /// Get the CfgChoice
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="nbhdCfgId">The NBHD CFG id.</param>
        /// <returns>1</returns>
        public static F35102NeighborhoodCfgData GetNeighborhoodCfgChoice(int nbhdId, int nbhdCfgId)
        {
            return F35102NeighborhoodCfgComp.GetNeighborhoodCfgChoice(nbhdId, nbhdCfgId);
        }

        #endregion Get Neighborhood Cfg Choice

        #region Save Neighborhood Cfg Details

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="neighborhoodConfigId">The Neighborhood Configuration id.</param>
        /// <param name="neighborhoodConfigDetails">The Neighborhood Configuration Details.</param>
        /// <param name="userId">UserID</param>
        public static void F35102_SaveNeighborhoodCfgDetails(int neighborhoodConfigId, string neighborhoodConfigDetails, int userId)
        {
            F35102NeighborhoodCfgComp.F35102_SaveNeighborhoodCfgDetails(neighborhoodConfigId, neighborhoodConfigDetails, userId);
        }

        #endregion Save Neighborhood Cfg Details

        #endregion F35102 Neighborhood Configuration

        #endregion Neighborhood

        #endregion

        #region F3040 Zoning

        #region F3040 Get Zoning

        /// <summary>
        /// Used to Get the Zoning Details
        /// </summary>
        /// <returns>Gets Typed DataSet containing the Zoning Details.</returns>
        public static F3040ZoningData F3040_GetZoningDetails()
        {
            return F3040ZoningComp.F3040_GetZoningDetails();
        }

        #endregion F3040 Get Zoning

        #region F3040 Save Zoning

        /// <summary>
        /// Used to Save the Zoning Details
        /// </summary>
        /// <param name="zoningDetails">The zoning details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed DataSet containing the Zoning Details to Save.</returns>
        public static int F3040_SaveZoningDetails(string zoningDetails, int userId)
        {
            return F3040ZoningComp.F3040_SaveZoningDetails(zoningDetails, userId);
        }

        #endregion F3040 Save Zoning

        #endregion F3040 Zoning

        #region F15035 Suspended Payments

        /// <summary>
        /// F15035s the suspended payments.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>typed dataset</returns>
        public static F15035SuspendedPaymentsData F15035SuspendedPayments(int receiptId)
        {
            return F15035SuspendedPaymentsComp.F15035SuspendedPayments(receiptId);
        }

        /// <summary>
        /// F15035_s the delete suspended payment.
        /// </summary>
        /// <param name="receiptId">The receipt ID.</param>
        /// <param name="userId">UserID</param>
        public static void F15035_DeleteSuspendedPayment(int receiptId, int userId)
        {
            F15035SuspendedPaymentsComp.F15035_DeleteSuspendedPayment(receiptId, userId);
        }

        /// <summary>
        /// F15035_s the check suspended accounts.
        /// </summary>
        /// <returns>status id</returns>
        public static int F15035_CheckSuspendedAccounts()
        {
            return F15035SuspendedPaymentsComp.F15035_CheckSuspendedAccounts();
        }

        #endregion F15035 Suspended Payments

        #region F8062 Components Configuration

        #region List Components Configuration

        /// <summary>
        /// F8062_s the list components configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>typed dataset containing the components configuration</returns>
        public static F8062ComponentsConfigData F8062_ListComponentsConfiguration(int applicationId)
        {
            return F8062ComponentsConfigComp.F8062_ListComponentsConfiguration(applicationId);
        }

        #endregion List Components Configuration

        #region List Feature Class

        /// <summary>
        /// F8062_s the list feature class.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>typed dataset containing the feature class details</returns>
        public static F8062ComponentsConfigData F8062_ListFeatureClass(int applicationId)
        {
            return F8062ComponentsConfigComp.F8062_ListFeatureClass(applicationId);
        }

        #endregion List Feature Clas

        #region Save Components Configuration

        /// <summary>
        /// F8062_s the save components configuration.
        /// </summary>
        /// <param name="componentsConfig">The components config.</param>
        /// <param name="userId">UserID</param>
        public static void F8062_SaveComponentsConfiguration(string componentsConfig, int userId)
        {
            F8062ComponentsConfigComp.F8062_SaveComponentsConfiguration(componentsConfig, userId);
        }

        #endregion Save Components Configuration

        #region Delete Components Configuration

        /// <summary>
        /// Deletes the Components Configuration.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        public static int F8062_DeleteComponentsConfiguration(int componentId, int userId)
        {
            return F8062ComponentsConfigComp.F8062_DeleteComponentsConfiguration(componentId, userId);
        }
        #endregion

        #endregion F8062 Components Configuration

        #region F8058 Resources Configuration

        #region List Resources Configuration

        /// <summary>
        /// F8058_s the list resources config details.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F8058ResourcesConfigData F8058_ListResourcesConfigDetails()
        {
            return F8058ResourceConfigComp.F8058_ListResourcesConfigDetails();
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
        public static int F8058_InsertResourcesConfigDetails(int equipmentId, string equiptResource, int applicationId, int userId)
        {
            return F8058ResourceConfigComp.F8058_InsertReosurcesConfigDetails(equipmentId, equiptResource, applicationId, userId);
        }

        #endregion Insert  Resources Configuration

        #region Delete Resources Configuration

        /// <summary>
        /// F8058_s the delete resources config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F8058_DeleteResourcesConfigDetails(int equipmentId, int userId)
        {
            return F8058ResourceConfigComp.F8058_DeleteResourcesConfigDetails(equipmentId, userId);
        }

        #endregion Delete Resources Configuration

        #endregion F8058 Resources Configuration

        #region F8060 Parts Configuration

        #region List Parts Configuration

        /// <summary>
        /// Lists the Parts Configuration details
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <returns>returns dataset contains Parts Configuration details</returns>
        public static F8060PartsConfigData F8060_ListPartsConfig(int componentId)
        {
            return F8060PartsConfigComp.F8060_ListPartsConfig(componentId);
        }

        #endregion

        #region List Components

        /// <summary>
        /// Lists the Components detail
        /// </summary>
        /// <returns>returns dataset contains Components details</returns>
        public static F8060PartsConfigData F8060_ListComponents()
        {
            return F8060PartsConfigComp.F8060_ListComponents();
        }

        #endregion

        #region Save Parts Configuration

        /// <summary>
        /// F8062_s the save Parts configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="partsConfig">The parts config.</param>
        /// <param name="userId">UserID</param>
        public static void F8060_SavePartsConfiguration(int partId, string partsConfig, int userId)
        {
            F8060PartsConfigComp.F8060_SavePartsConfiguration(partId, partsConfig, userId);
        }

        #endregion Save Parts Configuration

        #region Delete Parts Configuration

        /// <summary>
        /// Deletes the Parts Configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F8060_DeletePartsConfiguration(int partId, int userId)
        {
            return F8060PartsConfigComp.F8060_DeletePartsConfiguration(partId, userId);
        }
        #endregion

        #endregion F8060 Parts Configuration

        #region F1013 BatchPayments

        /// <summary>
        /// F1013_s the list unpaid receipts.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns BatchPaymentMgmtDataSet.</returns>
        public static F1013BatchPaymentMgmtData F1013_ListUnpaidReceipts(int? userId)
        {
            return F1013BatchPaymentsComp.F1013_ListUnpaidReceipts(userId);
        }

        /// <summary>
        /// F1013_s the save batch payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsXml">The payment items XML.</param>
        /// <param name="receiptItemsXml">The receipt items XML.</param>
        /// <returns>returns the error id.</returns>
        public static int F1013_SaveBatchPayment(int ppaymentId, int userId, string paymentItemsXml, string receiptItemsXml, string receiptDate)
        {
            return F1013BatchPaymentsComp.F1013_SaveBatchPayment(ppaymentId, userId, paymentItemsXml, receiptItemsXml, receiptDate);
        }

        #region F1013_ListSnapShotItems

        /// <summary>
        /// To list snap shot items collection.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <returns>Typed dataset containing the snap shot items collection</returns>
        public static F1013BatchPaymentMgmtData F1013_ListSnapShotItems(int snapShotId)
        {
            return F1013BatchPaymentsComp.F1013_ListSnapShotItems(snapShotId);
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
        public static int F1013_DeleteReceiptItems(int paymentId, string receiptItems, int userId)
        {
            return F1013BatchPaymentsComp.F1013_DeleteReceiptItems(paymentId, receiptItems, userId);
        }
        #endregion F1013_DeleteReceiptItems

        #endregion F1013 BatchPayments

        #region F9102 OwnerStatus
        /// <summary>
        /// To List OwnerStatus Details.
        /// </summary>
        /// <param name="typeId">The typeID.</param>
        /// <param name="keyId">The keyID.</param>
        /// <returns>Typed Dataset containing the OwnerStatus Details</returns>
        public static F9102OwnerStatusData F9102_GetOwnerStatusDetails(int typeId, int keyId)
        {
            return F9102OwnerStatusDetailsComp.F9102_GetOwnerStatusDetails(typeId, keyId);
        }
        #endregion F9102 OwnerStatus

        #region F95005 Reference Data

        #region List Refereence Data

        /// <summary>
        /// To List the Reference Data Details
        /// </summary>
        /// <param name="masterFormNo">masterFormNo</param>
        /// <returns>Typed DataSet containg the Reference Data Details</returns>
        public static DataSet F95005_ListReferenceData(int masterFormNo)
        {
            return F95005ReferenceDataComp.F95005_ListReferenceData(masterFormNo);
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
        public static int F95005_SaveReferenceData(string referenceData, string deletedData, string tableName, string keyColumn, int userId)
        {
            return F95005ReferenceDataComp.F95005_SaveReferenceData(referenceData, deletedData, tableName, keyColumn, userId);
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
        public static F96000OwnerManagementData F96000_GetOwnerManagementDetails(int ownerId)
        {
            return F96000OwnerManagementComp.F96000_GetOwnerManagementDetails(ownerId);
        }

        #endregion GetOwnerManagementDetails

        #region ListOwnerStatusType

        /// <summary>
        /// Lists the OwnerStatusType
        /// </summary>
        /// <returns>DataSet</returns>
        public static F96000OwnerManagementData F96000_ListOwnerStatusType()
        {
            return F96000OwnerManagementComp.F96000_ListOwnerStatusType();
        }


        /// <summary>
        /// F96000_s the country combo details.
        /// </summary>
        /// <returns></returns>
        public static F96000OwnerManagementData F96000_CountryComboDetails()
        {
            return F96000OwnerManagementComp.F96000_CountryComboDetails();
        }

        #endregion ListOwnerStatusType

        #region InsertOwnerManagementDetails

        /// <summary>
        /// Inserts  the OwnerManagementDetails
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerDetails">OwnerDetails</param>
        /// <param name="ownerStatus">ownerStatus</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F96000_InsertOwnerManagementDetails(int ownerId, string ownerDetails, string ownerStatus, int userId)
        {
            return F96000OwnerManagementComp.F96000_InsertOwnerManagementDetails(ownerId, ownerDetails, ownerStatus, userId);
        }
        #endregion InsertOwnerManagementDetails

        #region DeleteDatas

        public static void F96000_DeleteData(int statusId)
        {
            F96000OwnerManagementComp.F96000_DeleteData(statusId);
        }
        #endregion




        #endregion F96000 OwnerManagement

        #region F36011 Misc Improvement Overview

        #region List Depr Table

        /// <summary>
        /// To List the Depr Table details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListDeprTable(int valueSliceId)
        {
            return F36011MiscImprovementOverviewComp.F36011_ListDeprTable(valueSliceId);
        }
        
        #endregion List Depr Table

        #region List Misc Code

        /// <summary>
        ///To List Misc Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed dataset containing the Misc Code Details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListMiscCode(int valueSliceId)
        {
            return F36011MiscImprovementOverviewComp.F36011_ListMiscCode(valueSliceId);
        }

        #endregion List Misc Code

        #region List Misc Improvements

        /// <summary>
        /// To List Misc Improvements details.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <returns>Typed dataset containing the Misc Improvements details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListMiscImprovements(int miscId)
        {
            return F36011MiscImprovementOverviewComp.F36011_ListMiscImprovements(miscId);
        }

        #endregion List Misc Improvements

        #region List MICatalog Code

        /// <summary>
        /// To List Catalog Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containg the MICatalog Code Details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListCatalogCode(int valueSliceId)
        {
            return F36011MiscImprovementOverviewComp.F36011_ListCatalogCode(valueSliceId);
        }

        #endregion List MICatalog Code

        #region Delete MICode

        /// <summary>
        /// To Delete MID in Misc Improvements OverView.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <param name="userId">UserID</param>
        public static void F36011_DeleteMICode(int miscId, int userId)
        {
            F36011MiscImprovementOverviewComp.F36011_DeleteMICode(miscId, userId);
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
        public static int F36011_SaveMiscImprovement(int miscmId, string miscItems, int userId)
        {
            return F36011MiscImprovementOverviewComp.F36011_SaveMiscImprovement(miscmId, miscItems, userId);
        }

        #endregion Save Misc Improvements

        #region List Qualit Comm

        /// <summary>
        /// F36011_s the list quality comm.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Comm list table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListQualityComm()
        {
            return F36011MiscImprovementOverviewComp.F36011_ListQualityComm();
        }

        #endregion List Qualit Comm

        #region List Qualit Res

        /// <summary>
        /// F36011_s the list quality res.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Res table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListQualityRes()
        {
            return F36011MiscImprovementOverviewComp.F36011_ListQualityRes();
        }

        #endregion List Qualit Comm

        #region List Condition

        /// <summary>
        /// F36011_s the list Condition
        /// </summary>
        /// <returns>Typed dataset containing the Condition table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListCondition()
        {
            return F36011MiscImprovementOverviewComp.F36011_ListCondition();
        }

        #endregion List Condition

        #region List DeprFuncCategory

        /// <summary>
        /// F36011_s the list Depr FuncCategory
        /// </summary>
        /// <returns>Typed dataset containing the Depr FuncCategory table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListDeprFuncCategory()
        {
            return F36011MiscImprovementOverviewComp.F36011_ListDeprFuncCategory();
        }

        #endregion List DeprFuncCategory

        #region List MiscCatalogChoice

        /// <summary>
        /// F36012_s the list misc catalog choice.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <returns>Typed dataset containing the MiscCatalogChoice table</returns>
        public static F36011MiscImprovementOverviewData F36012_ListMiscCatalogChoice(int miscCodeId, int fieldNum)
        {
            return F36011MiscImprovementOverviewComp.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
        }

        #endregion List MiscCatalogChoice


        #region recalc MiscImprovement

        /// <summary>
        /// To List the recalc Depr Table details
        /// </summary>
        /// <returns>Typed dataset containing the funcDepr & physDepr Table details</returns>
        public static F36011MiscImprovementOverviewData F36011_RecalcMiscImprovement(bool withprimary, int? yearIn, string condition, int? economicLife, int? effectiveAge, decimal? physDeprPerc, decimal? funcDeprPerc, decimal? BaseCost, decimal? physDepr, decimal? funcDepr, int valueSliceId, int miscCodeId)
        {
            return F36011MiscImprovementOverviewComp.F36011_RecalcMiscImprovement(withprimary, yearIn, condition, economicLife, effectiveAge, physDeprPerc, funcDeprPerc, BaseCost, physDepr, funcDepr, valueSliceId, miscCodeId);
        }

        #endregion recalc MiscImprovement

        #endregion F36011 Misc Improvement Overview

        # region F3602 Copy or Move Misc Improvements.
        /// <summary>
        /// To Get Object Details.
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public static F3602CopyMoveMiscImprovement GetObjectDetails(int parcelId)
        {
            return F36011MiscImprovementOverviewComp.GetObjectDetails(parcelId);
        }

        /// <summary>
        /// To get object types list.
        /// </summary>
        /// <returns></returns>
        public static F3602CopyMoveMiscImprovement GetObjectTypesList()
        {
            return F36011MiscImprovementOverviewComp.GetObjectTypesList();
        }

        /// <summary>
        /// To get value slices list.
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public static F3602CopyMoveMiscImprovement GetValueSlicesList(int parcelId, int objectId)
        {
            return F36011MiscImprovementOverviewComp.GetValueSlicesList(parcelId, objectId);
        }

        /// <summary>
        /// To get Misc Improvement list.
        /// </summary>
        /// <param name="valueSliceID"></param>
        /// <returns></returns>
        public static F3602CopyMoveMiscImprovement GetMiscImprovementsList(int valueSliceID)
        {
            return F36011MiscImprovementOverviewComp.GetMiscImprovementsList(valueSliceID);
        }


        /// <summary>
        /// To process MiscImprovement Data.
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
        public static F3602CopyMoveMiscImprovement F3602_ProcessMiscImprovements(string copyMove, int parcelId, bool isNewObject, int existingObjectId, int newObjectTypeId, bool isNewValueslice, int existingValueslice, string miscImprovements, int userId)
        {
            return F36011MiscImprovementOverviewComp.F3602_ProcessMiscImprovements(copyMove, parcelId, isNewObject, existingObjectId, newObjectTypeId, isNewValueslice, existingValueslice, miscImprovements, userId);
        }

        #endregion Copy or Move Misc Improvements.

        #region F36010 Misc Improvement Catalog

        #region Get Misc Improvement Catalog

        /// <summary>
        /// Get Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>1</returns>
        public static F36010MiscImprovementCatalog F36010_GetMiscImprovementCatalog(int miscCodeId)
        {
            return F36010MiscImprovementComp.F36010_GetMiscImprovementCatalog(miscCodeId);
        }

        #endregion Get Misc Improvement Catalog

        #region Save Misc Improvement Catalog

        /// <summary>
        /// Save Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCatalogItems">miscCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>1</returns>
        public static int F36010_SaveMiscImprovementCatalog(int miscCodeId, string miscCatalogItems, int userId, string miscCatalogChoiceItems)
        {
            return F36010MiscImprovementComp.F36010_SaveMiscImprovementCatalog(miscCodeId, miscCatalogItems, userId, miscCatalogChoiceItems);
        }

        #endregion Save Misc Improvement Catalog

        #region Delete Misc Improvement Catalog

        /// <summary>
        /// Delete Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">1</param>
        /// <param name="userId">UserID</param>
        public static void F36010_DeleteMiscImprovementCatalog(int miscCodeId, int userId)
        {
            F36010MiscImprovementComp.F36010_DeleteMiscImprovementCatalog(miscCodeId, userId);
        }

        #endregion Delete Misc Improvement Catalog

        #region Check Misc Improvement Catalog

        /// <summary>
        /// Check Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCode">miscCode</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>Integer</returns>
        public static int F36010_CheckMiscImprovementCatalog(int miscCodeId, string miscCode, int rollYear)
        {
            return F36010MiscImprovementComp.F36010_CheckMiscImprovementCatalog(miscCodeId, miscCode, rollYear);
        }

        #endregion Check Misc Improvement Catalog

        #region List DeprTable

        /// <summary>
        /// F36010_s the list depr table.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>miscImprovementData</returns>
        public static F36010MiscImprovementCatalog F36010_ListDeprTable(int rollYear)
        {
            return F36010MiscImprovementComp.F36010_ListDeprTable(rollYear);
        }

        #endregion List DeprTable

        #endregion F36010 Misc Improvement Catalog

        #region F36001 Marshal And Swift Commercial

        #region Get Marshal And Swift Commercial

        /// <summary>
        /// To get marshal and swift commercial details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containing the Marshal And Swift Commercial details</returns>
        public static F36001MarshalAndSwiftCommercialData F36001_GetMarshalAndSwiftCommercial(int valueSliceId)
        {
            return F36001MarshalAndSwiftCommercialComp.F36001_GetMarshalAndSwiftCommercial(valueSliceId);
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
        public static int F36001_SaveMarshalAndSwiftCommercial(int valueSliceId, string estimateDetails, string occupancyDetails, string componentDetails, string depreciationXml, int userId)
        {
            return F36001MarshalAndSwiftCommercialComp.F36001_SaveMarshalAndSwiftCommercial(valueSliceId, estimateDetails, occupancyDetails, componentDetails, depreciationXml, userId);
        }

        #endregion Save Marshal And Swift Commercial

        #endregion F36001 Marshal And Swift Commercial

        #region F2550 TaxRollCorrection

        #region Get TaxRollCorrection

        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelId">The parcel ID.</param>
        /// <returns>DataSet</returns>
        public static F2550TaxRollCorrectionData F2550_ListParcelDetails(string parcelId, string scheduleId, string stateId, string centralXmlIds)
        {
            return F2550TaxRollCorrectionComp.F2550_ListParcelDetails(parcelId, scheduleId, stateId,centralXmlIds);
        }

        #endregion Get TaxRollCorrection

        #region ExecTaxRollCorrections

        /// <summary>
        /// F2550_s the exec tax roll corrections.
        /// </summary>
        /// <param name="parcelItems">The parcel items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>DataSer</returns>
        public static int F2550_ExecTaxRollCorrections(string parcelItems, int userId)
        {
            return F2550TaxRollCorrectionComp.F2550_ExecTaxRollCorrections(parcelItems, userId);
        }

        #endregion ExecTaxRollCorrections


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
        public static F2551EditStmtData F2551_ListEditStatementDetails(int parcelId, short typeId, int statementId, int ownerId, int userId)
        {
            return F2551EditStatementComp.F2551_ListEditStatementDetails(parcelId, typeId, statementId, ownerId, userId);
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
        public static F2551EditStmtData F2551_LoadStatementGridDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string changeXML, int userId)
        {
            return F2551EditStatementComp.F2551_LoadStatementGridDetails(parcelId, typeId, statementId, ownerId, itemXML, changeXML, userId);
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
        public static int SaveEditStatementtDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string headerXML, int userId)
        {
            return F2551EditStatementComp.SaveEditStatementtDetails(parcelId, typeId, statementId, ownerId, itemXML, headerXML, userId);
        }


        #endregion



        #endregion

        #region F2552 Statement Selection Form

        #region ListStatementSelectionDetails

        /// <summary>
        /// F2551_s the list EditStatement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public static F2552StatementSelectionData F2552_ListStatementSelectionDetails(int parcelId, int typeId, int userId)
        {
            return F2552StatementSelectionComp.F2552_ListStatementSelectionDetails(parcelId, typeId, userId);
        }


        #endregion ListStatementSelectionDetails

        #endregion

        #region Get Attachment Details

        /// <summary>
        /// List the attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public static F2550TaxRollCorrectionData F2550_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            return F2550TaxRollCorrectionComp.F2550_ListAttachmentDetails(formId, keyIds, userId, moduleId);
        }

        #endregion Get Attachment Details

        #region Delete Attachment Details

        /// <summary>
        /// Delete the attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public static void F2550_DeleteAttachmentDetails(int formId)
        {
            F2550TaxRollCorrectionComp.F2550_DeleteAttachmentDetails(formId);
        }
        #endregion Delete Attachment Details

        #region List Correction Code
        /// <summary>
        /// F2550_s the list correction code.
        /// </summary>
        /// <returns></returns>
        public static F2550TaxRollCorrectionData F2550_ListCorrectionCode()
        {
            return F2550TaxRollCorrectionComp.F2550_CorrectionCode();
        }
        #endregion

        #region Insert Correction Parcels Temp Table
        /// <summary>
        /// F2550_s the save correction parcels temp.
        /// </summary>
        /// <param name="correctionId">The correction id.</param>
        /// <param name="correctionTempItems">The correction temp items.</param>
        /// <param name="corrParcelIds">The corr parcel ids.</param>
        /// <param name="statementsIds">The statements ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F2550_SaveCorrectionParcelsTemp(int? correctionId, string correctionTempItems, string corrParcelIds, string statementsIds, int userId)
        {
            return F2550TaxRollCorrectionComp.F2550_InsertCorrectionParcelsTemp(correctionId, correctionTempItems, corrParcelIds, statementsIds, userId);
        }
        #endregion

        #endregion F2550 TaxRollCorrection

        #region F1401 ParcelSection

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public static F1401ParcelSearch F1401_GetParcelType(int? parcelId)
        {
            return F1401ParcelSelectionComp.F1401_GetParcelType(parcelId);
        }

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public static F1401ParcelSearch F1401_GetSearchResult(string parcelSearchXml)
        {
            return F1401ParcelSelectionComp.F1401_GetSearchResult(parcelSearchXml);
        }

        #endregion F1401 ParcelSection

        #region ObjectManagement

        /// <summary>
        /// F3001_s the get object detail.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>F3001ObjectManagementData</returns>
        public static F3001ObjectManagementData F3001_GetObjectDetail(int objectId)
        {
            return F3001ObjectManagementComp.F3001_GetObjectDetail(objectId);
        }

        /// <summary>
        /// F3001_s the save object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectItems">The objectItems.</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F3001_SaveObjectManagement(int objectId, string objectItems, int userId)
        {
            return F3001ObjectManagementComp.F3001_SaveObjectManagement(objectId, objectItems, userId);
        }

        /// <summary>
        /// F3001_s the delete object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">UserID</param>
        public static void F3001_DeleteObjectManagement(int objectId, int userId)
        {
            F3001ObjectManagementComp.F3001_DeleteObjectManagement(objectId, userId);
        }

        /// <summary>
        /// F3001_s the get parcel description.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        public static string F3001_GetParcelDescription(int parcelId)
        {
            return F3001ObjectManagementComp.F3001_GetParcelDescription(parcelId);
        }

        /// <summary>
        /// F3001_s the copy object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectXml">The object XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public static int F3001_CopyObject(int objectId, string objectXml, int userId)
        {
            return F3001ObjectManagementComp.F3001_CopyObject(objectId, objectXml, userId);
        }

        #endregion

        #region F27080ExemptionDefinition

        #region ExemptionTypeCombo

        /// <summary>
        /// List Excemption Type
        /// </summary>
        /// <param name="applicationId">Application ID</param>
        /// <returns>DataSet</returns>
        public static F27080ExemptionDefinitionData F27080_ListExemptionTypeCombo(int applicationId)
        {
            return F27080ExemptionDefinitionComp.F27080_ListExemptionTypeCombo(applicationId);
        }
        #endregion

        #region DataGrid ExemptionType

        #region Exemption error

        /// <summary>
        /// Get Exemption error
        /// </summary>
        /// <param name="exemptionId">Exemption ID</param>
        /// /// <param name="exemptionCode">Exemption Code</param>
        /// <returns>Message</returns>
        public static string F27080_GetExemptionError(int exemptionId, string exemptionCode)
        {
            return F27080ExemptionDefinitionComp.F27080_GetExemptionError(exemptionId, exemptionCode);
        }

        #endregion

        #region Delete Exemption

        /// <summary>
        /// Delete Exemption
        /// </summary>
        /// <param name="exemptionId">Exemption ID</param>
        ///  <param name="exemptionId">userId</param>
        /// /// <param name="exemptionCode">Exemption Code</param>
        /// <returns>NULL</returns>
        public static void F27080_DeleteExemption(int userId, int exemptionId, string exemptionCode)
        {
            F27080ExemptionDefinitionComp.F27080_DeleteExemption(userId, exemptionId, exemptionCode);
        }

        #endregion
        /// <summary>
        /// Get Exemption Data
        /// </summary>
        /// <param name="exemptionId">Exemption ID</param>
        /// <returns>DataSet</returns>
        public static F27080ExemptionDefinitionData F27080_FillExemptionTypeGrid(int exemptionId)
        {
            return F27080ExemptionDefinitionComp.F27080_FillExemptionTypeGrid(exemptionId);
        }

        #endregion

        #region SaveExemptionType

        /// <summary>
        /// Save Exemption Type
        /// </summary>
        /// <param name="exemptionId">Exemption ID</param>
        /// <param name="seniorExemption">Senior Excemption</param>
        /// <param name="exemptionType">Excemption Type</param>
        /// <param name="checkError">Checkerror</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F27080_SaveExemptionType(int exemptionId, string seniorExemption, string exemptionType, int checkError, int userId)
        {
            return F27080ExemptionDefinitionComp.F27080_SaveExemptionType(exemptionId, seniorExemption, exemptionType, checkError, userId);
        }

        #endregion

        #endregion


        #region F27081TIFDistrict

        #region GetTIFDetails

        /// <summary>
        /// F27081_GetTIFDistrictDetails
        /// </summary>
        /// <param name="TIFId">TIFId</param>
        /// <returns>Dataset</returns>
        public static F27081TIFDistrictData F27081_GetTIFDistrictDetails(int TIFIdDistId)
        {
            return F27081TifDistrictcomp.F27081_GetTIFDistrictDetails(TIFIdDistId);
        }

        #endregion

        #region SaveTIFDetails

        /// <summary>
        /// Save TIFDistrict Details
        /// </summary>
        /// <param name="TIFId">TIF ID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F27081_SaveTIFDistrictDetails(int? TIFIdDistId, string TIFDetails, int userId)
        {
            return F27081TifDistrictcomp.F27081_SaveTIFDistrictDetails(TIFIdDistId, TIFDetails, userId);
        }

        /// <summary>
        /// F27081_s the delete TIF district details.
        /// </summary>
        /// <param name="TIFIdDistId">The TIF id dist id.</param>
        /// <param name="userId">The user id.</param>
        public static string F27081_DeleteTIFDistrictDetails(int TIFIdDistId, int userId, bool IsReadyToDelete)
        {
             return F27081TifDistrictcomp.F27081_DeleteTIFDistrictDetails(TIFIdDistId, userId,IsReadyToDelete);
        }
        /// <summary>
        /// F27081_s the get TIF combo box details.
        /// </summary>
        /// <returns></returns>
        public static F27081TIFDistrictData F27081_GetTIFComboBoxDetails(int RollYear)
        {
            return F27081TifDistrictcomp.F27081_GetTIFComboBoxDetails(RollYear);
        }
        #endregion

        #endregion

        #region Agland Details

        #region F34100_GetAglandDetails
        /// <summary>
        /// F34100_GetAglandDetails
        /// </summary>
        /// <param name="TIFId">AglandId</param>
        /// <returns>Dataset</returns>
        public static F34100AglandUseData F34100_GetAglandDetails(int AglandID)
        {
            return F34100AglandUse.F34100_GetAglandDetails(AglandID);
        }
        #endregion

        #region F34100_SaveAglandDetails

        /// <summary>
        /// Save SaveAglandDetails
        /// </summary>
        /// <param name="AglandID">AglandID</param>
        /// <param name="AglandDetails">AglandDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F34100_SaveAglandDetails(int? AglandID, string AglandDetails, int userId)
        {
            return F34100AglandUse.F34100_SaveAglandDetails(AglandID, AglandDetails, userId);
        }
        #endregion

        #region Delete AglandDetails
        /// <summary>
        /// F391000s the delete Agland details.
        /// </summary>
        /// <param name="AglandId">The Agland id.</param>
        /// <param name="userId">The user id.</param>
        public static void F34100_DeleteAglandDetails(int AglandID, int userId)
        {
            F34100AglandUse.F34100_DeleteAglandDetails(AglandID, userId);
        }
        #endregion Delete BoardOfEqualizationDetails

        #endregion

        #region TopDollar

        #region F34110_GetTopDollarDetails
        /// <summary>
        /// F34110_GetTopDollarDetails
        /// </summary>
        /// <param name="TIFId">AglandId</param>
        /// <returns>Dataset</returns>
        public static F34110TopDollarData F34110_GetTopDollarDetails(int TopDollarID)
        {
            return F34110TopDollarComp.F34110_GetTopDollarDetails(TopDollarID);
        }
        #endregion

        #region F34110_SaveTopDollarDetails

        /// <summary>
        /// SaveTopDollarDetails
        /// </summary>
        /// <param name="TopDollarID">TopDollarID</param>
        /// <param name="TopDollarDetails">TopDollarDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F34110_SaveTopDollarDetails(int? TopDollarID, string TopDollarDetails, int userId)
        {
            return F34110TopDollarComp.F34110_SaveTopDollarDetails(TopDollarID, TopDollarDetails, userId);
        }
        #endregion

        #region DeleteTopDollarDetails
        /// <summary>
        /// F39100s the delete TopDollar Details.
        /// </summary>
        /// <param name="AglandId">TheTopDollarID.</param>
        /// <param name="userId">The user id.</param>
        public static void F34110_DeleteTopdDollarDetails(int TopDollarID, int userId)
        {
            F34110TopDollarComp.F34110_DeleteTopDollarDetails(TopDollarID, userId);
        }
        #endregion

        #region F34110_Calculate Non Crop Dollar

        /// <summary>
        /// F34110_CalculatenonCropDollar
        /// </summary>
        /// <param name="CropDollar">CropDollar</param>
        /// <param name="CountyFactor">CountyFactor</param>
        /// <returns>Dataset</returns>
        public static F34110TopDollarData F34110_CropTopDollar(decimal CropDollar, decimal CountyFactor)
        {
            return F34110TopDollarComp.F34110_CropTopDollar(CropDollar, CountyFactor);
        }


        #endregion F34110_Calculate Non Crop Dollar

        #endregion

        #region F29660TIFEvent

        #region GetTIFEvent

        /// <summary>
        /// F29660_GetTIFDistrictDetails
        /// </summary>
        /// <param name="TIFId">EventId</param>
        /// <returns>Dataset</returns>
        public static F29660TIFEventData F29660_GetTIFEventDetails(int EventId, int userId)
        {
            return F29660TIFEventComp.F29660_GetTIFEventDetails(EventId, userId);
        }

        #endregion

        #region SaveTIFEvent

        /// <summary>
        /// Save TIFEvent Details
        /// </summary>
        /// <param name="TIFId">Event ID</param>
        /// <param name="TIFId">TIF ID</param>
        /// <param name="BaseValue">BaseValue</param>
        /// <param name="userId">UserID</param>
        /// <returns>Int</returns>
        public static int F29660_SaveTIFEventDetails(int? EventId, int TIFId, decimal BaseValue, int userId)
        {
            return F29660TIFEventComp.F29660_SaveTIFEventDetails(EventId, TIFId, BaseValue, userId);
        }

        #endregion


        #endregion

        #region F29530EventAssociation

        #region FillEventAssociationGrid
        /// <summary>
        /// FillEventAssociationGrid
        /// </summary>
        /// <param name="eventId">eventID</param>
        /// <returns>DataSet</returns>
        public static F29530EventAssociationData F29530_FillAssociationEventGrid(int eventId)
        {
            return F29530EventAssociationComp.F29530_FillAssociationEventGrid(eventId);
        }

        #endregion

        #endregion

        #region F29500ParcelSplit

        /// <summary>
        /// F29500_s the base parcel value.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public static F29500ParcelSplitData F29500_GetBaseParcelValue(int parcelId)
        {
            return F29500ParcelSplitComp.F29500_GetBaseParcelValue(parcelId);
        }

        /// <summary>
        /// F29500_s the parcel split load.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public static F29500ParcelSplitData F29500_ParcelSplitLoad(int eventId)
        {
            return F29500ParcelSplitComp.F29500_ParcelSplitLoad(eventId);
        }

        /// <summary>
        /// F29500_s the save parcel split.
        /// </summary>
        /// <param name="splitDefinitionXml">The split definition XML.</param>
        /// <param name="splitHeaderXml">The split header XML.</param>
        /// <param name="parcelSplitXml">The parcel split XML.</param>
        /// <param name="parcelObjectXml">The parcel object XML.</param>
        /// <param name="cropXml">The Crop XML</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public static int F29500_SaveParcelSplit(string splitDefinitionXml, string splitHeaderXml, string parcelSplitXml, string parcelObjectXml, string cropXml, int userId)
        {
            return F29500ParcelSplitComp.F29500_SaveParcelSplit(splitDefinitionXml, splitHeaderXml, parcelSplitXml, parcelObjectXml, cropXml, userId);
        }

        /// <summary>
        /// F29500_s the create parcel.
        /// </summary>
        /// <param name="splitId">The split id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Return message</returns>
        public static string F29500_CreateParcel(int splitId, int userId)
        {
            return F29500ParcelSplitComp.F29500_CreateParcel(splitId, userId);
        }

        #endregion F29500ParcelSplit

        #region F36032 Land Codes

        #region F36032_ListLandItems

        /// <summary>
        /// F36032_s the list land items.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>The landCodeDataSet.</returns>
        public static F36032LandCodesData F36032_ListLandItems(int? rollYear)
        {
            return F36032LandCodesComp.F36032_ListLandItems(rollYear);
        }

        #endregion F36032_ListLandItems

        #region F36032_ListLandCodeDetails

        /// <summary>
        /// F36032_s the list land code details.
        /// </summary>
        /// <returns>the landCodesDataSet</returns>
        public static F36032LandCodesData F36032_ListLandCodeDetails()
        {
            return F36032LandCodesComp.F36032_ListLandCodeDetails();
        }

        #endregion F36032_ListLandCodeDetails

        #region F36032_DeleteLandCode

        /// <summary>
        /// F36032_s the delete land code.
        /// </summary>
        /// <param name="landCodeId">The land code ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value</returns>
        public static int F36032_DeleteLandCode(int landCodeId, int userId)
        {
            return F36032LandCodesComp.F36032_DeleteLandCode(landCodeId, userId);
        }

        #endregion F36032_DeleteLandCode

        #region F36032_SaveLandCodeDetails

        /// <summary>
        /// To save the land code deatils
        /// </summary>
        /// <param name="landCodeId">Land Code ID</param>
        /// <param name="landItems">Land Items</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer value containing the save land Code Id</returns>
        public static int F36032_SaveLandCodeDetails(int? landCodeId, string landItems, int userId)
        {
            return F36032LandCodesComp.F36032_SaveLandCodeDetails(landCodeId, landItems, userId);
        }

        #endregion F36032_SaveLandCodeDetails

        #endregion F36032 Land Codes

        #region F29510PaecelCombine

        /// <summary>
        /// Get Base Parcel Value
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <returns>DataSet</returns>
        public static F29510ParcelCombineData F29510_GetBaseParcelValue(int eventId)
        {
            return F29510ParcelCombineComp.F29510_GetBaseParcelValue(eventId);
        }

        /// <summary>
        /// Get Combine Parcel Details
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>DataSet</returns>
        public static DataSet F29510_GetCombineParcelDetails(int parcelId)
        {
            return F29510ParcelCombineComp.F29510_GetCombineParcelDetails(parcelId);
        }

        /// <summary>
        /// Save Combine Parcel Details
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="combineItems">CombineItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        public static int F29510_SaveCombineParcelDetails(int? combineId, string parcelNumber, string combineItems, int userId,bool IsAttachment,bool IsComment,bool IsPermit,bool IsAssociation,bool IsNewConstruction)
        {
            return F29510ParcelCombineComp.F29510_SaveCombineParcelDetails(combineId, parcelNumber, combineItems, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);
        }

        /// <summary>
        /// Create Combined Parcel Value
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="eventId">EventID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="userId">UserID</param>
        /// <returns>F29510ParcelCombineData</returns>
        public static F29510ParcelCombineData F29510_CreateCombinedParcel(int combineId, string eventId, string parcelNumber, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction)
        {
            return F29510ParcelCombineComp.F29510_CreateCombinedParcel(combineId, eventId, parcelNumber, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);
        }

        #endregion F29510PaecelCombine

        #region F36033 Land Code Values

        #region F36033_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public static F36033LandCodesValuesData F36033_ListLandCodeValues()
        {
            return F36033LandCodeValuesComp.F36033_ListLandCodeValues();
        }
        #endregion F36033_ListLandCodeValues

        /// <summary>
        /// F36065_s the list shape details.
        /// </summary>
        /// <returns></returns>
        public static F36035LandData F36035_ListShapeDetails()
        {
            return F36035LandValueSliceDetailsComp.F36035_ListShapeDetails();
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
        public static F36033LandCodesValuesData F36033_ListIndividualLandCodeValuesItems()
        {
            return F36033LandCodeValuesComp.F36033_ListIndividualLandCodeValuesItems();
        }

        #endregion F36033_ListIndividualLandCodeValuesItems

        #region F36033_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public static F36033LandCodesValuesData F36033_ListNeighborhoodType(int rollYear)
        {
            return F36033LandCodeValuesComp.F36033_ListNeighborhoodType(rollYear);
        }
        #endregion F36033_ListNeighborhood

        #region F36033_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public static int F36033_DeleteLandCodevalue(int luvId, int userId)
        {
            return F36033LandCodeValuesComp.F36033_DeleteLandCodevalue(luvId, userId);
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
        public static int F36033_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return F36033LandCodeValuesComp.F36033_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F36033_SaveLandCodeValue

        #endregion F36033 Land Code Values

        #region F39133Land Code Values

        #region F39133_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public static F39133LandCodeValueData F39133_ListLandCodeValues()
        {
            return F39133LandCodeValuesComp.F39133_ListLandCodeValues();
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
        public static F39133LandCodeValueData F39133_ListIndividualLandCodeValuesItems()
        {
            return F39133LandCodeValuesComp.F39133_ListIndividualLandCodeValuesItems();
        }

        #endregion F39133_ListIndividualLandCodeValuesItems

        #region F39133_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public static F39133LandCodeValueData F39133_ListNeighborhoodType(int rollYear)
        {
            return F39133LandCodeValuesComp.F39133_ListNeighborhoodType(rollYear);
        }
        #endregion F39133_ListNeighborhood

        #region F39133_CalculateNonCropValue
        ///<summary>
        /// F39133_CalculateNonCropValues
        /// </summary>
        /// <param name="RollYear">RollYear</param>
        /// <param name="CropRate">CropRate</param>
        /// <param name="NonCropRate">NonCropRate</param>
        public static F39133LandCodeValueData F39133_CalculateNonCropValue(int rollYear, decimal? CropRate, decimal? NonCropRate)
        {
            return F39133LandCodeValuesComp.F39133_CalculateNonCropValue(rollYear, CropRate, NonCropRate);
        }
        #endregion F39133_CalculateNonCropValue

        #region F39133_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        public static int F39133_DeleteLandCodevalue(int luvId, int userId)
        {
            return F39133LandCodeValuesComp.F39133_DeleteLandCodevalue(luvId, userId);
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
        public static int F39133_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return F39133LandCodeValuesComp.F39133_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F39133_SaveLandCodeValue

        #endregion F39133Land Code Values

        #region F36035 Land

        #region GetLandDetails

        /// <summary>
        /// Gets the F36035_ListLandDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>Type Dataset Returns LandDetails</returns>        
        public static F36035LandData F36035_ListLandDetails(int valueSliceId)
        {
            return F36035LandValueSliceDetailsComp.F36035_ListLandDetails(valueSliceId);
        }
        #endregion GetLandDetails

        #region GetLandTypeDetails

        /// <summary>
        /// Gets the F36035_ListLandDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>Type Dataset Returns LandTypeDetailss</returns>        
        public static F36035LandData F36035_ListLandTypeDetails(int valueSliceId)
        {
            return F36035LandValueSliceDetailsComp.F36035_ListLandTypeDetails(valueSliceId);
        }
        #endregion GetLandTypeDetails

        #region InsertLandDetails

        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public static int F36035_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return F36035LandValueSliceDetailsComp.F36035_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }
        #endregion InsertLandDetails

        #region DeleteLandDetails

        /// <summary>
        /// To Delete LandDetails.
        /// </summary>
        /// <param name="luid">The lUID.</param>
        /// <param name="userId">UserID</param>
        public static void F36035_DeleteLandDetails(int luid, int userId)
        {
            F36035LandValueSliceDetailsComp.F36035_DeleteLandDetails(luid, userId);
        }
        #endregion DeleteLandDetails

        #region GetLandCode

        /// <summary>
        /// F36035_s the get land code.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="valuesliceId">The valueslice id.</param>
        /// <returns></returns>
        public static F36035LandData F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId, int? AglandID)
        {
            return F36035LandValueSliceDetailsComp.F36035_GetLandCode(rollYear, landType1, landType2, landType3, valuesliceId, AglandID);
        }

        #endregion GetLandCode

        #region GetLandcode BaseValue
        /// <summary>
        /// Gets the F36035_List LandCodeBaseValue       
        /// </summary>
        ///<param name="landCode">Land Code</param>
        /// <param name="valueSliceId">Value Slice ID</param>
        /// <returns>Type Dataset Returns LandDetails</returns> 
        public static F36035LandData F36035_GetLandCodeBaseValue(string landCode, int valueSliceId, int? AglandID)
        {
            return F36035LandValueSliceDetailsComp.F36035_GetLandCodeBaseValue(landCode, valueSliceId, AglandID);
        }

        #endregion GetLandcode BaseValue

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
        public static F36035LandData F36035_GetUseBaseDollarPerUnit(byte programId, byte useAdjustmentType, string useAdjustment, decimal useBaseValue, int rollYear, decimal useMultiplier, decimal units)
        {
            return F36035LandValueSliceDetailsComp.F36035_GetUseBaseDollarPerUnit(programId, useAdjustmentType, useAdjustment, useBaseValue, rollYear, useMultiplier, units);
        }

        #endregion Get UseBaseDollarPerUnit Value

        #region List Influence Types

        /// <summary>
        /// F36035_s the list influence type.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>The influence type dataset</returns>
        public static F36035LandData F36035_ListInfluenceType(int valueSliceId)
        {
            return F36035LandValueSliceDetailsComp.F36035_ListInfluenceType(valueSliceId);
        }

        #endregion List Influence Types

        #region List Land Program

        /// <summary>
        /// F36035_s the list land program.
        /// </summary>
        /// <returns>The land program dataset.</returns>
        public static F36035LandData F36035_ListLandProgram()
        {
            return F36035LandValueSliceDetailsComp.F36035_ListLandProgram();
        }

        #endregion List Land Program

        #region Execute VFormula

        /// <summary>
        /// F36035_s the execute V formula.
        /// </summary>
        /// <param name="vformula">The vformula.</param>
        /// <param name="units">The units.</param>
        /// <returns></returns>
        public static DataSet F36035_ExecuteVFormula(string vformula, decimal units)
        {
            return F36035LandValueSliceDetailsComp.F36035_ExecuteVFormula(vformula, units);
        }

        #endregion Execute VFormula

        #endregion F36035 Land

        #region F39133Land Code Values

        #region GetLandDetails

        /// <summary>
        /// Gets the F39135_ListLandDetails        
        /// </summary>
        public static F39135LandData F39135_LandDetails(int valueSliceId)
        {
            return F39135LandDetailsComp.F39135_LandDetails(valueSliceId);
        }
        #endregion GetLandDetails

        #region GetLandTypeDetails

        /// <summary>
        /// Gets the F39135_ListLandDetails        
        /// </summary>
        public static F39135LandData F39135_Landtypes(int valueSliceId, int rollYear)
        {
            return F39135LandDetailsComp.F39135_Landtypes(valueSliceId, rollYear);
        }
        #endregion GetLandTypeDetails

        #region Land Use

        /// <summary>
        /// Gets the F39135_LandUseDetails        
        /// </summary>
        public static F39135LandData F39135_LandUseTypes(int valueSliceId)
        {
            return F39135LandDetailsComp.F39135_LandUseTypes(valueSliceId);
        }

        #endregion

        #region LandTotals

        /// <summary>
        /// Gets the LandTotals       
        /// </summary>
        public static F39135LandData F39135_GetLandTotals(int valueSliceId)
        {
            return F39135LandDetailsComp.F39135_GetLandTotals(valueSliceId);
        }


        #endregion


        #region GetWeightedRating
        /// <summary>
        /// F39135_s the get WeightedRating.
        /// </summary>
        public static F39135LandData F39135_WeightedRating(string landCode, decimal units, int? landUse, int valueSliceId)
        {
            return F39135LandDetailsComp.F39135_WeightedRating(landCode, units, landUse, valueSliceId);
        }


        #endregion

        #region CalculatedBaseValue
        /// <summary>
        /// F39135_s the get CalculatedBaseValue.
        /// </summary>
        public static F39135LandData F39135_CalculatedBaseValue(string LandCode, int adjustmentTypeID, decimal units, decimal baseCostUnit, decimal adjustment, int? AglandID, int valueSliceId)
        {
            return F39135LandDetailsComp.F39135_CalculatedBaseValue(LandCode, adjustmentTypeID, units, baseCostUnit, adjustment, AglandID, valueSliceId);
        }

        #endregion

        #region ListAdjustmentType
        ///<summary>
        ///F39135_s List Adjustment Type
        /// </summary>
        public static F39135LandData F39135_AdjustmentType()
        {
            return F39135LandDetailsComp.F39135_adjustmentTypes();
        }

        #endregion

        #region InsertLandDetails

        /// <summary>
        /// F39135_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public static int F39135_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return F39135LandDetailsComp.F39135_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }
        #endregion InsertLandDetails

        #endregion

        #region F81001 Event Fee Catalog

        #region Get Event Fee Catalog

        /// <summary>
        /// Get Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <returns>DataSet</returns>
        public static F81001FeeCatalogData F81001_GetEventFeeCatalog(int feeCatId)
        {
            return F81001EventFeeCatalogComp.F81001_GetEventFeeCatalog(feeCatId);
        }

        #endregion Get Misc Improvement Catalog

        #region Save Event Fee Catalog

        /// <summary>
        /// Save Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="feeCatalogItems">feeCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public static int F81001_SaveEventFeeCatalog(int feeCatId, string feeCatalogItems, int userId)
        {
            return F81001EventFeeCatalogComp.F81001_SaveEventFeeCatalog(feeCatId, feeCatalogItems, userId);
        }

        #endregion Save Event Fee Catalog

        #region Delete Event Fee Catalog

        /// <summary>
        /// Delete Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="userId">UserID</param>
        public static void F81001_DeleteEventFeeCatalog(int feeCatId, int userId)
        {
            F81001EventFeeCatalogComp.F81001_DeleteEventFeeCatalog(feeCatId, userId);
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
        public static int F81001_CheckEventFeeCatalog(int feeCatId, string formNumber, DateTime effectiveDate)
        {
            return F81001EventFeeCatalogComp.F81001_CheckEventFeeCatalog(feeCatId, formNumber, effectiveDate);
        }

        #endregion Check Event Fee Catalog

        #endregion F81001 Event Fee Catalog

        #region F36040 Crop Catalog

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public static F36040PermanentCropData F36040_ListNeighborhoodType()
        {
            return F36040PermanentCropComp.F36040_ListNeighborhoodType();
        }


        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public static F36040PermanentCropData F36040_ListCropCatalog()
        {
            return F36040PermanentCropComp.F36040_ListCropCatalog();
        }

        /// <summary>
        /// F36040_s the delete crop catalog.
        /// </summary>
        /// <param name="cropVId">The crop V id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public static int F36040_DeleteCropCatalog(int cropVId, int userId)
        {
            return F36040PermanentCropComp.F36040_DeleteCropCatalog(cropVId, userId);
        }

        /// <summary>
        /// F36040_s the save crop catalog.
        /// </summary>
        /// <param name="cropUnqiueId">The crop unqiue id.</param>
        /// <param name="cropCatalogItems">The crop catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public static int F36040_SaveCropCatalog(int? cropUnqiueId, string cropCatalogItems, int userId)
        {
            return F36040PermanentCropComp.F36040_SaveCropCatalog(cropUnqiueId, cropCatalogItems, userId);
        }

        /// <summary>
        /// F36040_s the type of the list crop neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static F36040PermanentCropData F36040_ListCropNeighborhoodType(int rollYear)
        {
            return F36040PermanentCropComp.F36040_ListCropNeighborhoodType(rollYear);
        }

        #endregion

        #region F36041 Crop

        #region Get Crop Details

        /// <summary>
        /// Get Crop Code
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        public static F36041CropData F36041_GetCrop(int valueSliceId)
        {
            return F36041CropComp.F36041_ListCropDetails(valueSliceId);
        }

        #endregion Get Crop Details

        #region Get Crop Code Details

        /// <summary>
        /// Get Crop Code
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        public static F36041CropData F36041_GetCropCode(int valueSliceId)
        {
            return F36041CropComp.F36041_ListCropCodeDetails(valueSliceId);
        }

        #endregion Get Crop Code Details

        #region Save Crop Details
        /// <summary>
        /// To Save the Crop Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <param name="cropItems">xml string containing the Crop Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing the key id</returns>
        public static int F36041_SaveCropCodeDetails(int valueSliceId, string cropItems, int userId)
        {
            return F36041CropComp.F36041_SaveCropCodeDetails(valueSliceId, cropItems, userId);
        }
        #endregion Save Crop Details

        #region F36041_DeleteCrop

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public static void F36041_DeleteCrop(int cropId, int userId)
        {
            F36041CropComp.F36041_DeleteCrop(cropId, userId);
        }

        #endregion F36041_DeleteCrop


        #region F36041_DeleteCropIds

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropIds">The crop ids.</param>
        /// <param name="userId">The user id.</param>
        public static void F36041_DeleteCropIds(string cropIds, int userId)
        {
            F36041CropComp.F36041_DeleteCropIds(cropIds, userId);
        }

        #endregion F36041_DeleteCrop

        #endregion F36041 Crop

        #region F81002 Event Fee

        /// <summary>
        /// Get the Event Fee data
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="form">Form ID</param>
        /// <returns>DataSet</returns>
        public static F81002EventFeeData F81002_GetEventFee(int eventId, int form)
        {
            return F81002EventFeeComp.F81002_GetEventFee(eventId, form);
        }

        /// <summary>
        /// To Save the Event Fee
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="feeItems">xml string containing the Event Fee Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing the key id</returns>
        public static int F81002_SaveEventFee(int eventId, string feeItems, int userId)
        {
            return F81002EventFeeComp.F81002_SaveEventFee(eventId, feeItems, userId);
        }

        /// <summary>
        /// Delete the Event Fee data
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="userId">UserID</param>
        public static void F81002_DeleteEventFee(int eventId, int userId)
        {
            F81002EventFeeComp.F81002_DeleteEventFee(eventId, userId);
        }
        #endregion F81002 Event Fee

        #region F3230 Checkin

        #region ChkInTypesXML
        /// <summary>
        /// ChkInTypesXML
        /// </summary>
        /// <returns>DataSet</returns>
        public static F3230CheckInData F3230_ChkInTypesXML()
        {
            return F3230FieldUseComp.F3230_ChkInTypesXML();
        }
        #endregion ChkInTypesXML

        /// <summary>
        /// F3230_ChkInInsertedFileXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInInsertedFileXML()
        {
            return F3230FieldUseComp.F3230_ChkInInsertedFileXML();
        }

        /// <summary>
        /// F3230_InsertFile
        /// </summary>
        /// <param name="insertxmlContent"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_InsertFile(string insertxmlContent)
        {
            return F3230FieldUseComp.F3230_InsertFile(insertxmlContent);
        }

        /// <summary>
        /// F3230_UpdateFile
        /// </summary>
        /// <param name="updatexmlContent"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_UpdateFile(string updatexmlContent)
        {
            return F3230FieldUseComp.F3230_UpdateFile(updatexmlContent);
        }

        /// <summary>
        /// F3230_ChkInLandCodeXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInLandCodeXML()
        {
            return F3230FieldUseComp.F3230_ChkInLandCodeXML();
        }

        /// <summary>
        /// F3230_ParcelID
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ParcelID()
        {
            return F3230FieldUseComp.F3230_ParcelID();
        }

        /// <summary>
        /// F3230_ChkInInsertXML
        /// </summary>
        /// <returns></returns>
        public static string F3230_ChkInInsertXML(out string ChkInInsertXML)
        {
            return F3230FieldUseComp.F3230_ChkInInsertXML(out ChkInInsertXML);
        }

        /// <summary>
        /// F3230_ChkInTerraGonInsertXML
        /// </summary>
        /// <returns></returns>
        public static string F3230_ChkInTerraGonInsertXML(out string ChkInInsertXML)
        {
            return F3230FieldUseComp.F3230_ChkInTerraGonInsertXML(out ChkInInsertXML);
        }

        /// <summary>
        /// F3230_GetChkOutParcelIDs
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_GetChkOutParcelIDs(int SnapShotID)
        {
            return F3230FieldUseComp.F3230_GetChkOutParcelIDs(SnapShotID);
        }

        /// <summary>
        /// F3230_GetCheckOutDetails
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static F3230CheckInData F3230_GetCheckOutDetails(int SnapShotID, int UserID)
        {
            return F3230FieldUseComp.F3230_GetCheckOutDetails(SnapShotID, UserID);
        }

        /// <summary>
        /// F3230_SaveChkOutParcelIDs
        /// </summary>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public static int F3230_SaveChkOutParcelIDs(string ParcelXML)
        {
            return F3230FieldUseComp.F3230_SaveChkOutParcelIDs(ParcelXML);
        }

        /// <summary>
        /// F3230_SaveCheckOutDetails
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        public static int F3230_SaveCheckOutDetails(string CheckOutXML)
        {
            return F3230FieldUseComp.F3230_SaveCheckOutDetails(CheckOutXML);
        }

        /// <summary>
        /// F3230_ChkInDeprXML
        /// </summary>
        /// <returns></returns>
        public static F3230CheckInData F3230_ChkInDeprXML()
        {
            return F3230FieldUseComp.F3230_ChkInDeprXML();
        }


        #region ChkInEstimateComponentGroupXML
        ///<summary>
        /// ChkInEstimateComponentGroupXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        public static F3230CheckInData F3230_ChkInEstimateComponentGroupXML()
        {
            return F3230FieldUseComp.F3230_ChkInEstimateComponentGroupXML();
        }
        #endregion ChkInEstimateComponentGroupXML

        #region ChkInNBHDXML
        ///<summary> 
        /// ChkInNBHDXML
        /// </summary>
        public static F3230CheckInData F3230_ChkInNBHDXML()
        {
            return F3230FieldUseComp.F3230_ChkInNBHDXML();
        }
        #endregion ChkInNBHDXML

        #region ChkInValueSliceXML
        ///<summary>
        /// ChkInValueSliceXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInValueSliceXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInValueSliceXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInValueSliceXML

        #region  ChkInCommentXML
        ///<summary>
        /// ChkInCommentXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInCommentXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInCommentXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInCommentXML

        #region ChkInEstimateXML
        ///<summary>
        /// ChkInTerraGonXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInEstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInEstimateXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInEstimateXML

        #region ChkInFileXML
        ///<summary>
        /// ChkInFileXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInFileXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInFileXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInFileXML

        #region ChkInLandValuesXML
        ///<summary>
        /// ChkInTerraGChkInLandValuesXMLonXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInLandValuesXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInLandValuesXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInLandValuesXML

        #region ChkInLandXML
        ///<summary>
        /// ChkInLandXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInLandXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInLandXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInLandXML

        #region ChkInMiscXML
        ///<summary>
        /// ChkInMiscXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInMiscXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInMiscXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInMiscXML

        #region ChkInMSC_EstimateXML
        ///<summary>
        /// ChkInMSC_EstimateXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInMSC_EstimateXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInMSC_EstimateXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInMSC_EstimateXML

        #region ChkInObjectXML
        ///<summary>
        /// ChkInObjectXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInObjectXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInObjectXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInObjectXML

        #region ChkInParcelValueXML
        ///<summary>
        /// ChkInParcelValueXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInParcelValueXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInParcelValueXML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInParcelValueXML

        #region ChkInParcelXML
        ///<summary>
        /// ChkInParcelXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInParcelXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInParcelXML(TableName, StartRow, out RowendValue);
        }

        #endregion ChkInParcelXML

        #region ChkInTerraGonXML
        /// <summary>
        /// ChkInTerraGonXML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInTerraGonXML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInTerraGonXML(TableName, StartRow, out RowendValue);
        }
        #endregion   ChkInTerraGonXML

        #region ChkInType2XML
        /// <summary>
        /// ChkInType2XML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInType2XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInType2XML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInType2XML

        #region ChkInType6XML
        /// <summary>
        /// ChkInType6XML
        /// </summary>
        /// <param name="eventId">TableName</param>
        /// <param name="userId">StartRow</param>
        /// <param name="userId">RowendValue</param>
        public static F3230CheckInData F3230_ChkInType6XML(string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkInType6XML(TableName, StartRow, out RowendValue);
        }
        #endregion ChkInType6XML

        #endregion

        #region F3230 FieldUse CheckOut


        #region F9065GetSnapshotDetail

        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F9065FieldUseData F9065_GetSnapshotDetail()
        {
            return F9065FieldUseComp.F9065_GetSnapshotDetail();
        }
        #endregion F9065GetSnapshotDetail

        #region F9065UpdateApplicationStatus
        /// <summary>
        /// F9065_s the update application status.
        /// </summary>
        /// <param name="ischeckedOut">if set to <c>true</c> [ischecked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [isonline].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer value</returns>
        public static int F9065_UpdateApplicationStatus(bool ischeckedOut, bool isOnline, int userId)
        {
            return F9065FieldUseComp.F9065_UpdateApplicationStatus(ischeckedOut, isOnline, userId);
        }
        #endregion F9065UpdateApplicationStatus

        #region F9065GetAuditCount
        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public static int F9065_GetAuditCount()
        {
            return F9065FieldUseComp.F9065_GetAuditCount();
        }
        #endregion F9065GetAuditCount

        #region F9065DeleteCheckOutTable
        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public static int F9065_DeleteCheckOutTable()
        {
            return F9065FieldUseComp.F9065_DeleteCheckOutTable();
        }
        #endregion F9065DeleteCheckOutTable

        #region F9065InsertFieldElement
        /// <summary>
        /// F9065_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F9065_InsertFieldElement(string fieldElement, int userId)
        {
            return F9065FieldUseComp.F9065_InsertFieldElement(fieldElement, userId);
        }
        #endregion F9065InsertFieldElement

        #region F9065GetPreviewDetail
        /// <summary>
        /// F9065_s the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F9065FieldUseData</returns>
        public static F9065FieldUseData F9065_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            return F9065FieldUseComp.F9065_GetPreviewDetail(snapShotId, snapShotDetail);
        }
        #endregion F9065GetPreviewDetail

        #region F9065_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public static F9065FieldUseData F9065_GetcfgConfiguration(string cfgname)
        {
            return F9065FieldUseComp.F9065_GetcfgConfiguration(cfgname);
        }

        #endregion F9065_GetcfgConfiguration

        #region F9065InsertApplicationConfiguration
        /// <summary>
        /// F9065_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F9065_InsertApplicationConfiguration(string configXml, int userId)
        {
            return F9065FieldUseComp.F9065_InsertApplicationConfiguration(configXml, userId);
        }
        #endregion F9065InsertApplicationConfiguration

        #region F3230GetSnapshotDetail

        /// <summary>
        /// F3230_AddValues
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyName"></param>
        /// <param name="Form"></param>
        /// <param name="ModuleID"></param>
        /// <param name="InsertedBy"></param>
        /// <returns></returns>
        public static int F3230_AddValues(int KeyID, string KeyName, int Form, int? ModuleID, int InsertedBy)
        {
            return F3230FieldUseComp.F3230_AddValues(KeyID, KeyName, Form, ModuleID, InsertedBy);
        }


        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F3230FieldUseData F3230_GetSnapshotDetail()
        {
            return F3230FieldUseComp.F3230_GetSnapshotDetail();
        }
        #endregion F3230GetSnapshotDetail

        #region F3230UpdateApplicationStatus
        /// <summary>
        /// F3230_s the update application status.
        /// </summary>
        /// <param name="ischeckedOut">if set to <c>true</c> [ischecked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [isonline].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer value</returns>
        public static int F3230_UpdateApplicationStatus(bool ischeckedOut, bool isOnline, int userId)
        {
            return F3230FieldUseComp.F3230_UpdateApplicationStatus(ischeckedOut, isOnline, userId);
        }
        #endregion F3230UpdateApplicationStatus

        #region F3230GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public static int F3230_GetAuditCount()
        //{
        //    return F3230FieldUseComp.F3230_GetAuditCount();
        //}
        #endregion F3230GetAuditCount

        #region F3230DeleteCheckOutTable
        /// <summary>
        /// Get Audit Count
        /// </summary>
        /// <returns>Integer</returns>
        public static int F3230_DeleteCheckOutTable()
        {
            return F3230FieldUseComp.F3230_DeleteCheckOutTable();
        }
        #endregion F3230DeleteCheckOutTable

        #region F3230InsertFieldElement
        /// <summary>
        /// F3230_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F3230_InsertFieldElement(string fieldElement, int userId)
        {
            return F3230FieldUseComp.F3230_InsertFieldElement(fieldElement, userId);
        }
        #endregion F3230InsertFieldElement

        #region F3230GetPreviewDetail
        /// <summary>
        /// F3230_s the preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_GetPreviewDetail(int snapShotId, string snapShotDetail)
        {
            return F3230FieldUseComp.F3230_GetPreviewDetail(snapShotId, snapShotDetail);
        }
        #endregion F9065GetPreviewDetail

        #region F3230_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        public static F3230FieldUseData F3230_GetcfgConfiguration(string cfgname)
        {
            return F3230FieldUseComp.F3230_GetcfgConfiguration(cfgname);
        }

        #endregion F9065_GetcfgConfiguration

        #region F3230InsertApplicationConfiguration
        /// <summary>
        /// F3230_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F3230_InsertApplicationConfiguration(string configXml, int userId)
        {
            return F3230FieldUseComp.F3230_InsertApplicationConfiguration(configXml, userId);
        }
        #endregion F3230InsertApplicationConfiguration


        #region ChkOutFormXML
        /// <summary>
        /// F3230_ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>        
        public static F3230FieldUseData F3230_ChkOutFormXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutFormXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutFormXML

        #region ParcelHeaderChkOutXML
        /// <summary>
        /// F25000_ParcelHeaderChkOutXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F25000FieldUseData F25000_ParcelHeaderChkOutXML(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_GetCheckOutDetails(snapShotId, snapShotValue);
        }
        #endregion ParcelHeaderChkOutXML

        #region ChkOutMiscXML
        /// <summary>
        /// F3230_ChkOutMiscXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutMiscXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutMiscXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutMiscXML

        #region ChkOutUserXML
        /// <summary>
        /// F3230_ChkOutUserXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutUserXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutUserXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutUserXML

        #region ChkOutEventXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutEventXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutEventXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutEventXML

        /// <summary>
        /// F3230_ChkOutParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutParcelXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutParcelXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }

        #region ChkOutOwnerXML
        /// <summary>
        /// F3230_ChkOutOwnerXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutOwnerXML


        #region ChkOutDeprMiscXML
        /// <summary>
        /// F3230ChkOutDeprMiscXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230ChkOutDeprMiscXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230ChkOutDeprMiscXML(snapShotId, snapShotValue);
        }

        public static F3230FieldUseData F3230ChkOutDeprXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230ChkOutDeprXML(snapShotId, snapShotValue);
        }


        #endregion ChkOutDeprMiscXML

        #region ChkOutEstimateCompXML
        /// <summary>
        /// F3230_ChkOutEstimateCompXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutEstimateCompXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutEstimateCompXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutEstimateCompXML

        #region ChkOutVSTGCitemXML
        /// <summary>
        /// ChkOutVSTGCitemXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutVSTGCitemXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutVSTGCitemXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutVSTGCitemXML

        #region ChkOutMSCEstimateXML
        /// <summary>
        /// ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutMSCEstimateXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutMSCEstimateXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutMSCEstimateXML

        #region ChkOutEstimateResultXML
        /// <summary>
        /// F3230_ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutEstimateResultXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutEstimateResultXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutEstimateResultXML

        #region ChkOutMSCEstimateOccupancyXML
        /// <summary>
        /// ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutMSCEstimateOccupancyXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutMSCEstimateOccupancyXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutMSCEstimateOccupancyXML

        #region ChkOutEstimateBuildingXML
        /// <summary>
        /// ChkOutEstimateBuildingXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutEstimateBuildingXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutEstimateBuildingXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutEstimateBuildingXML

        #region ChkOutLandValuesXML
        /// <summary>
        /// ChkOutLandValuesXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>   
        public static F3230FieldUseData F3230_ChkOutLandValuesXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutLandValuesXML(snapShotId, snapShotValue, TableName, StartRow, out  RowendValue);
        }
        #endregion ChkOutLandValuesXML

        #region ChkOutVSTerraGonXML
        /// <summary>
        /// F3230_ChkOutVSTerraGonXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutTerraGonXML(int snapShotId)
        {
            return F3230FieldUseComp.F3230_ChkOutTerraGonXML(snapShotId);
        }
        #endregion ChkOutVSTerraGonXML

        #region ChkOutEstimateComponentXML
        /// <summary>
        /// ChkOutEstimateComponentXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutEstimateComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutEstimateComponentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutEstimateComponentXML

        #region ChkOutCommentXML
        /// <summary>
        /// ChkOutCommentXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutCommentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutCommentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutCommentXML

        /// <summary>
        /// F3230_LockParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="LockAdminBy"></param>
        /// <param name="UserID"></param>
        /// <param name="UnlockParcelXML"></param>
        /// <returns></returns>
        public static int F3230_LockParcelID(int? SnapShotID, int? LockAdminBy, int? UserID, string UnlockParcelXML)
        {
            return F3230FieldUseComp.F3230_LockParcelID(SnapShotID, LockAdminBy, UserID, UnlockParcelXML);
        }

        /// <summary>
        /// F3230_ListLockedParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ListLockedParcelID(int? SnapShotID, out int RowendValue)
        {
            return F3230FieldUseComp.F3230_ListLockedParcelID(SnapShotID, out  RowendValue);
        }

        #region ChkOutVSTGComponentXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutVSTGComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutVSTGComponentXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutVSTGComponentXML

        #region ChkOutFileXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutFileXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutFileXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutFileXML

        #region ChkOutVSTGGonBldgXML
        /// <summary>
        /// ChkOutVSTGGonBldgXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutVSTGGonBldgXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutVSTGGonBldgXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion  ChkOutVSTGGonBldgXML


        #region ChkOutConfigXML
        /// <summary>
        /// F9065_s the get CHK out XML.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>int</returns>
        public static F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutConfigXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutConfigXML

        #region F3230_GetApexFilePathDetail
        /// <summary>
        /// F3230_Get Apex File Path Detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        public static F3230FieldUseData F3230GetApexFilePath(int snapShotId)
        {
            return F3230FieldUseComp.F3230GetApexFilePath(snapShotId);
        }
        #endregion F3230_GetApexFilePathDetail



        /// <summary>
        /// F3230_ChkOutCommonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F3230FieldUseData F3230_ChkOutCommonXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutCommonXML(snapShotId, snapShotValue);
        }


        /// <summary>
        /// f3230_ChkOutCorrectionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutCorrectionXML(int snapShotId)
        {
            return F3230FieldUseComp.f3230_ChkOutCorrectionXML(snapShotId);
        }

        /// <summary>
        /// f3230_ChkOutSaleXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutSaleXML(int snapShotId)
        {
            return F3230FieldUseComp.f3230_ChkOutSaleXML(snapShotId);
        }

        /// <summary>
        /// f3230_ChkOutReceiptXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutReceiptXML(int snapShotId)
        {
            return F3230FieldUseComp.f3230_ChkOutReceiptXML(snapShotId);
        }

        /// <summary>
        /// f3230_ChkOutStatementXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        public static F3230FieldUseData f3230_ChkOutStatementXML(int snapShotId)
        {
            return F3230FieldUseComp.f3230_ChkOutStatementXML(snapShotId);
        }



        #region ChkOutNBHDXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutNBHDXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutNBHDXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutNBHDXML

        #region ChkOutDistrictXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutDistrictXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutDistrictXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutDistrictXML

        #region ChkOutLegalXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutLegalXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutLegalXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutLegalXML

        #region ChkOutMisc_CatalogXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutMisc_CatalogXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutMisc_CatalogXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutMisc_CatalogXML

        #region ChkOutMiscTableXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutMiscTableXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutMiscTableXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutMiscTableXML

        #region ChkOutMOwnerXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutMOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutMOwnerXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutMOwnerXML

        #region ChkOutObjectXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutObjectXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutObjectXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutObjectXML

        #region ChkOutValueSliceXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F3230FieldUseData F3230_ChkOutValueSliceXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F3230FieldUseComp.F3230_ChkOutValueSliceXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutValueSliceXML

        #region ChkOutSitusXM
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>int</returns>
        public static F3230FieldUseData F3230_ChkOutSitusXML(int snapShotId, string snapShotValue)
        {
            return F3230FieldUseComp.F3230_ChkOutSitusXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutSitusXM

        #region ChkOutType2XML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>int</returns>
        public static F25000FieldUseData F25000_ChkOutType2XML(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_ChkOutType2XML(snapShotId, snapShotValue);
        }
        #endregion ChkOutType2XML

        #region ChkOutVersionXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F25000FieldUseData F25000_ChkOutVersionXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F25000FieldUseComp.F25000_ChkOutVersionXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutVersionXML

        #region ChkOutSeniorExemptionXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>int</returns>
        public static F25000FieldUseData F25000_ChkOutSeniorExemptionXML(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_ChkOutSeniorExemptionXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutSeniorExemptionXML

        #region ChkOutLandXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <param name="TableName">TableName</param>
        /// <param name="StartRow">StartRow</param>
        /// <param name="RowendValue">RowendValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F25000FieldUseData F25000_ChkOutLandXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            RowendValue = 0;
            return F25000FieldUseComp.F25000_ChkOutLandXML(snapShotId, snapShotValue, TableName, StartRow, out RowendValue);
        }
        #endregion ChkOutLandXML

        #region ChkOutAssessmentTypeXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F25000FieldUseData F25000_ChkOutAssessmentTypeXML(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_ChkOutAssessmentTypeXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutAssessmentTypeXML

        #region ChkOutParcelValueXML
        /// <summary>
        /// F3230_ChkOutEventXML
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">snapShotValue</param>
        /// <returns>F3230FieldUseData</returns>
        public static F25000FieldUseData F25000_ChkOutParcelValueXML(int snapShotId, string snapShotValue)
        {
            return F25000FieldUseComp.F25000_ChkOutParcelValueXML(snapShotId, snapShotValue);
        }
        #endregion ChkOutParcelValueXML

        #region InsertChkOutXML
        /// <summary>
        /// F9065_s the insert CHK out XML.
        /// </summary>
        /// <param name="xmlInsContent">Content of the XML ins.</param>
        /// <param name="tableXml">The table XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F3230_InsertChkOutXML(string xmlInsContent, string tableXml, int userId, bool IsDelete)
        {
            return F3230FieldUseComp.F3230_InsertChkOutXML(xmlInsContent, tableXml, userId, IsDelete);
        }
        #endregion InsertChkOutXML

        #region InsertChkInXML
        /// <summary>
        /// F3230_InsertChkOutXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static int F3230_InsertChkInXML(string xmlInsContent, string tableXml, int userId)
        {
            return F3230FieldUseComp.F3230_InsertChkInXML(xmlInsContent, tableXml, userId);
        }
        #endregion InsertChkInXML


        public static int F3230_InsertAddedRecordXML(string xmlInsContent, string tableXml, int userId)
        {
            return F3230FieldUseComp.F3230_InsertAddedRecordXML(xmlInsContent, tableXml, userId);
        }

        #endregion


        #region F3200_CAMASketch

        #region F3200_GetSketchData
        /// <summary>
        /// Get the Sketch Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>DataSet</returns>
        public static F3200CamaSketchData F3200_GetSketchData(int objectId)
        {
            return F3200CamaSketchComp.F3200_GetSketchData(objectId);
        }

        #endregion F3200_GetSketchData

        #region F3200_GetStyleList
        /// <summary>
        /// Get the Style List Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String</returns>
        public static string F3200_GetStyleList(int objectId)
        {
            return F3200CamaSketchComp.F3200_GetStyleList(objectId);
        }

        #endregion F3200_GetStyleList

        #region F3200_SaveSketchData
        /// <summary>
        /// Save the Sketch data
        /// </summary>
        /// <param name="objectId">objectId</param>
        /// <param name="sketchData">sketchData</param>
        /// <param name="userId">UserID</param>
        /// <returns>Dataset</returns>
        public static DataSet F3200_SaveSketchData(int objectId, string sketchData, int userId)
        {
            return F3200CamaSketchComp.F3200_SaveSketchData(objectId, sketchData, userId);
        }
        #endregion F3200_SaveSketchData

        #region F3200_CheckSmartPart

        /// <summary>
        /// Check the SmartPart
        /// </summary>
        /// <param name="formId">FormNumber</param>
        /// <returns>integer</returns>
        public static int F3200_CheckSmartPart(int formId)
        {
            return F3200CamaSketchComp.F3200_CheckSmartPart(formId);
        }

        #endregion F3200_CheckSmartPart

        #endregion F3200_CAMASketch

        #region F95010WebFormXML

        #region F95010GetWebFormXML
        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public static F95010GetWebFormXMLData GetWebFormXML(int? keyId, int form, int userId)
        {
            return F95010GetWebFormXMLComp.GetWebFormXML(keyId, form, userId);
        }
        #endregion F95010GetWebFormXML

        #endregion F95010WebFormXML

        #region F3510 Neighborhood Selection

        #region F3510_NeighborhoodSelection

        /// <summary>
        /// To Get the Neighborhood Selection details
        /// </summary>
        /// <param name="neighborhood">Neighborhood</param>
        /// <param name="childof">The childof.</param>
        /// <param name="rollyear">The rollyear.</param>
        /// <param name="type">Neighborhoodtype</param>
        /// <param name="description">Description</param>
        /// <returns>
        /// Typed Dataset Containing the Neighborhood Selection details
        /// </returns>
        public static F3510NeighborhoodSelectionData F3510_ListNeighborhoodSelection(string neighborhood, string childof, string rollyear, string type, string description)
        {
            return F3510NeighborhoodSelectionComp.F3510_ListNeighborhoodSelectionDetails(neighborhood, childof, rollyear, type, description);
        }

        #endregion F3510_NeighborhoodSelection

        #region F3510_NeighborhoodType

        /// <summary>
        /// F3510_s the type of the neighborhood list.
        /// </summary>
        /// <returns>NeighborhoodType</returns>
        public static F3510NeighborhoodSelectionData F3510_ListNeighborhoodType()
        {
            return F3510NeighborhoodSelectionComp.F3510_neighborhoodType();

        }

        #endregion

        #endregion F3510 Neighborhood Selection

        #region F2010_StateCodeSelection

        #region List StateCodeSelection

        /// <summary>
        /// F2010_s the list state code selection.
        /// </summary>
        /// <returns>F2010StateCodeSelectionData</returns>
        public static F2010StateCodeSelectionData F2010_ListStateCodeSelection()
        {
            return F2010StateCodeSelectionComp.F2010_ListStateCodeSelection();
        }

        #endregion List StateCodeSelection

        #endregion F2010_StateCodeSelection

        #region F9066 CheckIn

        #region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //public static int F9066_GetAuditCount()
        //{
        //    return F9066CheckInComp.F9066_GetAuditCount();
        //}
        #endregion GetAuditCount

        #region GetCheckInXML
        ///// <summary>
        ///// Get Check In Details
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public static F9066CheckInData F9066_GetCheckInData()
        //{
        //    return F9066CheckInComp.F9066_GetCheckInData();
        //}
        #endregion GetCheckInXML

        #region SaveXML
        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="insertValue">insertValue</param>
        /// <param name="updateValue">updateValue</param>
        public static void F9066_SaveData(string insertValue, string updateValue)
        {
            F9066CheckInComp.F9066_SaveData(insertValue, updateValue);
        }

        #endregion SaveXML

        #region DeleteData
        ///// <summary>
        ///// Delete the values
        ///// </summary>
        //public static int F9066_DeleteData()
        //{
        //    return F9066CheckInComp.F9066_DeleteData();
        //}
        #endregion DeleteData

        #endregion F9066 CheckIn


        #region F1430 Interest Calculator
        /// <summary>
        /// F1430_GetCalculatorDetails gets the calculator details on load.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>TypedDataSet</returns>
        public static F1430InterestCalculatorData F1430_GetCalculatorDetails(int statementId)
        {
            return F1430InterestCalculatorComp.F1430_GetCalculatorDetails(statementId);
        }

        /// <summary>
        /// F1430_GetInterestDetails get the interest and deliquency details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxAmount">The tax amount.</param>
        /// <returns>TypedDataSet</returns>
        public static F1430InterestCalculatorData F1430_GetInterestDetails(int statementId, DateTime interestDate, decimal taxAmount)
        {
            return F1430InterestCalculatorComp.F1430_GetInterestDetails(statementId, interestDate, taxAmount);
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
        public static int F1440_SaveRecieptinSnapShotBatchButtonControl(int snapshotId, int? receiptId, int userId)
        {
            return F1440BatchButtonComp.F1440_SaveRecieptinSnapShotBatchButtonControl(snapshotId, receiptId, userId);
        }

        #endregion F1440_SaveRecieptinSnapShotBatchButtonControl

        #endregion F1440 Batch Button SmartPart

        #region F82001 BuildingPermit

        #region GetBuildingPermitDetails

        /// <summary>
        /// F82001_s the get building permit details.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns>Typed dataset</returns>
        public static F82001BuildingPermitData F82001_GetBuildingPermitDetails(int eventID)
        {
            return F82001BuildingPermitComp.F82001_GetBuildingPermitDetails(eventID);
        }

        #endregion GetBuildingPermitDetails

        #region InsertBuildingPermitDetails

        /// <summary>
        /// F82001_s the insert building permit details.
        /// </summary>
        /// <param name="permitId">The permit id.</param>
        /// <param name="buildingPermitItems">The building permit items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer value</returns>
        public static int F82001_InsertBuildingPermitDetails(int permitId, string buildingPermitItems, int userId)
        {
            return F82001BuildingPermitComp.F82001_InsertBuildingPermitDetails(permitId, buildingPermitItems, userId);
        }

        #endregion InsertBuildingPermitDetails

        #endregion F82001 BuildingPermit

        #region F82002ContractorManagement

        /// <summary>
        /// List Excemption Type
        /// </summary>
        /// <param name="icontractorId">The icontractor id.</param>
        /// <param name="ContractorXML">The contractor XML.</param>
        /// <returns>DataSet</returns>
        public static F82002ContractorManagementData F82002_ListContractorManagementData(int? icontractorId, string contractorXmlvalue)
        {
            return F82002ContractorManagementComp.F82002_ListContractorManagementData(icontractorId, contractorXmlvalue);
        }


        public static int F82002_InsertBuildingPermitDetails(int? ContractorID, string ContractorItems, int UserID)
        {

            return F82002ContractorManagementComp.F82002_InsertBuildingPermitDetails(ContractorID, ContractorItems, UserID);
        }

        public static void F82002_DeleteContractorManagement(int contractorId, int UserID)
        {
            F82002ContractorManagementComp.F82002_DeleteContractorManagement(contractorId, UserID);
        }

        #endregion

        #region F36060DepreciationComp

        #region F36060_GetDepreciationTables

        /// <summary>
        /// To get the Depreciation  tables
        /// </summary>
        /// <param name="deprTableId">Deprtable id</param>
        /// <returns>Typed dataset containing the Deprecition and Deprecition items datatable</returns>
        public static F36060DepreciationData F36060_GetDepreciationTables(int deprTableId)
        {
            return F36060DepreciationComp.F36060_GetDepreciationTables(deprTableId);
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
            return F36060DepreciationComp.F36060_SaveDepreciationTables(deprTableId, deprecationItem, otherDeprItem, userId);
        }

        #endregion F36060_SaveDepreciationTables

        #region F36060_DeleteDePreciationTables

        /// <summary>
        /// To delete Depreciation Tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="userId">The user id.</param>
        public static void F36060_DeleteDepreciationTables(int deprTableId, int userId)
        {
            F36060DepreciationComp.F36060_DeleteDepreciationTables(deprTableId, userId);
        }

        #endregion F36060_DeleteDePreciationTables

        #endregion F36060DepreciationComp

        #region F49910InstrumentHeader

        #region Get Instrumentheader Details

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetInstrumentHeaderDetails(int instId)
        {
            return F49910InstrumentHeaderComp.F49910_GetInstrumentHeaderDetails(instId);
        }

        #endregion Get Instrumentheader Details

        #region ListInstrumentType

        /// <summary>
        /// F49910_GetInstrumentTypeDetails
        /// </summary>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetInstrumentTypeDetails()
        {
            return F49910InstrumentHeaderComp.F49910_GetInstrumentTypeDetails();
        }

        #endregion ListInstrumentType

        #region SaveInstrumentHeader Details

        /// <summary>
        /// F49910_SaveInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="paymentItems">paymentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F49910_SaveInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return F49910InstrumentHeaderComp.F49910_SaveInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion SaveInstrumentHeader Details

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
        public static int F49910CheckInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            return F49910InstrumentHeaderComp.F49910CheckInstrumentHeaderDetails(instId, instrumentItems, paymentItems, userId);
        }

        #endregion F49910CheckInstrumentHeader Deatils

        #region DeleteInstrumentHeader Details

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F49910_DeleteInstrumentHeader(int instId, int userId)
        {
            return F49910InstrumentHeaderComp.F49910_DeleteInstrumentHeader(instId, userId);
        }

        #endregion DeleteInstrumentHeader Details

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
        public static F49910InstrumentHeaderDataSet F49910_CopyInstrumentHeaderDetails(int instrumentId, int instrumentValue, int grantorValue, int granteeValue, int legalValue)
        {
            return F49910InstrumentHeaderComp.F49910_CopyInstrumentHeaderDetails(instrumentId, instrumentValue, grantorValue, granteeValue, legalValue);
        }

        #endregion CopyInstrumentDetails

        #region F49910_GetGranterAddressDetails

        /// <summary>
        /// F49910_GetGranterAddressDetails
        /// </summary>
        /// <param name="grantId">grantId</param>
        /// <returns>dataset</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetGranterAddressDetails(int grantId)
        {
            return F49910InstrumentHeaderComp.F49910_GetGranterAddressDetails(grantId);
        }

        #endregion F49910_GetGranterAddressDetails

        #region GetFeeDetails

        /// <summary>
        /// F49910_GetFeeDetails
        /// </summary>
        /// <param name="insTypeId">insTypeId</param>
        /// <returns>dataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetFeeDetails(int insTypeId)
        {
            return F49910InstrumentHeaderComp.F49910_GetFeeDetails(insTypeId);
        }

        #endregion GetFeeDetails

        #endregion F49910InstrumentHeader

        #region F2200EditShedule

        #region F2200_ListEditShedule

        public static F2200EditScheduleData F2200_ListEditScheduleDetails(int SheduleID)
        {
            return F2200EditScheduleComp.F2200_ListEditScheduleDetails(SheduleID);
        }

        public static F2200EditScheduleData F25050_GetScheduleDetails(int ScheduleID)
        {
            return F2200EditScheduleComp.F25050_GetScheduleDetails(ScheduleID);
        }

        public static int F2005_GetValidUser(int scheduleID,int userID)
        {
            return F2200EditScheduleComp.F2005_GetValidUser(scheduleID,userID);
        }

        public static int F2005_UpdateParcelLockDetails(int scheduleID, int LockValue, int userID)
        {
            return F2200EditScheduleComp.F2005_UpdateParcelLockDetails(scheduleID, LockValue, userID);
        }

        public static F2200EditScheduleData F2005_GetScheduleUserName( int userID)
        {
            return F2200EditScheduleComp.F2005_GetScheduleUserName(userID);
        }

        #endregion

        #region F2200_InsertEditSchedule

        public static F2200EditScheduleData F2200_InsertEditSchedule(int? ScheduleID, string ScheduleItems, int UserID)
        {
            return F2200EditScheduleComp.F2200_InsertEditSchedule(ScheduleID, ScheduleItems, UserID);
        }
        #endregion

        #region F2200_UpdateEditSchedule

        public static int F2200_UpdateEditSchedule(int ScheduleID, string ScheduleItems, int UserID)
        {
            return F2200EditScheduleComp.F2200_UpdateEditSchedule(ScheduleID, ScheduleItems, UserID);
        }

        #endregion

        #region F2200_DeleteEditSchedule
        public static int F2200_DeleteEditSchedule(int ScheduleID, int UserID)
        {
            return F2200EditScheduleComp.F2200_DeleteEditSchedule(ScheduleID, UserID);
        }
        #endregion

        #region List Assessment Type Details
        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        public static F2200EditScheduleData F2200_GetAssessmentTypeDetails(string assessmentType)
        {
            return F2200EditScheduleComp.F2200_GetAssessmentTypeDetails(assessmentType);
        }
        #endregion

        #region Get Penalty Percent

        /// <summary>
        /// Gets the penalty percent.
        /// </summary>
        /// <param name="filingDate">The filing date.</param>
        /// <returns>Penalty Percent</returns>
        public static decimal GetPenaltyPercent(string filingDate)
        {
            return F2200EditScheduleComp.GetPenaltyPercent(filingDate);
        }

        /// <summary>
        /// Get Farm Exempt 259.
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="isFarmExempt"></param>
        /// <param name="ExemptRollYear"></param>
        /// <param name="isEx259"></param>
        /// <param name="ex259Amount"></param>
        /// <returns></returns>
        public static decimal f2200_GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear, bool isEx259, decimal ex259Amount)
        {
            return F2200EditScheduleComp.F2200_GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear, isEx259, ex259Amount);
        }

        /// <summary>
        /// Get 259 Exemption Amount.
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        public static F2200EditScheduleData F2200_Get259ExemptionAmount(int scheduleId)
        {
            return F2200EditScheduleComp.F2200_Get259ExemptionAmount(scheduleId);
        }
        #endregion Get Penalty Percent

        #endregion

        #region F49920 Instument Search Engine

        #region Instrument Search Engine Load Item

        /// <summary>
        /// F49920_s the list instrument load.
        /// </summary>
        /// <returns></returns>
        public static F49920InstrumentSearchEngineData F49920_ListInstrumentLoad()
        {
            return F49920InstrumentSearchEngineComp.F49920_ListInstrumentLoad();
        }

        #endregion

        #region Insrument Search Engine Search

        /// <summary>
        /// F49920_s the list instrument search.
        /// </summary>
        /// <param name="instrumentcondition">The instrumentcondition.</param>
        /// <returns></returns>
        public static F49920InstrumentSearchEngineData F49920_ListInstrumentSearch(string instrumentcondition)
        {
            return F49920InstrumentSearchEngineComp.F49920_ListInstrumentSearch(instrumentcondition);
        }

        #endregion

        #endregion

        #region F49911 PartiesField Listing

        #region  List PartiesField

        /// <summary>
        /// F49911_s the list parties field.
        /// </summary>
        /// <returns></returns>
        public static F49910InstrumentHeaderDataSet F49911_ListPartiesField()
        {
            return F49910InstrumentHeaderComp.F49911_ListPartiesField();
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
        public static int F49911_InsertPartiesFieldDetails(int instid, string grantorItems, string granteeItems, int userId, int isCopy)
        {
            return F49910InstrumentHeaderComp.F49911_InsertPartiesFieldDetails(instid, grantorItems, granteeItems, userId, isCopy);
        }
        #endregion  Insert PartiesField

        #endregion F49911 PartiesField Listing

        #region F49912 LegalField

        #region List
        /// <summary>
        /// F49912_s the list legal field.
        /// </summary>
        /// <returns></returns>
        //public static F49910InstrumentHeaderDataSet F49912_ListLegalField(int instID )
        //{
        //    return F49910InstrumentHeaderComp.F49912_ListLegalField(instID);
        //}
        public static F49912LegalData F49912_ListLegalField(int instID)
        {
            return F49912LegalComp.F49912_ListLegalField(instID);
        }
        #endregion List

        #region Insert

        /// <summary>
        /// F49912_s the insert legal field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F49912_InsertLegalFieldDetails(int instid, string legalItems, int userId, int isCopy)
        {
            return F49912LegalComp.F49912_InsertLegalFieldDetails(instid, legalItems, userId, isCopy);
        }


        /// <summary>
        /// F49912_s the list sub division combo.
        /// </summary>
        /// <returns></returns>
        public static F49912LegalData F49912_ListSubDivisionCombo()
        {
            return F49912LegalComp.F49912_ListSubDivisionCombo();
        }

        #endregion Insert

        #endregion F49912 LegalField

        #region F36061 Depreciation Control

        #region F36061_ListDepr

        /// <summary>
        /// Used to List the Depr Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depr Details
        /// </returns>
        public static F36061DepreciationControlData F36061_ListDepr(int nbhdId)
        {
            return F36061DepreciationControlComp.F36061_ListDepr(nbhdId);
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
        public static F36061DepreciationControlData F36061_ListDeprControlItems(int nbhdId)
        {
            return F36061DepreciationControlComp.F36061_ListDeprControlItems(nbhdId);
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
        public static int F36061_SaveDeprControlItems(int? nbhdId, string deprControlItems, int userId)
        {
            return F36061DepreciationControlComp.F36061_SaveDeprControlItems(nbhdId, deprControlItems, userId);
        }

        #endregion F36061_SaveDeprControlItems

        #endregion F36061 Depreciation Control


        #region F36062 LandInfluence Control

        #region F36062_LandInfluenceItems

        /// <summary>
        /// Used to Get the Land Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Land Control Items Details
        /// </returns>
        public static F36062LandInfluenceData F36062_LandInfluenceItems(int nbhdId)
        {
            return F36062LandInfluenceControlComp.F36062_LandInfluenceItems(nbhdId);
        }

        #endregion F36062_LandInfluenceItems

        #region F36062_SaveInfluenceControl

        /// <summary>
        /// Used to save the Land Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The Land control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public static int F36062_SaveInfluenceControl(int? nbhdId, string InfluenceItems, int userId)
        {
            return F36062LandInfluenceControlComp.F36062_SaveInfluenceControl(nbhdId, InfluenceItems, userId);
        }

        #endregion F36062_SaveInfluenceControl

        #endregion F36062 LandInfluence Control

        #region F35050ScheduleLineItem

        #region ListScheduleLineItem

        /// <summary>
        /// F35050_GetScheduleLineItemDetails
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <returns>DataSet</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleLineItemDetails(int scheduleId)
        {
            return F35050ScheduleLineItemComp.F35050_GetScheduleLineItemDetails(scheduleId);
        }
        #endregion ListScheduleLineItem

        #region ListScheduleItem

        /// <summary>
        /// F35050_GetScheduleItem
        /// </summary>
        /// <returns>Dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleItem()
        {
            return F35050ScheduleLineItemComp.F35050_GetScheduleItem();
        }

        #endregion ListScheduleItem

        #region F35050ListTableDetails

        public static F35050ScheduleLineItemDataSet F35050_GetListTableDetails(int itemcategoryID)
        {
            return F35050ScheduleLineItemComp.F35050_GetListTableDetails(itemcategoryID);
        }

        #endregion ListTableDetails

        #region F35050ListOutTableDetails

        public static F35050ScheduleLineItemDataSet F35050_GetListOutTableDetails(int ScheduleID)
        {
            return F35050ScheduleLineItemComp.F35050_GetListOutTableDetails(ScheduleID);

        }

        #endregion F35050ListOutTableDetails

        #region ListScheduleCategory

        /// <summary>
        /// F35050_GetScheduleCategory
        /// </summary>
        /// <returns>Dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleCategory()
        {
            return F35050ScheduleLineItemComp.F35050_GetScheduleCategory();
        }

        #endregion ListScheduleCategory

        #region SaveScheduleLineItem

        /// <summary>
        /// F35050_SaveScheduleLineItem
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="scheduleItems">scheduleItems</param>
        /// <param name="userId">userId</param>
        /// <returns>Int</returns>
        public static int F35050_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return F35050ScheduleLineItemComp.F35050_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        #endregion SaveScheduleLineItem

        /// <summary>
        ///  #region F35050_CalculateAmount
        /// </summary>
        /// <param name="ScheduleItemID">ScheduleItemID</param>
        /// <param name="Rollyear">Rollyear</param>
        /// <param name="Year">Year</param>
        /// <param name="Qnty">Qnty</param>
        /// <returns></returns>
        #region F35050_CalculateAmount

        public static F35050ScheduleLineItemDataSet F35050_CalculateAmount(int ScheduleItemID, int Rollyear, int Year, int DeprDescription)
        {
            return F35050ScheduleLineItemComp.F35050_CalculateAmount(ScheduleItemID, Rollyear, Year, DeprDescription);
        }

        #endregion





        #region GetDepreciationValue

        /// <summary>
        /// F35050_GetDepreciationValue
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="recv">recv</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>DataSet</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetDepreciationValue(int scheduleId, int recv, int rollYear)
        {
            return F35050ScheduleLineItemComp.F35050_GetDepreciationValue(scheduleId, recv, rollYear);
        }
        #endregion GetDepreciationValue

        #region DeleteScheduleLineItem

        /// <summary>
        /// DeleteScheduleLineItem
        /// </summary>
        /// <param name="scheduleLineId">scheduleLineId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F35050_DeleteScheduleLineItem(int scheduleLineId, int userId)
        {
            return F35050ScheduleLineItemComp.F35050_DeleteScheduleLineItem(scheduleLineId, userId);
        }
        #endregion DeleteScheduleLineItem

        public static F35050ScheduleLineItemDataSet F35050_GetDeprPercentage(int rollyear, int deprtableID, int year)
        {
            return F35050ScheduleLineItemComp.F35050_GetDeprPercentage(rollyear, deprtableID, year);
        }

        #region DeleteSchedule

        /// <summary>
        /// F35050_s the delete schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status</returns>
        public static int F35050_DeleteSchedule(int scheduleId, int userId)
        {
            return F35050ScheduleLineItemComp.F35050_DeleteSchedule(scheduleId, userId);
        }

        #endregion

        #endregion F35050ScheduleLineItem

        #region F1402 Schedule Search
        /// <summary>
        /// F1402_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns></returns>
        public static F1402ScheduleSelectionData F1402_ListScheduleSearch(string ScheduleConditionXML)
        {
            return F1402ScheduleSearchComp.F1402_ListScheduleSearch(ScheduleConditionXML);
        }
        #endregion

        #region F27010 MiscAssessment

        #region GetRollYear
        /// <summary>
        /// F27010s the get roll year.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>DataSet</returns>
        public static int F27010GetRollYear(int parcelId)
        {
            return F27010MiscAssessmentComp.F27010GetRollYear(parcelId);
        }
        #endregion GetRollYear

        #region Get Assessment Type
        /// <summary>
        /// F27010s the type of the get assessment.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetAssessmentType(int rollYear)
        {
            return F27010MiscAssessmentComp.F27010GetAssessmentType(rollYear);
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
        public static F27010MiscAssessmentData F27010GetDistrict(int parcelId, int madTypeId, int rollYear)
        {
            return F27010MiscAssessmentComp.F27010GetDistrict(parcelId, madTypeId, rollYear);
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
        public static int F27010CheckDefaultDistrict(int parcelId, int madTypeId, int rollYear)
        {
            return F27010MiscAssessmentComp.F27010CheckDefaultDistrict(parcelId, madTypeId, rollYear);
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
        public static F27010MiscAssessmentData F27010GetMessage(int parcelId, int madTypeId, int madDistrictId)
        {
            return F27010MiscAssessmentComp.F27010GetMessage(parcelId, madTypeId, madDistrictId);
        }
        #endregion Get ToolTip Message

        #region GetMiscAssessment (MADType1)
        /// <summary>
        /// F27010s the get misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetMiscData(int madDistrictId, int parcelId)
        {
            return F27010MiscAssessmentComp.F27010GetMiscData(madDistrictId, parcelId);
        }
        #endregion GetMiscAssessment (MADType1)

        #region GetMiscAssessment (Other MADType)
        /// <summary>
        /// F27010s the get others misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetOthersMiscData(int madDistrictId, int parcelId, string procedureName)
        {
            return F27010MiscAssessmentComp.F27010GetOtherMiscData(madDistrictId, parcelId, procedureName);
        }
        #endregion GetMiscAssessment (Other MADType)

        #region GetDefaultMiscData

        /// <summary>
        /// F27010s the get default misc data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <returns></returns>
        public static F27010MiscAssessmentData F27010GetDefaultMiscData(int parcelId, int madTypeId)
        {
            return F27010MiscAssessmentComp.F27010GetDefaultMiscData(parcelId, madTypeId);
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
        public static int F27010_SaveMiscAssessment(int parcelId, string miscType, string madItem, string miscItems, int userId)
        {
            return F27010MiscAssessmentComp.F27010_SaveMiscAssessment(parcelId, miscType, madItem, miscItems, userId);
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
        public static F84401SignsPropertyData F84401_GetSignsProperties(int featureId)
        {
            return F84401SignsPropertiesComp.F84401_GetSignsProperties(featureId);
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
        public static int F84401_SaveSignsProperties(int featureId, string signsProperties, int userId)
        {
            return F84401SignsPropertiesComp.F84401_SaveSignsProperties(featureId, signsProperties, userId);
        }

        #endregion Save Signs Properties

        #region Delete Signs Properties

        /// <summary>
        /// F84401_s the delete signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="userId">The user id.</param>
        public static void F84401_DeleteSignsProperties(int featureId, int userId)
        {
            F84401SignsPropertiesComp.F84401_DeleteSignsProperties(featureId, userId);
        }

        #endregion Delete Signs Properties

        #endregion F84401 Signs Properties

        #region F29531 AssociationLink-LinkType

        /// <summary>
        /// F29531s the type of the association link.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static F29531AssciationLinkData F29531AssociationLinkType(int userid)
        {
            return F29531AssociationLinkComp.F29531AssociationLinkType(userid);
        }

        /// <summary>
        /// F29531_s the fill association link grid.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        public static F29531AssciationLinkData F29531_FillAssociationLinkGrid(int keyid, int formId)
        {
            return F29531AssociationLinkComp.F29531_FillAssociationLinkGrid(keyid, formId);
        }

        /// <summary>
        /// F29531_s the save association link.
        /// </summary>
        /// <param name="associationID">The association ID.</param>
        /// <param name="associationLinkItems">The association link items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29531_SaveAssociationLink(int associationID, string associationLinkItems, int userId)
        {
            return F29531AssociationLinkComp.F29531_SaveAssociationLink(associationID, associationLinkItems, userId);
        }

        /// <summary>
        /// Updates the association link details.
        /// </summary>
        /// <param name="associationDetails">The association details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void UpdateAssociationLinkDetails(string associationDetails, int userId)
        {
           F29531AssociationLinkComp.UpdateAssociationLinkDetails(associationDetails, userId);
        }
        /// <summary>
        /// F29531_s the get link text.
        /// </summary>
        /// <param name="cfgid">The cfgid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns></returns>
        public static string F29531_GetLinkText(int cfgid, int keyid)
        {
            return F29531AssociationLinkComp.F29531_GetLinkText(cfgid, keyid);
        }

        /// <summary>
        /// F29531_s the delete association link.
        /// </summary>
        /// <param name="associationId">The association id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29531_DeleteAssociationLink(int associationId, int userId)
        {
            F29531AssociationLinkComp.F29531_DeleteAssociationLink(associationId, userId);
        }

        #endregion

        #region F29610 HOHExemption

        /// <summary>
        /// F29610_s the get ho H exemption details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public static F29610HoHExemptionData F29610_GetHoHExemptionDetails(int eventid)
        {
            return F29610HoHExemptionComp.F29610_GetHoHExemptionDetails(eventid);
        }

        /// <summary>
        /// F29610_s the get calculation of ho H.
        /// </summary>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <param name="exemptionid">The exemptionid.</param>
        /// <returns></returns>
        public static F29610HoHExemptionData F29610_GetCalculationOfHoH(int scheduleid, int exemptionid)
        {
            return F29610HoHExemptionComp.F29610_GetCalculationOfHoH(scheduleid, exemptionid);
        }

        public static F29610HoHExemptionData F29610_GetOwnerPercent(int ownerId, int scheduleid)
        {
            return F29610HoHExemptionComp.F29610_GetOwnerPercent(ownerId, scheduleid);
        }

        /// <summary>
        /// F29610_s the save ho H exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="HoHItems">The ho H items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29610_SaveHoHExemptionDetails(int eventId, string HoHItems, int userId)
        {
            return F29610HoHExemptionComp.F29610_SaveHoHExemptionDetails(eventId, HoHItems, userId);
        }

        #endregion

        #region F9610 QuickFind


        /// <summary>
        /// F9610s the quick find.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        public static F9610QuickFind F9610QuickFind(int form, string keyword)
        {
            return F9610QuickFindComp.F9610QuickFind(form, keyword);
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
        public static F9110MasterNameSearchData F9110GetMasterNameSearch(string lastName, string firstName, string address)
        {
            return F9110MasterNameSearchComp.F9110GetMasterNameSearch(lastName, firstName, address);
        }

        #endregion

        #region F1411ParcelStatementSearch Form

        /// <summary>
        /// Gets the Parcel Statement search.
        /// </summary>
        /// <param name="Search">Search Number.</param>
        public static F1411ParcelStatementSearchData F1411ParcelStmtSearch(string searchNumber)
        {
            return F1411ParcelStatementSearchComp.f1411ParcelStatementSearch(searchNumber);
        }

        #endregion


        #region F29620Agland Application Details
        public static F29620AglandApplicationData F29620_GetAglandApplicationDetails(int eventid)
        {
            return F29620AglandApplicationComp.F29620_GetAglandApplicationDetails(eventid);
        }

        public static int F29620_SaveAglandApplicationDetails(int eventId, int ownerId, int userId)
        {
            return F29620AglandApplicationComp.F29620_SaveAglandApplicationDetails(eventId, ownerId, userId);
        }
        #endregion

        #region StateAssessedOwner
        /// <summary>
        /// F35075_s the get state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        public static F35075StateAssessedData F35075_GetStateAssessedOwnerDetails(int stateId)
        {
            return F35075StateAssessedOwnerComp.F35075_GetStateAssessedOwnerDetails(stateId);
        }


        /// <summary>
        /// F35075_s the save state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="assessedItems">The assessed items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35075_SaveStateAssessed(int? stateId, string assessedItems, int userId)
        {
            return F35075StateAssessedOwnerComp.F35075_SaveStateAssessedOwner(stateId, assessedItems, userId);
        }

        /// <summary>
        /// F35076_s the save state assessed grid.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="codeItems">The code items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35076_SaveStateAssessedGrid(int? stateId, string codeItems, int userId)
        {
            return F35075StateAssessedOwnerComp.F35076_SaveStateAssessedGrid(stateId, codeItems, userId);
        }


        /// <summary>
        /// F35075_s the delete state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35075_DeleteStateAssessed(int stateId, int userId)
        {
            return F35075StateAssessedOwnerComp.F35075_DeleteStateAssessed(stateId, userId);
        }

        /// <summary>
        /// F35076_s the delete state assessed details.
        /// </summary>
        /// <param name="stateIemId">The state iem id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35076_DeleteStateAssessedDetails(int stateIemId, int userId)
        {
            return F35075StateAssessedOwnerComp.F35076_DeleteStateAssessedDetails(stateIemId, userId);
        }


        #endregion

        #region F2204 CopySchedule

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F2204CopyScheduleData F25050GetParcelTypeDetails()
        {
            return F2204CopyScheduleComp.F25050GetParcelTypeDetails();
        }

        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F2204CopyScheduleData F25050GetScheduleTypeDetails()
        {
            return F2204CopyScheduleComp.F25050GetScheduleTypeDetails();
        }

        /// <summary>
        /// Creates the new parcel copy.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public static int F2204CreateNewScheduleCopy(int scheduleId, string scheduleItems, int userId)
        {
            return F2204CopyScheduleComp.F2204CreateNewScheduleCopy(scheduleId, scheduleItems, userId);
        }

        #endregion F2204 CopySchedule

        #region F24630 BoardOfEqualization

        #region Get BoardOfEqualizationDetails
        /// <summary>
        /// F29630s the get board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <returns>DataSet</returns>
        public static F29630BoardOfEqualizationData F29630GetBoardOfEqualizationDetails(int boeId)
        {
            return F29630BoardOfEqualizationComp.F29630GetBoardOfEqualizationDetails(boeId);
        }
        #endregion Get BoardOfEqualizationDetails

        #region Save BoardOfEqualizationDetails
        /// <summary>
        /// F29630s the save board of equalization details.
        /// </summary>
        /// <param name="boeElements">The boe elements.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id</param>
        public static void F29630SaveBoardOfEqualizationDetails(string boeElements, string boeValues, int userId)
        {
            F29630BoardOfEqualizationComp.F29630SaveBoardOfEqualizationDetails(boeElements, boeValues, userId);
        }
        #endregion Save BoardOfEqualizationDetails

        #region Delete BoardOfEqualizationDetails
        /// <summary>
        /// F29630s the delete board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29630DeleteBoardOfEqualizationDetails(int boeId, int userId)
        {
            F29630BoardOfEqualizationComp.F29630DeleteBoardOfEqualizationDetails(boeId, userId);
        }
        #endregion Delete BoardOfEqualizationDetails

        #region Push BoardOfEqualizationDetails
        /// <summary>
        /// F29630s the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29630PushBoardOfEqualizationDetails(int boeId, int userId)
        {
            F29630BoardOfEqualizationComp.F29630PushBoardOfEqualizationDetails(boeId, userId);
        }
        #endregion Push BoardOfEqualizationDetails

        #endregion F24630 BoardOfEqualization

        #region F9041 Query View Description

        #region Get QueryDescription

        /// <summary>
        /// F9041s the get query description.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static F9041QueryViewDescriptionData F9041GetQueryDescription(int queryViewId)
        {
            return F9041QueryViewDescriptionComp.F9041GetQueryDescription(queryViewId);
        }

        #endregion Get QueryDescription

        #endregion F9041 Query View Description

        #region F82006 Contractor Management

        #region Get Contractor and Employee List

        /// <summary>
        /// F82006_s the get contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <returns>contractManagementData</returns>
        public static F82006ContractManagementData F82006_GetContractorList(int contractorId)
        {
            return F82006ContractManagementComp.F82006_GetContractorList(contractorId);
        }

        #endregion Get Contractor and Employee List

        #region Save Contractor and Employee List

        /// <summary>
        /// F82006_s the save contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="contractorXml">The contractor XML.</param>
        /// <param name="contractorEmployeeXml">The contractor employee XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>errorId</returns>
        public static int F82006_SaveContractorList(int? contractorId, string contractorXml, string contractorEmployeeXml, int userId)
        {
            return F82006ContractManagementComp.F82006_SaveContractorList(contractorId, contractorXml, contractorEmployeeXml, userId);
        }

        #endregion Save Contractor and Employee List

        #region Delete Contractor and Employee List

        /// <summary>
        /// F82006_s the delete contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        public static void F82006_DeleteContractorList(int contractorId, int userId)
        {
            F82006ContractManagementComp.F82006_DeleteContractorList(contractorId, userId);
        }

        /// <summary>
        /// F82006_s the delete contractor employee.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="userId">The user id.</param>
        public static void F82006_DeleteContractorEmployee(int contractorId, int employeeId, int userId)
        {
            F82006ContractManagementComp.F82006_DeleteContractorEmployee(contractorId, employeeId, userId);
        }

        #endregion Delete Contractor and Employee List

        #endregion F82006 Contractor Management

        #region F9042 Analytics Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public static F9042ExcelAnalyticsData F9042_GetTemplate(int templateId)
        {
            return F9042TemplateNameComp.F9042_GetTemplate(templateId);
        }

        /// <summary>
        /// F9042_s the list template.
        /// </summary>
        /// <param name="queryView">The query view.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public static F9042ExcelAnalyticsData F9042_ListTemplate(string queryView)
        {
            return F9042TemplateNameComp.F9042_ListTemplate(queryView);
        }

        #endregion F9042 Analytics Template Selection

        /// <summary>
        /// Gets the snapshot details.
        /// </summary>
        /// <param name="FormNum">The form num.</param>
        /// <param name="UserId">The user id.</param>
        /// <returns></returns>
        public static F9044SnapshotOperations GetSnapshotDetails(int FormNum, int UserId)
        {
            return F9044SnapshotOperationsComp.GetSnapshotDetails(FormNum, UserId);
        }

        /// <summary>
        /// Gets the snapshot operation count.
        /// </summary>
        /// <param name="OperationId">The operation id.</param>
        /// <param name="LOSnapshotId">The LO snapshot id.</param>
        /// <param name="ROSnapshotId">The RO snapshot id.</param>
        /// <param name="UserId">The user id.</param>
        /// <returns></returns>
        public static F9044SnapshotOperations GetSnapshotOperationCount(int OperationId, int LOSnapshotId, int ROSnapshotId, int UserId)
        {
            return F9044SnapshotOperationsComp.GetSnapshotOperationCount(OperationId, LOSnapshotId, ROSnapshotId, UserId);
        }

        /// <summary>
        /// Inserts the snapshot details.
        /// </summary>
        /// <param name="OperationId">The operation id.</param>
        /// <param name="LOSnapshotId">The LO snapshot id.</param>
        /// <param name="ROSnapshotId">The RO snapshot id.</param>
        /// <param name="RecordCount">The record count.</param>
        /// <param name="NewSnapshotName">New name of the snapshot.</param>
        /// <param name="UserId">The user id.</param>
        public static void insertSnapshotDetails(int OperationId, int LOSnapshotId, int ROSnapshotId, int RecordCount, string NewSnapshotName, int UserId)
        {
            F9044SnapshotOperationsComp.insertSnapshotDetails(OperationId, LOSnapshotId, ROSnapshotId, RecordCount, NewSnapshotName, UserId);
        }

        #region F81003 Selection Catalog

        #region Get Selection Catalog Details

        /// <summary>
        /// F81003_s the get selection catalog details.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <returns>selection catalog dataset</returns>
        public static F81003SelectionCatalogData F81003_GetSelectionCatalogDetails(int catalogId)
        {
            return F81003SelectionCatalogComp.F81003_GetSelectionCatalogDetails(catalogId);
        }

        #endregion Get Selection Catalog Details

        #region List Selection Category

        /// <summary>
        /// F81003_s the list selection category.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>selection catalog dataset</returns>
        public static F81003SelectionCatalogData F81003_ListSelectionCategory(int userId)
        {
            return F81003SelectionCatalogComp.F81003_ListSelectionCategory(userId);
        }

        #endregion List Selection Category

        #region Save Selection Catalog

        /// <summary>
        /// F81003_s the save selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <returns>key id.</returns>
        public static int F81003_SaveSelectionCatalog(int? catalogId, string selectionItemsXml)
        {
            return F81003SelectionCatalogComp.F81003_SaveSelectionCatalog(catalogId, selectionItemsXml);
        }

        #endregion Selection Catalog

        #region Delete Selection Catalog

        /// <summary>
        /// F81003_s the delete selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        public static void F81003_DeleteSelectionCatalog(int catalogId)
        {
            F81003SelectionCatalogComp.F81003_DeleteSelectionCatalog(catalogId);
        }

        #endregion Delete Selection Catalog

        #endregion F81003 Selection Catalog

        #region F9510WebFormXML
        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet</returns>
        public static F95010GetWebFormXMLData F9510GetWebFormXML(int form, int userId)
        {
            return F9510WebFormXMLComp.GetWebFormXML(form, userId);
        }
        #endregion F9510WebFormXML

        #region F9075 List Template Selection

        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>listtemplateData</returns>
        public static F9075CommentTemplate F9075_ListTemplate(int form, int userid)
        {
            return F9075CommentComp.F9075_ListTemplate(form, userid);
        }

        #endregion F9075 List Template Selection
        #region F9075_DeleteCommentIds

        /// <summary>
        /// F9075_s the delete comment.
        /// </summary>
        /// <param name="commentIds">The comment ids.</param>
        /// <param name="userId">The user id.</param>
        public static void F9075_DeleteCommentIds(string commentIds, int userId)
        {
            F9075CommentComp.F9075_DeleteCommentIds(commentIds, userId);
        }

        #endregion F36041_DeleteCrop

        #region F9076New Comment Template

        #region F9076 list Template Selection

        /// <summary>
        /// F9076_gets the template.
        /// </summary>
        /// <param name="templateid">The templateid.</param>
        /// <returns>F9076NewCommentTemplateComp</returns>
        public static F9076NewCommentTemplateData F9076_getTemplate(int templateid)
        {
            return F9076NewCommentTemplateComp.F9076_getTemplate(templateid);
        }

        #endregion F9076 list Template Selection

        #region F9076 SaveNewCommentTemplate Selection


        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="commentItemsXml">The comment items XML.</param>
        /// <param name="isOverwrite">The is overwrite.</param>
        /// <returns>F9076NewCommentTemplateComp</returns>
        public static int F9076SaveNewCommentTemplate(int? templateId, string commentItemsXml, int isOverwrite)
        {
            return F9076NewCommentTemplateComp.F9076SaveNewCommentTemplate(templateId, commentItemsXml, isOverwrite);
        }

        #endregion F9076 SaveNewCommentTemplate Selection

        #region F9076 DeleteNewCommentTemplate Selection


        /// <summary>
        /// F9076_s the delete new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        public static void F9076_DeleteNewCommentTemplate(int templateId)
        {
            F9076NewCommentTemplateComp.F9076_DeleteNewCommentTemplate(templateId);
        }

        #endregion F9076 DeleteNewCommentTemplate Selection

        #endregion New Comment Template

        #region F29505CreateSubdivision

        /// <summary>
        /// F429505_s the list all comoboxes.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData F429505_ListAllComoboxes(int eventId)
        {
            return F29505CreateSubdivisionComp.F429505_ListAllComoboxes(eventId);
        }

        /// <summary>
        /// F29505_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData F29505_GetBaseParcelValue(int eventId)
        {
            return F29505CreateSubdivisionComp.F29505_GetBaseParcelValue(eventId);
        }

        /// <summary>
        /// F29505_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData ListLandCodes(int nbhdid, int rollyear)
        {
            return F29505CreateSubdivisionComp.ListLandCodes(nbhdid, rollyear);
        }

        /// <summary>
        /// F29505_s the create parcel.
        /// </summary>
        /// <param name="makeSubId">The make sub id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Return message</returns>
        public static string F29505_CreateParcel(int makeSubId, int userId)
        {
            return F29505CreateSubdivisionComp.F29505_CreateParcel(makeSubId, userId);
        }

        /// <summary>
        /// F29505_s the save division parcels.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="makeSubItemsXml">The make sub items XML.</param>
        /// <param name="makeSubParcelsXml">The make sub parcels XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29505_SaveDivisionParcels(int eventId, string makeSubItemsXml, string makeSubParcelsXml, int userId)
        {
            return F29505CreateSubdivisionComp.F29505_SaveDivisionParcels(eventId, makeSubItemsXml, makeSubParcelsXml, userId);
        }

        /// <summary>
        /// F29505_s the save sub division.
        /// </summary>
        /// <param name="makeSubID">The make sub ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29505_SaveSubDivision(int makeSubID, int userId)
        {
            return F29505CreateSubdivisionComp.F29505_SaveSubDivision(makeSubID, userId);
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
        public static F29505CreateSubdivisionData F29505_GetLandCode(int landType1, int landType2, int landType3, int nbhdid, int rollYear)
        {
            return F29505CreateSubdivisionComp.F29505_GetLandCode(landType1, landType2, landType3, nbhdid, rollYear);
        }

        #endregion

        #region F9025ValidationControl

        #region F9025 FormValidationDetails Selection

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="formid">The formid.</param>
        /// <param name="userid">The userid.</param>
        /// <returns>int</returns>
        public static int F9025FormValidationDetails(int formid, int userid)
        {
            return F9025ValidationControlComp.F9025FormValidationDetails(formid, userid);
        }
        #endregion F9025 FormValidationDetails Selection

        #region F9025 SaveValidationDetails Selection

        /// <summary>
        /// F9076s the save new comment template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="commentItemsXml">The comment items XML.</param>
        /// <param name="isOverwrite">The is overwrite.</param>
        /// <returns></returns>
        public static int F9025SaveValidationDetails(int formid, int userid, int keyid)
        {
            return F9025ValidationControlComp.F9025SaveValidationDetails(formid, userid, keyid);
        }
        #endregion F9025 SaveValidationDetails Selection

        #endregion F9025ValidationControl

        #region Selection

        /// <summary>
        /// F81004_s the get selection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>selection dataset</returns>
        public static F81004SelectionData F81004_GetSelectionDetails(int eventId, int form)
        {
            return F81004SelectionComp.F81004_GetSelectionDetails(eventId, form);
        }

        /// <summary>
        /// F81004_s the get selection catalog details.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>selection catalog data table</returns>
        public static F81004SelectionData.GetSelectionCatalogDetailsDataTable F81004_GetSelectionCatalogDetails(int categoryId)
        {
            return F81004SelectionComp.F81004_GetSelectionCatalogDetails(categoryId);
        }

        /// <summary>
        /// F81004_s the save selection items.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>eventId</returns>
        public static int F81004_SaveSelectionItems(int eventId, string selectionItemsXml, int userId)
        {
            return F81004SelectionComp.F81004_SaveSelectionItems(eventId, selectionItemsXml, userId);
        }

        #endregion Selection

        #region List District Assessment ParcelID

        /// <summary>
        /// F1031_s the get district assessment parcel ID.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public static F2200EditScheduleData f25050GetDistrictAssessmentParcelID(string parcelNumber, int parcelId)
        {
            return F2200EditScheduleComp.f25050GetDistrictAssessmentParcelID(parcelNumber, parcelId);
        }

        /// <summary>
        /// F2200s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public static F2200EditScheduleData f2200GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear)
        {
            return F2200EditScheduleComp.f2200GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear);
        }
        #endregion

        #region F29640 Frozen Value

        #region Get Frozen Value

        /// <summary>
        /// Gets the frozen value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Frozen Value</returns>
        public static F29640FrozenValueData GetFrozenValue(int eventId)
        {
            return F29640FrozenValueComp.GetFrozenValue(eventId);
        }

        #endregion Get Frozen Value

        #region Save Frozen Details

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        /// <param name="frozenElements">The frozen elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveFrozenDetails(string frozenElements, int userId)
        {
            F29640FrozenValueComp.SaveFrozenDetails(frozenElements, userId);
        }

        #endregion Save Frozen Details

        #region Delete Frozen Details

        /// <summary>
        /// Deletes the frozen details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="frozenId">The frozen id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteFrozenDetails(int eventId, int frozenId, int userId)
        {
            F29640FrozenValueComp.DeleteFrozenDetails(eventId, frozenId, userId);
        }

        #endregion Delete Frozen Details

        #endregion F29640 Frozen Value

        #region F29650 Exemption

        #region List Exemption Type

        /// <summary>
        /// Gets the type of the exemption.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Exemption Type</returns>
        public static F29650ExemptionData GetExemptionType(int eventId)
        {
            return F29650ExemptionComp.GetExemptionType(eventId);
        }

        #endregion List Exemption Type

        #region Get Exemption

        /// <summary>
        /// Gets the exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Exemption Details</returns>
        public static F29650ExemptionData GetExemptionDetails(int eventId)
        {
            return F29650ExemptionComp.GetExemptionDetails(eventId);
        }

        #endregion Get Exemption

        #region Get Exemption loss

        /// <summary>
        /// Gets the exemption loss.
        /// </summary>
        /// <param name="lossValue">The loss value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Exemption Loss</returns>
        public static decimal GetExemptionLoss(decimal lossValue, decimal maxValue)
        {
            return F29650ExemptionComp.GetExemptionLoss(lossValue, maxValue);
        }

        #endregion Get Exemption Loss

        #region Save Exemption

        /// <summary>
        /// Saves the exemption details.
        /// </summary>
        /// <param name="exemptionElements">The exemption elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveExemptionDetails(string exemptionElements, int userId)
        {
            F29650ExemptionComp.SaveExemptionDetails(exemptionElements, userId);
        }

        #endregion Save Exemption

        #region Delete Exemption

        /// <summary>
        /// Deletes the exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="exemptionEventId">The exemption event id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteExemptionDetails(int eventId, int exemptionEventId, int userId)
        {
            F29650ExemptionComp.DeleteExemptionDetails(eventId, exemptionEventId, userId);
        }

        #endregion Delete Exemption

        #endregion F29650 Exemption

        #region F35060 Schedule Item Code

        #region Get Schedule Item Code

        /// <summary>
        /// Gets the schedule item codes.
        /// </summary>
        /// <returns>DataSet contains Schedule Item Codes</returns>
        public static F35060ScheduleItemCodeData GetScheduleItemCodes()
        {
            return F35060ItemCodeComp.GetScheduleItemCodes();
        }

        #endregion Get Schedule Item Code

        #region Save Schedule Item Code

        /// <summary>
        /// Saves the schedule item codes.
        /// </summary>
        /// <param name="scheduleCodeElements">The schedule code elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveScheduleItemCodes(string scheduleCodeElements, int userId)
        {
            F35060ItemCodeComp.SaveScheduleItemCodes(scheduleCodeElements, userId);
        }

        #endregion Save Schedule Item Code

        #region Delete Schedule Item Code

        /// <summary>
        /// Deletes the schedule item codes.
        /// </summary>
        /// <param name="itemCodeId">The item code id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteScheduleItemCodes(string itemCodeId, int userId)
        {
            F35060ItemCodeComp.DeleteScheduleItemCodes(itemCodeId, userId);
        }

        #endregion Delete Schedule Item Code

        #endregion F35060 Schedule Item Code

        #region F2409Review Status
        /// <summary>
        /// F2409_s the type of the reviewstatus inspection.
        /// </summary>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ReviewstatusInspectionType()
        {
            return F2409ReviewStatusComp.F2409_ReviewstatusInspectionType();
        }

        /// <summary>
        /// F2409_s the reviewstatus inspection by user.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ReviewstatusInspectionByUser(int applicationId)
        {
            return F2409ReviewStatusComp.F2409_ReviewstatusInspectionByUser(applicationId);
        }

        /// <summary>
        /// F2409_ReviewStatusData
        /// </summary>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_Reviewstatus()
        {
            return F2409ReviewStatusComp.F2409_Reviewstatus();
        }


        /// <summary>
        /// F2409_s the list reviewstatus.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F2409ReviewStatusData F2409_ListReviewstatus(int parcelId)
        {
            return F2409ReviewStatusComp.F2409_ListReviewstatus(parcelId);
        }

        /// <summary>
        /// F2409s the update parcel review details.
        /// </summary>
        /// <param name="reviewXML">The review XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F2409UpdateParcelReviewDetails(string reviewXML, int userId)
        {
            F2409ReviewStatusComp.F2409UpdateParcelReviewDetails(reviewXML, userId);
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
        /// <returns>Confirmation for Save</returns>
        public static int F2205CreateSchedule(int? scheduleId, bool isNewSchedule, string scheduleHeaderItems, string scheduleItems, int userId)
        {
            return F2205MoveScheduleComp.F2205CreateSchedule(scheduleId, isNewSchedule, scheduleHeaderItems, scheduleItems, userId);
        }

        #endregion F2205 Move Schedule

        #region F350555 PPLine Items
        /// <summary>
        /// F35055_s the get PP line items details.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns></returns>
        public static F35055PPLineItemData F35055_GetPPLineItemsDetails(int scheduleID)
        {
            return F35055PPLineItemComp.F35055_GetPPLineItemsDetails(scheduleID);
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
        /// <returns></returns>
        public static F35055PPLineItemData F35055_GetValueCalculation(int scheduleId, int ppDeprTableId, Int64 originalValue, int trend, Int16 year, Int16 rollYear)
        {
            return F35055PPLineItemComp.F35055_GetValueCalculation(scheduleId, ppDeprTableId, originalValue, trend, year, rollYear);
        }

        /// <summary>
        /// F35055_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35055_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return F35055PPLineItemComp.F35055_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35055_s the update schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static int F35055_UpdateScheduleLineItem(int scheduleId, string scheduleItems, int userId, Int16 rollYear)
        {
            return F35055PPLineItemComp.F35055_UpdateScheduleLineItem(scheduleId, scheduleItems, userId, rollYear);
        }

        /// <summary>
        /// F35055_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35055_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return F35055PPLineItemComp.F35055_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
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
        public static int CheckTrendRollYear(int? trendYearId, int rollYear)
        {
            return F36066TrendComp.CheckTrendRollYear(trendYearId, rollYear);
        }

        #endregion Check Trend

        #region Get Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <returns>Trend Details</returns>
        public static F36066TrendData GetTrendDetails(int trendYearId)
        {
            return F36066TrendComp.GetTrendDetails(trendYearId);
        }

        #endregion Get Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendYearItems">The trend year items.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation value for save</returns>
        public static int SaveTrend(int? trendYearId, string trendYearItems, string trendItems, int userId)
        {
            return F36066TrendComp.SaveTrend(trendYearId, trendYearItems, trendItems, userId);
        }

        #endregion Save Trend

        #region Delete Trend

        /// <summary>
        /// Deletes the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteTrend(int? trendYearId, string trendItems, int userId)
        {
            F36066TrendComp.DeleteTrend(trendYearId, trendItems, userId);
        }

        #endregion Delete Trend

        #endregion F36066 Trend

        #region F35051 Schedule Line Items

        /// <summary>
        /// F35051_s the get schedule line item details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The schedule line items dataset.</returns>
        public static F35051ScheduleLineItemsData F35051_GetScheduleLineItemDetails(int scheduleId)
        {
            return F35051ScheduleLineItemsComp.F35051_GetScheduleLineItemDetails(scheduleId);
        }

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public static int F35051_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return F35051ScheduleLineItemsComp.F35051_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public static int F35051_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return F35051ScheduleLineItemsComp.F35051_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
        }

        /// <summary>
        /// F35051_s the get depr percentage.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="year">The year.</param>
        /// <returns>The schedule line items dataset.</returns>
        public static F35051ScheduleLineItemsData F35051_GetDeprPercentage(Int16 rollYear, int deprTableId, Int16 year)
        {
            return F35051ScheduleLineItemsComp.F35051_GetDeprPercentage(rollYear, deprTableId, year);
        }

        #endregion F35051 Schedule Line Items

        #region F25055 Personal Property Header

        /// <summary>
        /// Gets the property header details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>Personal property header details</returns>
        public static F25055PropertyHeaderData GetPropertyHeaderDetails(int scheduleId)
        {
            return F25055PersonalPropertyComp.GetPropertyHeaderDetails(scheduleId);
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
        public static int F36065_CheckDeprRollYear(int? deprYearId, int rollYear)
        {
            return F36065PersonalDepreciationComp.F36065_CheckDeprRollYear(deprYearId, rollYear);
        }

        #endregion Check Depreciation RollYear

        #region Get Depreciation Details

        /// <summary>
        /// F36065_s the get depr details.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <returns>DataSet contains Depreciation details</returns>
        public static F36065PersonalDeprData F36065_GetDeprDetails(int deprYearId)
        {
            return F36065PersonalDepreciationComp.F36065_GetDeprDetails(deprYearId);
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
        public static int F36065_SaveDepreciation(int? deprYearId, string deprYearItems, string depreciationItems, int userId)
        {
            return F36065PersonalDepreciationComp.F36065_SaveDepreciation(deprYearId, deprYearItems, depreciationItems, userId);
        }

        #endregion Save Depreciation

        #region Delete Depreciation

        /// <summary>
        /// F36065_s the delete depreciattion.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        public static void F36065_DeleteDepreciattion(int? deprYearId, string depreciationItems, int userId)
        {
            F36065PersonalDepreciationComp.F36065_DeleteDepreciattion(deprYearId, depreciationItems, userId);
        }

        #endregion Delete Depreciation

        #endregion F36065 Personal Property Depreciation

        #region F15020 Receipt Type

        /// <summary>
        /// F15020_s the get receipt types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns></returns>
        public static F1070ReceiptTypeData F15020_GetReceiptTypes(int userId, short formId, int keyId)
        {
            return F1070ReceiptTypeComp.F15020_GetReceiptTypes(userId, formId, keyId);
        }

        #endregion F15020 Receipt Type

        #region F1504 Copy Account

        /// <summary>
        /// F1504_s the get copy account sub fund.
        /// </summary>
        /// <returns></returns>
        public static F1504CopyAccountData F1504_GetCopyAccountSubFund()
        {
            return F1504CopyAccountComp.F1504_GetCopyAccountSubFund();
        }

        /// <summary>
        /// F1504_s the get account detail.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        public static F1504CopyAccountData F1504_GetAccountDetail(int accountId)
        {
            return F1504CopyAccountComp.F1504_GetAccountDetail(accountId);
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
        public static F1504CopyAccountData F1504_SaveCopyAccountDetails(int rollYear, string subFund, string description, string function, string bars, string accObject, string line, string userId)
        {
            return F1504CopyAccountComp.F1504_SaveCopyAccountDetails(rollYear, subFund, description, function, bars, accObject, line, userId);
        }

        #endregion

        #region F32012 Catalog

        /// <summary>
        /// F3200_s the get sketch data.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        ///// <returns>Catalog data</returns>
        public static F32012CatalogData F32012_GetCatalogData(int valueSliceId)
        {
            return F32012CatalogComp.F32012_GetCatalogData(valueSliceId);
        }

        /// <summary>
        /// F32012_s the save catalog.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="catalogData">The catalog data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation value for save</returns>
        public static DataSet F32012_SaveCatalog(int objectId, string catalogData, int userId)
        {
            return F32012CatalogComp.F32012_SaveCatalog(objectId, catalogData, userId);
        }

        #endregion F32012 Catalog

        #region F3205 Apex Sketch

        ///<summary>
        /// ApexSketchImage
        /// </summary>
        public static F3205ApexSketchData F3205pcgetSketchFilePath(int parcelId, int userId)
        {
            return F3205ApexSketchComp.F3205pcgetSketchFilePath(parcelId, userId);
        }

        /// <summary>
        ///F3205 pcget SketchLinks Exist.
        /// </summary>
        public static F3205ApexSketchData F3205pcgetSketchLinksExist(int parcelId, int userId)
        {
            return F3205ApexSketchComp.F3205pcgetSketchLinksExist(parcelId, userId);
        }

        /// <summary>
        /// Saves the sketch Image Path.
        /// </summary>
        public static F3205ApexSketchData F3205pcinsSketchImage(int parcelId, int userId, int pageCount)
        {
            return F3205ApexSketchComp.F3205pcinsSketchImage(parcelId, userId, pageCount);
        }

        /// <summary>
        /// insert Apex Sketch
        /// </summary>
        public static void SaveApexSketch(string SketchDataXML, int parcelId, int userId)
        {
            F3205ApexSketchComp.SaveApexSketch(SketchDataXML, parcelId, userId);
        }

        ///<summary>
        /// ReCalc Values
        /// </summary>
        public static string F3205_pcexeReCalcValues(int userId, int parcelId)
        {
            return F3205ApexSketchComp.F3205_pcexeReCalcValues(userId, parcelId);
        }


        #endregion F3205 Apex Sketch

        #region F1403 ParcelSection

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1403ParcelSearch</returns>
        public static F1403ParcelSearch F1403_GetParcelType(int? parcelId)
        {
            return F1403ParcelSelectionComp.F1403_GetParcelType(parcelId);
        }

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1403ParcelSearch</returns>
        public static F1403ParcelSearch F1403_GetSearchResult(string parcelSearchXml)
        {
            return F1403ParcelSelectionComp.F1403_GetSearchResult(parcelSearchXml);
        }

        /// <summary>
        /// F1403_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public static F1403ParcelSearch F1403_GetSaleTrackingRollYear(int eventID)
        {
            return F1403ParcelSelectionComp.F1403_GetSaleTrackingRollYear(eventID);
        }
        #endregion F1403 ParcelSection

        #region F1404 Schedule Search
        /// <summary>
        /// F1404_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns></returns>
        public static F1404ScheduleSelectionData F1404_ListScheduleSearch(string ScheduleConditionXML)
        {
            return F1404ScheduleSearchComp.F1404_ListScheduleSearch(ScheduleConditionXML);
        }

        /// <summary>
        /// F1403_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public static F1404ScheduleSelectionData F1404_GetScheduleType(int? scheduleId)
        {
            return F1404ScheduleSearchComp.F1404_GetScheduleType(scheduleId);
        }
        /// <summary>
        /// F1403_s the get Schedule tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public static F1404ScheduleSelectionData F1404_GetScheduleTrackingRollYear(int eventID)
        {
            return F1404ScheduleSearchComp.F1404_GetScheduleTrackingRollYear(eventID);
        }
        #endregion

        #region F1405 State Search

        /// <summary>
        /// F1405_s the list state search.
        /// </summary>
        /// <param name="StateConditionXML">The state condition XML.</param>
        /// <returns></returns>
        public static F1405StateSelectionData F1405_ListStateSearch(string StateConditionXML)
        {
            return F1405StateSearchComp.F1405_ListStateSearch(StateConditionXML);
        }

        /// <summary>
        /// F1403_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        //public static F1405StateSelectionData F1405_GetStateType(int? stateId)
        //{
        //    return F1405StateSearchComp.F1405_GetStateType(stateId);
        //}
        #endregion F1405 State Search

        #region F28000 Discretionary Details

        #region Get Discretionary Details

        /// <summary>
        /// Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Discretionary Details</returns>
        public static F28000DiscretionaryData F28000_GetDiscretionaryDetails(int eventId)
        {
            return F28000Discretionarycomp.F28000_GetDiscretionaryDetails(eventId);
        }

        #endregion Get Discretionary Details

        #region Get Class Details

        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        public static F28000DiscretionaryData F28000_GetClass(int stateId, int eventId)
        {
            return F28000Discretionarycomp.F28000_GetClass(stateId, eventId);
        }

        #endregion Get Class Details

        #region Exemption Amount

        /// <summary>
        /// Exemption Amount
        /// </summary>
        /// <param name="rollYear">roll Year</param>
        /// <param name="exemptionYear">Exemption Year</param>
        /// <param name="subjectAmount">Subject Amount</param>
        /// <returns>Exemption Amount</returns>
        public static F28000DiscretionaryData F28000_GetExemptionAmount(int rollYear, int exemptionYear, decimal subjectAmount)
        {
            return F28000Discretionarycomp.F28000_GetExemptionAmount(rollYear, exemptionYear, subjectAmount);
        }

        #endregion Exemption Amount

        #region Save Discretionary Details

        /// <summary>
        /// Save Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML string</param>
        /// <param name="userId">User ID</param>
        /// <returns>Confirmation Value</returns>
        public static int F28000_SaveDiscretionaryDetail(int eventId, int? discretionaryId, string discretionaryItems, int userId)
        {
            return F28000Discretionarycomp.F28000_SaveDiscretionaryDetail(eventId, discretionaryId, discretionaryItems, userId);
        }

        #endregion Save Discretionary Details

        #region Delete Discretionary Details

        /// <summary>
        /// Delete Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML String</param>
        /// <param name="userId">USer ID</param>
        public static void F28000_DeletediscretionaryDetails(int? discretionaryId, string discretionaryItems, int userId)
        {
            F28000Discretionarycomp.F28000_DeletediscretionaryDetails(discretionaryId, discretionaryItems, userId);
        }

        #endregion Delete Discretionary Details

        #endregion F28000 discretionary Details

        #region F28100 BOE

        #region Get BOE Details

        /// <summary>
        /// Get BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>BOE Details</returns>
        public static F28100BOEData F28100_GetBOEDetails(int eventId)
        {
            return F28100BOEComp.F28100_GetBOEDetails(eventId);
        }

        #endregion Get BOE Details

        #region Get Total Amount

        /// <summary>
        /// Get Total amounts
        /// </summary>
        /// <param name="boeId">boe ID</param>
        /// <param name="eventId">Event ID</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Total values</returns>
        public static F28100BOEData F28100_GetTotalAmount(int boeId, int eventId, string assessedValues)
        {
            return F28100BOEComp.F28100_GetTotalAmount(boeId, eventId, assessedValues);
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
        public static int F28100_SaveBOEDetails(int eventId, string boeItems, string assessedValues, int userId)
        {
            return F28100BOEComp.F28100_SaveBOEDetails(eventId, boeItems, assessedValues, userId);
        }

        #endregion Save BOE Details

        #region Delete BOE Details

        /// <summary>
        /// Delete BOE
        /// </summary>
        /// <param name="boeId">BOE ID</param>
        /// <param name="userId">The User ID</param>
        public static void F28100_DeleteBOEDetails(int? boeId, int userId)
        {
            F28100BOEComp.F28100_DeleteBOEDetails(boeId, userId);
        }

        #endregion Delete BOE Details

        #region Push Value
        /// <summary>
        /// F28100 the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28100_PushBOEDetails(int boeId, int userId)
        {
            F28100BOEComp.F28100_PushBOEDetails(boeId, userId);
        }
        #endregion Push Value

        #region Local Values

        /// <summary>
        /// Get Local Values
        /// </summary>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assesed Value</returns>
        public static F28100BOEData F28100_GetLocalValues(string assessedValues)
        {
            return F28100BOEComp.F28100_GetLocalValues(assessedValues);
        }

        #endregion Local Values

        #region County Values

        /// <summary>
        /// Get County Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Assessed Value</returns>
        public static F28100BOEData F28100_GetCountyValues(bool isLocal, string assessedValues)
        {
            return F28100BOEComp.F28100_GetCountyValues(isLocal, assessedValues);
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
        public static F28100BOEData F28100_GetStateValues(bool isLocal, bool isCounty, string assessedValues)
        {
            return F28100BOEComp.F28100_GetStateValues(isLocal, isCounty, assessedValues);
        }

        #endregion State Values

        #endregion F28100 BOE

        #region F29551 Parcel Sale Tracking

        /// <summary>
        /// DataSet to populate combo values
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>DataSet to populate combos</returns>
        public static F29551ParcelSaleTrackingData F29551_GetParcelSaleComboDetails(int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_GetParcelSaleComboDetails(userId);
        }

        /// <summary>
        /// DataSet to Populate Grid and other controls
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User ID</param>
        /// <returns>DataSet to populate Controls</returns>
        public static F29551ParcelSaleTrackingData F29551_GetParcelSaleTrackingDetails(int eventId, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_GetParcelSaleTrackingDetails(eventId, userId);
        }

        /// <summary>
        /// Data to populate Owner Grid
        /// </summary>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="ownerId">The Owner Id</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Owner Details DataSet</returns>
        public static F29551ParcelSaleTrackingData F29551_GetOwnerDetails(int? saleId, int? ownerId, int? parcelId, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_GetOwnerDetails(saleId, ownerId, parcelId, userId);
        }

        /// <summary>
        /// Parcel and Owner details
        /// </summary>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="parcelCollection">Parcel Collections</param>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Parcel and Owner details</returns>
        public static F29551ParcelSaleTrackingData F29551_GetParcelOwnerDetails(int? parcelId, string parcelCollection, int? saleId, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_GetParcelOwnerDetails(parcelId, parcelCollection, saleId, userId);
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
        public static int F29551_SaveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_SaveParcelSaleDetails(eventId, saleItems, parcelItems, ownerItems, userId);
        }

        /// <summary>
        /// Create Sale Versions
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="checkedParcels">Checked Parcels List</param>
        /// <returns>Message returned from SP</returns>
        public static string F29551_CreateSaleVersions(int eventId, int userId, string checkedParcels)
        {
            return F29551ParcelSaleTrackingComp.F29551_CreateSaleVersions(eventId, userId, checkedParcels);
        }

        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        public static string F29551_TransferOwnership(int eventId, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_TransferOwnership(eventId, userId);
        }

        /// <summary>
        /// F29551_s the update sale parcel.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message returned from SP</returns>
        public static string F29551_UpdateSaleParcel(int eventId, int userId)
        {
            return F29551ParcelSaleTrackingComp.F29551_UpdateSaleParcel(eventId, userId);
        }

        #endregion F29511 Parcel Sale Tracking

        #region F9045 Generic Search

        /// <summary>
        /// F9045s the get configuration.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <returns>Configuration Details</returns>
        public static F9045GenericSearchData F9045GetConfiguration(int genericSearchId)
        {
            return F9045GenericSearchComp.F9045GetConfiguration(genericSearchId);
        }

        /// <summary>
        /// F9045s the get search results.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Search Results</returns>
        public static F9045GenericSearchData F9045GetSearchResults(int genericSearchId, string searchString, int userId)
        {
            return F9045GenericSearchComp.F9045GetSearchResults(genericSearchId, searchString, userId);
        }

        #endregion F9045 Generic Search

        #region F3201 Sketch Link

        /// <summary>
        /// F3201_s the get sketch link data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Sketch Link Data</returns>
        public static F3201SketchLinkData F3201_GetSketchLinkData(int parcelId, int userId)
        {
            return F3201SketchLinkComp.F3201_GetSketchLinkData(parcelId, userId);
        }

        /// <summary>
        /// F3201_s the save sketch link data.
        /// </summary>
        /// <param name="linkXML">The link XML.</param>
        /// <param name="parcelId">The Parcel Id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Message</returns>
        public static string F3201_SaveSketchLinkData(string linkXML, int parcelId, int userId)
        {
            return F3201SketchLinkComp.F3201_SaveSketchLinkData(linkXML, parcelId, userId);
        }

        #endregion F3201 Sketch Link

        #region F1500 Sample Form Details
        /// <summary>
        /// F1500_s the get sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <returns>Returns Form and Form config details</returns>
        public static F1500SampleForm F1500_GetSampleFormDetails(int FormID)
        {
            return F1500GetSampleFormComp.F1500_GetSampleFormDetails(FormID);
        }
        #endregion

        public static F1500SampleForm F1500_GetSampleFormDetails()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inserts the sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="SampleFormDetails">The sample form details.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public static int InsertSampleFormDetails(int FormID, string SampleFormDetails, int UserID)
        {
            return F1500GetSampleFormComp.InsertSampleFormDetails(FormID, SampleFormDetails, UserID);
        }

        /// <summary>
        /// Gets the application id.
        /// </summary>
        /// <returns></returns>
        public static F1500SampleForm GetApplicationId()
        {
            return F1500GetSampleFormComp.GetApplicationId();
        }

        /// <summary>
        /// Gets the menu id details.
        /// </summary>
        /// <returns>Returns menu goroupid</returns>
        public static F1500SampleForm GetMenuIdDetails()
        {
            return F1500GetSampleFormComp.GetMenuIdDetails();
        }


        /// <summary>
        /// F1500_s the delete fom ID details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="GroupID">The group ID.</param>
        public static void F1500_DeleteFomIDDetails(int FormID, int GroupID)
        {
            F1500GetSampleFormComp.F1500_DeleteFomIDDetails(FormID, GroupID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static int InsertFieldUseDetails(int KeyID, string KeyField, int UserID)
        {
            return F3230FieldUseComp.InsertFieldUseDetails(KeyID, KeyField, UserID);
        }

        #region F25006pareclNavigation
        public static F25006ParcelNavigation GetParcelDetails(int keyID, bool IsNext)
        {
            return F25006ParcelNavigationComp.GetParcelDetails(keyID, IsNext);
        }
        #endregion

        #region F35080
        /// <summary>
        /// F35080_s the central assessed owner details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        public static F35080CentralAssessedOwner F35080_CentralAssessedOwnerDetails(int CentralId)
        {
            return F35080CentralAssessedownerComp.F35080_CentralAssessedOwnerDetails(CentralId);
        }

        /// <summary>
        /// F35080_s the property class combo.
        /// </summary>
        /// <returns></returns>
        public static F35080CentralAssessedOwner F35080_PropertyClassCombo()
        {
            return F35080CentralAssessedownerComp.F35080_PropertyClassCombo();
        }
        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F35080_DeleteOwnerDetails(int centralId, int userId)
        {
            F35080CentralAssessedownerComp.F35080_DeleteOwnerDetails(centralId, userId);
        }

        /// <summary>
        /// F35080_s the insert owner central details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="centralXML">The central XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35080_InsertOwnerCentralDetails(int? centralId, string centralXML, int userId)
        {
            return F35080CentralAssessedownerComp.F35080_InsertOwnerCentralDetails(centralId, centralXML, userId);
        }

        /// <summary>
        /// F35080_s the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        public static F35080CentralAssessedOwner F35080_OwnerDetails(int ownerId)
        {
            return F35080CentralAssessedownerComp.F35080_OwnerDetails(ownerId);
        } 
        #endregion

        #region F35081
        /// <summary>
        /// F35081_s the central assessed grid details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        public static F35081CentralAssessedGridData F35081_CentralAssessedGridDetails(int CentralId)
        {
            return F35081CentralAssessedGridComp.F35081_CentralAssessedGridDetails(CentralId);
        }

        /// <summary>
        /// F35081_s the central assessed rate details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="personalProperty">The personal property.</param>
        /// <param name="realProperty">The real property.</param>
        /// <returns></returns>
        public static F35081CentralAssessedGridData F35081_CentralAssessedRateDetails(int subFundId, decimal personalProperty, decimal realProperty, string description, string centralXMLList)
        {
            return F35081CentralAssessedGridComp.F35081_CentralAssessedRateDetails(subFundId, personalProperty, realProperty,description,centralXMLList);
        }

        /// <summary>
        /// F35081_s the insert owner assessed grid.
        /// </summary>
        /// <param name="centralXMLItems">The central XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F35081_InsertOwnerAssessedGrid(string centralXMLItems, int centralId, int userId)
        {
             F35081CentralAssessedGridComp.F35081_InsertOwnerAssessedGrid(centralXMLItems, centralId, userId);
        }

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="removeXMLItems">The remove XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35081_DeleteOwnerGridDetails(string removeXMLItems, int centralId, int userId)
        {
            F35081CentralAssessedGridComp.F35081_DeleteOwnerGridDetails(removeXMLItems, centralId, userId);
        } 
        #endregion

        #region F35085

        /// <summary>
        /// F35085_s the import type combo.
        /// </summary>
        /// <returns></returns>
        public static F35085CentrallyAssessedImportData F35085_ImportTypeCombo()
        {
            return F35085CentrallyAssessedImportComp.F35085_ImportTypeCombo();
        }

        /// <summary>
        /// F35085_s the central assessed owner details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F35085CentrallyAssessedImportData F35085_CentralAssessedImportDetails(int importId)
        {
            return F35085CentrallyAssessedImportComp.F35085_CentralAssessedImportDetails(importId);
        }

        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35085_DeletetemplateDetails(int importId, int userId)
        {
            F35085CentrallyAssessedImportComp.F35085_DeletetemplateDetails(importId, userId);
        }
        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static DataSet F35085_InsertCentralTemplateDetails(int? importId, string importXML, int userId)
        {
            return F35085CentrallyAssessedImportComp.F35085_InsertCentralTemplateDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F35085_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return F35085CentrallyAssessedImportComp.F35085_ExecuteImport(importId, importXML, userId, isProcess);
        }

        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F35085_ExecuteCheckForErrors(int importId, int userId)
        {
            F35085CentrallyAssessedImportComp.F35085_ExecuteCheckForErrors(importId, userId);
        }
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F35085_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return F35085CentrallyAssessedImportComp.F35085_CreateImportRecords(importId, userId, isProcess);
        }
        #endregion

        #region F16072


        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <returns></returns>
        public static F16072MiscReceiptTemplate F16072_GetMiscteplateDetails(int misctemplateId)
        {
            return F16072MiscReceiptTemplateComp.F16072_GetMiscteplateDetails(misctemplateId);
        }

        /// <summary>
        /// F16072_s the save misc receipt template.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscHeaderDetails">The misc header details.</param>
        /// <param name="accountDetails">The account details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F16072_SaveMiscReceiptTemplate(int? misctemplateId, string miscHeaderDetails, string accountDetails, int userId)
        {
            return F16072MiscReceiptTemplateComp.F16072_SaveMiscReceiptTemplate(misctemplateId, miscHeaderDetails, accountDetails, userId);
        }

        /// <summary>
        /// F16072_s the delete misctemplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="userId">The user id.</param>
        public static void F16072_DeleteMisctemplateDetails(int misctemplateId, int userId)
        {
            F16072MiscReceiptTemplateComp.F16072_DeleteMisctemplateDetails(misctemplateId, userId);
        }


        /// <summary>
        /// F16072_s the delete misc gridtemplate.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscIds">The misc ids.</param>
        /// <param name="userId">The user id.</param>
        public static void F16072_DeleteMiscGridtemplate(int misctemplateId, string miscIds, int userId)
        {
            F16072MiscReceiptTemplateComp.F16072_DeleteMiscGridtemplate(misctemplateId, miscIds, userId);
        }
        #endregion


        #region F16071
        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns></returns>
        public static F16071JournalEntryTemplateData F16071_GetJournalTeplateDetails(int templateId)
        {
            return F16071JournalEntryTemplateComp.F16071_GetJournalTeplateDetails(templateId);
        }

        /// <summary>
        /// F16071_s the save header template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F16071_SaveHeaderTemplateDetails(int? templateId, int rollYear, string description, int userId)
        {
            return F16071JournalEntryTemplateComp.F16071_SaveHeaderTemplateDetails(templateId, rollYear, description, userId);
        }

        /// <summary>
        /// F16071_s the save grid template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        public static void F16071_SaveGridTemplateDetails(int? templateId, string gridDetails, int userId)
        {
            F16071JournalEntryTemplateComp.F16071_SaveGridTemplateDetails(templateId, gridDetails, userId);
        }

        /// <summary>
        /// F16071_s the delete journal header details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user id.</param>
        public static void F16071_DeleteJournalHeaderDetails(int templateId, int userId)
        {
            F16071JournalEntryTemplateComp.F16071_DeleteJournalHeaderDetails(templateId, userId);
        }

        /// <summary>
        /// F16071_s the delete journal grid details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        public static void F16071_DeleteJournalGridDetails(int templateId, string gridDetails, int userId)
        {
            F16071JournalEntryTemplateComp.F16071_DeleteJournalGridDetails(templateId, gridDetails, userId);
        }

        #endregion

        #region F19062
        /// <summary>
        /// F14062_s the grid result details.
        /// </summary>
        /// <param name="ownerIds">The owner ids.</param>
        /// <param name="statementIds">The statement ids.</param>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="stateIds">The state ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static F14062StatementPullListData F14062_GridResultDetails(string ownerIds,string statementIds,string parcelIds,string scheduleIds,string stateIds,int userId)
        {
            return F14062StatementPullListComp.F14062_GridResultDetails(ownerIds, statementIds, parcelIds, scheduleIds, stateIds, userId);
		}

        /// <summary>
        /// F14062_s the get statement pull list details.
        /// </summary>
        /// <returns></returns>
		public static F14062StatementPullListData F14062_GetStatementPullListDetails()
        {
            return F14062StatementPullListComp.F14062_GetStatementPullListDetails();
        }

        /// <summary>
        /// F1407_s the get pull list status.
        /// </summary>
        /// <returns></returns>
	    public static F14062StatementPullListData F1407_GetPullListStatus()
        {
            return F14062StatementPullListComp.F1407_GetPullListStatus();
        }

        /// <summary>
        /// F1407_s the type of the get pull list.
        /// </summary>
        /// <returns></returns>
	    public static F14062StatementPullListData F1407_GetPullListType()
        {
            return F14062StatementPullListComp.F1407_GetPullListType();
        }

        /// <summary>
        /// F14062_s the save grid details.
        /// </summary>
        /// <param name="pullListItems">The pull list items.</param>
        /// <param name="userId">The user id.</param>
        public static void F14062_SaveGridDetails(string pullListItems, int userId)
        {
            F14062StatementPullListComp.F14062_SaveGridDetails(pullListItems, userId);
        }

       /// <summary>
       /// F14062_s the delete statement pull list.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="isProcess">if set to <c>true</c> [is process].</param>
       /// <returns></returns>
        public static string F14062_DeleteStatementPullList(string pullListItems, int userId, bool isProcess)
       {
           return F14062StatementPullListComp.F14062_DeleteStatementPullList(pullListItems, userId, isProcess);
       }
        #endregion


        #region 11024

        /// <summary>
        /// F11024_s the get multiple journal template details.
        /// </summary>
        /// <param name="jetTemplateID">The jet template ID.</param>
        /// <returns></returns>
        public static F11024MultiplejournalEntryData F11024_GetMultipleJournalTemplateDetails(int jetTemplateID)
        {
            return F11024multipleJournalEntryComp.F11024_GetMultipleJournalTemplateDetails(jetTemplateID);
        }

        /// <summary>
        /// F16072_s the save multiple journal template.
        /// </summary>
        /// <param name="transferDate">The transfer date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="journalTemplateDetails">The journal template details.</param>
        public static void F11024_SaveMultipleJournalTemplate(string transferDate, int userId, string description, string journalTemplateDetails)
        {
            F11024multipleJournalEntryComp.F11024_SaveMultipleJournalTemplate(transferDate, userId, description, journalTemplateDetails);
        }

        /// <summary>
        /// F11024_s the search template details.
        /// </summary>
        /// <returns></returns>
        public static F11024MultiplejournalEntryData F11024_SearchTemplateDetails()
        {
           return F11024multipleJournalEntryComp.F11024_SearchTemplateDetails();
        }

        #endregion


        #region F29555

        /// <summary>
        /// F29555PersonalPropertySaleData
        /// </summary>
        /// <returns></returns>
        public static F29555PersonalPropertySaleData F29555_DeedtypeComboBox()
        {
            return F29555PersonalPropertySaleComp.F29555_DeedtypeComboBox();
        }

        /// <summary>
        /// F29555_s the save transfer ownership.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static string F29555_SaveTransferOwnership(int eventId, int userId)
        {
            return F29555PersonalPropertySaleComp.F29555_SaveTransferOwnership(eventId, userId);
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
        public static F29555PersonalPropertySaleData F29555_GetPersonalSalesOwner(int? pSsaleId, int? ownerId, int? scheduleId, int userid, string scheduleString)
        {
            return F29555PersonalPropertySaleComp.F29555_GetPersonalSalesOwner(pSsaleId, ownerId, scheduleId, userid, scheduleString);
        }

        /// <summary>
        /// F29555_s the get sales scheduleand owners.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="pSsaleId">The p ssale id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static F29555PersonalPropertySaleData F29555_GetSalesScheduleandOwners(int? scheduleId, string scheduleIds, int? pSsaleId, int userid)
        {
            return F29555PersonalPropertySaleComp.F29555_GetSalesScheduleandOwners(scheduleId, scheduleIds, pSsaleId, userid);
        }

        /// <summary>
        /// F29555_s the schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public static F29555PersonalPropertySaleData F29555_ScheduleSaleTracking(int eventId, int userid)
        {
            return F29555PersonalPropertySaleComp.F29555_ScheduleSaleTracking(eventId, userid);
        }

        /// <summary>
        /// F29555_s the save sales owner.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        public static void F29555_SaveSalesOwner(int pSaleId, string ownerDetails, int userId)
        {
            F29555PersonalPropertySaleComp.F29555_SaveSalesOwner(pSaleId, ownerDetails, userId);
        }

        /// <summary>
        /// F29555_s the save sales schedule.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        public static void F29555_SaveSalesSchedule(int pSaleId, string scheduleItems, int userId)
        {
            F29555PersonalPropertySaleComp.F29555_SaveSalesSchedule(pSaleId, scheduleItems, userId);
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
        public static int F29555_SaveScheduleSaleTracking(int eventId, string pSaleItems, string scheduleItems, string ownerDetails, int userId)
        {
            return F29555PersonalPropertySaleComp.F29555_SaveScheduleSaleTracking(eventId, pSaleItems, scheduleItems, ownerDetails, userId);
        }
        #endregion

        #region F2201

        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public static F2201CentrallyAssessedSearchData F2201_GetPersonalPropertyDescription(string code)
        {
            return F2201CentrallyAssessedSearchComp.F2201_GetPersonalPropertyDescription(code);
        }

        /// <summary>
        /// F2201_s the get personal property search.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public static F2201CentrallyAssessedSearchData F2201_GetPersonalPropertySearch(string code, string description)
        {
            return F2201CentrallyAssessedSearchComp.F2201_GetPersonalPropertySearch(code, description);
        }
        #endregion

        #region F1406

        /// <summary>
        /// F1406_s the central assessed grid details.
        /// </summary>
        /// <param name="centralSearchXML">The central search XML.</param>
        /// <returns></returns>
        public static F1406CentralAssessedSearchData F1406_CentralAssessedGridDetails(string centralSearchXML)
        {
            return F1406CentralAssessedSearchComp.F1406_CentralAssessedGridDetails(centralSearchXML);
        }

        /// <summary>
        /// F1406_s the load propert class combo.
        /// </summary>
        /// <returns></returns>
        public static F1406CentralAssessedSearchData F1406_LoadPropertClassCombo()
        {
            return F1406CentralAssessedSearchComp.F1406_LoadPropertClassCombo();
        }
        #endregion

        #region F2550Configured State

        /// <summary>
        /// F2550_s the state of the get configured.
        /// </summary>
        /// <returns></returns>
        public static F2550TaxRollCorrectionData F2550_GetConfiguredState()
        {
            return F2550TaxRollCorrectionComp.F2550_GetConfiguredState();
        }
	   
        #endregion

        #region F1203

        /// <summary>
        /// F1203s the load due date management.
        /// </summary>
        /// <returns></returns>
        public static F1203DueDateManagementData F1203LoadDueDateManagement()
        {
            return F1203DueDateManagementComp.F1203LoadDueDateManagement();
        }

        /// <summary>
        /// F1203_s the save due date management.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dueDateXML">The due date XML.</param>
        public static void F1203_SaveDueDateManagement(int userId, string dueDateXML)
        {
             F1203DueDateManagementComp.F1203_SaveDueDateManagement(userId, dueDateXML);
        }
        #endregion

        #region F29636


        /// <summary>
        /// F29636_s the get BOE details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29636BOEData F29636_GetBOEDetails(int eventId)
        {
            return F29636BOEComp.F29636_GetBOEDetails(eventId);
        }

        /// <summary>
        /// F29636_s the BOE type details.
        /// </summary>
        /// <returns></returns>
        public static F29636BOEData F29636_BOETypeDetails()
        {
            return F29636BOEComp.F29636_BOETypeDetails();
        }


        /// <summary>
        /// F29636_s the save BOE details.
        /// </summary>
        /// <param name="boeElemenets">The boe elemenets.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        public static void F29636_SaveBOEDetails(string boeElemenets, string boeValues, int userId)
        {
            F29636BOEComp.F29636_SaveBOEDetails(boeElemenets, boeValues, userId);
        }

        /// <summary>
        /// F29636_s the push BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static string F29636_PushBOEDetails(int boeId, int userId)
        {
            return F29636BOEComp.F29636_PushBOEDetails(boeId, userId);
        }

        /// <summary>
        /// F29636_s the delete BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29636_DeleteBOEDetails(int boeId, int userId)
        {
            F29636BOEComp.F29636_DeleteBOEDetails(boeId, userId);
        }

        #endregion


        #region F9105
        /// <summary>
        /// F9105_s the name of the execute copy.
        /// </summary>
        /// <param name="copyData">The copy data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F9105_ExecuteCopyName(string copyData, int userId)
        {
            return F9105CopyNameAddressManagementComp.F9105_ExecuteCopyName(copyData, userId);
        }
        #endregion

        #region Permit Import Template

        #region Get
        /// <summary>
        /// Gets the Permit Import Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Permit Import Template Details</returns>
        public static F23200PermitImportTemplate GetPermitImportTemplate(int templateId)
        {
            return PermitImportTemplateComp.GetPermitImportTemplate(templateId);
        }
        #endregion

        #region List PermitImportFileType
        /// <summary>
        /// Lists the type of the permit import file.
        /// </summary>
        /// <returns>returns the dataset containing the Permit Import FileType</returns>
        public static F23200PermitImportTemplate ListPermitImportFileType()
        {
            return PermitImportTemplateComp.ListPermitImportFileType();
        }
        #endregion

        #region Save Permit Import Template


        public static int SavePermitImportTemplate(int? templateId, string permitImportXML, int userId)
        {
            return PermitImportTemplateComp.SavePermitImportTemplate(templateId, permitImportXML, userId);
        }
        #endregion

        #region Delete Permit Import Template

        /// <summary>
        /// Deletes the Permit Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeletePermitTemplate(int templateId,  int userId)
        {
            return PermitImportTemplateComp.DeletePermitTemplate(templateId, userId);
        }
        #endregion

        #endregion


        #region Income Source Details

        #region Get
        /// <summary>
        /// Gets the Income Source Details
        /// /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource ID.</param>
        /// <returns>Income Source Details</returns>
      
        public static F36090IncomeSourceData GetIncomeSourceDetail(int IncomeSourceID)
        {
            return IncomeSourceComp.GetIncomeSourceData(IncomeSourceID);
        }
        #endregion

        #region List UnitTerms
        /// <summary>
        /// Lists the type of the Unit Terms.
        /// </summary>
        /// <returns>returns the dataset containing the Unit Terms</returns>
        public static F36090IncomeSourceData ListUnitTerms()
        {
            return IncomeSourceComp.ListUnitTerms();
        }
        #endregion

        #region Save Income Source


        public static int SaveIncomeSourceDetails(int? IncomeSourceID, string IncomeSourceItems, int userId)
        {
            return IncomeSourceComp.SaveIncomeSourceDetails(IncomeSourceID, IncomeSourceItems, userId);
        }
        #endregion

        #region Delete Income Source Details

        /// <summary>
        /// Deletes the Income Source Details.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteIncomeSource(int IncomeSourceID, int userId)
        {
            return IncomeSourceComp.DeleteIncomeSource(IncomeSourceID, userId);
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
        public static F23300MADImportTemplate GetMADImportTemplate(int templateId)
        {
            return MADImportTemplateComp.GetMADImportTemplate(templateId);
        }
        #endregion

        #region List MADImportFileType

        ///// <summary>
        ///// Lists the type of the MAD import file.
        ///// </summary>
        ///// <returns>returns the dataset containing the MAD Import FileType</returns>
        public static F23300MADImportTemplate ListMADImportFileType()
        {
            return MADImportTemplateComp.ListMADImportFileType();
        }

        #endregion

        #region Save MAD Import Template

        public static int SaveMADImportTemplate(int? templateId, string madImportXML, int userId)
        {
            return MADImportTemplateComp.SaveMADImportTemplate(templateId, madImportXML, userId);
        }
        #endregion

        #region Delete MAD Import Template

        /// <summary>
        /// Deletes the MAD Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteMADTemplate(int templateId, int userId)
        {
            return MADImportTemplateComp.DeleteMADTemplate(templateId, userId);
        }
        #endregion

        #endregion

        #region Permit Import

        /// <summary>
        /// F28210_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return  F23210PermitImportComp.F28210_ExecuteImport(importId, importXML, userId, isProcess);
        }

        /// <summary>
        /// F28210_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28210_ExecuteCheckForErrors(int importId, int userId)
        {
            F23210PermitImportComp.F28210_ExecuteCheckForErrors(importId, userId);
        }

       
        /// <summary>
        /// F28210_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28210_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return F23210PermitImportComp.F28210_CreateImportRecords(importId, userId, isProcess);
        }

        /// <summary>
        /// F28210_s the delete Permit Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28210_DeletePermitImportDetails(int importId, int userId)
        {
            F23210PermitImportComp.F28210_DeletePermitImportDetails(importId, userId);
        }
        /// <summary>
        /// F28210_s the insert permit import template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28210_InsertImportPermitDetails(int? importId, string importXML, int userId)
        {
            return F23210PermitImportComp.F28210_InsertImportPermitDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F28210_s the Import permit details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F28210PermitImport F28210_PermitImportDetails(int importId)
        {
            return F23210PermitImportComp.F28210_PermitImportDetails(importId);
        }
        #endregion

        #region MAD Import

        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return F23310MADImportComp.F28310_ExecuteImport(importId, importXML, userId, isProcess);
        }

        /// <summary>
        /// F28310_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28310_ExecuteCheckForErrors(int importId, int userId)
        {
            F23310MADImportComp.F28310_ExecuteCheckForErrors(importId, userId);
        }


        /// <summary>
        /// F28310_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28310_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return F23310MADImportComp.F28310_CreateImportRecords(importId, userId, isProcess);
        }

        /// <summary>
        /// F28310_s the delete MAD Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28310_DeleteMADImportDetails(int importId, int userId)
        {
            F23310MADImportComp.F28310_DeleteMADImportDetails(importId, userId);
        }
        /// <summary>
        /// F28310_s the insert MAD import template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28310_InsertImportMADDetails(int? importId, string importXML, int userId)
        {
            return F23310MADImportComp.F28310_InsertImportMADDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F28210_s the Import MAD details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F28310MADImport F28310_MADImportDetails(int importId)
        {
            return F23310MADImportComp.F28310_MADImportDetails(importId);
        }

        ///// <summary>
        ///// Lists the type of the MAD import file.
        ///// </summary>
        ///// <returns>returns the dataset containing the MAD Import FileType</returns>
        public static F28310MADImport ListDistrictType()
        {
            return F23310MADImportComp.ListDistrictType();
        }
        #endregion

        #region Permit Import Template Selection
        /// <summary>
        /// Gets the Permit Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Permit Import Template Details.</returns>
        public static ListPermitImportTemplateData GetPermitImportTemplateDetails(string TemplateName,string Description,string FileType)
        {
            return F23210PermitImportComp.GetPermitImportTemplateDetails(TemplateName,Description,FileType);
        }

        #endregion

        #region Snapshot Import Template Selection
        /// <summary>
        /// Gets the Permit Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of Permit Import Template Details.</returns>
        public static ListSnapshotImportTemplateData GetSnapshotImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            return  SnapshotImportComp.GetSnapshotImportTemplateDetails(TemplateName, Description, FileType);
        }

        #endregion

        #region MAD Import Template Selection

        /// <summary>
        /// Gets the MAD Import Template Details 
        /// </summary>
        /// <returns> The dataset containing the list of MAD Import Template Details.</returns>
        public static ListMADimportTemplateData GetMADImportTemplateDetails(string TemplateName, string Description, string FileType)
        {
            return F23310MADImportComp.GetMADImportTemplateDetails(TemplateName, Description, FileType);
        }

        #endregion

        #region Income Sources Approach
        /// <summary>
        /// Get Income Sources
        /// </summary>
        /// <param name="valueSliceId">value slice id</param>
        /// <returns>Income Sources</returns>
        public static F36091IncomeApproachData F36091_GetIncomeSources(int valueSliceId)
        {
            return F36091IncomeApproachComp.F36091_GetIncomeSources(valueSliceId);
        }
        #endregion

        #region Manage Payment Data
        /// <summary>
        /// Get Manage Payment
        /// </summary>
        /// <param name="ReceiptID">Receipt id</param>
        /// <returns>Manage Payment</returns>
        public static F1557PayamentManagementData GetPaymentManagement(int ReceiptID)
        {
            return F1557ManagePaymentComp.GetPaymentManagement(ReceiptID);
        }

   
        /// <summary>
        /// F1557_s the insert payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">UserID</param>
        public static void F1557_InsertPayment(string receiptPayment, int userId)
        {
            F1557ManagePaymentComp.F1557_InsertPayment(receiptPayment, userId);
        }


        /// <summary>
        /// F1557_s the update payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">UserID</param>
        public static void F1557_UpdatePayment(string receiptPayment, int userId)
        {
            F1557ManagePaymentComp.F1557_UpdatePayment(receiptPayment, userId);
        }

        /// <summary>
        /// F1557_s the Delete payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">UserID</param>
        public static void F1557_DeletePaymentIds(string PaymentIDs, int userId)
        {
            F1557ManagePaymentComp.F1557_DeletePaymentIds(PaymentIDs, userId);
        }

        #endregion




        #region Income Sources Approach Item Values
        /// <summary>
        /// Get Income Approach Item Details
        /// </summary>
        /// <param name="IncomeApproachDetails">IncomeApproachDetails</param>
        /// <returns>Income Approach</returns>
        public static F36091IncomeApproachData F36091_GetIncomeApproachItemDetails(string IncomeApproachDetails)
        {
            return F36091IncomeApproachComp.F36091_GetIncomeApproachItemDetails(IncomeApproachDetails);
        }
        #endregion



        /// <summary>
        /// F36091_s the insert Income Approach details.
        /// </summary>
        /// <param name="valueSliceId">The valueSlice id.</param>
        /// <param name="SourceGridDetails">The SourceGridDetails XML.</param>
        /// <param name="IncomeApproachDetails">The IncomeApproachDetails XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F36091_SaveIncomeSourceDetails(int valueSliceId, string SourceGridDetails, string IncomeApproachDetails, int userId)
        {
             F36091IncomeApproachComp.F36091_SaveIncomeSourceDetails(valueSliceId, SourceGridDetails, IncomeApproachDetails, userId);
        }

        #region Get Source Code Details

        /// <summary>
        /// Get Source Code
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        public static F36091IncomeApproachData F36091_ListSourceDetails(int valueSliceId)
        {
            return F36091IncomeApproachComp.F36091_ListSourceDetails(valueSliceId);
        }


        /// <summary>
        /// Get Income Approach Code
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        public static F36091IncomeApproachData F36091_ListApproachValues(int incomeSourceID, decimal Units, decimal ContractPerUnit, out decimal contract, out decimal marketperunit, out decimal market)
        {
            return F36091IncomeApproachComp.F36091_ListApproachValues(incomeSourceID, Units, ContractPerUnit, out contract,out  marketperunit,out  market);
        }

        #endregion Get Approach Code Details

        #region F36091_DeleteIncomeSource

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropIds">The crop ids.</param>
        /// <param name="userId">The user id.</param>
        public static void F36091_DeleteIncomeSource(string incomesourceIds, int userId)
        {
            F36091IncomeApproachComp.F36091_DeleteIncomeSource(incomesourceIds, userId);
        }

        #endregion F36091_DeleteIncomeSource


        #region Snapshot Import Template

        #region Get
        /// <summary>
        /// Gets the Snapshot Import Template
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>Snapshot Import Template Details</returns>
        public static F23500SnapshotTemplate GetSnapshotImportTemplate(int templateId)
        {
            return SnapshotImportTemplateComp.GetSnapshotImportTemplate(templateId);
        }
        #endregion

        #region Save Snapshot Import Template

        public static int SaveSnapshotImportTemplate(int? templateId, string snapshotImportXML, int userId)
        {
            return SnapshotImportTemplateComp.SaveSnapshotImportTemplate(templateId, snapshotImportXML, userId);
        }
        #endregion

        #region Delete Snapshot Import Template

        /// <summary>
        /// Deletes the Snapshot Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string DeleteSnapshotTemplate(int templateId, int userId)
        {
            return SnapshotImportTemplateComp.DeleteSnapshotTemplate(templateId, userId);
        }
        #endregion

        #region List SnapshotImportFileType

        ///// <summary>
        ///// Lists the type of the Snapshot import file.
        ///// </summary>
        ///// <returns>returns the dataset containing the Snapshot Import FileType</returns>
        public static F23500SnapshotTemplate ListSnapshotImportFileType()
        {
            return SnapshotImportTemplateComp.ListSnapshotImportFileType();
        }

        #endregion

        #endregion Snapshot Import Template

        #region Snapshot Import

        /// <summary>
        /// F28510_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_ExecuteImport(int importId, string importXML, int userId, bool isProcess)
        {
            return SnapshotImportComp.F28510_ExecuteImport(importId, importXML, userId, isProcess);
        }

        /// <summary>
        /// F28210_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28510_ExecuteCheckForErrors(int importId, int userId)
        {
            SnapshotImportComp.F28510_ExecuteCheckForErrors(importId, userId);
        }


        /// <summary>
        /// F28210_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        public static string F28510_CreateImportRecords(int importId, int userId, bool isProcess)
        {
            return SnapshotImportComp.F28510_CreateImportRecords(importId, userId, isProcess);
        }

        /// <summary>
        /// F28210_s the delete Permit Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        public static void F28510_DeleteSnapshotImportDetails(int importId, int userId)
        {
            SnapshotImportComp.F28510_DeleteSnapshotImportDetails(importId, userId);
        }
        /// <summary>
        /// F28210_s the insert permit import template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F28510_InsertImportSnapshotDetails(int? importId, string importXML, int userId)
        {
            return SnapshotImportComp.F28510_InsertImportSnapshotDetails(importId, importXML, userId);
        }

        /// <summary>
        /// F28210_s the Import permit details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        public static F23510SnapshotImport F28510_SnapshotImportDetails(int importId)
        {
            return SnapshotImportComp.F28510_SnapshotImportDetails(importId);
        }
        #endregion

    
    }
}
