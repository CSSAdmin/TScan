//--------------------------------------------------------------------------------------------
// <copyright file="F1105WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 July 06		GUHAN S	           Created
//*********************************************************************************/


namespace D1100
{
    #region namespace
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
    using TerraScan.ServiceHelper;
    using TerraScan.BusinessEntities;
    #endregion

    /// <summary>
    /// F1105WorkItem
    /// </summary>
    public class F1105WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the type of the excise individual.
        /// </summary>
        /// <returns>Details Of All Six Header</returns>
        public ExciseIndividualType GetExciseIndividualType
        {
            get { return WSHelper.GetExciseIndividualType(); }
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
        /// Loads the excise tax affidavit1.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Details Of All Six Header</returns>
        public ExciseTaxAffidavitData LoadExciseTaxAffidavit(int statementId)
        {
            return WSHelper.GetExciseTaxAffidavitDetails(statementId);
        }

        /// <summary>
        /// Gets the excise tax affidavit calulate amount due.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateid">The excise rateid.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>Calulate value as dataset</returns>
        public ExciseTaxAffidavitAmountDueData GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateid, int taxCode, double taxableSaleAmount) 
        {
            return WSHelper.GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateid, taxCode, taxableSaleAmount); 
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public ExciseTaxAffidavitData.ListAffidavitStatementIdDataTable GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            return WSHelper.GetAffidavitStatementId(formId, orderField, orderBy).ListAffidavitStatementId;
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// Executes the affdvt query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>Returns ExecuteAffdvtQuery Dataset</returns>
        public QueryByFormData ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn)
        {
            return WSHelper.ExecuteAffdvtQuery(formId, whereCondnSql, orderByCondn);
        }

        /// <summary>
        /// Saves the affi davit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, int userID)
        {
            return WSHelper.SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails, userID);
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns GetDistrictSelection Dataset</returns>
        public AffidavitDistrictSelectionData GetDistrictSelection(int exciseRateId)
        {
            return WSHelper.GetDistrictSelection(exciseRateId);
        }

        /// <summary>
        /// Deletes the affidavit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userID">The user ID.</param>
        public void DeleteAffidavitDetails(int statementId, int userID)
        {
            WSHelper.DeleteAffidavitDetails(statementId, userID);
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
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="M:Microsoft.Practices.CompositeUI.WorkItem.Run"/>
        /// method is called on the <see cref="T:Microsoft.Practices.CompositeUI.WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
