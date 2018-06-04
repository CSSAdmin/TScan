// -------------------------------------------------------------------------------------------------
// <copyright file="F1100WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------
namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1100 WorkItem
    /// </summary>
    public class F1100WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the excise tax statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>ExciseTaxStatementData entity variable</returns>
        public ExciseTaxStatementData GetExciseTaxStatement(int statementId)
        {
            return WSHelper.GetExciseTaxStatement(statementId);
        }        

        /// <summary>
        /// Lists the excise tax statement.
        /// </summary>
        /// <returns>ExciseTaxStatementData.ListExciseTaxStatementIDDataTable details</returns>
        public ExciseTaxStatementData.ListExciseTaxStatementIDDataTable ListExciseTaxStatement()
        {
            return WSHelper.ListExciseTaxStatemnet().ListExciseTaxStatementID;
        }

        /// <summary>
        /// Saves the excise tax receipt.
        /// </summary>
        /// <param name="statementItems">The Statement Items.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>The typed dataset</returns>
        public ExciseTaxStatementData SaveExciseTaxReceipt(string statementItems, int userID)
        {
            return WSHelper.SaveExciseTaxReceipt(statementItems, userID);
        }

        /// <summary>
        /// Executes the Query.
        /// </summary>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>the dataset</returns>
        public QueryData ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            return WSHelper.ExecuteQuery(whereCondition, orderByCondition, formId);
        }

        /// <summary>
        /// Executes the snapshot.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>the dataset</returns>
        public QueryData ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            return WSHelper.ExecuteSnapshot(snapshotId, whereCondition, orderByCondition, formId);
        }

        /// <summary>
        /// Fetches the query result for the query id passed.
        /// </summary>
        /// <param name="queryId"> The query ID of the query to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The dataset containing the query results.</returns>
        public QueryData GetQueryResult(int queryId, string orderByCondn)
        {
            return WSHelper.GetQueryResult(queryId, orderByCondn);
        }

        /// <summary>
        /// Get the result of the snapshot.
        /// </summary>
        /// <param name="snapShotId"> The snapshot ID of the snapshot to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The datset containing the snapshot results.</returns>
        public QueryData GetSnapShotResult(int snapShotId, string orderByCondn)
        {
            return WSHelper.GetSnapShotResult(snapShotId, orderByCondn);
        }

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [is auto print].</param>
        public void SaveAutoPrint(int formId, int userId, bool autoPrint)
        {
            WSHelper.SaveAutoPrint(formId, userId, autoPrint);
        }

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns true/false</returns>
        public int GetAutoPrintStatus(int formId, int userId)
        {         
            return WSHelper.GetAutoPrintStatus(formId, userId);
        }

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        /// <returns> The string containing the recipiet's validity information.</returns>
        public string GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            return (string)WSHelper.GetValidReceiptTest(statementId, receiptDate);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        } 
    }
}
