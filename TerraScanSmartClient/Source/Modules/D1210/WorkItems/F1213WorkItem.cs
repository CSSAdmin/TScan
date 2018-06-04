//--------------------------------------------------------------------------------------------
// <copyright file="F1213WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1213 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1213 WorkItem
    /// </summary>
    public class F1213WorkItem : WorkItem
    {
        #region Public Methods

        #region List DepositHistroy Details

        /// <summary>
        /// Lists the deposit history details.
        /// </summary>
        /// <returns>the DataSet Which Holds the DepositHistoryDetails</returns>
        public DepositHistoryData ListDepositHistoryDetails()
        {
            return WSHelper.ListDepositHistoryDetails();
        }

        #endregion

        #region Get DepositHistory Serach Results

        /// <summary>
        /// Gets the deposit history search result.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>DepositHistoryDataSet contains the resulted Search</returns>
        public DepositHistoryData GetDepositHistorySearchResult(int form, string whereCondnSql)
        {
            return WSHelper.GetDepositHistorySearchResult(form, whereCondnSql);
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
            WSHelper.UpdateDepositHistory(clid, userId);
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataset containing the AccountNames.</returns>
        public DepositHistoryData.ListAccountNameDataTable ListAccountNames()
        {
            return WSHelper.ListAccountNames();
        }

        #endregion

        #region AttachMent Comment Count

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Attchment Count</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Comments Count DataTable</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion

        #endregion

        #region Protected Methods

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

        #endregion
    }
}
