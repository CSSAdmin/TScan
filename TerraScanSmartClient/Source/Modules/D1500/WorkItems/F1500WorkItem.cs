// -------------------------------------------------------------------------------------------------
// <copyright file="F1500WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1500WorkItem class
    /// </summary>
    public class F1500WorkItem : WorkItem
    {
        #region Public Methods

        /// <summary>
        /// F1500_s the get description.
        /// </summary>
        /// <param name="keyID">The key ID.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <returns>description</returns>
        public AccountManagementData F1500_GetDescription(string keyID, string elementName)
        {
            return WSHelper.F1500_GetDescription(keyID, elementName);
        }
        #region Get SubFund Items

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

        #endregion

        #region Get Function Items

        /// <summary>
        /// F1500_s the get function items.
        /// </summary>
        /// <param name="function">The function.</param>
        /// <returns>F1500_GetFunctionItems</returns>
        public AccountManagementData F1500_GetFunctionItems(string function)
        {
            return WSHelper.F1500_GetFunctionItems(function);
        }

        #endregion

        /// <summary>
        /// F1500_s the list account details.
        /// </summary>
        /// <param name="accountID">The account ID.</param>
        /// <returns>AccountDetails</returns>
        public AccountManagementData F1500_ListAccountDetails(int accountID)
        {
            return WSHelper.F1500_ListAccountDetails(accountID);
        }

        /// <summary>
        /// F1500_s the create or edit account.
        /// </summary>
        /// <param name="accountID">The account ID.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <returns>F1500_CreateOrEditAccount</returns>
        public int F1500_CreateOrEditAccount(int accountID, string acctEmelemts,int userId)
        {
           return WSHelper.F1500_CreateOrEditAccount(accountID, acctEmelemts, userId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetCommentsCount</returns>
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
        /// <returns>GetAttachmentCount</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #region Get Configuration Value

        /// <summary>
        /// F1500_s the get configuration value.
        /// </summary>
        /// <param name="cfgName">Name of the CFG.</param>
        /// <returns>F1500_GetConfigurationValue</returns>
        public AccountManagementData F1500_GetConfigurationValue(string cfgName)
        {
            return WSHelper.F1500_GetConfigurationValue(cfgName);
        }

        #endregion

        #region GetGenericElementMgmt

        /// <summary>
        /// To Get the Generic Element Management details
        /// </summary>
        /// <param name="keyValue">The key value(Element ID)</param>
        /// <param name="description">The Description</param>
        /// <param name="formName">The Form Name</param>
        /// <returns>Typed Dataset containing the Element ID and Description Value</returns>
        public F1503GenericManagementData F1500_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            return WSHelper.F1503_GetGenericElementMgmt(keyValue, description, formName);
        }

        #endregion GetGenericElementMgmt

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
