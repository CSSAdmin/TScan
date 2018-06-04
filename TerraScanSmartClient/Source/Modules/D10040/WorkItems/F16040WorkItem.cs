

namespace D10040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F16040WorkItem:WorkItem
    {
        #region Improvement District Definition.

        /// <summary>
        /// Lists the interest method.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F16040ImprovementDistrictDefinition ListInterestMethod()
        {
            return WSHelper.ListInterestMethod();
        }

        /// <summary>
        /// Lists the Interest Delq method.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F16040ImprovementDistrictDefinition ListInterestDelqDetails()
        {
            return WSHelper.ListInterestDelqDetails();
        }

        /// <summary>
        /// Get District Details.
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        public F16040ImprovementDistrictDefinition GetDistrictDetails(int districtId)
        {
            return WSHelper.GetDistrictDetails(districtId);
        }

        /// <summary>
        /// Get District Distribution Type Details.
        /// </summary>
        /// <returns></returns>
        public F16040ImprovementDistrictDefinition GetDistributionDetails()
        {
            return WSHelper.GetDistributionDetails();
        }

        /// <summary>
        /// Get District Definition Details.
        /// </summary>
        /// <param name="saDistrictId"></param>
        /// <returns></returns>
        public F16040ImprovementDistrictDefinition GetDistrictDefinitionDetails(int saDistrictId)
        {
            return WSHelper.GetDistrictDefinitionDetails(saDistrictId);
        }

        /// <summary>
        /// Get Executed Rollover Improvement District.
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public F16040ImprovementDistrictDefinition RollOver_ImprovementDistrict(int districtId, int userId)
        {
            return WSHelper.RollOver_ImprovementDistrict(districtId, userId);
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
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Lists the improve District type.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F16040ImprovementDistrictDefinition ImprovementDistrictTypelist(string districtIdType)
        {
            return WSHelper.ImprovementDistrictTypelist(districtIdType);
        }

        /// <summary>
        /// Save improvement district definition.
        /// </summary>
        public string F16040_SaveImproveDistrictDefinition(string districtItem,string distributionItem, int userId)
        {
           return WSHelper.F16040_SaveImproveDistrictDefinition(districtItem,distributionItem, userId);
        }

        /// <summary>
        /// Update Improvement District Details.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="districtItem"></param>
        /// <param name="distributionItem"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string F16040_UpdateImproveDistrictDefinition(int districtNo, string districtItem, string distributionItem, int userId)
        {
            return WSHelper.F16040_UpdateImproveDistrictDefinition(districtNo, districtItem, distributionItem, userId);
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

        #endregion

    }
}
