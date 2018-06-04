// -------------------------------------------------------------------------------------------------
// <copyright file="F1100WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D1030
{
    #region NameSpace

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

    #endregion NameSpace

    /// <summary>
    /// F1030 WorkItem
    /// </summary>
    public class F1030WorkItem : WorkItem
    {
        #region WorkItemEvents

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

        #endregion WorkItemEvents

        #region Attachment and Comments

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

        #endregion Attachment and Comments

        #region ListMethods

        /// <summary>
        /// F1030_s the type of the list district definition.
        /// </summary>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public F1030SpecialDistrictDefinitionData F1030_ListDistrictDefinitionType()
        {
           return WSHelper.F1030_ListDistrictDefinitionType();

       }

        #endregion ListMethods

        #region GetMethods

       /// <summary>
        /// F1030_s the get district definition details.
        /// </summary>
        /// <param name="DistrictNo">The district no.</param>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
       public F1030SpecialDistrictDefinitionData F1030_GetDistrictDefinitionDetails(int districtNo)
        {
           return WSHelper.F1030_GetDistrictDefinitionDetails(districtNo);
       }

       #endregion GetMethods

        #region DeleteMethods

       /// <summary>
       /// F1030 Delete DistrictDefinition
       /// </summary>
       /// <param name="specialDistrictID">The special district ID.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public int F1030_DeleteDistrictDefinition(int specialDistrictID,int userId)
       {
           return WSHelper.F1030_DeleteDistrictDefinition(specialDistrictID, userId);
       }

       /// <summary>
       /// F1030 Delete DistrictDefinitionRate
       /// </summary>
       /// <param name="specialDistrictRateItemId">The special district rate item id.</param>
       /// <param name="userId">The user id.</param>
       public void F1030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId,int userId)
       {
           WSHelper.F1030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
       }

       #endregion DeleteMethods

        #region SaveMethods

       /// <summary>
       /// F1030_s the save district definition.
       /// </summary>
       /// <param name="districtNo">The district no.</param>
       /// <param name="districtItem">The district item.</param>
       /// <param name="rateItem">The rate item.</param>
       /// <param name="distributionItem">The distribution item.</param>
       /// <param name="flagOverride">if set to <c>true</c> [flag override].</param>
       /// <param name="userId">The user id.</param>
       /// <returns>KeyID</returns>
        public string F1030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, int userId)
        {
            return WSHelper.F1030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, userId);
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
       #endregion SaveMethods
   }
}
