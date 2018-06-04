// -------------------------------------------------------------------------------------------------
// <copyright file="F16030WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D10030
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
    public class F16030WorkItem : WorkItem
    {
        #region ListMethods

        /// <summary>
        /// F1030_s the type of the list district definition.
        /// </summary>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
        public F1030SpecialDistrictDefinitionData F16030_ListDistrictDefinitionType()
        {
           return WSHelper.F16030_ListDistrictDefinitionType();
        }

        #endregion ListMethods

        #region GetMethods

       /// <summary>
       /// F16030_s the get district definition details.
       /// </summary>
       /// <param name="districtNo">The district no.</param>
        /// <returns>F1030SpecialDistrictDefinitionData</returns>
       public F1030SpecialDistrictDefinitionData F16030_GetDistrictDefinitionDetails(int districtNo)
       {
           return WSHelper.F16030_GetDistrictDefinitionDetails(districtNo);
       }

       #endregion GetMethods

        #region DeleteMethods

       /// <summary>
       /// F16030_s the delete district definition.
       /// </summary>
       /// <param name="specialDistrictId">The special district ID.</param>
       /// <param name="userId">The user id.</param>
       /// <returns>return value</returns>
        public int F16030_DeleteDistrictDefinition(int specialDistrictId, int userId)
        {
            return WSHelper.F1030_DeleteDistrictDefinition(specialDistrictId, userId);
        }

        /// <summary>
        /// F16030_s the delete district definition rate.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item id.</param>
        /// <param name="userID">The user Id.</param>
        public void F16030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId)
        {
            WSHelper.F16030_DeleteDistrictDefinitionRate(specialDistrictRateItemId, userId);
        }

       #endregion DeleteMethods

        #region SaveMethods

        /// <summary>
        /// F16030_s the save district definition.
        /// </summary>
        /// <param name="districtNo">The district no.</param>
        /// <param name="districtItem">The district item.</param>
        /// <param name="rateItem">The rate item.</param>
        /// <param name="distributionItem">The distribution item.</param>
        /// <param name="flagOverride">if set to <c>true</c> [flag override].</param>
        /// <param name="flagValidation">if set to <c>true</c> [flag validation].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>KeyID</returns>
        public string F16030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, bool flagValidation, int userId)
        {
            return WSHelper.F16030_SaveDistrictDefinition(districtNo, districtItem, rateItem, distributionItem, flagOverride, flagValidation, userId);
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

        #region To Get Configuration Roll Year

       /// <summary>
       /// Gets the config Roll Year.
       /// </summary>
       /// <param name="configName">Name of the config.</param>
       /// <returns>GetConfigDetails</returns>
       public CommentsData GetConfigDetails(string configName)
       {
           return WSHelper.GetConfigDetails(configName);
       }

       #endregion To Get Configuration Roll Year

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
   }
}
