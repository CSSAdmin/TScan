//--------------------------------------------------------------------------------------------
// <copyright file="F9503WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F9503 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17-11-2006       Shiva              Created
//*********************************************************************************/

namespace D9500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F9503 WorkItem
    /// </summary>
    public class F9503WorkItem : WorkItem
    {
        #region Public Methods

        #region List SubFund Details

        /// <summary>
        /// F9503_s the get sub fund management details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <returns>DataSet F9503SubFungMgmtData </returns>
        public F9503SubFundManagementData F9503_GetSubFundManagementDetails(int? subFundId)
        {
            return WSHelper.F9503_GetSubFundManagementDetails(subFundId);
        }

        #endregion

        #region Save and Edit SubFund Management Data

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <returns>primary keyId or errorMessage</returns>
        public int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments,int userID)
        {
            return WSHelper.F9503_CreateOrEditSubFund(subFundId, subFundElments,userID);
        }

        #endregion

        #region AttachMent/CommentCount

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the Count</returns>
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
        /// <returns>DataTable Contains the attachment and Comment Count</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion

        #region GetDefault Year

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
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
