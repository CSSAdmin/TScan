
namespace TerraScan.ServiceHelper
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessEntities;
    using System.Data;
    using System.Collections;
    using System.Diagnostics;
    using System.Configuration;
    using System.Xml;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// wrapper class for webservice
    /// </summary>
    public static class WCFHelper
    {
        /// <summary>
        /// WebService Instance
        /// </summary>
        private static SmartClientServiceClient smartClientService;
        
        static WCFHelper()
        {
            smartClientService = new SmartClientServiceClient();
        }

        #region  Excise Tax Affidavit

        #region  For Loading The Details

        /// <summary>
        /// Gets the type of the excise individual.
        /// </summary>
        /// <returns>returns ExciseIndividualType </returns>
        public static ExciseIndividualType GetExciseIndividualType()
        {
            string exciseTaxAffidavitValue;
            ExciseIndividualType exciseIndividualType = new ExciseIndividualType();
            exciseTaxAffidavitValue = smartClientService.GetExciseIndividualType();
            exciseIndividualType.ReadXml(Utilities.SharedFunctions.XmlParser(exciseTaxAffidavitValue));
            return exciseIndividualType;
        }

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static ExciseTaxAffidavitData GetExciseTaxAffidavitDetails(int statmentId)
        {
            string exciseTaxAffidavitDataValue;
            ExciseTaxAffidavitData exciseTaxAffidavitData = new ExciseTaxAffidavitData();
            exciseTaxAffidavitDataValue = smartClientService.GetExciseTaxAffidavitDetails(statmentId);
            exciseTaxAffidavitData.ReadXml(Utilities.SharedFunctions.XmlParser(exciseTaxAffidavitDataValue));
            return exciseTaxAffidavitData;
        }

        /// <summary>
        /// Excises the tax affidavit calulate amount due.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateID">The excise rate ID.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>returns dataset</returns>
        public static ExciseTaxAffidavitAmountDueData GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateID, int taxCode, double taxableSaleAmount)
        {
            string exciseAffidavitAmountDueDataValue;
            ExciseTaxAffidavitAmountDueData exciseTaxAffidavitAmountDueData = new ExciseTaxAffidavitAmountDueData();
            exciseAffidavitAmountDueDataValue = smartClientService.GetExciseTaxAffidavitCalulateAmountDue(saleDate, paymentDate, exciseRateID, taxCode, taxableSaleAmount);
            exciseTaxAffidavitAmountDueData.ReadXml(Utilities.SharedFunctions.XmlParser(exciseAffidavitAmountDueDataValue));
            return exciseTaxAffidavitAmountDueData;
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public static ExciseTaxAffidavitData GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            string affidavitStatementIdDataValue;
            ExciseTaxAffidavitData affidavitStatementIdData = new ExciseTaxAffidavitData();
            affidavitStatementIdDataValue = smartClientService.GetAffidavitStatementId(formId, orderField, orderBy);
            affidavitStatementIdData.ReadXml(Utilities.SharedFunctions.XmlParser(affidavitStatementIdDataValue));
            return affidavitStatementIdData;
        }

        /// <summary>
        /// Executes the affdvt query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>Returns ExecuteAffdvtQuery Dataset</returns>
        public static QueryByFormData ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn)
        {
            string queryByFormValue;
            QueryByFormData queryByFormData = new QueryByFormData();
            queryByFormValue = smartClientService.ExecuteAffdvtQuery(formId, whereCondnSql, orderByCondn);
            queryByFormData.ReadXml(Utilities.SharedFunctions.XmlParser(queryByFormValue));
            return queryByFormData;
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <returns>returns dataset containing AffiDavit Details</returns>
        public static int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails)
        {
            return smartClientService.SaveAffiDavitDetails(statementId, partiesAddress, parcelDetails, exciseAffidavitDetails);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public static PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            string ownerDetailDataValue;
            PartiesOwnerDetailsData ownerDetailData = new PartiesOwnerDetailsData();
            ownerDetailDataValue = smartClientService.GetOwnerDetails(ownerId);
            ownerDetailData.ReadXml(Utilities.SharedFunctions.XmlParser(ownerDetailDataValue));
            return ownerDetailData;
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public static AffidavitDistrictSelectionData GetDistrictSelection(int exciseRateId)
        {
            string districtSelectionValue;
            AffidavitDistrictSelectionData districtSelectionDataSet = new AffidavitDistrictSelectionData();
            districtSelectionValue = smartClientService.GetDistrictSelection(exciseRateId);
            districtSelectionDataSet.ReadXml(Utilities.SharedFunctions.XmlParser(districtSelectionValue));
            return districtSelectionDataSet;
        }

        /// <summary>
        /// Deletes the affidavit details for the particular statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        public static void DeleteAffidavitDetails(int statementId)
        {
            smartClientService.DeleteAffidavitDetails(statementId);
        }

        #endregion

        #endregion
    }
}

