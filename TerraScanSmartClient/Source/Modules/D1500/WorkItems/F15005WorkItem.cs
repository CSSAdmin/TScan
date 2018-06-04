//--------------------------------------------------------------------------------------------
// <copyright file="F15005WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15005 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18-12-2006       Shiva              Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F15005 FormSlice - SubFundMgmt WorkItem
    /// </summary>
    public class F15005WorkItem : WorkItem
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
        /// F15005_s the check sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>error id </returns>
        public int F15005_CheckSubFund(int? subFundId, string subFund, int rollYear)
        {
            return WSHelper.F15005_CheckSubFund(subFundId, subFund, rollYear);
        }

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <returns>the subFund Id</returns>
        public int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments, int userId)
        {
            return WSHelper.F9503_CreateOrEditSubFund(subFundId, subFundElments, userId);
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
        /// <returns>Datateble contains the Attachment and CommentCount</returns>
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

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormDetails dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

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


        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesF2200_InsertEditScheduleOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }
    }
}
