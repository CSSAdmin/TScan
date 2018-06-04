// -------------------------------------------------------------------------------------------------
// <copyright file="F1020WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------using System;
namespace D1020
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
    /// F1020 WorkItem
    /// </summary>
    public class F1020WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the real estate statement Id's
        /// </summary>
        /// <param name="sortField"> The orderbycolumn name.</param>
        /// <param name="orderBy"> The orderby direction.</param>
        /// <returns> The Typed dataset containing the list of real estate statementids.</returns>
        public RealEstateData GetRealEstateStatementIds(string sortField, string orderBy)
        {
            return WSHelper.GetRealEstateStatementIds(sortField, orderBy);
        }

        /// <summary>
        /// Gets the real estate statement based on the statement id
        /// </summary>
        /// <param name="statementId"> The statement id of the statement to be fetched.</param>
        /// <returns> The Typed dataset containing the statement information of the statementid.</returns>
        public RealEstateData GetRealEstateStatement(int statementId)
        {
            return WSHelper.GetRealEstateStatement(statementId);         
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
        /// Method to exceute a sql query
        /// </summary>
        /// <param name="whereCondition">The whereCondition to be applied on query.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns> The datatset having the exceuted query results.</returns>
        public QueryData ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            return WSHelper.ExecuteQuery(whereCondition, orderByCondition, formId);
        }

        /// <summary>
        /// Method to apply filter on snapshot
        /// </summary>
        /// <param name="snapshotId"> The id used to retrieve snapshotitems to which filter applied</param>
        /// <param name="whereCondition">wherecondition used to query snapshotitems</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>Dataset</returns>
        public QueryData ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            return WSHelper.ExecuteSnapshot(snapshotId, whereCondition, orderByCondition, formId);
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
