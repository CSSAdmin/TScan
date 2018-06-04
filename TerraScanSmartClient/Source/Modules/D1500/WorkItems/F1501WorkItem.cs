// -------------------------------------------------------------------------------------------------
// <copyright file="F1501WorkItem.cs" company="Congruent">
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
    /// F1501WorkItem Class File
    /// </summary>
    public class F1501WorkItem : WorkItem
    {
        #region Public Methods

        #region Get Description

        public AccountManagementData F1501_GetDescription(string keyID, string elementName)
        {
            return WSHelper.F1500_GetDescription(keyID, elementName);
        }

        #endregion

        #region Get SubFund Items

        public F9503SubFundManagementData F9503_GetSubFundItems(string subFund, short rollYear)
        {
            return WSHelper.F9503_GetSubFundItems(subFund, rollYear);
        }

        #endregion

        #region Get Function Items

        public AccountManagementData F1501_GetFunctionItems(string function)
        {
            return WSHelper.F1500_GetFunctionItems(function);
        }

        #endregion

        #region List RollYears

        public F1501GLConfigurationData F1501_ListRollYear()
        {
            return WSHelper.F1501_ListRollYear();
        }

        #endregion

        #region List GL config Details

        public F1501GLConfigurationData F1501_ListGLConfigDetails(int rollYear)
        {
            return WSHelper.F1501_ListGLConfigDetails(rollYear);
        }

        #endregion

        #region Get GL config Details

        public F1501GLConfigurationData F1501_GetGLConfigDetails(int gLConfigID)
        {
            return WSHelper.F1501_GetGLConfigDetails(gLConfigID);
        }

        #endregion

        #region Save and Edit the GL config Details

        public int F1501_CreateOrEditGLConfigDetails(int gLConfigID, string gLConfigElements, int userId)
        {
            return WSHelper.F1501_CreateOrEditGLConfigDetails(gLConfigID, gLConfigElements, userId);
        }

        #endregion

        #region Attachments and Comments

        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

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

        #region Get Configuration Value

        public AccountManagementData F1501_GetConfigurationValue(string cfgName)
        {
            return WSHelper.F1500_GetConfigurationValue(cfgName);
        }

        #endregion

        #endregion

        #region GetGenericElementMgmt

        /// <summary>
        /// To Get the Generic Element Management details
        /// </summary>
        /// <param name="keyValue">The key value(Element ID)</param>
        /// <param name="description">The Description</param>
        /// <param name="formName">The Form Name</param>
        /// <returns>Typed Dataset containing the Element ID and Description Value</returns>
        public F1503GenericManagementData F1501_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            return WSHelper.F1503_GetGenericElementMgmt(keyValue, description, formName);
        }

        #endregion GetGenericElementMgmt

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
