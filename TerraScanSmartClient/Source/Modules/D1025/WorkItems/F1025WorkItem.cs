// -------------------------------------------------------------------------------------------------
// <copyright file="F1025WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D1025
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1025WorkItem Class File
    /// </summary>
    public class F1025WorkItem : WorkItem
    {
        #region List RollYears

        /// <summary>
        /// F1025_s the list roll year.
        /// </summary>
        /// <returns></returns>
        public F1025AutoFundTransferData F1025_ListRollYear()
        {
            return WSHelper.F1025_ListRollYear();
        }
        
        #endregion

        #region List AutoFundAccountTransfer Details

        /// <summary>
        /// F1025_s the list auto fund transfer details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F1025AutoFundTransferData F1025_ListAutoFundTransferDetails(int rollYear)
        {
            return WSHelper.F1025_ListAutoFundTransferDetails(rollYear);
        }
        
        #endregion List AutoFundAccountTransfer Details

        #region Delete AutoFundAccountTransfer Details

        /// <summary>
        /// F1025_s the delete auto fund transfer details.
        /// </summary>
        /// <param name="autoTransferID">The auto transfer ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns></returns>
        public int F1025_DeleteAutoFundTransferDetails(int autoTransferID,int userID)
        {
            return WSHelper.F1025_DeleteAutoFundTransferDetails(autoTransferID, userID);
        }

        #endregion Delete AutoFundAccountTransfer Details

        #region Update AutoFundAccountTransfer

        /// <summary>
        /// F1025_s the update auto fund transfer details.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F1025_UpdateAutoFundTransferDetails(string autoFundItems,int userId)
        {
            return WSHelper.F1025_UpdateAutoFundTransferDetails(autoFundItems, userId);
        }
        #endregion Update AutoFundAccountTransfer

        #region Check RollYear
        /// <summary>
        /// F1025_s the check roll year.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <returns></returns>
        public int F1025_CheckRollYear(string autoFundItems)
        {
            return WSHelper.F1025_CheckRollYear(autoFundItems);
        }
        #endregion Check RollYear

        #region Attachment and comments

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }
        #endregion Attachment and comments

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Gets the Account Name
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>returns dataset contains Account Name</returns>
        public ExciseTaxRateData GetAccountName(int accountId)
        {
            return WSHelper.GetAccountName(accountId);
        }

        /// <summary>
        /// F9503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>F9503_GetSubFundItems</returns>
        public F9503SubFundManagementData F9503_GetSubFundItems(string subFund, short rollYear)
        {
            return WSHelper.F9503_GetSubFundItems(subFund, rollYear);
        }


        #region F1515_GetSubFundSelection

        /// <summary>
        /// To Get the Sub Fund Selection Details
        /// </summary>
        /// <param name="subFund">The Sub fund</param>
        /// <param name="description">The Description</param>
        /// <param name="rollYear">The Roll year</param>
        /// <returns>Typed Dataset containing the Sub Fund Selection Details</returns>
        public F1515SubFundSelectionData F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            return WSHelper.F1515_GetSubFundSelection(subFund, description, rollYear, iscash);
        }

        #endregion F1515_GetSubFundSelection


    }
}
