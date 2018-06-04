// -------------------------------------------------------------------------------------------------
// <copyright file="F1226WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/10/2006       Ranjani            Created// 
//*********************************************************************************/
namespace D1210
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
    /// F1226 WorkItem
    /// </summary>
    public class F1226WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the Cash Ledger ID
        /// </summary>
        /// <returns>Cash Ledger ID DataTable</returns>
        public CheckDetailData.ListCashLedgerIDDataTable F1226_ListCashLedger()
        {
            return WSHelper.F1226_ListCashLedger().ListCashLedgerID;
        }

        /// <summary>
        /// Gets the Cash Ledger(check) Detail
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <returns>CheckDetailData with Cash Ledger Detail</returns>
        public CheckDetailData F1226_GetCashLedger(int clid)
        {
            return WSHelper.F1226_GetCashLedger(clid);
        }    

        /// <summary>
        /// Updates the Cash Ledger Status
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="functionDate">The function date.</param>
        /// <param name="functionId">The function id.</param>
        public void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId)
        {
            WSHelper.F1226_UpdateCashLedgerStatus(clid, userId, functionDate, functionId, loginUserId);
        }      

        /// <summary>
        /// Delete the Cash Ledger
        /// </summary>
        /// <param name="clid">The clid.</param>
        public void F1226_DeleteCashLedger(int clid, int userId)
        {
            WSHelper.F1226_DeleteCashLedger(clid, userId);
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
