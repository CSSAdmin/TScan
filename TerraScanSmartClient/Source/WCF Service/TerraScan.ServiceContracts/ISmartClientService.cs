// -------------------------------------------------------------------------------------------
// <copyright file="ISmartClientService.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update CountyConfiguration</summary>
// Release history
// **********************************************************************************
// Date             Author              Description
// ----------      ---------       ---------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------
namespace TerraScan.ServiceContracts
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ServiceModel;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// ISmartClientService
    /// </summary>
    /// ///[ServiceContractAttribute(SessionMode = SessionMode.Required ,Namespace = "http://TerraScan.ServiceContracts", Name = "ISmartClientService")]
    [ServiceContractAttribute(Namespace = "http://TerraScan.ServiceContracts", Name = "ISmartClientService")]
    public interface ISmartClientService
    {
        /// <summary>
        /// Checks the installation.
        /// </summary>
        /// <param name="test">The test.</param>
        /// <returns>string</returns>
        [OperationContract]
        string CheckInstallation(string test);

        /// <summary>
        /// Check Mortage Import Report
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="receiptDate">Receipt ID</param>
        /// <returns>String</returns>
        [OperationContract]
        string CheckMortgageImportValidReceipt(int importId, DateTime receiptDate);

        /// <summary>
        /// Check Next Number
        /// </summary>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="nextNum">Next Number</param>
        /// <param name="formula">Formula</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        System.Data.DataSet CheckNextNumber(int rollYear, int nextNum, string formula);

        /// <summary>
        /// Check whether the Query already Existed 
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="savedQueryName">Query Name</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int CheckQueryExist(int formId, string savedQueryName);

        /// <summary>
        /// Check whether the SnapShot already Existed 
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="savedSnapShotName">SnapShot Name</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int CheckSnapShotExist(int formId, string savedSnapShotName);

        /// <summary>
        /// Clear Temporary Records
        /// </summary>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void ClearTemporaryRecords(int userId);

        /// <summary>
        /// Compile Posting Recordser
        /// </summary>
        /// <param name="postDate">Post Date</param>
        /// <param name="selectedPostTypes">Post Types</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string CompilePostingRecordSet(DateTime postDate, string selectedPostTypes, int userId);

        #region Income Source Approach F36091

        /// <summary>
        /// Get Income Sources Approach
        /// </summary>
        /// <param name="ValueSliceId">ValueSliceId </param>
        /// <returns>string</returns>
        [OperationContract]
        string F36091_GetIncomeSources(int valueSliceId);

        #region Save Source Approach

        /// <summary>
        /// Save Income Approach
        /// </summary>
        /// <param name="districtNo">ValueSliceId Number</param>
        /// <returns>string</returns>
        [OperationContract]
        void F36091_SaveIncomeSourceDetails(int valueSliceId, string SourceGridDetails, string IncomeApproachDetails, int userId);
        #endregion

        #region Get Source Details
        /// <summary>
        /// Get Source Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        [OperationContract]
        string F36091_ListSourceDetails(int valueSliceId);

        #endregion Get Source Details

        #region Get Income Approach Item Details
        /// <summary>
        /// Get Income Approach Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        [OperationContract]
        string F36091_GetIncomeApproachItemDetails(string IncomeApproachDetails);

        #endregion Get Income Approach Item Details



        #region Get Approach Details

        /// <summary>
        /// Get Approach Details
        /// </summary>
        /// <param name="incomeSourceID">incomeSourceID</param>
        /// <param name="Units">Units</param>
        ///  <param name="ContractPerUnit">ContractPerUnit</param>
        /// <returns>String</returns>
        [OperationContract]
        string F36091_ListApproachValues(int incomeSourceID, decimal Units, decimal ContractPerUnit,out decimal contract, out decimal marketperunit, out decimal market);

        #endregion Get Approach Details

       #region F36091_DeleteIncomeSource

        /// <summary>
        /// F36091_s the delete income source.
        /// </summary>
        /// <param name="incomesourceId">The income source id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F36091_DeleteIncomeSource(string incomesourceIds, int userId);

        #endregion F36091_DeleteIncomeSource


        #endregion

        #region Get Manage Payment F1557

        /// <summary>
        /// Get Manage Payment
        /// </summary>
        /// <param name="ReceiptID">ReceiptID </param>
        /// <returns>string</returns>
        [OperationContract]
        string GetPaymentManagement(int ReceiptID);

        /// <summary>
        /// Insert Payment
        /// </summary>
        /// <param name="receiptPayment">Receipt Payment</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1557_InsertPayment(string receiptPayment, int userId);

        /// <summary>
        /// Update Payment
        /// </summary>
        /// <param name="receiptPayment">Receipt Payment</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1557_UpdatePayment(string receiptPayment, int userId);

       /// <summary>
        /// F1557 the delete Payment Management.
        /// </summary>
        /// <param name="PaymentIDs">Payment Management.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F1557_DeletePayment(string PaymentIDs, int userId);

       
        #endregion


        /// <summary>
        /// Create Receipte
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Template Name</param>
        /// <param name="filePath">File Path</param>
        /// <param name="typeId">Type Id</param>
        /// <param name="receiptDate">ReceiptDate</param>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="payCode">Pay code</param>
        /// <param name="userId">User ID</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="resetErrorCheck">Reset value</param>
        /// <returns>String</returns>
        [OperationContract]
        string CreateReceipt(int importId, int templateId, string templateName, string filePath, int typeId, DateTime receiptDate, DateTime interestDate, bool payCode,int firstHalfPaycode, int userId, int rollYear, int? ppaymentId, bool resetErrorCheck);

        /// <summary>
        /// Delete Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void DeleteAffidavitDetails(int statementId, int userId);

        /// <summary>
        /// Delete Attachment
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void DeleteAttachments(int fileId, int userId);

        /// <summary>
        /// Delete Comments
        /// </summary>
        /// <param name="keyId">Key ID</param>
        /// <param name="formId">Form ID</param>
        /// <param name="commentId">Comment ID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void DeleteComments(int keyId, int formId, int commentId, int userId);

        /// <summary>
        /// Delete Event Engine Details
        /// </summary>
        /// <param name="detailId">Detail ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int DeleteEventEngineTVDetails(int detailId, int userId);

        /// <summary>
        /// Delete Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">Excise Rate ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int DeleteExciseTaxRate(int exciseRateId, int userId);

        /// <summary>
        /// Delete GDocEventHeader
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="childFlag">Child Flag</param>
        /// <param name="userId">GDocEventHeader</param>
        [OperationContract]
        void DeleteGDocEventHeader(int eventId, int childFlag, int userId);

        /// <summary>
        /// Delete Group Details
        /// </summary>
        /// <param name="groupId">Group ID</param>
        /// <param name="userId">DeleteGroupDetails</param>
        [OperationContract]
        void DeleteGroupDetails(int groupId, int userId);

        /// <summary>
        /// Delete Mortgage Import
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string DeleteMortgageImport(int importId, int userId);


        /// <summary>
        /// Delete Permit Import Template
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string DeletePermiTemplate(int templateId, int userId);


        /// <summary>
        /// Delete Income Source Details
        /// /// </summary>
        /// <param name="IncomeSourceID">IncomeSource ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string DeleteIncomeSource(int IncomeSourceID, int userId);


        /// <summary>
        /// Delete MAD Import Template
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string DeleteMADTemplate(int templateId, int userId);


        /// <summary>
        /// Delete Snapshot Import Template
        /// </summary>
        /// <param name="importId">Import ID</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        string DeleteSnapshotTemplate(int templateId, int userId);

        /// <summary>
        /// Delete Mortgage Import File Entries
        /// </summary>
        /// <param name="importId">Import Id</param>
        /// <param name="userId">UserID</param>
        /// <returns>string</returns>
        [OperationContract]
        string DeleteMortgageImportFileEntries(int importId, int userId);

        /// <summary>
        /// Delete Mortgage Template
        /// </summary>
        /// <param name="templateId">Template Id</param>
        /// <param name="overrideStatus">Override Status</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int DeleteMortgageTemplate(int templateId, bool overrideStatus, int userId);

        /// <summary>
        /// Delete Query
        /// </summary>
        /// <param name="queryId">Query ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void DeleteQuery(int queryId, int userId);

        /// <summary>
        /// Delete Query Utility
        /// </summary>
        /// <param name="queryId">Query ID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void DeleteQueryUtility(int queryId, int userId);

        /// <summary>
        /// Delete SnapShot
        /// </summary>
        /// <param name="snapShotId">SnapShot ID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void DeleteSnapShot(int snapShotId, int userId);

        /// <summary>
        /// Delete Snapshot Utility
        /// </summary>
        /// <param name="snapshotId">SnapShot ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void DeleteSnapshotUtility(int snapshotId, int userId);

        /// <summary>
        /// Delete User Details
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="loginUserId">Login User ID</param>
        [OperationContract]
        void DeleteUserDetails(int userId, int loginUserId);

        /// <summary>
        /// Execute AffdvtQuery
        /// </summary>
        /// <param name="formId">Form ID</param>
        /// <param name="whereCondnSql">Condition</param>
        /// <param name="orderByCondn">Order By</param>
        /// <returns>String</returns>
        [OperationContract]
        string ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn);

        /// <summary>
        /// Execute Query
        /// </summary>
        /// <param name="whereCondition">Condition</param>
        /// <param name="orderByCondition">Order by</param>
        /// <param name="formId">Form ID</param>
        /// <returns>String</returns>
        [OperationContract]
        string ExecuteQuery(string whereCondition, string orderByCondition, int formId);

        /// <summary>
        /// Execute Snapshot
        /// </summary>
        /// <param name="snapshotId">Snapshot ID</param>
        /// <param name="whereCondition">Where class</param>
        /// <param name="orderByCondition">Order By value</param>
        /// <param name="formId">Form ID</param>
        /// <returns>String</returns>
        [OperationContract]
        string ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId);

        /// <summary>
        /// Get Minimum TaxDue
        /// </summary>
        /// <param name="statmentId">Statement ID</param>
        /// <param name="interestDate">Interest Date</param>
        /// <returns>String</returns>
        [OperationContract]
        decimal F1003_GetMinTaxDue(int statmentId, string interestDate);

        /// <summary>
        /// Get Interest Amount
        /// </summary>
        /// <param name="statmentId">Statement ID</param>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="taxDueAmount">Tax Due Amount</param>
        /// <returns>Decimal</returns>
        [OperationContract]
        decimal F1004_GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount);

        /// <summary>
        /// Get Valid Receipt
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="receiptDate">Receipt Date</param>
        /// <returns>String</returns>
        [OperationContract]
        string F1009_GetValidReceiptTest(int statementId, DateTime receiptDate);

        /// <summary>
        /// Get MiscReceipt Template
        /// </summary>
        /// <param name="miscTemplateId">MiscTemplate ID</param>
        /// <returns>String</returns>
        [OperationContract]
        string F1021_GetMiscReceiptTemplate(int miscTemplateId);

        /// <summary>
        /// Save MiscReceipt Template
        /// </summary>
        /// <param name="miscTemplateDetails">MiscTemplate Details</param>
        /// <param name="templateItems">Template Items</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1021_SaveMiscReceiptTemplate(string miscTemplateDetails, string templateItems, int userId);

        /// <summary>
        /// Delete MiscReceipt Template
        /// </summary>
        /// <param name="miscTemplateId">MiscTemplate ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1022_DeleteMiscReceiptTemplate(int miscTemplateId, int userId);

        /// <summary>
        /// ListMiscReceiptTemplate
        /// </summary>
        /// <returns>String</returns>
        [OperationContract]
        string F1022_ListMiscReceiptTemplate();

        /// <summary>
        /// Check Roll Year
        /// </summary>
        /// <param name="autoFundItems">Fund Items</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1025_CheckRollYear(string autoFundItems);

        /// <summary>
        /// Delete AutoFund Transfer Details
        /// </summary>
        /// <param name="autoTransferId">The auto transfer id.</param>
        /// <param name="userId">USer ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1025_DeleteAutoFundTransferDetails(int autoTransferId, int userId);

        /// <summary>
        /// List AutoFund Transfer Details
        /// </summary>
        /// <param name="rollYear">Roll YEar</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1025_ListAutoFundTransferDetails(int rollYear);

        /// <summary>
        /// List Roll Year
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1025_ListRollYear();

        /// <summary>
        /// UpdateAutoFundTransferDetails
        /// </summary>
        /// <param name="autoFundItems">Auto FundItems</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1025_UpdateAutoFundTransferDetails(string autoFundItems, int userId);

        /// <summary>
        /// Delete District Definition
        /// </summary>
        /// <param name="specialDistrictId">Special District ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1030_DeleteDistrictDefinition(int specialDistrictId, int userId);

        /// <summary>
        /// Delete District Definition Rate
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item id.</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId);

        /// <summary>
        /// Get District Definition Details
        /// </summary>
        /// <param name="districtNo">District Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1030_GetDistrictDefinitionDetails(int districtNo);

        /// <summary>
        /// List District Definition Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1030_ListDistrictDefinitionType();

        /// <summary>
        /// SaveDistrictDefinition
        /// </summary>
        /// <param name="districtNo">District Number</param>
        /// <param name="districtItem">District Item</param>
        /// <param name="rateItem">Rate Item</param>
        /// <param name="distributionItem">Distribution Item</param>
        /// <param name="flagOverride">Flag Override</param>
        /// <param name="userId">UserId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, int userId);

        /// <summary>
        /// Delete District Definition
        /// </summary>
        /// <param name="specialDistrictId">special DistrictID</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F16030_DeleteDistrictDefinition(int specialDistrictId, int userId);

        /// <summary>
        /// Delete District Definition Rate
        /// </summary>
        /// <param name="specialDistrictRateItemId">Special DistrictRate ItemID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F16030_DeleteDistrictDefinitionRate(int specialDistrictRateItemId, int userId);

        /// <summary>
        /// Get District Definition Details
        /// </summary>
        /// <param name="districtNo">District Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F16030_GetDistrictDefinitionDetails(int districtNo);

        /// <summary>
        /// List District Definition Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F16030_ListDistrictDefinitionType();

        /// <summary>
        /// Save DistrictDefinition
        /// </summary>
        /// <param name="districtNo">District Number</param>
        /// <param name="districtItem">District Item</param>
        /// <param name="rateItem">Rate Item</param>
        /// <param name="distributionItem">Distribution Item</param>
        /// <param name="flagOverride">Flag Override</param>
        /// <param name="flagValidation">Flag Validation</param>
        /// <param name="userId">User ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F16030_SaveDistrictDefinition(int districtNo, string districtItem, string rateItem, string distributionItem, bool flagOverride, bool flagValidation, int userId);

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        [OperationContract]
        string F16031_ListDistrictAssessmentDetails(int workingfileId);

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="sadistrictId">The sadistrict id.</param>
        /// <returns>
        /// returns dataset containing specialDistrict Details
        /// </returns>
        [OperationContract]
        string F16031_ListDistrictAssessment(int sadistrictId);

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        [OperationContract]
        string F16031_GetSpecialAssessmentParcel(string parcelNumber, int? parcelId, int? rollYear);

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        [OperationContract]
        string F16031_DeleteDistrictAssessment(int workingfileId, int userId);

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="isoverride">if set to <c>true</c> [isoverride].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">The userId.</param>
        /// <returns>Key ID</returns>
        [OperationContract]
        int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, int userId);

        /// <summary>
        /// F16031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        [OperationContract]
        string F16031_CheckSpecialAssessment(string districtProperty);


        ///<SUMMARY>
        /// f16031 exe Writer\Cancel Statement
        /// </SUMMARY>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="workingFileID">The workingFile Id.</param>
        /// <param name="User">The User ID.</param>
        [OperationContract]
        void F16031_ExeWriteTaxStatement(int workingFileId, int userId, bool isCancel);

        /// <summary>
        /// DeleteDistrictAssessment
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1031_DeleteDistrictAssessment(int statementId, int userId);

        /// <summary>
        /// Get District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">Parcel Number</param>
        /// <param name="parcelId">Parcel ID</param>
        /// <param name="rollYear">rollYear</param> 
        /// <returns>string</returns>
        [OperationContract]
        string F1031_GetDistrictAssessmentParcelID(string parcelNumber, int? parcelId, int? rollYear);

        /// <summary>
        /// List District Assessment
        /// </summary>
        /// <param name="districtId">District ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1031_ListDistrictAssessment(int districtId);

        /// <summary>
        /// ListDistrictAssessmentDetails
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1031_ListDistrictAssessmentDetails(int statementId);

        /// <summary>
        /// List District AssessmentID
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1031_ListDistrictAssessmentIDs();

        /// <summary>
        /// Save District Assessment Details
        /// </summary>
        /// <param name="districtProperty">District Property</param>
        /// <param name="districtRates">District Rates</param>
        /// <param name="overrideStatus">Override Status</param>
        /// <param name="ownerRide">Owner Ride</param>
        /// <param name="userId">user Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool overrideStatus, bool ownerRide, int userId);

        /// <summary>
        /// Check Special DistrictS tatement Or Owner
        /// </summary>
        /// <param name="districtProperty">District Property</param>
        /// <param name="statementFlag">Statement Flag</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1031_CheckSpecialDistrictStatementOrOwner(string districtProperty, bool statementFlag);

        /// <summary>
        /// List Post Types
        /// </summary>
        /// <param name="form">Form Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1033_ListPostTypes(int? form);

        /// <summary>
        /// List Special Districts
        /// </summary>
        /// <param name="district">District ID</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="description">Description</param>
        /// <param name="postTypeId">Post TypeID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1033_ListSpecialDistricts(int? district, int? rollYear, string description, int? postTypeId);

        /// <summary>
        /// List Interest Method.
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string ListInterestMethod();

        /// <summary>
        /// List Interest Delq details.
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string ListInterestDelqDetails();

        /// <summary>
        /// Get District Details.
        /// </summary>
        /// <param name="districtId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetDistrictDetails(int districtId);

        /// <summary>
        /// Improvement district type list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ImprovementDistrictTypelist(string districtType);

        /// <summary>
        /// Get Distribution Details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetDistributionDetails();

        /// <summary>
        /// Get District Definition.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetDistrictDefinitionDetails(int districtID);

        /// <summary>
        /// Executes Rollover improvement dist.
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        string RollOver_ImprovementDistrict(int districtId, int userId);

        /// <summary>
        /// Save District Definition.
        /// </summary>
        /// <param name="districtItem"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [OperationContract]
        string F16040_SaveImproveDistrictDefinition(string districtItem,string distributionItem, int userid);

        /// <summary>
        /// Update Improvement District Details.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="districtItem"></param>
        /// <param name="distributionItem"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [OperationContract]
        string F16040_UpdateImproveDistrictDefinition(int districtNo, string districtItem, string distributionItem, int userid);
        
        /// <summary>
        /// Get District Parcels.
        /// </summary>
        /// <param name="DistrictId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetDistrictParcels(int DistrictId);

        /// <summary>
        /// List District Parcels.
        /// </summary>
        /// <param name="parcelval"></param>
        /// <param name="parcelId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        [OperationContract]
        string ListDistrictParcelsDetails(string parcelval, int? parcelId, int? rollYear);

        /// <summary>
        /// Save Improvement District Parcels.
        /// </summary>
        /// <param name="districtProperty"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        string F16041_SaveDistrictParcels(string districtProperty, int userId);

        /// <summary>
        /// Delete Improvement Parcels.
        /// </summary>
        /// <param name="workingFileItemId"></param>
        /// <param name="userId"></param>
        [OperationContract]
        string F16041_DeleteDistrictParcels(int workingFileItemId, int userId);

        /// <summary>
        /// Check Parcel Details.
        /// </summary>
        /// <param name="districtProperty"></param>
        /// <returns></returns>
        [OperationContract]
        string CheckParcelDetails(string districtProperty);

        /// <summary>
        /// Get Real Property Statement
        /// </summary>
        /// <param name="statementId">StatementId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F11020_GetRealPropertyStatement(int statementId);

        /// <summary>
        /// Get Real Property Statements
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15030_GetRealPropertyStatements(int statementId);

        /// <summary>
        /// F25050s the get district assessment parcel ID.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string f25050GetDistrictAssessmentParcelID(string parcelNumber, int parcelId);

        /// <summary>
        /// Get WorkQueue SearchResult
        /// </summary>
        /// <param name="parcelNumber">Parcel Number</param>
        /// <param name="name">Name</param>
        /// <param name="receiptDate">Receipt Date</param>
        /// <param name="address">Address</param>
        /// <param name="taxCode">Tax Code</param>
        /// <param name="treasurer">Treasurer</param>
        /// <param name="assessor">Assessor</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1107_ExciseWorkQueue_GetWorkQueueSearchResult(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, string statementNumber);

        /// <summary>
        /// Get Submit Affidavit
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1108_GetSubmitAffidavit(string statementId);

        /// <summary>
        /// Get Submit Affidavit Reply
        /// </summary>
        /// <param name="reetReplyXml">XML</param>
        /// <param name="userId">User ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1108_GetSubmitAffidavitReply(string reetReplyXml, int userId);

        /// <summary>
        /// List Configuration Detail
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1108_ListConfigurationDetail();

        /// <summary>
        /// List Management Queue
        /// </summary>
        /// <param name="parcelNumber">Parcel Number</param>
        /// <param name="name">Name</param>
        /// <param name="saleDate">Sale Date</param>
        /// <param name="address">Address</param>
        /// <param name="taxCode">Tax Code</param>
        /// <param name="receiptNumber">Receipt Number</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1108_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string receiptNumber, string statementNumber);

        /// <summary>
        /// Save ReplyReet Xml
        /// </summary>
        /// <param name="reetXml">reetXml</param>
        /// <param name="reetReplyXml">reetReplyXml</param>
        /// <param name="userId">User ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1108_SaveReplyReetXml(string reetXml, string reetReplyXml, int userId);

        /// <summary>
        /// Filter Search Affidavit
        /// </summary>
        /// <param name="filterXml">Filtered values</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1109_FilterSearchAffidavit(string filterXml);

        /// <summary>
        /// List Management Queue
        /// </summary>
        /// <param name="parcelNumber">Parcel Number</param>
        /// <param name="name">Name</param>
        /// <param name="receiptDate">Receipt Date</param>
        /// <param name="address">Address</param>
        /// <param name="taxCode">Tax Code</param>
        /// <param name="treasurer">Treasurer</param>
        /// <param name="assessor">Assessor</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="statementNumber">Statement Number</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1109_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, int rollYear, string statementNumber);

        /// <summary>
        /// List RollYear
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1109_ListRollYear();

        /// <summary>
        /// Management Queue Filter Result
        /// </summary>
        /// <param name="statusFilterId">status Filter Id</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="filterFromDate">Filter From Date</param>
        /// <param name="filterToDate">Filter To Date</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1109_ManagementQueueFilterResult(int statusFilterId, int rollYear, string filterFromDate, string filterToDate);

        /// <summary>
        /// Get Disbursement Account Names
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1210_DisbursementAccountNames();

        /// <summary>
        /// Get Disbursement Details
        /// </summary>
        /// <param name="postDate">Post Date</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1210_GetDisbursementDetails(DateTime postDate);

        /// <summary>
        /// Save Disbursement
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <param name="userId">User Id</param>
        /// <param name="postDate">Post Date</param>
        /// <param name="agencies">Agencies</param>
        /// <param name="overrideStatus">Override Status</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1210_SaveDisbursement(int registerId, int userId, DateTime postDate, string agencies, int overrideStatus);

        /// <summary>
        /// Create Checks
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="createChecksTclId">create Checks TclId</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1211_CreateChecks(int userId, string createChecksTclId);

        /// <summary>
        /// Delete CheckStaging
        /// </summary>
        /// <param name="tclIdDelete">tclIdDelete</param>
        /// <param name="userId">User Id</param>
        [OperationContract]
        void F1211_DeleteCheckStaging(string tclIdDelete, int userId);

        /// <summary>
        /// Get Disbursement CheckList
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1211_GetDisbursementCheckList();

        /// <summary>
        /// Update Agency Valid Status
        /// </summary>
        /// <param name="tclIds">tclIds</param>
        /// <param name="validStatus">Valid Status</param>
        /// <param name="userId">User Id</param>
        [OperationContract]
        void F1211_UpdateAgencyValidStatus(string tclIds, int validStatus, int userId);

        /// <summary>
        /// Update Check Staging
        /// </summary>
        /// <param name="tclId">tclId</param>
        /// <param name="disbursementCheck">Disbursement Check</param>
        /// <param name="checkItems">Check Items</param>
        /// <param name="userId">User Id</param>
        [OperationContract]
        void F1211_UpdateCheckStaging(int tclId, string disbursementCheck, string checkItems, int userId);

        /// <summary>
        /// Get Account Names
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1214_AccountNames();

        /// <summary>
        /// List Refund Payments
        /// </summary>
        /// <param name="form">Form Number</param>
        /// <param name="whereCondnSql">Where Condition</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1214_ListRefundPayments(int form, string whereCondnSql);

        /// <summary>
        /// Prepare Checks
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItems">Payment Items</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1214_PrepareChecks(int registerId, int ownerId, DateTime interestDate, int userId, string paymentItems);

        /// <summary>
        /// Get Account Names
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1220_AccountNames();

        /// <summary>
        /// Get Account Register Details
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <param name="beginningDate">Beginning Date</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1220_GetAccountRegisterDetails(int registerId, DateTime beginningDate);

        /// <summary>
        /// Get Reconciled Details
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1220_GetReconciledDetails(int registerId);

        /// <summary>
        /// List Account Register
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <param name="beginningDate">Beginning Date</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1220_ListAccountRegister(int registerId, DateTime beginningDate);

        /// <summary>
        /// Update Cash Ledger
        /// </summary>
        /// <param name="clid">Cash Ledger ID</param>
        /// <param name="overRide">Over Ride</param>
        /// <param name="checkDetails">check Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1221_UpdateCashLedger(int clid, int overRide, string checkDetails, int userId);

        /// <summary>
        /// Get Account Names
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1224_AccountNames();

        /// <summary>
        /// Create Checks
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="startingCheckNumber">Starting Check Number</param>
        /// <param name="checkItems">Check Items</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1224_CreateChecks(int registerId, int userId, long startingCheckNumber, string checkItems);

        /// <summary>
        /// Get Check Number
        /// </summary>
        /// <param name="registerId">Register ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1224_GetCheckNumber(int registerId);

        /// <summary>
        /// List UnPrinted Checks
        /// </summary>
        /// <param name="registerId">Register ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1224_ListUnPrintedChecks(int registerId);

        /// <summary>
        /// Delete Cash Ledger
        /// </summary>
        /// <param name="clid">Cash Ledger ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1226_DeleteCashLedger(int clid, int userId);

        /// <summary>
        /// Get Cash Ledger
        /// </summary>
        /// <param name="clid">Cash Ledger ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1226_GetCashLedger(int clid);

        /// <summary>
        /// List Cash Ledger
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1226_ListCashLedger();

        /// <summary>
        /// UpdateCashLedgerStatus
        /// </summary>
        /// <param name="clid">Cash Ledger ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="functionDate">Function Date</param>
        /// <param name="functionId">Function Id</param>
        /// <param name="loginUserId">Login UserId</param>
        [OperationContract]
        void F1226_UpdateCashLedgerStatus(int clid, int userId, DateTime functionDate, int functionId, int loginUserId);

        /// <summary>
        /// Save Master Receipt Details
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <param name="receiptSourceId">Receipt SourceId</param>
        /// <param name="otherParameterInfo">Other Parameter Information</param>
        /// <param name="sharedPaymentId">Shared Payment ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1405_SaveMasterReceipt(int statementId, int receiptSourceId, string otherParameterInfo, int? sharedPaymentId);

        /// <summary>
        /// Delete Owner Receipting
        /// </summary>
        /// <param name="ownerId">Owner Id</param>
        /// <param name="ownerXml">Owner Xml</param>
        /// <param name="statementXml">Statement Xml</param>
        /// <param name="userId">User ID</param>
        /// <param name="formBackColor">Form BAckcolor</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1410_DeleteOwnerReceipting(int ownerId, string ownerXml, string statementXml, int userId, string formBackColor);

        /// <summary>
        /// Get Owner Receipting
        /// </summary>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="ownerId">Owner Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1410_GetOwnerReceipting(string interestDate, string ownerId, string parcelIDs);

        /// <summary>
        /// List Owner Receipting
        /// </summary>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="statementXml">Statement Xml</param>
        /// <param name="formBackColor">Form BackColor</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1410_ListOwnerReceipting(string interestDate, string statementXml, string formBackColor);

        /// <summary>
        /// Save Owner Receipting
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="receiptDate">Receipt Date</param>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="ppaymentId">ppaymentId</param>
        /// <param name="paymentOption">Payment Option</param>
        /// <param name="statementXml">Statement Xml</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1410_SaveOwnerReceipting(int userId, string receiptDate, string interestDate, int ppaymentId, int paymentOption, string statementXml);

        /// <summary>
        /// F1410_SaveOwnerReceiptPreview
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="statementDetails">statementDetails</param>
        /// <returns>int</returns>
        [OperationContract]
        int F1410_SaveOwnerReceiptPreview(int userId, string statementDetails);

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        [OperationContract]
        string F1410_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId);


        /// <summary>
        /// List Mortgage Name
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1423_ListMortgageName();

        /// <summary>
        /// Update RealProperty Statement
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <param name="statementItems">Statement Items</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1423_UpdateRealPropertyStatement(int statementId, string statementItems, int userId);

        /// <summary>
        /// Create Or Edit Account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="acctEmelemts">Account Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1500_CreateOrEditAccount(int accountId, string acctEmelemts, int userId);

        /// <summary>
        /// Get Configuration Value
        /// </summary>
        /// <param name="cfgName">Configuration Name</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_GetConfigurationValue(string cfgName);

        /// <summary>
        /// Get Description
        /// </summary>
        /// <param name="keyId">key ID</param>
        /// <param name="elementName">Element Name</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_GetDescription(string keyId, string elementName);

        /// <summary>
        /// Get Function Items
        /// </summary>
        /// <param name="function">Function Name</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_GetFunctionItems(string function);

        /// <summary>
        /// Get SubFund Items
        /// </summary>
        /// <param name="subFund">SubFund Name</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_GetSubFundItems(string subFund, short rollYear);

        /// <summary>
        /// List Account Details
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_ListAccountDetails(int accountId);

        /// <summary>
        /// List Register Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1500_ListRegisterType();


        /// <summary>
        /// F1505_s the district Copy Form
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtText">The district details.</param>
        /// <param name="rollyear">The rollyear.</param>
        /// <param name="description">The description.</param>
        /// <param name="isactive">The isactive.</param>
        /// <param name="districtTypeId">The districtTypeId.</param>
        /// <param name="ExciseId">The ExciseRateID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message</returns>
        [OperationContract]
        string F1505ExecuteCopyDistrict(int districtId, string districtText, int rollyear, string description, bool isactive, int districtTypeId, int ExciseId, int userId);

        /// <summary>
        /// Check District
        /// </summary>
        /// <param name="districtId">District ID</param>
        /// <param name="district">District Name</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15002_CheckDistrict(int? districtId, string district, int rollYear);

        /// <summary>
        /// F15002_s the create or edit district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtDetails">The district details.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F15002_CreateOrEditDistrictMgmt(int? districtId, string districtDetails, string districtFundItems, int userId);

        /// <summary>
        /// Get Distirct FundDetails
        /// </summary>
        /// <param name="districtId">Distirct ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15002_GetDistirctFundDetails(int? districtId);

        /// <summary>
        /// List All Funds 
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="fund">Fund</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15002_ListAllFunds(int? fundId, string fund, int? rollYear);

        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        string F15002_GetDistrictType(int userId);

        /// <summary>
        /// Check Fund
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="fund">Fund</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15003_CheckFund(int? fundId, string fund, int rollYear);

        /// <summary>
        /// Create Or Edit Fund Management
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <param name="fund">Fund</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="description">Description</param>
        /// <param name="fundGroupId">Fund GroupId</param>
        /// <param name="fundItems">Fund Items</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15003_CreateOrEditFundMgmt(int? fundId, string fund, int rollYear, string description, int? fundGroupId, string fundItems, int userId);

        /// <summary>
        /// Get SubFund Details
        /// </summary>
        /// <param name="fundId">Fund ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15003_GetFundSubFundDetails(int? fundId);

        /// <summary>
        /// List Available SubFunds
        /// </summary>
        /// <param name="subFund">SubFund</param>
        /// <param name="description">Description</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="fundId">Fund ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15003_ListAvailableSubFunds(string subFund, string description, int? rollYear, int? fundId);

        /// <summary>
        /// List Fund Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F15003_ListFundType();

        /// <summary>
        /// Check Duplicate Agency
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <param name="agencyName">Agency Name</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15004_CheckDuplicateAgency(int agencyId, string agencyName);

        /// <summary>
        /// Create Or Edit Agency Details
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <param name="acctEmelemts">Account Emelemts</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15004_CreateOrEditAgencyDetails(int agencyId, string acctEmelemts, int userId);

        /// <summary>
        /// Get Agency Details
        /// </summary>
        /// <param name="agencyId">Agency ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15004_GetAgencyDetails(int agencyId);

        /// <summary>
        /// Check SubFund
        /// </summary>
        /// <param name="subFundId">SubFund ID</param>
        /// <param name="subFund">SubFund</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15005_CheckSubFund(int? subFundId, string subFund, int rollYear);

        /// <summary>
        /// Check Duplicate Account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="acctEmelemts">Account Emelemts</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15007_CheckDuplicateAccount(int accountId, string acctEmelemts);

        /// <summary>
        /// Create Or Edit GLConfigDetails
        /// </summary>
        /// <param name="configId">Configuration ID</param>
        /// <param name="configElements">Configuration Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1501_CreateOrEditGLConfigDetails(int configId, string configElements, int userId);

        /// <summary>
        /// Get GLConfigDetails
        /// </summary>
        /// <param name="configId">Configuration ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1501_GetGLConfigDetails(int configId);

        /// <summary>
        /// List GLConfigDetails
        /// </summary>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1501_ListGLConfigDetails(int rollYear);

        /// <summary>
        /// List RollYear
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F1501_ListRollYear();

        /// <summary>
        /// Delete Affidavit Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F15010_DeleteAffidavitDetails(int statementId, int userId);

        /// <summary>
        /// Get Affidavit StatementId
        /// </summary>
        /// <param name="formId">Form Id</param>
        /// <param name="orderField">Order Field</param>
        /// <param name="orderBy">Order By</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetAffidavitStatementId(int formId, string orderField, string orderBy);

        /// <summary>
        /// Get District Selection
        /// </summary>
        /// <param name="exciseRateId">Excise RateId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetDistrictSelection(int exciseRateId);

        /// <summary>
        /// Get Excise Individual Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetExciseIndividualType();

        /// <summary>
        /// Get Excise Tax Affidavit Calulate Amount Due
        /// </summary>
        /// <param name="saleDate">Sale Date</param>
        /// <param name="paymentDate">Payment Date</param>
        /// <param name="exciseRateId">ExciseRateID</param>
        /// <param name="taxCode">TaxCode</param>
        /// <param name="taxableSaleAmount">Taxable Sale Amount</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount);

        /// <summary>
        /// Get Excise Tax Affidavit Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetExciseTaxAffidavitDetails(int statementId);

        /// <summary>
        /// Get Owner Details
        /// </summary>
        /// <param name="ownerId">Owner ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15010_GetOwnerDetails(int ownerId);

        /// <summary>
        /// Get Owner Status.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        [OperationContract]
        string F15010_GetOwnerStatus(int ownerId);

        /// <summary>
        /// F15010_s the get parcel detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F15010_GetParcelDetail(int? parcelId, string parcelNumber);

        /// <summary>
        /// Save AffiDavit Details
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="partiesAddress">Parties Address</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="exciseAffidavitDetails">Excise Affidavit Details</param>
        /// <param name="mobileHomeDetails">Mobile Home Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15010_SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, string mobileHomeDetails, int userId);

        /// <summary>
        /// Get Excise Statement
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15011_GetExciseStatement(int statementId);

        /// <summary>
        /// F15010_s the list excise WAC.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F15010_ListExciseWAC();

        /// <summary>
        /// F5010_s the list excise individual.
        /// </summary>
        /// <param name="ExciseIndividualElements">The excise individual elements.</param>
        /// <returns></returns>
        [OperationContract]
        string F15010_ListExciseIndividual(string ExciseIndividualElements);

        /// <summary>
        /// F15010_s the list open space field.
        /// </summary>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <returns></returns>
        [OperationContract]
        string F15010_ListOpenSpaceField(string parcelIds);

        /// <summary>
        /// Save Excise Statement
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <param name="interestDate">Interest Date</param>
        /// <param name="receiptDate">Receipt Date</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F15011_SaveExciseStatement(int statementId, DateTime interestDate, DateTime receiptDate, int userId);

        /// <summary>
        /// Get Excise Receipt
        /// </summary>
        /// <param name="statementId">Statement ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15012_GetExciseReceipt(int statementId);

        /// <summary>
        /// Delete Excise Tax Rate
        /// </summary>
        /// <param name="exciseRateId">Excise Rate ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15013_DeleteExciseTaxRate(int exciseRateId, int userId);

        /// <summary>
        /// Get AccountName
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15013_GetAccountName(int accountId);

        /// <summary>
        /// Get District Name
        /// </summary>
        /// <param name="districtId">District ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15013_GetDistrictName(int districtId);

        /// <summary>
        /// Get Excise TaxRate
        /// </summary>
        /// <param name="exciseRateId">Excise Rate ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15013_GetExciseTaxRate(int exciseRateId);

        /// <summary>
        /// List Excise TaxRate
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F15013_ListExciseTaxRate();

        /// <summary>
        /// Save Excise TaxRate
        /// </summary>
        /// <param name="exciseRateId">Excise Rate ID</param>
        /// <param name="exciseTaxDetails">Excise Tax Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15013_SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId);

        /// <summary>
        /// List Statement Ownership
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15015_ListStatementOwnership(int statementId);

        /// <summary>
        /// F15015_s the type of the list M owner.
        /// </summary>
        /// <returns>String</returns>
        [OperationContract]
        string F15015_ListMOwnerType();

        /// <summary>
        /// Save Statement Ownership
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <param name="statementOwner">Statement Owner</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F15015_SaveStatementOwnership(int statementId, string statementOwner, int userId);

        /// <summary>
        /// List ALL Owner Details
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="address1">Address 1</param>
        /// <param name="address2">Address 2</param>
        /// <param name="city">City</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15015_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city);


        /// <summary>
        /// Get Statement HeaderSlim Details
        /// </summary>
        /// <param name="statementlId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15016_GetStatementHeaderSlimDetails(int statementlId);

        /// <summary>
        /// Ge tMiscReceipt
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15018_GetMiscReceipt(int receiptId);

        /// <summary>
        /// Save district details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        [OperationContract]
        string F1024_SaveDistrictDetails(int levyOption, int districtId, decimal amountValue, int userId, bool IsReplace, string SubFundXML);

        /// <summary>
        /// List district distribution details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <param name="IsRelace">IsReplace</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        [OperationContract]
        string GetDistrictDistributionData(int LevyOptionId, int districtId, decimal amount, int userId, string subfundsXML, bool isreplace);

        /// <summary>
        /// Gets the district sub fund Items data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <returns>returns DistrictSelectionData</returns>
        [OperationContract]
        string GetDistrictData(int districtId);



        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        [OperationContract]
        string F15018_ListAccountDetails(string filterValue, int? rollYear,int? formNo);

        /// <summary>
        /// Check RollYear
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <param name="receiptSourceId">Receipt Source ID</param>
        /// <param name="journalItems">Journal Items</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15019_CheckRollYear(int statementId, int receiptSourceId, string journalItems);

        /// <summary>
        /// Delete Account Element Management
        /// </summary>
        /// <param name="functionId">Function Id</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1502_DeleteAccountElementMgmt(string functionId, int userId);

        /// <summary>
        /// Get Account Element Management
        /// </summary>
        /// <param name="function">Function</param>
        /// <param name="description">Description</param>
        /// <param name="type">Type</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1502_GetAccountElementMgmt(string function, string description, int type);

        /// <summary>
        /// Save Account Element Management
        /// </summary>
        /// <param name="functionElemnts">Function Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1502_SaveAccountElementMgmt(string functionElemnts, int userId);

        /// <summary>
        /// Get Receipt Details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15020_GetReceiptDetails(int receiptId);

        /// <summary>
        /// List HistoryGrid
        /// </summary>
        /// <param name="statementId">Statement Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15020_ListHistoryGrid(int statementId);

        /// <summary>
        /// Delete Generic Element Management
        /// </summary>
        /// <param name="elementId">Element ID</param>
        /// <param name="formName">Form Name</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F1503_DeleteGenericElementMgmt(string elementId, string formName, int userId);

        /// <summary>
        /// Get Generic Element Management
        /// </summary>
        /// <param name="keyValue">key Value</param>
        /// <param name="description">Description</param>
        /// <param name="formName">Form Name</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1503_GetGenericElementMgmt(string keyValue, string description, string formName);

        /// <summary>
        /// Save Generic Element Management
        /// </summary>
        /// <param name="functionElemnts">Function Elemnts</param>
        /// <param name="formName">Form Name</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1503_SaveGenericElementMgmt(string functionElemnts, string formName, int userId);

        /// <summary>
        /// Get Receipt Payment
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15104_GetReceiptPayment(int receiptId);

        /// <summary>
        /// Update Receipt Payment
        /// </summary>
        /// <param name="receiptPayment">Receipt Payment</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F15104_UpdateReceiptPayment(string receiptPayment, int userId);

        /// <summary>
        /// Get District Selection Data
        /// </summary>
        /// <param name="districtId">District ID</param>
        /// <param name="district">District</param>
        /// <param name="description">Description</param>
        /// <param name="rollYear">Roll Year</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1512_GetDistrictSelectionData(int districtId, string district, string description, int rollYear);

        /// <summary>
        /// Get Fund Selection
        /// </summary>
        /// <param name="fund">Fund</param>
        /// <param name="description">Description</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1513_GetFundSelection(string fund, string description);

        /// <summary>
        /// F1513_CentralFundItemValidation
        /// </summary>
        /// <param name="fundId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        [OperationContract]
        int F1513_CentralFundItemValidation(int fundId, int rollYear);

        /// <summary>
        /// Get SubFund Selection
        /// </summary>
        /// <param name="subFund">SubFund</param>
        /// <param name="description">Description</param>
        /// <param name="rollYear">Roll Year</param>
        /// <param name="iscash">IsCash value</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash);

        /// <summary>
        /// Get Institution Detail
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1530_GetInstitutionDetail(int institutionId);

        /// <summary>
        /// Save Institution
        /// </summary>
        /// <param name="institutionId">Institution ID</param>
        /// <param name="institutionElements">Institution Elements</param>
        /// <param name="userId">user Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1530_SaveInstitution(int institutionId, string institutionElements, int userId);

        /// <summary>
        /// Get Cash Account Detail
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1531_GetCashAccountDetail(int registerId);

        /// <summary>
        /// Save Cash Account
        /// </summary>
        /// <param name="registerId">Register Id</param>
        /// <param name="registerItems">Register Items</param>
        /// <param name="userId">UserId</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1531_SaveCashAccount(int registerId, string registerItems, int userId);

        /// <summary>
        /// Get Institution Contact Detail
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F1532_GetInstitutionContactDetail(int contactId);

        /// <summary>
        /// Save Institution Contact
        /// </summary>
        /// <param name="contactId">Contact ID</param>
        /// <param name="acctEmelemts">Account Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1532_SaveInstitutionContact(int contactId, string acctEmelemts, int userId);

        /// <summary>
        /// Get Parcel Details
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25000_GetParcelDetails(int parcelId);

        /// <summary>
        /// Get QuickValueSummary Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_FieldSummary(int keyId);

        /// <summary>
        /// Get BuildingPermits Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_BuildingPermits(int keyId);


        /// <summary>
        /// Get Ancestry Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_GetAncestryData(int keyId);

        /// <summary>
        /// Get Correction Details
        /// </summary>
        /// <param name="parcelId">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_GetCorrection(int keyId);

        /// <summary>
        /// Get History Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_GetHistoryData(int keyId);

        /// <summary>
        /// Get ParcelOwnerShip Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_GetParcelOwnerShip(int keyId);

        /// <summary>
        /// Get GetPhotos Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <param name="Form">Form</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_GetPhotos(int keyId, int form);

        /// <summary>
        /// Get ParcelSale Details
        /// </summary>
        /// <param name="KEY">KEY ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25090_ParcelSale(int keyId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_GetCheckOutDetails(int snapShotId, string snapShotValue);

        /// <summary>
        /// Get Parcel Details for F25051
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F25051_GetParcelDetails(int parcelId);

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F25051OwnerOccupied();

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F25051ParcelClassTypes();

        /// <summary>
        /// Update Parcel Header Details for F25051
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int f25051ParcelHeaderDetails(int parcelId, string parcelDetails, int userId);

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ListPrimaryImprovement();

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ListPrimaryLandType();


        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int UpdateParcelHeaderDetails(int parcelId, string parcelDetails,bool isCopyHeader,int userId);

        #region F26000ParcelheaderForm
        /// <summary>
        /// Update Parcel Header Details
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelDetails">Parcel Details</param>
        /// <param name="userId">User ID</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int UpdateParcelHeaderFormDetails(int parcelId, string parcelDetails, int userId, int rollYear);

        /// <summary>
        /// Get Parcel Details
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F26000_GetParcelFormDetails(int parcelId);

        /// <summary>
        /// F26000_s the exemption details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="exemptionFromAmount">The exemption from amount.</param>
        /// <returns></returns>
        [OperationContract]
        string F26000_ExemptionDetails(int parcelId, string exemptionCode, decimal? exemptionFromAmount);

        /// <summary>
        /// F26000_s the exempt field details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="exmptionId">The exmption id.</param>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <returns></returns>
        [OperationContract]
        string F26000_ExemptFieldDetails(int parcelId, int exmptionId, string exemptionCode);

        /// <summary>
        /// F26000_s the class code details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        [OperationContract]
        string F26000_ClassCodeDetails(string filterValue);

        /// <summary>
        /// Lists the primary improvement.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string PrimaryImprovementList();

        /// <summary>
        /// Lists the type of the primary land.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string PrimaryLandTypeList();

        [OperationContract]
        string F26000_GetApprisalType();
        #endregion

        #region SubFormsofF26000

        /// <summary>
        /// F2101_s the get location selection.
        /// </summary>
        /// <param name="locationCode">The location code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        [OperationContract]
        string f2101_GetLocationSelection(string locationCode, string description);

        /// <summary>
        /// F2102_s the get grouping selection.
        /// </summary>
        /// <param name="groupCode">The group code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        [OperationContract]
        string f2102_GetGroupingSelection(string groupCode, string description);

        /// <summary>
        /// F2103_s the get exemption selection.
        /// </summary>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="description">The description.</param>
        /// <param name="percent">The percent.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        [OperationContract]
        string f2103_GetExemptionSelection(string exemptionCode, string description, decimal? percent, decimal? maximum, int? rollYear);

        #endregion

        /// <summary>
        /// Get Parcel Type Details
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string GetParcelTypeDetails(int parcelId);

        /// <summary>
        /// GetParcelAttachmentDetails
        /// </summary>
        ///<param name="oldParcelID">oldParcelID</param>
        ///<param name="newParcelID">newParcelID</param>
        ///<param name="userID">userID</param>
        ///<param name="moduleID">moduleID</param>
        ///<returns>string</returns>
        [OperationContract]
        string GetParcelAttachmentDetails(int oldParcelID, int newParcelID, int userID, int moduleID);

        /// <summary>
        /// Create New Parcel Copy
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelTypeId">The parcel type id.</param>
        /// <param name="copyAllObjects">Copy All Objects</param>
        /// <param name="copyAllSlices">Copy All Slices</param>
        /// <param name="copyAttachments">Copy Attachments</param>
        /// <param name="copyComments">Copy Comments</param>
        /// <param name="parcelElements">Parcel Elements</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int CreateNewParcelCopy(int parcelId, int parcelTypeId, int copyAllObjects, int copyAllSlices, int copyAttachments, int copyComments, string parcelElements, int userId);

        /// <summary>
        /// Get Parcel Locking Details
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F2001_getParcelLockingDetails(int keyId);

        /// <summary>
        /// Get Parcel Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string GetParcelType();

        /// <summary>
        /// get Parcel Locking Username
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F2001_getParcelLockingUsername(int userId);

        /// <summary>
        /// Get Valid User Name
        /// </summary>
        /// <param name="prcelId">Parcel ID</param>
        /// <param name="userId">User ID</param>
        /// <param name="formNo">Form Number</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F2001_GetValidUserName(int prcelId, int userId, string formNo);

        /// <summary>
        /// Update Parcel Locking Details
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="valueLock">value Lock</param>
        /// <param name="adminLock">Admin Lock</param>
        /// <param name="lockAppraisal">Lock Appraisal</param>
        /// <param name="userId">User Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F2001_UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int userId);

        /// <summary>
        /// Get ComboData
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F15050_ComboData();

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="feeId">Fee ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F15050_getDatas(int feeId);

        /// <summary>
        /// F15050_s the save fee management.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="feeTypeId">The fee type id.</param>
        /// <returns>return value</returns>
        [OperationContract]
        int F15050_SaveFeeManagement(int feeId, string description, decimal amount, int accountId, int userId, byte feeTypeId);

        /// <summary>
        /// Apply Fees
        /// </summary>
        /// <param name="sysSnapshotId">System Snapshot ID</param>
        /// <param name="amount">Amount</param>
        /// <param name="description">Description</param>
        /// <param name="accountId">Account Id</param>
        /// <param name="userId">UserId</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F15050_ApplyFees(string feeXML, decimal amount, string description, int accountId, int userId);

        /// <summary>
        /// F15050_s the list fee types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The fee mgmt dataset xml string</returns>
        [OperationContract]
        string F15050_ListFeeTypes(int userId);

        /// <summary>
        /// F15050_s the remove template.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F15050_RemoveTemplate(int feeId, int userId);

        /// <summary>
        /// Get Misc Assessment
        /// </summary>
        /// <param name="madistrictId">District ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27000_GetMiscAssessment(int madistrictId);

        /// <summary>
        /// List District Item Type
        /// </summary>
        /// <param name="madistrictTypeId">District Type ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27000_ListMADistrictItemType(int madistrictTypeId);

        /// <summary>
        /// List District Type
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F27000_ListMADistrictType();

        /// <summary>
        /// Save Details
        /// </summary>
        /// <param name="distributionItems">Distribution Items</param>
        /// <param name="subHeaderItems">SubHeader Items</param>
        /// <param name="userId">User Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F27000_SaveMADetails(string distributionItems, string subHeaderItems, int userId);

        /// <summary>
        /// List ALL Owner Details
        /// </summary>
        /// <param name="firstName">First Name</param>
        /// <param name="lastName">Last Name</param>
        /// <param name="address1">Address 1</param>
        /// <param name="address2">Address 2</param>
        /// <param name="city">City</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27006_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city);

        /// <summary>
        /// List Parcel Ownership
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27006_ListParcelOwnership(int parcelId);

        /// <summary>
        /// Save Parcel Ownership
        /// </summary>
        /// <param name="parcelOwnership">Parcel Ownership</param>
        /// <param name="parcelId">Parcel ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F27006_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId, bool isfuturePush);

        /// <summary>
        /// Get Parcel HeaderSlim Details
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27007_GetParcelHeaderSlimDetails(int parcelId);

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string ListMOwnerType();

        /// <summary>
        /// List Parcel Ownership
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27008_ListParcelOwnership(int parcelId);

        /// <summary>
        /// Save Parcel Ownership
        /// </summary>
        /// <param name="parcelOwnership">Parcel Ownership</param>
        /// <param name="parcelId">Parcel ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F27008_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId);

        /// <summary>
        /// Get Owner Details
        /// </summary>
        /// <param name="ownerId">Owner ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27008_GetOwnerDetails(int ownerId, int userId);

        /// <summary>
        /// Get Appraisal Summary Objects
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35000_GetAppraisalSummaryObjects(int parcelId);

        /// <summary>
        /// F35000_s the check appraisal summary user.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35000_CheckAppraisalSummaryUser(int valueSliceId, int objectId, int userId);

        /// <summary>
        /// Insert Object
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="objectTypeId">The object type id.</param>
        /// <param name="description">Description</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F35000_InsertObject(int parcelId, short objectTypeId, string description, int userId);

        /// <summary>
        /// Insert Object
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="Propertities">Propertities</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F35000_SaveAppraisal(int parcelId, string propertiesXML, int userId);

        /// <summary>
        /// Insert Or UpdateV alueSlice
        /// </summary>
        /// <param name="valueSliceId">value Slice ID</param>
        /// <param name="valueSliceHeaderItems">valueSlice Header Items</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F35000_InsertOrUpdateValueSlice(int? valueSliceId, string valueSliceHeaderItems, int userId);

        /// <summary>
        /// List Object Slice Types
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F35000_ListObjectSliceTypes(int? ParcelId);

        /// <summary>
        /// List Slice Types
        /// </summary>
        /// <param name="objectId">Value ObjectID</param> 
        /// <returns>string</returns>
        [OperationContract]
        string F35000_ListSliceTypes(int objectId);

        /// <summary>
        /// F35000_s the object total.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35000_ObjectTotal(int parcelId);

        /// <summary>
        /// Delete ValueSlice
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F35001_DeleteValueSlice(int valueSliceId, int userId);

        /// <summary>
        /// Get Adjustment SliceValue
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <param name="type">Type</param>
        /// <param name="isvalue">Isvalue</param>
        /// <param name="adjustmentValue">Adjustment Value</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35001_GetAdjustmentSliceValue(int valueSliceId, byte type, bool isvalue, decimal adjustmentValue);

        /// <summary>
        /// Get ValueSlice Header Details
        /// </summary>
        /// <param name="valueSliceId">ValueSlice ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35001_GetValueSliceHeader(int valueSliceId);

        #region List Adjustment Types

        /// <summary>
        /// F35002_s the type of the list adjustment.
        /// </summary>
        /// <param name="masterFromNo">The master from no.</param>
        /// <returns>Adjustment Types dataTable</returns>
        [OperationContract]
        string F35002_ListAdjustmentType(int? masterFromNo);

        #endregion List Adjustment Types

        /// <summary>
        /// Save Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood Header ID</param>
        /// <param name="nbhDetails">Neighborhood Header Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId);

        /// <summary>
        /// Get Parent Neighborhood HeaderDetails
        /// </summary>
        /// <param name="rollyear">Roll year</param>
        /// <param name="type">Type</param>
        /// <param name="parentNeighborhood">Parent Neighborhood</param>
        /// <returns>string</returns>
        [OperationContract]
        string GetParentNeighborhoodHeaderDetails(int rollyear, int type, int parentNeighborhood);

        /// <summary>
        /// Get Neighborhood HeaderDetails
        /// </summary>
        /// <param name="neighborId">Neighborhood ID</param>
        /// <returns>string</returns>
        [OperationContract]
        string GetNeighborhoodHeaderDetails(int neighborId);

        /// <summary>
        /// Delete NeighborhoodHeader
        /// </summary>
        /// <param name="nbhdId">NeighborhoodHeader ID</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int DeleteNeighborhoodHeader(int nbhdId, int userId);

        /// <summary>
        /// Get Neighborhood Header User Details
        /// </summary>
        /// <param name="applicationId">ApplicationId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35100_GetNeighborhoodHeaderUserDetails(int applicationId);

        ////[OperationContract]
        ////string F35100_GetNeighborhoodGroupDetails(int rollYear);

        /// <summary>
        /// Save Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F35100_SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId);

        /// <summary>
        /// Copy Neighborhood Header Details
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <param name="userId">User ID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F3511_ExeNeighborhoodDetails(int nbhId, string newnbhdName, int userId);

        /// <summary>
        /// Delete Neighborhood Header
        /// </summary>
        /// <param name="nbhdId">Neighborhood ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void F35100_DeleteNeighborhoodHeader(int nbhdId, int userId);

        /// <summary>
        /// Duplicate Neighborhood Header Check
        /// </summary>
        /// <param name="nbhId">Neighborhood ID</param>
        /// <param name="nbhDetails">Neighborhood Details</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int DuplicateNeighborhoodHeaderCheck(int nbhId, string nbhDetails);

        /// <summary>
        /// Get Parcel Sale Tracking Details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29550_GetParcelSaleTrackingDetails(int eventId);

        /// <summary>
        /// Get Push Owner
        /// </summary>
        /// <param name="saleId">Sale Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29550_GetPushOwner(int saleId);

        /// <summary>
        /// Get Parcel Details
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <param name="neewParcelId">New ParcelId</param>
        /// <param name="saleId">SaleId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29550_GetParcelDetails(string parcelIdDetails, int neewParcelId, int saleId);

        /// <summary>
        /// Get Parcel Sale Tracking Combo Details
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F29550_GetParcelSaleTrackingComboDetails();

        /// <summary>
        /// Get Parcels Owner Details
        /// </summary>
        /// <param name="parcelIdDetails">Parcel Details</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29550_GetParcelsOwnerDetails(string parcelIdDetails);

        /// <summary>
        /// save Parcel Sale Details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="saleItems">Sale Items</param>
        /// <param name="parcelItems">Parcel Items</param>
        /// <param name="ownerItems">Owner Items</param>
        /// <param name="userId">User Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F29550_saveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId);

        /// <summary>
        /// Get SeniorExemption Details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29600_GetSeniorExemptionDetails(int eventId, int userId);

        /// <summary>
        /// Get SeniorExemption Code
        /// </summary>
        /// <param name="effectiveDate">Effective Date</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29600_GetSeniorExemptionCode(string effectiveDate);

        /// <summary>
        /// save SeniorExemption Details
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <param name="seniorExemptDetails">Senior Exempt Details</param>
        /// <param name="userId">User Id</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F29600_saveSeniorExemptionDetails(int eventId, string seniorExemptDetails, int userId);

        /// <summary>
        /// Get TIFDistrict Details
        /// </summary>
        /// <param name="TIFId">TIF Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F27081_GetTIFDistrictDetails(int TIFIdDistId);


        /// <summary>
        /// F27080_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFId">TIfID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F27081_SaveTIFDistrictDetails(int? TIFIdDistId, string TIFDetails, int userId);

        /// <summary>
        /// F27081_s the delete TIF district details.
        /// </summary>
        /// <param name="TIFIdDistId">The TIF id dist id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        string F27081_DeleteTIFDistrictDetails(int TIFIdDistId, int userId, bool IsReadyToDelete);

        /// <summary>
        /// F27081_s the get TIF combo box details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F27081_GetTIFComboBoxDetails(int RollYear);

        /// <summary>
        /// Get AglandDetails
        /// </summary>
        /// <param name="AglandID">AglandID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F34100_GetAglandDetails(int AglandID);


        /// <summary>
        /// F34100_SaveAglandDetails
        /// </summary>
        /// <param name="AglandId">AglandID</param>
        /// <param name="AglandDetails">AglandDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F34100_SaveAglandDetails(int? AglandID, string AglandDetails, int userId);


        /// <summary>
        /// F39100s the delete Agland details.
        /// </summary>
        /// <param name="AglandId">The Agland id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F34100_DeleteAglandDetails(int Agland, int userId);


        /// <summary>
        /// Get TopDollarDetails
        /// </summary>
        /// <param name="AglandID">TopDollarID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F34110_GetTopDollarDetails(int TopDollarID);


        /// <summary>
        /// F34100_SaveTopDollarDetails
        /// </summary>
        /// <param name="AglandId">TopDollarID</param>
        /// <param name="AglandDetails">TopDollarDetails</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F34110_SaveTopDollarDetails(int? TopDollarID, string TopDollarDetails, int userId);


        /// <summary>
        /// F39110s the delete TopDollar details.
        /// </summary>
        /// <param name="TopDollarId">The TopDollar id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F34110_DeleteTopDollarDetails(int TopDollarId, int userId);

        /// <summary>
        /// Calculate non crop Top Dollar
        /// </summary>
        /// <param name="CropDollar">CropDollar</param>
        /// <param name="CountyFactor">CountyFactor</param>
        /// <returns>string</returns>
        [OperationContract]
        string F34110_CropTopDollar(decimal CropDollar, decimal CountyFactor);


        /// <summary>
        /// Get TIFEvent Details
        /// </summary>
        /// <param name="EventId">Event Id</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29660_GetTIFEventDetails(int EventId, int userId);

        /// <summary>
        /// F27080_SaveTIFEvent
        /// </summary>
        /// <param name="EventId">EventID</param>
        /// <param name="TIFId">TIfID</param>
        /// <param name="BaseValue">BaseValue</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F29660_SaveTIFEventDetails(int? EventId, int TIfId, decimal BaseValue, int userId);



        #region F35102 Neighborhood Configuration

        #region Get Neighborhood Cfg Details

        /// <summary>
        /// Get the Receipt Header
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the Receipt header.
        /// </returns>
        [OperationContract]
        string GetNeighborhoodCfgDetails(int nbhdId);

        #endregion Get Neighborhood Cfg Details

        #region Get Neighborhood Cfg Choice

        /// <summary>
        /// Get the Cfg Choice
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="nbhdCfgId">The NBHD CFG id.</param>
        /// <returns>1</returns>
        [OperationContract]
        string GetNeighborhoodCfgChoice(int nbhdId, int nbhdCfgId);

        #endregion Get Neighborhood Cfg Choice

        #region Save Neighborhood Cfg Details

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="neighborhoodConfigId">The Neighborhood Config id.</param>
        /// <param name="neighborhoodConfigDetails">The Neighborhood Config number.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F35102_SaveNeighborhoodCfgDetails(int neighborhoodConfigId, string neighborhoodConfigDetails, int userId);

        #endregion Save Neighborhood Cfg Details

        #endregion F35102 Neighborhood Configuration

        /// <summary>
        /// Lists the neighborhood parcel locks.
        /// </summary>
        /// <param name="neighborId">The neighbor id.</param>
        /// <returns>String or dataset value in string</returns>
        [OperationContract]
        string ListNeighborhoodParcelLocks(int neighborId);

        /// <summary>
        /// Updates the parcel locking details.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="valueLock">The value lock.</param>
        /// <param name="adminLock">The admin lock.</param>
        /// <param name="lockAppraisal">The lock appraisal.</param>
        /// <param name="primaryId">The primary id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string</returns>
        [OperationContract]
        string UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int primaryId, int userId);

        /// <summary>
        /// F8000_s the get G doc business.
        /// </summary>
        /// <returns>String or dataset value in string</returns>
        [OperationContract]
        string F8000_GetGDocBusiness();

        /// <summary>
        /// F8000_s the get G doc diameter.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>String or dataset value in string</returns>
        [OperationContract]
        string F8000_GetGDocDiameter(int featureClassId);

        /// <summary>
        /// F8000_s the get G doc property reference.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="refField">The ref field.</param>
        /// <returns>String or dataset value in string</returns>
        [OperationContract]
        string F8000_GetGDocPropertyReference(int featureClassId, string refField);

        /// <summary>
        /// F8000_s the get G doc user.
        /// </summary>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8000_GetGDocUser();

        /// <summary>
        /// F8016_s the get stoppage event details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8016_GetStoppageEventDetails(int eventId);

        /// <summary>
        /// F8016_s the save stoppage event details.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8016_SaveStoppageEventDetails(string eventItems, int userId);

        /// <summary>
        /// F8040_s the delete time.
        /// </summary>
        /// <param name="timeId">The time id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8040_DeleteTime(int timeId, int userId);

        /// <summary>
        /// F8040_s the check event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F8040_CheckEventId(int formId, int keyId);

        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8040_ListTimeInformation(int formId, int keyId);

        /// <summary>
        /// F8040_s the list time resource information.
        /// </summary>
        /// <param name="isactive">The isactive.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8040_ListTimeResourceInformation(int isactive);

        /// <summary>
        /// F8040_s the save time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8040_SaveTime(string timeDetails, int userId);

        /// <summary>
        /// F8040_s the update time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8040_UpdateTime(string timeDetails, int userId);

        /// <summary>
        /// F8042_s the get time footer details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8042_GetTimeFooterDetails(int eventId, int formId);

        /// <summary>
        /// F8044_s the delete material item.
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Intgere value</returns>
        [OperationContract]
        int F8044_DeleteMaterialItem(int materialId, int userId);

        /// <summary>
        /// F8044_s the list material details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8044_ListMaterialDetails(int formId, int keyId);

        /// <summary>
        /// F8044_s the type of the list materials resource.
        /// </summary>
        /// <param name="flagActiveAndAll">The flag active and all.</param>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8044_ListMaterialsResourceType(int flagActiveAndAll, int eventId);

        /// <summary>
        /// F8044_s the save material details.
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8044_SaveMaterialDetails(string materialItems, int userId);

        /// <summary>
        /// F8044_s the update material details.
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8044_UpdateMaterialDetails(string materialItems, int userId);

        /// <summary>
        /// F8046_s the get materials footer details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8046_GetMaterialsFooterDetails(int eventId, int formId);

        /// <summary>
        /// F8056_s the delete inspection details.
        /// </summary>
        /// <param name="inspectionId">The inspection id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F8056_DeleteInspectionDetails(int inspectionId, int userId);

        /// <summary>
        /// F8056_s the list inspection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8056_ListInspectionDetails(int eventId);

        /// <summary>
        /// F8056_s the save inspection details.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8056_SaveInspectionDetails(string eventItems, int userId);

        /// <summary>
        /// F8056_s the update inspection details.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8056_UpdateInspectionDetails(string eventItems, int userId);

        /// <summary>
        /// F84121_s the delete sanitary manhole properties.
        /// </summary>
        /// <param name="manholeId">The manhole id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84121_DeleteSanitaryManholeProperties(int manholeId, int userId);

        /// <summary>
        /// F84121_s the get sanitary manhole properties.
        /// </summary>
        /// <param name="manholeId">The manhole id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84121_GetSanitaryManholeProperties(int manholeId);

        /// <summary>
        /// F84121_s the save sanitary manhole properties.
        /// </summary>
        /// <param name="manholeId">The manhole id.</param>
        /// <param name="sanitaryManholeProperties">The sanitary manhole properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F84121_SaveSanitaryManholeProperties(int manholeId, string sanitaryManholeProperties, int userId);

        /// <summary>
        /// F84122_s the get sanitary manhole location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84122_GetSanitaryManholeLocation(int keyId);

        /// <summary>
        /// F84122_s the save sanitary manhole location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryManholeLocation">The sanitary manhole location.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F84122_SaveSanitaryManholeLocation(int keyId, string sanitaryManholeLocation, int userId);

        /// <summary>
        /// F84123_s the delete sanitary pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84123_DeleteSanitaryPipeProperties(int pipeId, int userId);

        /// <summary>
        /// F84123_s the get sanitary pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84123_GetSanitaryPipeProperties(int pipeId);

        /// <summary>
        /// F84123_s the save sanitary pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="sanitaryPipeProperties">The sanitary pipe properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F84123_SaveSanitaryPipeProperties(int pipeId, string sanitaryPipeProperties, int userId);

        /// <summary>
        /// F84124_s the get sanitary pipe location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84124_GetSanitaryPipeLocation(int keyId, int formId);

        /// <summary>
        /// F84124_s the save sanitary pipe location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="sanitaryPipeLocation">The sanitary pipe location.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Intgere value</returns>
        [OperationContract]
        int F84124_SaveSanitaryPipeLocation(int keyId, string sanitaryPipeLocation, int userId);

        /// <summary>
        /// F84721_s the delete water valve properties.
        /// </summary>
        /// <param name="valveId">The valve id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84721_DeleteWaterValveProperties(int valveId, int userId);

        /// <summary>
        /// F84721_s the get water valve properties.
        /// </summary>
        /// <param name="valveId">The valve id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84721_GetWaterValveProperties(int valveId);

        /// <summary>
        /// F84721_s the save water valve properties.
        /// </summary>
        /// <param name="valveId">The valve id.</param>
        /// <param name="waterValveProperties">The water valve properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F84721_SaveWaterValveProperties(int valveId, string waterValveProperties, int userId);

        /// <summary>
        /// F84722_s the get water valve location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84722_GetWaterValveLocation(int keyId, int formId);

        /// <summary>
        /// F84722_s the save water valve location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="waterValveLocation">The water valve location.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F84722_SaveWaterValveLocation(int keyId, string waterValveLocation, int formId, int userId);

        /// <summary>
        /// F84723_s the check main valve id.
        /// </summary>
        /// <param name="mainValveId">The main valve id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F84723_CheckMainValveId(int mainValveId);

        /// <summary>
        /// F84723_s the delete water hydrant properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84723_DeleteWaterHydrantProperties(int hydrantId, int userId);

        /// <summary>
        /// F84723_s the get water hydrant properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84723_GetWaterHydrantProperties(int hydrantId);

        /// <summary>
        /// F84723_s the save water hydrant properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="waterHydrantPropties">The water hydrant propties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F84723_SaveWaterHydrantProperties(int hydrantId, string waterHydrantPropties, int userId);

        /// <summary>
        /// F84725_s the delete water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84725_DeleteWaterPipeProperties(int pipeId, int userId);

        /// <summary>
        /// F84725_s the get water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84725_GetWaterPipeProperties(int pipeId);

        /// <summary>
        /// F84725_s the save water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeProperties">The water pipe properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F84725_SaveWaterPipeProperties(int pipeId, string waterPipeProperties, int userId);

        /// <summary>
        /// F84726_s the get water pipe location.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F84726_GetWaterPipeLocation(int pipeId);

        /// <summary>
        /// F84726_s the save water pipe location.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeLocation">The water pipe location.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F84726_SaveWaterPipeLocation(int pipeId, string waterPipeLocation, int userId);

        /// <summary>
        /// Gets the system id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <returns>The System Id.</returns>
        [OperationContract]
        int GetSystemId(int userId, int formNumber);

        /// <summary>
        /// F8901_s the get work order engine.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <param name="isopen">The isopen.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8901_GetWorkOrderEngine(int systemId, int isopen);

        /// <summary>
        /// F8901_s the type of the get work order.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8901_GetWorkOrderType(int systemId);

        /// <summary>
        /// F8901_s the save work order engine.
        /// </summary>
        /// <param name="workOrderItems">The work order items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8901_SaveWorkOrderEngine(string workOrderItems, int userId);

        /// <summary>
        /// F8902_s the delete.
        /// </summary>
        /// <param name="workId">The work id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8902_Delete(int workId, int userId);

        /// <summary>
        /// F8902_s the get header.
        /// </summary>
        /// <param name="workId">The work id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8902_GetHeader(int workId);

        /// <summary>
        /// F8902_s the save header.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F8902_SaveHeader(string header, int userId);

        /// <summary>
        /// F8904_s the get event grid details.
        /// </summary>
        /// <param name="workId">The work id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8904_GetEventGridDetails(int workId);

        /// <summary>
        /// F8910_s the get work order general.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8910_GetWorkOrderGeneral(int workorderId);

        /// <summary>
        /// F8910_s the save work order general.
        /// </summary>
        /// <param name="workOrderGeneral">The work order general.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8910_SaveWorkOrderGeneral(string workOrderGeneral, int userId);

        /// <summary>
        /// F8912_s the get work order call in.
        /// </summary>
        /// <param name="workorderId">The workorder id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8912_GetWorkOrderCallIn(int workorderId);

        /// <summary>
        /// F8912_s the save work order call in.
        /// </summary>
        /// <param name="workOrderCall">The work order call.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F8912_SaveWorkOrderCallIn(string workOrderCall, int userId);

        /// <summary>
        /// F9001_s the get next working day.
        /// </summary>
        /// <returns>Date time</returns>
        [OperationContract]
        DateTime F9001_GetNextWorkingDay();

        /// <summary>
        /// F9002_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9002_GetUserDetails(int applicationId);

        /// <summary>
        /// F9008_s the get report details.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9008_GetReportDetails(int userId);

        /// <summary>
        /// F9008_s the save report details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="printerConf">The printer conf.</param>
        [OperationContract]
        void F9008_SaveReportDetails(int userId, string printerConf);

        /// <summary>
        /// F9013_s the check next number.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <returns>dataset value in string formate</returns>
        [OperationContract]
        System.Data.DataSet F9013_CheckNextNumber(int rollYear, int nextNum, string formula);

        /// <summary>
        /// F9013_s the list next number configuration.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9013_ListNextNumberConfiguration(int rollYear, int userId);

        /// <summary>
        /// F9013_s the list next number roll year.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9013_ListNextNumberRollYear(int userId);

        /// <summary>
        /// F9013_s the update next number config details.
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F9013_UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId);

        /// <summary>
        /// F9015_s the delete query.
        /// </summary>
        /// <param name="sqlId">The SQL id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F9015_DeleteQuery(int sqlId, int userId);

        /// <summary>
        /// F9033_s the get default layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9033_GetDefaultLayout(int queryViewId);


        /// <summary>
        /// F9033_s the get system snap shot record set.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <param name="masterFormNO">The master form NO.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">The key id collection.</param>
        /// <param name="isFilter">The is filter.</param>
        /// <returns>dataset value in string formate</returns>
        [OperationContract]
        DataSet F9033_GetSystemSnapShotRecordSet(int systemSnapShotId, int masterFormNO, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter);

        /// <summary>
        /// F9033_s the list query view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9033_ListQueryView(int formId);

        /// <summary>
        /// F9033_s the list query snap shot.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9033_ListQuerySnapShot(int queryViewId);

        /// <summary>
        /// F9033_s the list query layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset value in string formate</returns>
        [OperationContract]
        string F9033_ListQueryLayout(int queryViewId, int userId);

        /// <summary>
        /// F9033_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotxml">The system snap shotxml.</param>
        /// <returns>Intgere Value</returns>
        [OperationContract]
        int F9033_InsertSnapShotItems(int? userId, string systemSnapShotxml);

        /// <summary>
        /// F9033_s the query engine.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet F9033_QueryEngine(int queryViewId);

        /// <summary>
        /// F9033_s the get snap shot record set.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet F9033_GetSnapShotRecordSet(int snapShotId, int queryViewId);

        /// <summary>
        /// F9033_s the get system snapshot count.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9033_GetSystemSnapshotCount(int systemSnapShotId);

        /// <summary>
        /// F9039s the list query view column.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9039ListQueryViewColumn(int queryViewId);

        /// <summary>
        /// F9039s the get command result.
        /// </summary>
        /// <param name="replaceId">The replace ID.</param>
        /// <param name="commandResult">The command result.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9039GetCommandResult(int replaceId, string commandResult);

        /// <summary>
        /// F9039s the update query data.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="keyField">The key field.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="updateField">The update field.</param>
        /// <param name="doprocessValue">The doprocess value.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9039UpdateQueryData(int queryViewId, string keyField, string keyId, string updateField, int doprocessValue, int userId);

        /// <summary>
        /// F9038_s the delete layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout ID.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F9038_DeleteLayoutInformation(int queryLayoutId, int userId);

        /// <summary>
        /// F9038_s the load layout information.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9038_LoadLayoutInformation(int queryViewId, int userId);

        /// <summary>
        /// F9038_s the save layout information.
        /// </summary>
        /// <param name="queryLayoutId">The query layout id.</param>
        /// <param name="layoutManagement">The layout management.</param>
        /// <param name="layoutxmlValue">The layoutxml value.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F9038_SaveLayoutInformation(int queryLayoutId, string layoutManagement, string layoutxmlValue, int userId);

        /// <summary>
        /// F9060_s the delete audit configuration.
        /// </summary>
        /// <param name="auditTableId">The audit table id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F9060_DeleteAuditConfiguration(int auditTableId, int userId);

        /// <summary>
        /// F9060_s the list auditing columns.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9060_ListAuditingColumns(string tableName);

        /// <summary>
        /// F9060_s the list auditing tables.
        /// </summary>
        /// <param name="tableType">Type of the table.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9060_ListAuditingTables(string tableType);

        /// <summary>
        /// F9060_s the save audit configuration.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="auditColumns">The audit columns.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F9060_SaveAuditConfiguration(string tableName, string auditColumns, int userId);

        /// <summary>
        /// F9075_s the get comment.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9075_GetComment(int keyId, int formId);

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments, int userId);

        /// <summary>
        /// F9503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9503_GetSubFundItems(string subFund, short rollYear);

        /// <summary>
        /// F9503_s the get sub fund management details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9503_GetSubFundManagementDetails(int? subFundId);

        /// <summary>
        /// F9600_s the list searchresult.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9600_ListSearchresult(string searchValue, int appId);

        /// <summary>
        /// F9600_s the list sort result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <param name="searchOrder">if set to <c>true</c> [search order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9600_ListSortResult(string searchValue, int appId, bool searchOrder, bool groupOrder);

        /// <summary>
        /// Gets the name of the account.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetAccountName(int accountId);

        /// <summary>
        /// Gets the account selection data.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="objectname">The objectname.</param>
        /// <param name="line">The line.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="desciption">The desciption.</param>
        /// <param name="iscash">The iscash.</param>
        /// <returns>The account selection dataset string.</returns>
        [OperationContract]
        string GetAccountSelectionData(string subFund, string bars, string functionName, string objectname, string line, int rollYear, string desciption, int iscash);

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetAffidavitStatementId(int formId, string orderField, string orderBy);

        /// <summary>
        /// Gets the name of the attachement function.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetAttachementFunctionName(int formId);

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int GetAttachmentCount(int formId, int receiptId, int userId);

        /// <summary>
        /// Gets the attachment items.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetAttachmentItems(int formId, int keyId, int userId);

        /// <summary>
        /// Gets the state of the authentication.
        /// </summary>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetAuthenticationState();

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int GetAutoPrintStatus(int formId, int userId);

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetComments(int keyId, int formId, int userId);

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetCommentsCount(int formId, int keyId, int userId);

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetConfigDetails(string configName);

        /// <summary>
        /// Gets the config information.
        /// </summary>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetConfigInformation();

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="msversionId">The msversion id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetConnectionString(int msversionId);

        /// <summary>
        /// Gets the county configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetCountyConfiguration(int applicationId, int userId);

        /// <summary>
        /// Gets the deposit history search result.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetDepositHistorySearchResult(int form, string whereCondnSql);

        /// <summary>
        /// Gets the name of the district.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetDistrictName(int districtId);

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetDistrictSelection(int exciseRateId);

        /// <summary>
        /// Gets the error engine.
        /// </summary>
        /// <param name="errorTypeId">The error type id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetErrorEngine(int errorTypeId);

        /// <summary>
        /// Gets the event engine data header.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetEventEngineDataHeader(int featureClassId, int featureId);

        /// <summary>
        /// Gets the event engine event properties.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetEventEngineEventProperties(int eventId);

        /// <summary>
        /// Gets the excise district copy.
        /// </summary>
        /// <param name="exciserateId">The exciserate id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseDistrictCopy(int exciserateId);

        /// <summary>
        /// Gets the type of the excise individual.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseIndividualType();

        /// <summary>
        /// Gets the excise tax affidavit calulate amount due.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount);

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxAffidavitDetails(int statementId);

        /// <summary>
        /// Gets the excise tax affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxAffidavitStatus(int statementId, int treasurerStatus);

        /// <summary>
        /// Gets the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxRate(int exciseRateId);

        /// <summary>
        /// Gets the excise tax receipt.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxReceipt(int statementId);

        /// <summary>
        /// Gets the excise tax statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetExciseTaxStatement(int statementId);

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="userId">userId</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetFilePath(string source, int formId, int keyId, string extension, int userId);

        /// <summary>
        /// F9005_s the get original file path.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <returns>FilePath</returns>
        [OperationContract]
        string F9005_GetOriginalFilePath(int fileId, int userId);

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetFormDetails(int form, int userId);

        /// <summary>
        /// Gets the parcel details.
        /// </summary>
        /// <param name="form">The key Id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetParcelDetails(int keyID, bool IsNext);

        /// <summary>
        /// Gets the translated form details.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string GetTranslatedFormDetails(int formNo, string keyValue);

        /// <summary>
        /// F9002_s the get form management details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9002_GetFormManagementDetails(int form, int userId);

        /// <summary>
        /// Gets the form items.
        /// </summary>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetFormItems();

        /// <summary>
        /// Gets the form permissions.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetFormPermissions(int userId, int applicationId);

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetFormTitle(int formId);

        /// <summary>
        /// Gets the G doc comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetGDocComment(int eventId);

        /// <summary>
        /// Gets the G doc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetGDocEventHeader(int eventId);

        /// <summary>
        /// Gets the group details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetGroupDetails(int userId);

        /// <summary>
        /// Gets the group permission details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetGroupPermissionDetails(int userId);

        /// <summary>
        /// Gets the interest amount.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>decimal value</returns>
        [OperationContract]
        decimal GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount);

        /// <summary>
        /// Gets the journal entry details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetJournalEntryDetails(int receiptId);

        /// <summary>
        /// Gets the type of the linear event.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetLinearEventType(int eventId);

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMasterNameSearch(string lastName, string firstName, string address);

        /// <summary>
        /// Gets the menu items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        System.Data.DataSet GetMenuItems(int userId, int applicationId);

        /// <summary>
        /// Gets the min tax due.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <returns>Decimal Value</returns>
        [OperationContract]
        decimal GetMinTaxDue(int statmentId, string interestDate);

        /// <summary>
        /// Gets the mortgage import statement.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="nextAvailableRecord">if set to <c>true</c> [next available record].</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMortgageImportStatement(int importId, bool nextAvailableRecord);

        /// <summary>
        /// Gets the mortgage import statement ids.
        /// </summary>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetMortgageImportStatementIds();

        /// <summary>
        /// Gets the mortgage import template details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMortgageImportTemplateDetails();

        /// <summary>
        /// Gets the mortgage template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMortgageTemplate(int templateId);


        /// <summary>
        /// Gets the Permit Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPermitImportTemplate(int templateId);

        /// <summary>
        /// Gets the income Source Detail.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSource id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetIncomeSourceDetail(int IncomeSourceID);

        /// <summary>
        /// Gets the MAD Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMADImportTemplate(int templateId);

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetOwnerDetails(int ownerId);

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPayment(int ppaymentId);

        /// <summary>
        /// Gets the multiple payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMultiplePayment(string ppaymentId);

        /// <summary>
        /// Gets the payment items details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPaymentItemsDetails();

        ///<summary>
        ///Get the Payee Detail
        /// </summary>
        /// <param name="PayeeID">The PayeeID</param>
        /// <returns>String or Dataset</returns>
        [OperationContract]
        string F1019_GetPayeeDetails(int PayeeID);

        /// <summary>
        /// Gets the type of the point event.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPointEventType(int eventId);

        /// <summary>
        /// Gets the post id details.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPostIdDetails(int postId);

        /// <summary>
        /// Gets the program id.
        /// </summary>
        /// <param name="fileTypeId">The file type id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetProgramId(int fileTypeId);

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetQueryResult(int queryId, string orderByCondn);

        /// <summary>
        /// Gets the query utility list.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetQueryUtilityList(int formId);

        /// <summary>
        /// Gets the real estate statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetRealEstateStatement(int statementId);

        /// <summary>
        /// Gets the real estate statement count.
        /// </summary>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int GetRealEstateStatementCount();

        /// <summary>
        /// Gets the real estate statement ids.
        /// </summary>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetRealEstateStatementIds(string orderField, string orderBy);

        /// <summary>
        /// Gets the receipt details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetReceiptDetails(int receiptId);

        #region F15100 Receipt Header Details

        #region GetReceiptHeaderDetails

        /// <summary>
        /// GetReceiptHeaderDetails
        /// </summary>
        /// <param name="receiptId">receiptId</param>
        /// <returns>string or dataset</returns>
        [OperationContract]
        string GetReceiptHeaderDetails(int receiptId);

        /// <summary>
        /// GetReceiptHeaderDetails
        /// </summary>
        /// <param name="receiptId">receiptId</param>
        /// <returns>string or dataset</returns>
        [OperationContract]
        string GetReceiptListDetails(int receiptId);

        #endregion GetReceiptHeaderDetails

        #region Save Receipt Header

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F15100_SaveReceiptHeaderreceiptNumber(int receiptId, string receiptNumber, int userId);

        #endregion Save Receipt Header

        #endregion F15100 Receipt Header Details

        /// <summary>
        /// Gets the receipt statement header details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetReceiptStatementHeaderDetails(int receiptId);

        /// <summary>
        /// Gets the report details.
        /// </summary>
        /// <param name="reportId">The report id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetReportDetails(int reportId, int userId);

        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSandwichAndItsSliceInformation(int form, int keyId, int userId);

        /// <summary>
        /// Gets the sandwich sub title information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Sandwich And Its Slice Information</returns>
        [OperationContract]
        string GetSandwichSubTitleInformation(int form, int keyId, int userId);

        /// <summary>
        /// Gets the snap shot result.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="orderByCondn">The order by condn.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSnapShotResult(int snapShotId, string orderByCondn);

        /// <summary>
        /// Gets the snapshot utility list.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSnapshotUtilityList(int formId);

        /// <summary>
        /// Gets the SQL category.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSQLCategory();

        /// <summary>
        /// Gets the SQL description.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSQLDescription(int categoryId);

        /// <summary>
        /// Gets the SQL query result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetSQLQueryResult(string sqlQuery);

        /// <summary>
        /// Gets the SQL string.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSQLString(int categoryId, int sqlId);

        /// <summary>
        /// Gets the tender type configuration.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetTenderTypeConfiguration();

        /// <summary>
        /// Gets the user group details.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetUserGroupDetails(int applicationId);

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet GetUserInformation(string userName, int applicationId);

        /// <summary>
        /// Gets the valid receipt test.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetValidReceiptTest(int statementId, DateTime receiptDate);

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetWorkOrderDetails(int featureClassId);

        /// <summary>
        /// Inserts the account.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="errorTypeId">The error type id.</param>
        [OperationContract]
        void InsertAccount(int userId, int errorTypeId);

        /// <summary>
        /// Inserts the error engine.
        /// </summary>
        /// <param name="errorDate">The error date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userIP">The user IP.</param>
        /// <param name="errorTypeId">The error type id.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="comment">The comment.</param>
        [OperationContract]
        void InsertErrorEngine(string errorDate, int userId, string userIP, int errorTypeId, string parameter, string comment);

        /// <summary>
        /// Inserts the G doc event engine data.
        /// </summary>
        /// <param name="eventEngineInsertData">The event engine insert data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int InsertGDocEventEngineData(string eventEngineInsertData, int userId);

        /// <summary>
        /// Gets the G doc event engine feature class id.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int GetGDocEventEngineFeatureClassId(int featureId);

        /// <summary>
        /// Inserts the group details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="userGroup">The user group.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string InsertGroupDetails(int groupId, string groupName, string description, string userGroup, int userId);

        /// <summary>
        /// Inserts the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="queryName">Name of the query.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="userWhereCondition">The user where condition.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int InsertQueryUtility(int queryId, string queryName, int formId, string description, int userId, string whereCondition, string userWhereCondition, int overrideValue);

        /// <summary>
        /// Inserts the reverse GL post.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void InsertReverseGLPost(int postId, int userId);

        /// <summary>
        /// Inserts the snapshot utility.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="snapshotFormId">The snapshot form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <returns>Intgere value</returns>
        [OperationContract]
        int InsertSnapshotUtility(int snapshotId, string snapshotName, int snapshotFormId, string description, int recordCount, int userId, int overrideValue, string keyIds);

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListAccountNames();

        /// <summary>
        /// Lists the deposit history details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListDepositHistoryDetails();

        /// <summary>
        /// Lists the event engine detail types.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListEventEngineDetailTypes();

        /// <summary>
        /// Lists the event engine TV details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListEventEngineTVDetails(int eventId);

        /// <summary>
        /// Lists the event type status details.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListEventTypeStatusDetails(int featureClassId);

        /// <summary>
        /// Lists the excise rate district.
        /// </summary>
        /// <param name="district">The district.</param>
        /// <param name="year">The year.</param>
        /// <param name="description">The description.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListExciseRateDistrict(string district, int year, string description);

        /// <summary>
        /// Lists the excise tax rate.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListExciseTaxRate();

        /// <summary>
        /// Lists the excise tax statement.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListExciseTaxStatement();

        /// <summary>
        /// Lists the G doc event header status.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListGDocEventHeaderStatus(int eventId);

        /// <summary>
        /// Lists the help document form.
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListHelpDocumentForm(string formFile);

        /// <summary>
        /// Lists the history grid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListHistoryGrid(int statementId);

        /// <summary>
        /// Lists the type of the mortgage import file.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListMortgageImportFileType();

        /// <summary>
        /// Lists the mortgage template.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListMortgageTemplate();

        /// <summary>
        /// Lists the type of the Permit import file.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPermitImportFileType();

        /// <summary>
        /// Lists the type of the Unit Terms in income Source File.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListUnitTerms();


        /// <summary>
        /// Lists the type of the District Type file.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListDistrictType();


        /// <summary>
        /// Lists the type of the MAD import Template file.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListMADImportFileType();

        /// <summary>
        /// Lists the type of the Snapshot import Template file.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListSnapshotImportFileType();


        /// <summary>
        /// Lists the next number configuration.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListNextNumberConfiguration();

        /// <summary>
        /// Lists the posting errors.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPostingErrors(int userId);

        /// <summary>
        /// Lists the posting history.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <param name="postTypeId">The post type id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPostingHistory(int count, int postTypeId);

        /// <summary>
        /// Lists the posting queue.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPostingQueue();

        /// <summary>
        /// Lists the post types.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPostTypes(int form);

        /// <summary>
        /// Lists the preview posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListPreviewPosting(DateTime postDate);

        /// <summary>
        /// Lists the query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        System.Data.DataSet ListQuery(int formId, int userId);

        /// <summary>
        /// Lists the receipt items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListReceiptItems(int receiptId);

        /// <summary>
        /// F15101_s the update transaction items.
        /// </summary>
        /// <param name="transactionItems">The transaction items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status</returns>
        [OperationContract]
        int F15101_UpdateTransactionItems(string transactionItems, int userId);

        /// <summary>
        /// Lists the receipt owners.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListReceiptOwners(int receiptId);

        /// <summary>
        /// Lists the snap shot.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet ListSnapShot(int formId);

        /// <summary>
        /// Lists the sort query.
        /// </summary>
        /// <param name="savedQueryId">The saved query id.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet ListSortQuery(int savedQueryId, string orderByCondition, int formId);

        /// <summary>
        /// Lists the sort snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet ListSortSnapShot(int snapShotId, string orderByCondition, int formId);

        /// <summary>
        /// Lists the type of the tender.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListTenderType(bool allowOverUnder);

        /// <summary>
        /// Lists the user names.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string ListUserNames();

        /// <summary>
        /// Loads the event engine data.
        /// </summary>
        /// <param name="featureClassId">The feature class id.</param>
        /// <param name="featureId">The feature id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string LoadEventEngineData(int featureClassId, int featureId);

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="severity">The severity.</param>
        /// <param name="title">The title.</param>
        /// <param name="timeStamp">The time stamp.</param>
        /// <param name="machineName">Name of the machine.</param>
        /// <param name="appDomainName">Name of the app domain.</param>
        /// <param name="processId">The process id.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="managedThreadName">Name of the managed thread.</param>
        /// <param name="win32ThreadId">The win32 thread id.</param>
        /// <param name="message">The message.</param>
        /// <param name="formattedMessage">The formatted message.</param>
        [OperationContract]
        void LogException(int eventId, int priority, string severity, string title, DateTime timeStamp, string machineName, string appDomainName, string processId, string processName, string managedThreadName, string win32ThreadId, string message, string formattedMessage);

        /// <summary>
        /// Mortgages the import check errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="recieptDate">The reciept date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string MortgageImportCheckErrors(int importId, int templateId, string templateName, int typeId, string filePath, DateTime recieptDate, DateTime interestDate, bool payCode, int userId, int rollYear,int firstHalfpaycode, int ppaymentId);

        /// <summary>
        /// Performs the posting.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <param name="selectedPostTypes">The selected post types.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string PerformPosting(DateTime postDate, string selectedPostTypes, int userId);

        /// <summary>
        /// RDLs to code_ delete.
        /// </summary>
        /// <param name="deletexmlString">The deletexml string.</param>
        /// <param name="formId">The form id.</param>
        [OperationContract]
        void RdlToCode_Delete(string deletexmlString, string formId);

        /// <summary>
        /// RDLs to code_ fill combo.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet RdlToCode_FillCombo(string storedProcedureName);

        /// <summary>
        /// RDLs to code_ get.
        /// </summary>
        /// <param name="getxmlString">The getxml string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet RdlToCode_Get(string getxmlString, string formId);

        /// <summary>
        /// RDLs to code_ save.
        /// </summary>
        /// <param name="savexmlString">The savexml string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int RdlToCode_Save(string savexmlString, string formId);

        /// <summary>
        /// Saves the affi davit details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, int userId);

        /// <summary>
        /// Saves the attachments.
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="fileTypeId">The file type id.</param>
        /// <param name="source">The source.</param>
        /// <param name="primary">The primary.</param>
        /// <param name="description">The description.</param>
        /// <param name="eventDate">The event date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="publicValue">The public value.</param>
        /// <param name="isroll">The isroll.</param>
        /// <param name="linktype">The linktype.</param>
        /// <param name="aurl">The aurl.</param>
        /// <param name="pfileid">The pfileid.</param>
        /// <param name="sourceConfig">The config file</param>
        /// <returns>File path data</returns>
        [OperationContract]
        string SaveAttachments(int? fileId, string extension, int formId, int keyId, int fileTypeId, string source, int primary, string description, string eventDate, int userId, int publicValue, int isroll, int linktype, string aurl, int pfileid, string sourceConfig);

        /// <summary>
        /// Create Thumbnails
        /// </summary>
        /// <param name="fileId">The file id.</param>
        /// <param name="userId">The userId.</param>
        [OperationContract]
        void GenerateThumbnail(int? fileId, int userId, string fileIdXml);

        /// <summary>
        /// Saves the auto print.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="autoPrint">if set to <c>true</c> [auto print].</param>
        [OperationContract]
        void SaveAutoPrint(int formId, int userId, bool autoPrint);

        /// <summary>
        /// Saves the comments.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="commentDate">The comment date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="printOnReceipt">The print on receipt.</param>
        /// <param name="publicComment">The public comment.</param>
        /// <param name="ispriority">The ispriority.</param>
        /// <param name="isroll">The isroll.</param>
        /// <param name="commentPriorityId">commentPriorityId</param>
        [OperationContract]
        void SaveComments(int commentId, int formId, int keyId, DateTime commentDate, int userId, string comments, int printOnReceipt, int publicComment, int ispriority, int isroll, int commentPriorityId);

        /// <summary>
        /// Saves the event engine event properties.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveEventEngineEventProperties(string eventItems, int userId);

        /// <summary>
        /// Saves the event engine TV details.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveEventEngineTVDetails(string eventItems, int userId);

        /// <summary>
        /// Saves the excise distrcit copy.
        /// </summary>
        /// <param name="district">The district.</param>
        /// <param name="basedOnYear">The based on year.</param>
        /// <param name="newDistrictYear">The new district year.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int SaveExciseDistrcitCopy(int district, int basedOnYear, int newDistrictYear, int userId);

        /// <summary>
        /// Saves the excise tax rate.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="exciseTaxDetails">The excise tax details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveExciseTaxRate(int exciseRateId, string exciseTaxDetails, int userId);

        /// <summary>
        /// Saves the excise tax receipt.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveExciseTaxReceipt(string statementItems, int userId);

        /// <summary>
        /// Saves the G doc comment.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="comment">The comment.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveGDocComment(int eventId, string comment, int userId);

        /// <summary>
        /// Saves the G doc event header.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveGDocEventHeader(string eventItems, int userId);

        /// <summary>
        /// Saves the group permission details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="formPermissions">The form permissions.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveGroupPermissionDetails(int groupId, string formPermissions, int userId);

        /// <summary>
        /// Saves the type of the linear event.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveLinearEventType(string eventItems, int userId);

        /// <summary>
        /// Saves the mortgage import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="resetErrorCheck">if set to <c>true</c> [reset error check].</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveMortgageImport(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfPayCode, bool resetErrorCheck);

        /// <summary>
        /// Saves the mortgage import entries.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="payCode">The pay code.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="mortgageImportEntries">The mortgage import entries.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveMortgageImportEntries(int importId, int templateId, string templateName, int typeId, string filePath, DateTime receiptDate, DateTime interestDate, bool payCode, int userId, int rollYear, int ppaymentId,int firstHalfpaycode, string mortgageImportEntries);


        /// <summary>
        /// Saves the mortgage import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">The type id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="filePath">The file path.</param>
        /// <param name="statementIdPos">The statement id pos.</param>
        /// <param name="statementIdWid">The statement id wid.</param>
        /// <param name="statementNumPos">The statement num pos.</param>
        /// <param name="statementNumWid">The statement num wid.</param>
        /// <param name="amountPos">The amount pos.</param>
        /// <param name="amountWid">The amount wid.</param>
        /// <param name="commentPos">The comment pos.</param>
        /// <param name="commentWid">The comment wid.</param>
        /// <param name="bankCodePos">The bank code pos.</param>
        /// <param name="bankCodeWid">The bank code wid.</param>
        /// <param name="loanNumPos">The loan num pos.</param>
        /// <param name="loanNumWid">The loan num wid.</param>
        /// <param name="taxPayNamePos">The tax pay name pos.</param>
        /// <param name="taxPayNameWid">The tax pay name wid.</param>
        /// <param name="ParcelNumPos">The parcel num pos.</param>
        /// <param name="ParcelNumWid">The parcel num wid.</param>
        /// <param name="PostTypePos">The post type pos.</param>
        /// <param name="PostTypeWid">The post type wid.</param>
        /// <param name="OwnerIDPos">The owner ID pos.</param>
        /// <param name="OwnerIDWid">The owner ID wid.</param>
        [OperationContract]
        void SaveMortgageImportTemplate(int templateId, string templateName, int typeId, int userId, string description, string filePath, int statementIdPos, int statementIdWid, int statementNumPos, int statementNumWid, int amountPos, int amountWid, int commentPos, int commentWid, int bankCodePos, int bankCodeWid, int loanNumPos, int loanNumWid, int taxPayNamePos, int taxPayNameWid, int ParcelNumPos, int ParcelNumWid, int PostTypePos, int PostTypeWid, int OwnerIDPos, int OwnerIDWid, int CartIdPos, int CartidWid);


        /// <summary>
        /// Saves the permit import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">user id.</param>
        
        /// 
        [OperationContract]
        int SavePermitImportTemplate(int? templateId, string permitImportXML, int userId);

        /// <summary>
        /// Saves the income source import Details.
        /// </summary>
        /// <param name="IncomeSourceID">The IncomeSourceID.</param>
        /// <param name="IncomeSourceItems">IncomeSourceItems.</param>
        /// <param name="typeId">user id.</param>

        /// 
        [OperationContract]
        int SaveIncomeSourceDetails(int? IncomeSourceID, string IncomeSourceItems, int userId);

        /// <summary>
        /// Saves the MAD import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">user id.</param>

        /// 
        [OperationContract]
        int SaveMADImportTemplate(int? templateId, string madImportXML, int userId);

        /// <summary>
        /// Saves the Snapshot import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="typeId">user id.</param>

        /// 
        [OperationContract]
        int SaveSnapshotImportTemplate(int? templateId, string madImportXML, int userId);

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int SavePayment(int ppaymentId, string paymentItems, int userId, int ownerId);

        /// <summary>
        /// Saves the payment items details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsDetails">The payment items details.</param>
        [OperationContract]
        void SavePaymentItemsDetails(int registerId, decimal amount, int userId, string paymentItemsDetails);

        /// <summary>
        /// Saves the type of the point event.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SavePointEventType(string eventItems, int userId);

        /// <summary>
        /// Saves the query.
        /// </summary>
        /// <param name="savedQueryName">Name of the saved query.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="savedQueryNote">The saved query note.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="savedQueryDate">The saved query date.</param>
        /// <param name="savedQuery">The saved query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="canOverride">if set to <c>true</c> [can override].</param>
        /// <returns>dataset</returns>
        [OperationContract]
        System.Data.DataSet SaveQuery(string savedQueryName, int formId, string savedQueryNote, int userId, DateTime savedQueryDate, string savedQuery, string whereCondn, bool canOverride);

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveReceipt(string receiptItems, string paymentItems, int userId);

        /// <summary>
        /// Saves the snap shot.
        /// </summary>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="snapshotNote">The snapshot note.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="snapshotDate">The snapshot date.</param>
        /// <param name="snapshotQuery">The snapshot query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="keyIDs">The key I ds.</param>
        /// <param name="canOverride">if set to <c>true</c> [can override].</param>
        /// <returns>Dataset</returns>
        [OperationContract]
        System.Data.DataSet SaveSnapShot(string snapshotName, int formId, string snapshotNote, int userId, DateTime snapshotDate, string snapshotQuery, string whereCondn, string keyIDs, bool canOverride);

        /// <summary>
        /// Saves the SQL query.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="description">The description.</param>
        /// <param name="statement">The statement.</param>
        /// <param name="moduleId">The module id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int SaveSQLQuery(int categoryId, string description, string statement, int moduleId, int userId, int sqlId);

        /// <summary>
        /// Saves the user details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="nameDisplay">The name display.</param>
        /// <param name="nameFull">The name full.</param>
        /// <param name="nameNet">The name net.</param>
        /// <param name="email">The email.</param>
        /// <param name="active">The active.</param>
        /// <param name="administrator">The administrator.</param>
        /// <param name="applicationId">The application id.</param>
        /// <param name="loginUserId">The login user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string SaveUserDetails(int userId, string nameDisplay, string nameFull, string nameNet, string email, int active, int administrator,int appraiser, int applicationId, int loginUserId);

        /// <summary>
        /// Updates the county config details.
        /// </summary>
        /// <param name="configId">The config id.</param>
        /// <param name="configDescription">The config description.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void UpdateCountyConfigDetails(int configId, string configDescription, int userId);

        /// <summary>
        /// Updates the deposit history.
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void UpdateDepositHistory(int clid, int userId);

        /// <summary>
        /// Updates the event engine TV details.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void UpdateEventEngineTVDetails(string eventItems, int userId);

        /// <summary>
        /// Updates the excise affidavit status.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="treasurerStatus">The treasurer status.</param>
        /// <param name="statusId">The status id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void UpdateExciseAffidavitStatus(int statementId, int treasurerStatus, int statusId, int userId);

        /// <summary>
        /// Updates the journal entry details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptSourceId">The receipt source id.</param>
        /// <param name="journalItems">The journal items.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int UpdateJournalEntryDetails(int statementId, int receiptSourceId, string journalItems);

        /// <summary>
        /// Updates the next number config details.
        /// </summary>
        /// <param name="nextNumId">The next num id.</param>
        /// <param name="nextNum">The next num.</param>
        /// <param name="formula">The formula.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void UpdateNextNumberConfigDetails(int nextNumId, int nextNum, string formula, int userId);

        /// <summary>
        /// Validations the specified user name text.
        /// </summary>
        /// <param name="userNameText">The user name text.</param>
        /// <param name="passwordText">The password text.</param>
        /// <returns>Boolean value</returns>
        [OperationContract]
        bool Validation(string userNameText, string passwordText);

        /// <summary>
        /// wListAddresses
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string wListAddresses();

        /// <summary>
        /// wListStreets.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string wListStreets();

        /// <summary>
        /// F27006_s the check ownership details.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F27006_CheckOwnershipDetails(string ownershipDetails);

        #region M&S RE

        /// <summary>
        /// F36000_s the get house type collection.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F36000_GetHouseTypeCollection(int valueSliceId);

        /// <summary>
        /// F36000_s the get depr percentage.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F36000_GetDeprPercentage(int age, decimal objectCondition, int deprTableId);

        /// <summary>
        /// F36000_s the get depr table name id.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="propertyQuality">The property quality.</param>
        /// <returns>int</returns>
        [OperationContract]
        int F36000_GetDeprTableNameId(int valueSliceId, int propertyQuality);

        /// <summary>
        /// F36000_s the save depreciation details.
        /// </summary>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        [OperationContract]
        int F36000_SaveDepreciationDetails(string depreciationXml, int valueSliceId, int userId);

        #endregion

        /// <summary>
        /// F15110_s the get receipt actions.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F15110_GetReceiptActions(int receiptId);

        /// <summary>
        /// F1557_s the insert refund interest.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        /// <param name="userID">The user ID.</param>
        [OperationContract]
        void F1557_InsertRefundInterest(int receiptID, int userID);

        /// <summary>
        /// F1555_s the get receipt details.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F1555_GetReceiptDetails(int receiptId);

        /// <summary>
        /// Reverse receipt details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="sharedPaymentId">Shared Payment Id</param>
        /// <param name="userId">User ID</param>
        /// <returns>Reverse Payment Details</returns>
        [OperationContract]
        string F1556_ReverseReceiptDetails(int receiptId, int sharedPaymentId, int userId);

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.HillsideZoneCollection F36000_GetHTCXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.FoundationZoneCollection F36000_GetFoundationXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.SeismicZoneCollection F36000_GetSeismicXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace._EnergyZones F36000_GetEnergyZoneXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.WindZoneCollection F36000_GetWindZoneXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.WallEnergyZoneCollection F36000_WallEnergyZoneXml();

        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.Estimate CreateNewEstimate(int ConstType, string Zip);

        #region F25011 Street List Management

        #region Get the Master Street Data

        /// <summary>
        /// To Get Master Street Data.
        /// </summary>
        /// <param name="streetId">StreetId</param>
        /// <returns>Typed DataSet Containing the Master Street data.</returns>
        [OperationContract]
        string F25011_GetMasterStreetList(int streetId);

        #endregion Get the Master Street Data

        #region List Master Street List

        /// <summary>
        /// To List Master Street List.
        /// </summary>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed DataSet Containing the Master Street List details.</returns>
        [OperationContract]
        string F25011_ListMasterStreetList(int streetID, string streetName, string city);

        #endregion List Master Street List

        #region List Street City Directional Suffix

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <returns>Typed Dataset conitaining the Street's City, Directional and Suffixs details</returns>
        [OperationContract]
        string F25011_ListStreetCityDirectionalSuffixDetails();

        #endregion List Street City Directional Suffix

        #region Save Street List Management

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <param name="streetId">Street ID</param>
        /// <param name="streetListMgmt">Street List Management</param>
        /// <param name="userId">UserID</param>
        /// <returns>The current Saved streetId</returns>
        [OperationContract]
        int F25011_SaveStreetListManagement(int streetId, string streetListMgmt, int userId);

        #endregion Save Street List Management

        #region Delete Street List

        /// <summary>
        /// F25011_s the delete street list.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Deleted Flag</returns>
        [OperationContract]
        int F25011_DeleteStreetList(int streetId, int userId);

        #endregion Delete Street List

        #endregion F25011 Street List Management

        #region F2000 ParcelStatus

        #region List ParcelStatus
        /// <summary>
        /// To List the ParcelStatus.
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>Returns Typed Dataset It Contains ParcelStatusTable.</returns>
        [OperationContract]
        string F2000_ListParcelStatus(int parcelId);

        #endregion

        #region F2000 Update Parce Status
        /// <summary>
        /// To update Parcel Status
        /// </summary>
        /// <param name="parcelId">FormParcelID</param>
        /// <param name="description">Description</param>
        /// <param name="parcelType">parcelType</param>
        /// <param name="isexempt">The isexempt.</param>
        /// <param name="isownerPrimary">The isowner primary.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F2000_UpdateParcelStatus(int parcelId, string description, string parcelType, int isexempt, int isownerPrimary, int userId);

        #endregion

        #region F2000 Delete ParcelID[Form.ParcelID]

        /// <summary>
        /// Delete Form ParcelID
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F2000_DeleteParcelStatus(int parcelId, int userId);

        #endregion


        #region List Parcel Status

        /// <summary>
        /// ListRecordLockStatus
        /// </summary>
        /// <param name="formNo">formNo</param>
        /// <param name="keyId">keyId</param>
        /// <returns></returns>

        [OperationContract]
        string ListRecordLockStatus(int formNo, int keyId);


        #endregion

        #endregion

        #region Added Method For thialk
        /// <summary>
        /// Checks the installation1.
        /// </summary>
        /// <param name="checkInstall">The check install.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string CheckInstallation1(string checkInstall);

        /// <summary>
        /// Gets the database schema.
        /// </summary>
        /// <returns>Byte value</returns>
        [OperationContract]
        byte[] GetDatabaseSchema();

        #endregion

        #region F25009 Legal Management

        /// <summary>
        /// F25009_s the get legal management.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F25009_GetLegalManagement(int parcelId, int userId);

        /// <summary>
        /// F25009_s the save legal management.
        /// </summary>
        /// <param name="legalId">The legal id.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Intgere value</returns>
        [OperationContract]
        int F25009_SaveLegalManagement(int legalId, string legalItems, bool isFuturePush, int userId);

        /// <summary>
        /// F25009_s the list subdivision.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F25009_ListSubdivision();

        #endregion

        #region F25003 Situs Management

        #region List Situs Management Details

        /// <summary>
        /// To List Situs Mangement Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="situsId">The situs id.</param>
        /// <returns>Typed Dataset containing the Situs Mangement Details</returns>
        [OperationContract]
        string F25003_ListSitusMangement(int parcelId, int situsId);

        #endregion List Situs Management Details

        #region List Street Details

        /// <summary>
        /// To List Street Details.
        /// </summary>
        /// <returns>Typed Dataset containing the Street Details</returns>
        [OperationContract]
        string F25003_ListStreet();

        #endregion List Street Details

        #region List Unit Type Details

        /// <summary>
        /// To list Unit Type Details.
        /// </summary>
        /// <returns>Typed DataSet containing the Unit Type Details</returns>
        [OperationContract]
        string F25003_ListUnitType();

        #endregion List Unit Type Details

        #region Save Situs Management

        /// <summary>
        /// To Save List Mangement Details.
        /// </summary>
        /// <param name="situsId">The situs id.</param>
        /// <param name="situsItems">The situs items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Intger value containing the new SitusID</returns>
        [OperationContract]
        int F25003_SaveListMangement(int situsId, string situsItems, bool isFuturePush, int userId);

        #endregion Save Situs Management

        #region Delete Situs Management

        /// <summary>
        /// To Delete the Situs management
        /// </summary>
        /// <param name="situsId">situsId</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F25003_DeleteSitusManagement(int situsId, int userId);

        #endregion Delete Situs Management

        #endregion F25003 Situs Management

        #region F3602 Copy or Move Misc Improvements.
        /// <summary>
        /// Get Object Details
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetObjectDetails(int parcelId);

        /// <summary>
        /// Get object type list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetObjectTypesList();

        /// <summary>
        /// Get Value slice List.
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="objectId"></param>
        /// <returns></returns>
        [OperationContract]
        string GetValueSlicesList(int parcelId, int objectId);

        /// <summary>
        /// Get Misc Improvement Details.
        /// </summary>
        /// <param name="valuesliceID"></param>
        /// <returns></returns>
        [OperationContract]
        string GetMiscImprovementsList(int valuesliceID);

        /// <summary>
        /// Move or Copy Misc Improvement Details.
        /// </summary>
        /// <param name="copyMove"></param>
        /// <param name="parcelId"></param>
        /// <param name="isNewObject"></param>
        /// <param name="existingObjectId"></param>
        /// <param name="newObjectTypeId"></param>
        /// <param name="isNewValueslice"></param>
        /// <param name="existingValueslice"></param>
        /// <param name="miscImprovements"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        string F3602_ProcessMiscImprovements(string copyMove, int parcelId, bool isNewObject, int existingObjectId, int newObjectTypeId, bool isNewValueslice, int existingValueslice, string miscImprovements, int userId);
        
        #endregion 

        #region F1060 Suspended Payment Selection

        #region F1060 List Suspended Payment

        /// <summary>
        /// To List Suspended Payment.
        /// </summary>
        /// <param name="SEARCH XML">Search Detail.</param>
        /// <returns>Typed DataSet containing the Suspended Payment Details.</returns>
        [OperationContract]
        string F1060_ListSuspendedPayment(string searchDetail);

        #endregion F1060 List Suspended Payment

        #endregion F1060 Suspended Payment Selection

        #region F9040 SnapShot Utility

        #region F9040_ListBatchButtonSnapShots

        /// <summary>
        /// To List the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="formsSliceNo">The forms slice no.</param>
        /// <returns>Typed DataSet containg the list of F1440 Batch Button SnapShots for Current form slice</returns>
        [OperationContract]
        string F9040_ListBatchButtonSnapShots(int formsSliceNo);

        #endregion F9040_ListBatchButtonSnapShots

        #region F9040_SaveBatchButtonSnapShots

        /// <summary>
        /// To save the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetails">The snap shot details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the snapshot id</returns>
        [OperationContract]
        int F9040_SaveBatchButtonSnapShots(int snapShotId, string snapShotDetails, int userId);

        #endregion F9040_SaveBatchButtonSnapShots

        #region ListSnapShots

        /// <summary>
        /// Lists the SnapShots for the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9040SnapShotUtilityData Dataset</returns>
        [OperationContract]
        string F9040_ListSnapShots(int formId);

        #endregion ListSnapShot

        #region SaveSnapShot

        /// <summary>
        /// F9040_s the save snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotxml">The snap shotxml.</param>
        /// <param name="snapshotItemsxml">The snapshot itemsxml.</param>
        /// <param name="filterXML">Filter XML</param>
        /// <param name="pinType">Pinning Type</param>
        /// <param name="userId">UserID</param>
        /// <param name="parentSnapShotID">Parent Snapshot ID</param>
        /// <returns>the saved snapshotid</returns>
        [OperationContract]
        int F9040_SaveSnapShot(int snapShotId, string snapShotxml, string snapshotItemsxml, string filterXML, string pinType, int userId, int parentSnapShotID);

        #endregion SaveSnapShot

        #region DeleteSnapShot

        /// <summary>
        /// To Delete F040 Snapshot
        /// </summary>
        /// <param name="snapshotId">The snapshotId</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F9040_DeleteSnapShot(int snapshotId, int userId);

        #endregion DeleteSnapShot

        #endregion F9040 SnapShot Utility

        #region F9070 ReportListing

        /// <summary>
        /// F9070s the get report listing details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F9070_GetReportListingDetails(int userId);

        #endregion F9070 ReportListing

        #region F95101 Audit Trail

        /// <summary>
        /// To List Audit Trail records
        /// </summary>
        /// <param name="form">Form ID</param>
        /// <param name="keyId">Key ID</param>
        /// <returns>Typed DataSet Containing the Audit Trail Details</returns>
        [OperationContract]
        string F95101_ListAuditTrail(int form, int keyId);

        #endregion F95101 Audit Trail

        #region F25008 Parcel MiscellaneousComp

        #region Get Parcel Miscellaneous

        /// <summary>
        /// Get the ParcelMiscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <returns>Parcel Miscellaneous</returns>
        [OperationContract]
        string F25008_ParcelMiscellaneousData(int parcelId);

        #endregion Get Parcel Miscellaneous

        #region Get Parcel Miscellaneous Configuration

        /// <summary>
        /// Get the ParcelMiscellaneous Configuration Data
        /// </summary>
        /// <returns>Parcel Miscellaneous Config</returns>
        [OperationContract]
        string F25008_ParcelMiscellaneousConfigData();

        #endregion Get Parcel Miscellaneous Configuration

        #region Save Audit Column Configuration

        /// <summary>
        /// Save the ParcelMiscellaneous Records
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="miscellaneous">miscellaneous</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F25008_SaveParcelMiscellaneous(int parcelId, string miscellaneous, int userId);

        #endregion Save Audit Column Configuration

        #endregion F25008 Parcel MiscellaneousComp

        #region F35101 Neighborhood Group Header

        /// <summary>
        /// F35101_s the get neighborhood group header.
        /// </summary>
        /// <param name="nbhdGroupId">The NBHD group id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F35101_GetNeighborhoodGroupHeader(int nbhdGroupId);

        /// <summary>
        /// F35101_s the save neighborhood group header.
        /// </summary>
        /// <param name="nbhdGroupId">The NBHD group id.</param>
        /// <param name="neighborhoodGroupHeader">The neighborhood group header.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Intgere value</returns>
        [OperationContract]
        int F35101_SaveNeighborhoodGroupHeader(int nbhdGroupId, string neighborhoodGroupHeader, int userId);

        /// <summary>
        /// F35101_s the delete neighborhood group header.
        /// </summary>
        /// <param name="nbhdGroupId">The NBHD group id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F35101_DeleteNeighborhoodGroupHeader(int nbhdGroupId, int userId);

        #endregion

        #region F3040 Zoning

        #region F3040 Get Zoning

        /// <summary>
        /// Used to Get the Zoning Details
        /// </summary>
        /// <returns>Gets Typed DataSet containing the Zoning Details.</returns>
        [OperationContract]
        string F3040_GetZoningDetails();

        #endregion F3040 Get Zoning

        #region F3040 Save Zoning

        /// <summary>
        /// Used to Save the Zoning Details
        /// </summary>
        /// <param name="zoningDetails">The zoning details.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Typed DataSet containing the Zoning Details to Save.</returns>
        [OperationContract]
        int F3040_SaveZoningDetails(string zoningDetails, int userId);

        #endregion F3040 Save Zoning

        #endregion F3040 Zoning

        #region F15035 Suspended Payments

        /// <summary>
        /// F15035s the suspended payments.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>typed dataset containing the suspended payment details</returns>
        [OperationContract]
        string F15035SuspendedPayments(int receiptId);

        /// <summary>
        /// F15035_s the delete suspended payment.
        /// </summary>
        /// <param name="receiptId">The receipt ID.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F15035_DeleteSuspendedPayment(int receiptId, int userId);

        /// <summary>
        /// F15035_s the check suspended accounts.
        /// </summary>
        /// <returns>status id</returns>
        [OperationContract]
        int F15035_CheckSuspendedAccounts();

        #endregion F15035 Suspended Payments

        #region F8062 Components Configuration

        #region List Components Configuration

        /// <summary>
        /// F8062_s the list components configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>string containing the typed dataset of the component configuration</returns>
        [OperationContract]
        string F8062_ListComponentsConfiguration(int applicationId);

        #endregion List Components Configuration

        #region List Feature Class

        /// <summary>
        /// F8062_s the list feature class.
        /// </summary>
        /// <param name="applicationId">applicationID</param>
        /// <returns>string containing the typed dataset of the feature class</returns>
        [OperationContract]
        string F8062_ListFeatureClass(int applicationId);

        #endregion List Feature Clas

        #region Save Components Configuration

        /// <summary>
        /// F8062_s the save components configuration.
        /// </summary>
        /// <param name="componentsConfig">The components config.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F8062_SaveComponentsConfiguration(string componentsConfig, int userId);

        #endregion Save Components Configuration

        #region Delete Components Configuration

        /// <summary>
        /// Deletes the Components Configuration.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F8062_DeleteComponentsConfiguration(int componentId, int userId);

        #endregion

        #endregion F8062 Components Configuration

        #region F8058 Resources Configuration

        #region List Resources Configuration

        /// <summary>
        /// F8058_s the list resources config details.
        /// </summary>
        /// <returns>String</returns>
        [OperationContract]
        string F8058_ListResourcesConfigDetails();

        #endregion List Resources Configuration

        #region Insert Resources Configuration

        /// <summary>
        /// F8058_s the insert reosurces config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="equiptResource">The equipt resource.</param>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F8058_InsertResourcesConfigDetails(int equipmentId, string equiptResource, int applicationId, int userId);

        #endregion Insert Resources Configuration

        #region Delete Resources Configuration

        /// <summary>
        /// F8058_s the delete resources config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F8058_DeleteResourcesConfigDetails(int equipmentId, int userId);

        #endregion Delete Resources Configuration

        #endregion F8062 Resources Configuration

        #region F1013 Unpaid Reciepts.

        /// <summary>
        /// F1013_s the list unpaid receipts.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F1013_ListUnpaidReceipts(int? userId);

        /// <summary>
        /// F1013_s the save batch payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="paymentItemsXml">The payment items XML.</param>
        /// <param name="receiptItemsXml">The receipt items XML.</param>
        /// <returns>Integer value</returns>
        [OperationContract]
        int F1013_SaveBatchPayment(int ppaymentId, int userId, string paymentItemsXml, string receiptItemsXml, string receiptDate);

        #region F1013_ListSnapShotItems

        /// <summary>
        /// To list snap shot items collection.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <returns>Typed dataset containing the snap shot items collection</returns>
        [OperationContract]
        string F1013_ListSnapShotItems(int snapShotId);

        #endregion F1013_ListSnapShotItems

        #region F1013_DeleteReceiptItems

        /// <summary>
        /// F1013_s the delete receipt items.
        /// </summary>
        /// <param name="paymentId">The payment id.</param>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F1013_DeleteReceiptItems(int paymentId, string receiptItems, int userId);

        #endregion F1013_DeleteReceiptItems

        #endregion

        #region F8060 Parts Configuration

        #region List Parts Configuration

        /// <summary>
        /// Lists the Parts Configuration details
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <returns>returns dataset contains Parts Configuration details</returns>
        [OperationContract]
        string F8060_ListPartsConfig(int componentId);

        #endregion

        #region List Components

        /// <summary>
        /// Lists the Components detail
        /// </summary>
        /// <returns>returns dataset contains Components details</returns>
        [OperationContract]
        string F8060_ListComponents();

        #endregion

        #region Save Parts Configuration

        /// <summary>
        /// F8062_s the save Parts configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="partsConfig">The parts config.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F8060_SavePartsConfiguration(int partId, string partsConfig, int userId);

        #endregion Save Parts Configuration

        #region Delete Parts Configuration

        /// <summary>
        /// Deletes the Parts Configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F8060_DeletePartsConfiguration(int partId, int userId);

        #endregion

        #endregion

        #region OwnerStatus

        /// <summary>
        /// To List OwnerStatus Details.
        /// </summary>
        /// <param name="typeId">The type id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>
        /// Typed Dataset containing the OwnerStatus Details
        /// </returns>
        [OperationContract]
        string F9102_GetOwnerStatusDetails(int typeId, int keyId);
        #endregion OwnerStatus

        #region F95005 Reference Data

        #region List Refereence Data

        /// <summary>
        /// To List the Reference Data Details
        /// </summary>
        /// <param name="masterFormNo">masterFormNo</param>
        /// <returns>Typed DataSet containg the Reference Data Details</returns>
        [OperationContract]
        System.Data.DataSet F95005_ListReferenceData(int masterFormNo);

        #endregion List Refereence Data

        #region Save Reference Data

        /// <summary>
        /// To Save the Reference Data Details
        /// </summary>
        /// <param name="referenceData">Xml String containing the Reference Data Details</param>
        /// <param name="deletedData">Xml string containing the deleted data in Reference Data Details.</param>
        /// <param name="tableName">Tabel Name of the Reference Data</param>
        /// <param name="keyColumn">Key Column Name of the Reference Data Table</param>
        /// <param name="userId">UserID</param>
        /// <returns>
        /// Integer Value containing Whther save is performed or Not
        /// if Saved return = 0
        /// else Unsaved return = -1
        /// </returns>
        [OperationContract]
        int F95005_SaveReferenceData(string referenceData, string deletedData, string tableName, string keyColumn, int userId);

        #endregion Save Reference Data

        #endregion F95005 Reference Data

        #region F96000 OwnerManagement

        #region GetOwnerManagementDetails

        /// <summary>
        /// Gets the F96000_GetOwnerDetails
        /// It Returns two table[OwnerDetails,OwnerList]
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>
        /// Type Dataset Returns two table[OwnerDetails,OwnerList]
        /// </returns>
        [OperationContract]
        string F96000_GetOwnerManagementDetails(int ownerId);

        #endregion GetOwnerManagementDetails

        #region ListOwnerStatusType

        /// <summary>
        /// Lists the OwnerStatusType
        /// </summary>    
        /// <returns>String</returns>
        [OperationContract]
        string F96000_ListOwnerStatusType();

        /// <summary>
        /// F96000_s the country combo details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F96000_CountryComboDetails();

        #endregion ListOwnerStatusType

        #region Insert OwnerManagementDetails
        /// <summary>
        /// Inserts the F96000_OwnerManagementDetails
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerDetails">Owner Details</param>
        /// <param name="ownerStatus">ownerStatus</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F96000_InsertOwnerManagementDetails(int ownerId, string ownerDetails, string ownerStatus, int userId);

        #endregion Insert OwnerManagementDetails

        #region DeleteDatas

        /// <summary>
        /// F96000_DeleteData
        /// </summary>
        /// <param name="statusId">statusId</param>
        /// <returns></returns>
        [OperationContract]
        void F96000_DeleteData(int statusId);

        #endregion

        #endregion F96000 OwnerManagement

        #region F36011 Misc Improvement Overview

        #region List Depr Table

        /// <summary>
        /// To List the Depr Table details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        [OperationContract]
        string F36011_ListDeprTable(int valueSliceId);

        #endregion List Depr Table

        #region List Misc Code

        /// <summary>
        ///To List Misc Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed dataset containing the Misc Code Details</returns>
        [OperationContract]
        string F36011_ListMiscCode(int valueSliceId);

        #endregion List Misc Code

        #region List Misc Improvements

        /// <summary>
        /// To List Misc Improvements details.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <returns>Typed dataset containing the Misc Improvements details</returns>
        [OperationContract]
        string F36011_ListMiscImprovements(int miscId);

        #endregion List Misc Improvements

        #region List MICatalog Code

        /// <summary>
        /// To List Catalog Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containg the MICatalog Code Details</returns>
        [OperationContract]
        string F36011_ListCatalogCode(int valueSliceId);

        #endregion List MICatalog Code

        #region Delete MICode

        /// <summary>
        /// To Delete MID in Misc Improvements OverView.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F36011_DeleteMICode(int miscId, int userId);

        #endregion Delete MICode

        #region Save Misc Improvements

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="miscmId">mid</param>
        /// <param name="miscItems">xml string containing the Misc Improvents Overview Details</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer value containing the key id</returns>
        [OperationContract]
        int F36011_SaveMiscImprovement(int miscmId, string miscItems, int userId);

        #endregion Save Misc Improvements

        #region List Qualit Comm

        /// <summary>
        /// F36011_s the list quality comm.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F36011_ListQualityComm();

        #endregion List Qualit Comm

        #region List Qualit Res

        /// <summary>
        /// F36011_s the list quality res.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F36011_ListQualityRes();

        #endregion List Qualit Comm

        #region List Condition

        /// <summary>
        /// F36011_s the list condition.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F36011_ListCondition();

        #endregion List Condition

        #region List DeprFuncCategory

        /// <summary>
        /// F36011_s the list depr func category.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F36011_ListDeprFuncCategory();

        #endregion List DeprFuncCategory

        #region List MiscCatalogChoice

        /// <summary>
        /// F36012_s the list misc catalog choice.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <returns></returns>
        [OperationContract]
        string F36012_ListMiscCatalogChoice(int miscCodeId, int fieldNum);

        #endregion List MiscCatalogChoice

        #region RecalcMisc Improvements

        /// <summary>
        /// To List the RecalcMisc Improvements Table details
        /// </summary>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        [OperationContract]
        string F36011_RecalcMiscImprovement(bool withprimary, int? yearIn, string condition, int? economicLife, int? effectiveAge, decimal? physDeprPerc, decimal? funcDeprPerc, decimal? BaseCost, decimal? physDepr, decimal? funcDepr, int valueSliceId, int miscCodeId);

        #endregion

        #endregion F36011 Misc Improvement Overview

        #region F36010 Misc Improvement Catalog

        /// <summary>
        /// Get Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>1</returns>
        [OperationContract]
        string F36010_GetMiscImprovementCatalog(int miscCodeId);

        /// <summary>
        /// Save Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCatalogItems">miscCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer </returns>
        [OperationContract]
        int F36010_SaveMiscImprovementCatalog(int miscCodeId, string miscCatalogItems, int userId, string miscCatalogChoiceItems);

        /// <summary>
        /// Delete Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F36010_DeleteMiscImprovementCatalog(int miscCodeId, int userId);

        /// <summary>
        /// Check Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCode">miscCode</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F36010_CheckMiscImprovementCatalog(int miscCodeId, string miscCode, int rollYear);

        /// <summary>
        /// F36010_s the list depr table.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>deprTable</returns>
        [OperationContract]
        string F36010_ListDeprTable(int rollYear);

        #endregion F36010 Misc Improvement Catalog

        #region  F36001 Marshal And Swift Commercial

        #region Get Marshal And Swift Commercial

        /// <summary>
        /// To get marshal and swift commercial details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containing the Marshal And Swift Commercial details</returns>
        [OperationContract]
        string F36001_GetMarshalAndSwiftCommercial(int valueSliceId);

        #endregion Get Marshal And Swift Commercial

        #region Save Marshal And Swift Commercial

        /// <summary>
        /// To save marshal and swift commercial.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="estimateDetails">The estimate details.</param>
        /// <param name="occupancyDetails">The occupancy details.</param>
        /// <param name="componentDetails">The component details.</param>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer Value</returns>
        [OperationContract]
        int F36001_SaveMarshalAndSwiftCommercial(int valueSliceId, string estimateDetails, string occupancyDetails, string componentDetails, string depreciationXml, int userId);

        #endregion Save Marshal And Swift Commercial

        #endregion F36001 Marshal And Swift Commercial

        #region F9080 RollYearManagement

        /// <summary>
        /// F9080_s the get Roll Year Management.
        /// </summary>
        /// <param name="RollYear">The RollYear.</param>
        /// <param name="UserId">The User ID.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F9080_GetRollYearManagement(short rollYear, int userId);


        /// <summary>
        /// F9080_s the list Roll Year Management.
        /// </summary>
        /// <param name="UserId">The User ID.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F9080_ListRollYearManagement(int userId);


        /// <summary>
        /// F9080_s the Exec Roll Year Management.
        /// </summary>
        /// <param name="RollOverId">The RollOver ID.</param>
        /// <param name="UserId">The User ID.</param>
        [OperationContract]
        string F9080_ExecRollYearStep(short rollOverId, int userId);

        #endregion F9080 RollYearManagement

        #region F2550 TaxRollCorrection

        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelId">The parcel ID.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F2550_ListParcelDetails(string parcelId, string scheduleId, string stateId, string centralXmlIds);

        /// <summary>
        /// F2550_s the exec tax roll corrections.
        /// </summary>
        /// <param name="parcelItems">The parcel items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F2550_ExecTaxRollCorrections(string parcelItems, int userId);

        /// <summary>
        /// F2550_s the list correction code.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F2550_ListCorrectionCode();

        #region List Attachment Details

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        [OperationContract]
        string F2550_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId);

        #endregion List Attachment Details

        #region Delete Attachment Details

        /// <summary>
        /// Delete attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        [OperationContract]
        void F2550_DeleteAttachmentDetails(int formId);

        #endregion Delete Attachment Details

        #region Insert CorrectionParcelsTemp Table
        /// <summary>
        /// F2550_s the save correction parcels temp.
        /// </summary>
        /// <param name="correctionId">The correction id.</param>
        /// <param name="correctionTempItems">The correction temp items.</param>
        /// <param name="corrParcelIds">The corr parcel ids.</param>
        /// <param name="statementsIds">The statements ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F2550_SaveCorrectionParcelsTemp(int? correctionId, string correctionTempItems, string corrParcelIds, string statementsIds, int userId);
        #endregion

        #endregion F2550TaxRollCorrection

        #region EditStatemenDetails


        /// <summary>
        /// F2551_s the list EditStatement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        [OperationContract]
        string F2551_ListEditStatementDetails(int parcelId, short typeId, int statementId, int ownerId, int userId);

        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="ChangeXML">The Change XML.</param>
        /// <param name="ItemsXML">The ItemsXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        [OperationContract]
        string F2551_LoadStatementGridDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string changeXML, int userId);

        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        [OperationContract]
        int SaveEditStatementtDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string headerXML, int userId);

        #endregion

        #region StatemenSelectionDetails


        /// <summary>
        /// F2552_s the list StatementSelection details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        [OperationContract]
        string F2552_ListStatementSelectionDetails(int parcelId, int typeId, int userId);

        #endregion StatemenSelectionDetails

        #region F1401 ParcelSection

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        [OperationContract]
        string F1401_GetParcelType(int? parcelId);

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        [OperationContract]
        string F1401_GetSearchResult(string parcelSearchXml);

        #endregion F1401 ParcelSection

        #region F3001 Object Management

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F3001_GetObjectDetail(int objectId);

        /// <summary>
        /// F3001_s the save object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectItems">The objectItems.</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        [OperationContract]
        int F3001_SaveObjectManagement(int objectId, string objectItems, int userId);

        /// <summary>
        /// F3001_s the delete object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F3001_DeleteObjectManagement(int objectId, int userId);

        /// <summary>
        /// F3001_s the get parcel description.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F3001_GetParcelDescription(int parcelId);

        /// <summary>
        /// F3001_s the copy object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectXml">The object XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F3001_CopyObject(int objectId, string objectXml, int userId);

        #endregion

        #region Login
        /// <summary>
        /// Gets the name of the user net.
        /// </summary>
        /// <param name="userFullName">Full name of the user.</param>
        /// <returns>dataset</returns>
        [OperationContract]
        DataSet GetUserNetName(string userFullName);
        #endregion

        #region F27080ExemptionType

        /// <summary>
        /// F27080_ListExemptionTypeCombo
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>dataset</returns>
        [OperationContract]
        string F27080_ListExemptionTypeCombo(int applicationId);

        /// <summary>
        /// F27080_FillExemptionTypeGrid
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        /// <returns>dataset</returns>
        [OperationContract]
        string F27080_FillExemptionTypeGrid(int exemptionId);

        /// <summary>
        /// F27080_GetExemptionError
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        /// /// <param name="exemptionCode">exemptionCode</param>
        /// <returns>Message</returns>
        [OperationContract]
        string F27080_GetExemptionError(int exemptionId, string exemptionCode);


        /// <summary>
        /// F27080_DeleteExemption
        /// </summary>
        /// <param name="exemptionId">exemptionId</param>
        /// <param name="userId">userId</param>
        /// /// <param name="exemptionCode">exemptionCode</param>
        /// <returns>NULL</returns>
        [OperationContract]
        void F27080_DeleteExemption(int userId, int exemptionId, string exemptionCode);

        #region SaveExemptionType

        #endregion
        /// <summary>
        /// F27080_SaveExemptionType
        /// </summary>
        /// <param name="exemptionId">exemptionID</param>
        /// <param name="seniorExemption">seniorExemption</param>
        /// <param name="exemptionType">Exemption Type</param>
        /// <param name="checkError">Check Error</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F27080_SaveExemptionType(int exemptionId, string seniorExemption, string exemptionType, int checkError, int userId);

        #endregion

        #region F29530AssociationEvents

        /// <summary>
        /// F29530_FillAssociationEventGrid
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>  string</returns>
        [OperationContract]
        string F29530_FillAssociationEventGrid(int eventId);

        #endregion

        #region F29500ParcelSplit

        /// <summary>
        /// F29500_s the get base parcel value.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29500_GetBaseParcelValue(int parcelId);

        /// <summary>
        /// F29500_s the parcel split load.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F29500_ParcelSplitLoad(int eventId);

        /// <summary>
        /// F29500_s the save parcel split.
        /// </summary>
        /// <param name="splitDefinitionXml">The split definition XML.</param>
        /// <param name="splitHeaderXml">The split header XML.</param>
        /// <param name="parcelSplitXml">The parcel split XML.</param>
        /// <param name="parcelObjectXml">The parcel object XML.</param>
        /// <param name="cropXml">The Crop XML</param>
        /// <param name="userId">UserId</param>
        /// <returns>int</returns>
        [OperationContract]
        int F29500_SaveParcelSplit(string splitDefinitionXml, string splitHeaderXml, string parcelSplitXml, string parcelObjectXml, string cropXml, int userId);

        /// <summary>
        /// F29500_s the create parcel.
        /// </summary>
        /// <param name="splitId">The split id.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        string F29500_CreateParcel(int splitId, int userId);

        #endregion F29500ParcelSplit

        #region F36032 Land Codes

        #region F36032_ListLandItems

        /// <summary>
        /// F36032_s the list land items.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>The landCodeDataSet.</returns>
        [OperationContract]
        string F36032_ListLandItems(int? rollYear);

        #endregion F36032_ListLandItems

        #region F36032_ListLandCodeDetails

        /// <summary>
        /// F36032_s the list land code details.
        /// </summary>
        /// <returns>the landCodesDataSet</returns>
        [OperationContract]
        string F36032_ListLandCodeDetails();

        #endregion F36032_ListLandCodeDetails

        #region F36032_DeleteLandCode

        /// <summary>
        /// F36032_s the delete land code.
        /// </summary>
        /// <param name="landCodeId">The land code ID.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F36032_DeleteLandCode(int landCodeId, int userId);

        #endregion F36032_DeleteLandCode

        #region F36032_SaveLandCodeDetails

        /// <summary>
        /// To save the land code deatils
        /// </summary>
        /// <param name="landCodeId">Land Code ID</param>
        /// <param name="landItems">Land Items</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer value containing the save land Code Id</returns>
        [OperationContract]
        int F36032_SaveLandCodeDetails(int? landCodeId, string landItems, int userId);

        #endregion F36032_SaveLandCodeDetails

        #endregion F36032 Land Codes

        #region F29510ParcelCombine

        /// <summary>
        /// Get Base parcel value.
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F29510_GetBaseParcelValue(int eventId);

        /// <summary>
        /// Get Combine Parcel value
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        System.Data.DataSet F29510_GetCombineParcelDetails(int parcelId);

        /// <summary>
        /// Save Combine Parcel Details
        /// </summary>
        /// <param name="combineId">combineId</param>
        /// <param name="parcelNumber">Parcel Number</param>
        /// <param name="combineItems">Combine Items</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        [OperationContract]
        int F29510_SaveCombineParcelDetails(int? combineId, string parcelNumber, string combineItems, int userId,bool IsAttachment,bool IsComment,bool IsPermit,bool IsAssociation,bool IsNewConstruction);

        /// <summary>
        /// Create Combine Parcel Value
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="eventId">EventID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="userId">UserID</param>
        /// <returns>F29510ParcelCombineData</returns>
        [OperationContract]
        string F29510_CreateCombinedParcel(int combineId, string eventId, string parcelNumber, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction);

        #endregion

        #region F36033 Land Code Values

        #region F36033_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        [OperationContract]
        string F36033_ListLandCodeValues();

        /// <summary>
        /// F36065_s the list shape details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F36035_ListShapeDetails();

        #endregion F36033_ListLandCodeValues

        #region F36033_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        [OperationContract]
        string F36033_ListIndividualLandCodeValuesItems();

        #endregion F36033_ListIndividualLandCodeValuesItems

        #region F36033_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        [OperationContract]
        string F36033_ListNeighborhoodType(int rollYear);
        #endregion F36033_ListNeighborhood

        #region F36033_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F36033_DeleteLandCodevalue(int luvId, int userId);

        #endregion F36033_DeleteLandCodeValue

        #region F36033_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved key id</returns>
        [OperationContract]
        int F36033_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId);

        #endregion F36033_SaveLandCodeValue

        #endregion F36033 Land Code Values

        #region F39133 Land Code Values

        #region F39133_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        [OperationContract]
        string F39133_ListLandCodeValues();

        #endregion F39133_ListLandCodeValues

        #region F39133_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        [OperationContract]
        string F39133_ListIndividualLandCodeValuesItems();

        #endregion F39133_ListIndividualLandCodeValuesItems

        #region F39133_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        [OperationContract]
        string F39133_ListNeighborhoodType(int rollYear);
        #endregion F39133_ListNeighborhood

        #region F39133_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer Value</returns>
        [OperationContract]
        int F39133_DeleteLandCodevalue(int luvId, int userId);

        #endregion F39133_DeleteLandCodeValue

        #region F39133_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">UserID</param>
        /// <returns>saved key id</returns>
        [OperationContract]
        int F39133_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId);

        #endregion F39133_SaveLandCodeValue

        #region F39133_CalculateNonCropValues
        /// <summary>
        /// F39133_CalculateNonCropValues.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="CropRate">Crop Rate</param>
        /// <param name="NonCropRate">Non Crop Rate</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        [OperationContract]
        string F39133_CalculateNonCropValue(int rollYear, decimal? CropRate, decimal? NonCropRate);

        #endregion F39133_CalculateNonCropValues

        #endregion F39133 Land Code Values

        #region F36035 Land Details

        #region GetLand Details

        /// <summary>
        /// Gets the ListLandDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>ListLandDetails</returns>        
        [OperationContract]
        string F36035_ListLandDetails(int valueSliceId);

        #endregion GetLand Details

        #region GetLandType Details

        /// <summary>
        /// Gets the GetLandTypeDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>LandTypeDetails</returns>        
        [OperationContract]
        string F36035_ListLandTypeDetails(int valueSliceId);

        #endregion GetLandType Details

        #region InsertLandDetails

        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        [OperationContract]
        int F36035_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId);

        #endregion InsertLandDetails

        #region DeleteLandDetails
        /// <summary>
        /// DeleteLandDetails
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F36035_DeleteLandDetails(int luid, int userId);

        #endregion DeleteLandDetails

        #region GetLandCode

        /// <summary>
        /// F36035_s the get land code.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="valuesliceId">The valueslice id.</param>
        /// <returns></returns>
        [OperationContract]
        string F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId, int? AglandID);

        #endregion GetLandCode

        #region GetLandCodeBaseValue

        /// <summary>
        /// Gets the LandCode BaseValue    
        /// </summary>
        /// <param name="landCode">Land Code</param>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>LandCode</returns>        
        [OperationContract]
        string F36035_GetLandCodeBaseValue(string landCode, int valueSliceId, int? AglandID);

        #endregion GetLandCodeBaseValue

        #region List Influence Types

        /// <summary>
        /// F36035_s the type of the list influence.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>he influence type dataset</returns>
        [OperationContract]
        string F36035_ListInfluenceType(int valueSliceId);

        #endregion List Influence Types

        #region List Land Program

        /// <summary>
        /// F36035_s the list land program.
        /// </summary>
        /// <returns>The land program dataset.</returns>
        [OperationContract]
        string F36035_ListLandProgram();

        #endregion List Land Program

        #region Get UseBaseDollarPerUnit Value

        /// <summary>
        /// F36035_s the get use base dollar per unit.
        /// </summary>
        /// <param name="programId">The program id.</param>
        /// <param name="useAdjustmentType">Type of the use adjustment.</param>
        /// <param name="useAdjustment">The use adjustment.</param>
        /// <param name="useBaseValue">The use base value.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="useMultiplier">The use multiplier.</param>
        /// <param name="units">The units.</param>
        /// <returns>The use base doller per unit dataset.</returns>
        [OperationContract]
        string F36035_GetUseBaseDollarPerUnit(byte programId, byte useAdjustmentType, string useAdjustment, decimal useBaseValue, int rollYear, decimal useMultiplier, decimal units);

        #endregion Get UseBaseDollarPerUnit Value

        #region Execute VFormula

        /// <summary>
        /// F36035_s the execute V formula.
        /// </summary>
        /// <param name="vformula">The vformula.</param>
        /// <param name="units">The units.</param>
        /// <returns>Dataset contains the result of formula</returns>
        [OperationContract]
        string F36035_ExecuteVFormula(string vformula, decimal units);

        #endregion Execute VFormula

        #endregion F36035 Land Details

        #region F39135LandDetails

        #region F39135 LandDetails
        /// <summary>
        /// Gets the ListLandDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>ListLandDetails</returns>        
        [OperationContract]
        string F39135_LandDetails(int valueSliceId);


        #endregion

        #region F39135 GetLandTypeDetails
        /// <summary>
        /// Gets the GetLandTypeDetails        
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>LandTypeDetails</returns>   
        [OperationContract]
        string F39135_Landtypes(int valueSliceId, int rollYear);

        #endregion

        #region F39135 GetLandUseTypes
        /// <summary>
        /// F39135_s the LandUseType.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        [OperationContract]
        string F39135_LandUseTypes(int valueSliceId);
        #endregion F39135 GetLandUseTypes

        #region F39135 GetLandTotals
        /// <summary>
        /// F39135_s the GetLandTotals.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        [OperationContract]
        string F39135_GetLandTotals(int valueSliceId);
        #endregion F39135_GetLandTotals

        #region GetWeightedRating
        /// <summary>
        /// F39135_s the get WeightedRating.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="landCode">landCode</param>
        /// <param name="units">units</param>
        /// <param name="landUse">landUse</param>
        /// <returns>the landTypeDetails</returns>
        [OperationContract]
        string F39135_WeightedRating(string landCode, decimal units, int? landUse, int valueSliceId);

        #endregion GetWeightedRating

        #region CalculatedBaseValue
        /// <summary>
        /// F39135_s the get CalculatedBaseValue.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="adjustmentTypeID">adjustmentTypeID</param>
        /// <param name="units">units</param>
        /// <param name="baseCostUnit">baseCostUnit</param>
        /// <param name="adjustment">adjustment</param>
        /// <returns>the landTypeDetails</returns>
        [OperationContract]
        string F39135_CalculatedBaseValue(string LandCode, int adjustmentTypeID, decimal units, decimal baseCostUnit, decimal adjustment, int? AglandID, int valueSliceId);
        #endregion

        #region List Adjustment Types
        /// <summary>
        /// F39135_s the list AdjustmentType.
        /// </summary>
        [OperationContract]
        string F39135_AdjustmentType();

        #endregion

        #region InsertLandDetails

        /// <summary>
        /// F39135_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        [OperationContract]
        int F39135_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId);

        #endregion InsertLandDetails

        #endregion F39135LandDetails

        #region F81001 Event Fee Catalog

        /// <summary>
        /// Get Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <returns>TypedDataSet</returns>
        [OperationContract]
        string F81001_GetEventFeeCatalog(int feeCatId);

        /// <summary>
        /// Save Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="feeCatalogItems">feeCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F81001_SaveEventFeeCatalog(int feeCatId, string feeCatalogItems, int userId);

        /// <summary>
        /// Delete Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="userId">userId</param>
        [OperationContract]
        void F81001_DeleteEventFeeCatalog(int feeCatId, int userId);

        /// <summary>
        /// Check Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="formNumber">formNumber</param>
        /// <param name="effectiveDate">effectiveDate</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F81001_CheckEventFeeCatalog(int feeCatId, string formNumber, DateTime effectiveDate);

        #endregion F81001 Event Fee Catalog

        #region F36040 Crop Catalog

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>NeighborhoodType String.</returns>
        [OperationContract]
        string F36040_ListNeighborhoodType();

        /// <summary>
        /// F36040_s the list crop catalog.
        /// </summary>
        /// <returns>Crop Catalog String.</returns>
        [OperationContract]
        string F36040_ListCropCatalog();

        /// <summary>
        /// F36040_s the delete crop catalog.
        /// </summary>
        /// <param name="cropVId">The crop V id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction</returns>
        [OperationContract]
        int F36040_DeleteCropCatalog(int cropVId, int userId);

        /// <summary>
        /// F36040_s the save crop catalog.
        /// </summary>
        /// <param name="cropUnqiueId">The crop unqiue id.</param>
        /// <param name="cropCatalogItems">The crop catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction</returns>
        [OperationContract]
        int F36040_SaveCropCatalog(int? cropUnqiueId, string cropCatalogItems, int userId);

        /// <summary>
        /// F36040_s the type of the list crop neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        [OperationContract]
        string F36040_ListCropNeighborhoodType(int rollYear);

        #endregion

        #region F36041 Crop

        #region Get Crop Details

        /// <summary>
        /// Get Crop Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        [OperationContract]
        string F36041_GetCrop(int valueSliceId);

        #endregion Get Crop Details

        #region Get Crop Code Details
        /// <summary>
        /// Get Crop Code Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <returns>String</returns>
        [OperationContract]
        string F36041_GetCropCode(int valueSliceId);

        #endregion Get Crop Code Details

        #region Save Crop Details
        /// <summary>
        /// To Save the Crop Details
        /// </summary>
        /// <param name="valueSliceId">ValueSliceID</param>
        /// <param name="cropItems">CropItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer </returns>
        [OperationContract]
        int F36041_SaveCropCodeDetails(int valueSliceId, string cropItems, int userId);

        #endregion Save Crop Details

        #region F36041_DeleteCrop

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F36041_DeleteCrop(int cropId, int userId);

        #endregion F36041_DeleteCrop

        #region F36041_DeleteCropIds

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F36041_DeleteCropIds(string cropIds, int userId);

        #endregion F36041_DeleteCropIds

        #endregion F36041 Crop

        #region F81002 Event Fee
        /// <summary>
        /// Get the Event Fee data
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="form">Form Number</param>
        /// <returns>TypedDataSet</returns>
        [OperationContract]
        string F81002_GetEventFee(int eventId, int form);

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="feeItems">FeeItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>Integer </returns>
        [OperationContract]
        int F81002_SaveEventFee(int eventId, string feeItems, int userId);

        /// <summary>
        /// Delete the Event Fee data
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="userId">UserID</param>
        [OperationContract]
        void F81002_DeleteEventFee(int eventId, int userId);

        #endregion F81002 Event Fee

        #region F3230 Check in

        #region F3230_ChkInCommentXML
        /// <summary>
        /// F3230_ChkInCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInCommentXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInCommentXML

        #region F3230_ChkInEstimateXML
        /// <summary>
        /// F3230_ChkInEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInEstimateXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInEstimateXML

        #region F3230_ChkInFileXML
        /// <summary>
        /// F3230_ChkInFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInFileXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInFileXML

        /// <summary>
        /// F3230_ChkInInsertedFileXML
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInInsertedFileXML();

        /// <summary>
        /// F3230_UpdateFile
        /// </summary>
        /// <param name="updatexmlContent"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_UpdateFile(string updatexmlContent);


        /// <summary>
        /// F3230_ChkInInsertedFileXML
        /// </summary>
        /// <param name="insertxmlContent"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_InsertFile(string insertxmlContent);

        #region F3230_ChkInLandValuesXML
        /// <summary>
        /// F3230_ChkInLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInLandValuesXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInLandValuesXML

        #region F3230_ChkInLandXML
        /// <summary>
        /// F3230_ChkInLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInLandXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInLandXML

        #region F3230_ChkInMiscXML
        /// <summary>
        /// F3230_ChkInMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInMiscXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInMiscXML

        #region F3230_ChkInMSC_EstimateXML
        /// <summary>
        /// F3230_ChkInMSC_EstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInMSC_EstimateXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInMSC_EstimateXML

        #region F3230_ChkInObjectXML
        /// <summary>
        /// F3230_ChkInObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInObjectXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInObjectXML

        #region F3230_ChkInParcelValueXML
        /// <summary>
        /// F3230_ChkInParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInParcelValueXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInParcelValueXML

        #region F3230_ChkInParcelXML
        /// <summary> 
        /// F3230_ChkInParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInParcelXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInParcelXML

        #region F3230_ChkInTerraGonXML
        /// <summary>
        /// F3230_ChkInTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInTerraGonXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInTerraGonXML

        #region F3230_ChkInType2XML
        /// <summary>
        /// F3230_ChkInType2XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInType2XML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInType2XML

        #region F3230_ChkInType6XML
        /// <summary>
        /// F3230_ChkInType6XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInType6XML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInType6XML

        #region F3230_ChkInValueSliceXML
        /// <summary>
        /// F3230_ChkInValueSliceXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInValueSliceXML(string TableName, int StartRow, out int RowendValue);
        #endregion F3230_ChkInValueSliceXML

        #region F3230_ChkInTypesXML
        /// <summary>
        /// F3230_ChkInTypesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInTypesXML();
        #endregion F3230_ChkInTypesXML

        /// <summary>
        /// F3230_ChkInLandCodeXML
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInLandCodeXML();

        /// <summary>
        /// F3230_ChkOutParcelIDs
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_GetChkOutParcelIDs(int SnapShotID);

        /// <summary>
        /// F3230_CheckOutDetails
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_GetCheckOutDetails(int SnapShotID, int UserID);

        /// <summary>
        /// F3230_SaveChkOutParcelIDs
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        [OperationContract]
        int F3230_SaveChkOutParcelIDs(string ParcelXML);

        /// <summary>
        /// F3230_SaveCheckOutDetails
        /// </summary>
        /// <param name="CheckOutXML"></param>
        /// <returns></returns>
        [OperationContract]
        int F3230_SaveCheckOutDetails(string CheckOutXML);

        /// <summary>
        /// F3230_ParcelID
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ParcelID();

        /// <summary>
        /// F3230_ChkInDeprXML
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInDeprXML();

        /// <summary>
        /// F3230_ChkInInsertXML
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F3230_ChkInInsertXML(out string ChkInInsertXML);

        /// <summary>
        /// F3230_ChkInTerraGonInsertXML
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F3230_ChkInTerraGonInsertXML(out string ChkInInsertXML);

        #region F3230_ChkInEstimateComponentGroupXML

        /// <summary>
        /// F3230_ChkInEstimateComponentGroupXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInEstimateComponentGroupXML();
        #endregion F3230_ChkInEstimateComponentGroupXML

        #region F3230_ChkInNBHDXML
        /// <summary>
        /// F3230_ChkInNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230CheckInData F3230_ChkInNBHDXML();
        #endregion F3230_ChkInNBHDXML

        #endregion F3230 Check in

        #region F3230 FieldUse CheckOut

        #region F9065UpdateApplicationStatus
        /// <summary>
        /// F9065_s the update application status.
        /// </summary>
        /// <param name="isCheckedOut">if set to <c>true</c> [is checked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [is online].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F9065_UpdateApplicationStatus(bool isCheckedOut, bool isOnline, int userId);
        #endregion F9065UpdateApplicationStatus

        #region F9065GetSnapshotDetail
        /// <summary>
        /// F9065_s the get snapshot detail.
        /// </summary>
        /// <returns>F9065FieldUseData</returns>
        [OperationContract]
        string F9065_GetSnapshotDetail();
        #endregion F9065GetSnapshotDetail

        #region F9065_GetAuditCount
        /// <summary>
        /// F9065_s the get audit count.
        /// </summary>
        /// <returns>integer</returns>
        [OperationContract]
        int F9065_GetAuditCount();
        #endregion F9065_GetAuditCount

        #region F9065DeleteCheckOutTable
        /// <summary>
        /// F9065_s the delete check out table.
        /// </summary>
        /// <returns>integer</returns>
        [OperationContract]
        int F9065_DeleteCheckOutTable();
        #endregion F9065DeleteCheckOutTable

        #region F9065InsertFieldElement
        /// <summary>
        /// F9065_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F9065_InsertFieldElement(string fieldElement, int userId);
        #endregion F9065InsertFieldElement

        #region F9065GetPreviewDetai
        /// <summary>
        /// F9065_s the get preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F9065_GetPreviewDetail(int snapShotId, string snapShotDetail);
        #endregion F9065GetPreviewDetai

        #region F9065InsertApplicationConfiguration
        /// <summary>
        /// F9065_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        [OperationContract]
        int F9065_InsertApplicationConfiguration(string configXml, int userId);
        #endregion F9065InsertApplicationConfiguration

        #region F9065_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        [OperationContract]
        string F9065_GetcfgConfiguration(string cfgname);

        #endregion F9065_GetcfgConfiguration

        #region F3230UpdateApplicationStatus
        /// <summary>
        /// F3230_s the update application status.
        /// </summary>
        /// <param name="isCheckedOut">if set to <c>true</c> [is checked out].</param>
        /// <param name="isOnline">if set to <c>true</c> [is online].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F3230_UpdateApplicationStatus(bool isCheckedOut, bool isOnline, int userId);
        #endregion F3230pdateApplicationStatus

        #region F3230GetSnapshotDetail
        /// <summary>
        /// F3230_s the get snapshot detail.
        /// </summary>
        /// <returns>F3230FieldUseData</returns>
        [OperationContract]
        string F3230_GetSnapshotDetail();
        #endregion F3230GetSnapshotDetail

        #region F3230_GetAuditCount
        ///// <summary>
        ///// F3230_s the get audit count.
        ///// </summary>
        ///// <returns>integer</returns>
        //[OperationContract]
        //int F3230_GetAuditCount();
        #endregion F3230_GetAuditCount

        #region F3230DeleteCheckOutTable
        /// <summary>
        /// F3230_s the delete check out table.
        /// </summary>
        /// <returns>integer</returns>
        [OperationContract]
        int F3230_DeleteCheckOutTable();
        #endregion F3230DeleteCheckOutTable

        #region F3230InsertFieldElement
        /// <summary>
        /// F3230_s the insert field element.
        /// </summary>
        /// <param name="fieldElement">The field element.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F3230_InsertFieldElement(string fieldElement, int userId);
        #endregion F3230InsertFieldElement

        #region F3230GetPreviewDetai
        /// <summary>
        /// F3230_s the get preview detail.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetail">The snap shot detail.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F3230_GetPreviewDetail(int snapShotId, string snapShotDetail);
        #endregion F3230GetPreviewDetai

        #region F3230InsertApplicationConfiguration
        /// <summary>
        /// F9065_s the insert application configuration.
        /// </summary>
        /// <param name="configXml">The config XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        [OperationContract]
        int F3230_InsertApplicationConfiguration(string configXml, int userId);
        #endregion F3230InsertApplicationConfiguration

        #region F3230_GetcfgConfiguration

        /// <summary>
        /// To get Configruation Value
        /// </summary>
        /// <param name="cfgname">The cfgname.</param>
        /// <returns>
        /// Typed dataset containing the Configruation Value.
        /// </returns>
        [OperationContract]
        string F3230_GetcfgConfiguration(string cfgname);

        #endregion F3230_GetcfgConfiguration

        #region f3230_AddValues

        [OperationContract]
        int F3230_AddValues(int KeyID, string KeyName, int Form, int? ModuleID, int InsertedBy);

        #endregion
        #region ChkOutConfigXML
        /// <summary>
        /// F3230_ChkOutConfigXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutConfigXML(int snapShotId, string snapShotValue);
        #endregion ChkOutConfigXML

        /// <summary>
        /// F3230_ChkOutCommonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutCommonXML(int snapShotId, string snapShotValue);

        /// <summary>
        /// f3230_ChkOutCorrectionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData f3230_ChkOutCorrectionXML(int snapShotId);

        /// <summary>
        /// f3230_ChkOutSaleXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData f3230_ChkOutSaleXML(int snapShotId);

        /// <summary>
        /// f3230_ChkOutReceiptXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData f3230_ChkOutReceiptXML(int snapShotId);

        /// <summary>
        /// f3230_ChkOutStatementXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData f3230_ChkOutStatementXML(int snapShotId);


        #region ChkOutFormXM
        /// <summary>
        /// F3230_ChkOutFormXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutFormXML(int snapShotId, string snapShotValue);


        #endregion ChkOutFormXML


        #region F3230_GetAPexFilePath
        /// <summary>
        /// F3230_GetAPexFilePath
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230GetApexFilePath(int snapShotId);

        #endregion F3230_GetAPexFilePath


        #region ChkOutDistrictXML
        /// <summary>
        /// F3230_ChkOutDistrictXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutDistrictXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutDistrictXML

        #region ChkOutLegalXML
        /// <summary>
        /// F3230_ChkOutLegalXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutLegalXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutLegalXML

        #region ChkOutMisc_CatalogXML
        /// <summary>
        /// F3230_ChkOutMisc_CatalogXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMisc_CatalogXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutMisc_CatalogXML

        #region ChkOutMiscTableXML
        /// <summary>
        /// F3230_ChkOutMiscTableXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMiscTableXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutMiscTableXML

        #region ChkOutMOwnerXML
        /// <summary>
        /// F3230_ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutMOwnerXML

        #region ChkOutObjectXML
        /// <summary>
        /// ChkOutObjectXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutObjectXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutObjectXML

        #region ChkOutValueSliceXML
        /// <summary>
        /// ChkOutValueSliceXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutValueSliceXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutValueSliceXML

        #region ChkOutLandXML
        /// <summary>
        /// ChkOutLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutLandXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutLandXML

        #region ChkOutVersionXML
        /// <summary>
        /// ChkOutVersionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutVersionXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutVersionXML

        #region ChkOutSitusXML
        /// <summary>
        /// F3230_ChkOutSitusXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutSitusXML(int snapShotId, string snapShotValue);
        #endregion ChkOutSitusXML

        #region ChkOutSeniorExemptionXML
        /// <summary>
        /// ChkOutSeniorExemptionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutSeniorExemptionXML(int snapShotId, string snapShotValue);
        #endregion ChkOutSeniorExemptionXML

        #region ChkOutAssessmentTypeXML
        /// <summary>
        /// ChkOutAssessmentTypeXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutAssessmentTypeXML(int snapShotId, string snapShotValue);
        #endregion ChkOutAssessmentTypeXML

        #region ChkOutParcelValueXML
        /// <summary>
        /// ChkOutParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutParcelValueXML(int snapShotId, string snapShotValue);
        #endregion ChkOutParcelValueXML

        #region ChkOutType2XML
        /// <summary>
        /// ChkOutType2XML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F25000FieldUseData F25000_ChkOutType2XML(int snapShotId, string snapShotValue);
        #endregion ChkOutType2XML

        #region ChkOutNBHDXML
        /// <summary>
        /// ChkOutNBHDXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutNBHDXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutNBHDXML

        #region ChkOutMiscXML
        /// <summary>
        /// F3230_ChkOutMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMiscXML(int snapShotId, string snapShotValue);
        #endregion ChkOutMiscXML

        #region ChkOutUserXML
        /// <summary>
        /// F3230_ChkOutUserXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutUserXML(int snapShotId, string snapShotValue);
        #endregion ChkOutUserXML

        #region ChkOutEventXML
        /// <summary>
        /// F3230_ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutEventXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);

        #endregion ChkOutEventXML

        /// <summary>
        /// F3230_ChkOutParcelXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutParcelXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);

        #region ChkOutOwnerXML
        /// <summary>
        /// F3230_ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutOwnerXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutOwnerXML

        #region ChkOutDeprMiscXML
        /// <summary>
        /// F3230ChkOutDeprMiscXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230ChkOutDeprMiscXML(int snapShotId, string snapShotValue);

        /// <summary>
        /// F3230ChkOutDeprXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230ChkOutDeprXML(int snapShotId, string snapShotValue);

        #endregion ChkOutDeprMiscXML

        #region ChkOutEstimateCompXML
        /// <summary>
        /// F3230_ChkOutEstimateCompXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutEstimateCompXML(int snapShotId, string snapShotValue);
        #endregion ChkOutEstimateCompXML

        #region ChkOutVSTGCitemXML
        /// <summary>
        /// F3230_ChkOutVSTGCitemXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutVSTGCitemXML(int snapShotId, string snapShotValue);
        #endregion ChkOutVSTGCitemXML

        #region ChkOutMSCEstimateXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMSCEstimateXML(int snapShotId, string snapShotValue);
        #endregion ChkOutMSCEstimateXML

        #region  ChkOutEstimateResultXML
        /// <summary>
        /// F3230_ChkOutEstimateResultXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutEstimateResultXML(int snapShotId, string snapShotValue);
        #endregion ChkOutEstimateResultXML

        #region  ChkOutMSCEstimateOccupancyXML
        /// <summary>
        /// F3230_ChkOutMSCEstimateOccupancyXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutMSCEstimateOccupancyXML(int snapShotId, string snapShotValue);
        #endregion ChkOutMSCEstimateOccupancyXML

        #region ChkOutEstimateBuildingXML
        /// <summary>
        /// F3230_ChkOutEstimateBuildingXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutEstimateBuildingXML(int snapShotId, string snapShotValue);
        #endregion ChkOutEstimateBuildingXML

        #region  ChkOutLandValuesXML
        /// <summary>
        /// F3230_ChkOutLandValuesXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutLandValuesXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutLandValuesXML

        #region  ChkOutTerraGonXML
        /// <summary>
        /// ChkOutTerraGonXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutTerraGonXML(int snapShotId);
        #endregion ChkOutVSTerraGonXML

        #region  ChkOutEstimateComponentXML
        /// <summary>
        /// ChkOutEstimateComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutEstimateComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutEstimateComponentXML

        #region ChkOutCommentXML
        /// <summary>
        /// ChkOutCommentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutCommentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutCommentXML

        #region  ChkOutVSTGComponentXML
        /// <summary>
        /// ChkOutVSTGComponentXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutVSTGComponentXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutVSTGComponentXML

        #region  ChkOutFileXML
        /// <summary>
        /// F3230_ChkOutFileXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutFileXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutFileXML

        #region ChkOutVSTGGonBldgXML
        /// <summary>
        /// F3230_ChkOutMOwnerXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ChkOutVSTGGonBldgXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue);
        #endregion ChkOutVSTGGonBldgXML

        /// <summary>
        /// F3230_ListLockedParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="ParcelXML"></param>
        /// <returns></returns>
        [OperationContract]
        F3230FieldUseData F3230_ListLockedParcelID(int? SnapShotID, out int RowendValue);

        /// <summary>
        /// F3230_LockParcelID
        /// </summary>
        /// <param name="SnapShotID"></param>
        /// <param name="LockAdminBy"></param>
        /// <param name="UserID"></param>
        /// <param name="UnlockParcelXML"></param>
        /// <returns></returns>
        [OperationContract]
        int F3230_LockParcelID(int? SnapShotID, int? LockAdminBy, int? UserID, string UnlockParcelXML);


        /// <summary>
        /// F9065_s the get CHK out XML.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotValue">The snap shot value.</param>
        /// <returns>string</returns>
        //[OperationContract]
        //F3230FieldUseData F3230_GetChkOutXML(int snapShotId, string snapShotValue);


        #region InsertChkInXML
        /// <summary>
        /// F3230_InsertChkInXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        int F3230_InsertChkInXML(string xmlInsContent, string tableXml, int userId);
        #endregion InsertChkInXML

        /// <summary>
        /// F3230_InsertAddedRecordXML
        /// </summary>
        /// <param name="xmlInsContent"></param>
        /// <param name="tableXml"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract]
        int F3230_InsertAddedRecordXML(string xmlInsContent, string tableXml, int userId);

        #region InsertChkOutXML
        /// <summary>
        /// F9065_s the insert CHK out XML.
        /// </summary>
        /// <param name="xmlInsContent">Content of the XML ins.</param>
        /// <param name="tableXml">The table XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        [OperationContract]
        int F3230_InsertChkOutXML(string xmlInsContent, string tableXml, int userId, bool IsDelete);
        #endregion InsertChkOutXML


        #endregion

        #region F3200CamaSketch

        #region F3200_GetSketchData
        /// <summary>
        /// Get the Sketch Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String</returns>
        [OperationContract]
        string F3200_GetSketchData(int objectId);
        #endregion F3200_GetSketchData

        #region F3200_GetStyleList
        /// <summary>
        /// Get the Style List Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String</returns>
        [OperationContract]
        string F3200_GetStyleList(int objectId);
        #endregion F3200_GetStyleList

        #region F3200_SaveSketch
        /// <summary>
        /// Save the Sketch data
        /// </summary>
        /// <param name="objectId">objectId</param>
        /// <param name="sketchData">sketchData</param>
        /// <param name="userId">UserID</param>
        /// <returns>String</returns>
        [OperationContract]
        DataSet F3200_SaveSketchData(int objectId, string sketchData, int userId);
        #endregion F3200_SaveSketch

        #region F3200_CheckSmartPart

        /// <summary>
        /// Check the SmartPart
        /// </summary>
        /// <param name="formId">FormNumber</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F3200_CheckSmartPart(int formId);

        #endregion F3200_CheckSmartPart

        #endregion F3200CamaSketch

        #region F95010WebFormXML

        #region F95010GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        [OperationContract]
        string GetWebFormXML(int? keyId, int form, int userId);

        #endregion F95010GetWebFormXML

        #endregion F95010WebFormXML

        #region 3510 Neighborhood Selection

        /// <summary>
        /// F3510_s the type of the list neighborhood.
        /// </summary>
        /// <returns>NeighborhoodType String.</returns>
        [OperationContract]
        string F3510_ListNeighborhoodType();

        /// <summary>
        /// Get Neighborhood Selection Details
        /// </summary>
        /// <param name="neighborhood">neighborhood</param>
        /// <param name="childof">Childof</param>
        /// <param name="rollyear">Rollyear</param>
        /// <param name="type">Type</param>
        /// <param name="description">Description</param>
        /// <returns>String</returns>
        [OperationContract]
        string F3510_ListNeighborhoodSelection(string neighborhood, string childof, string rollyear, string type, string description);

        #endregion

        #region F2010_StateCodeSelection

        #region List StateCodeSelection

        /// <summary>
        /// F2010_s the list state code selection.
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F2010_ListStateCodeSelection();

        #endregion List StateCodeSelection

        #endregion F2010_StateCodeSelection

        #region F9066CheckIn

        #region GetAuditCount
        ///// <summary>
        ///// Get Audit Count
        ///// </summary>
        ///// <returns>Integer</returns>
        //[OperationContract]
        //int F9066_GetAuditCount();
        #endregion GetAuditCount

        #region GetCheckInXML
        ///// <summary>
        ///// Get Check In Details
        ///// </summary>
        ///// <returns>DataSet</returns>
        //[OperationContract]
        //string F9066_GetCheckInData();
        #endregion GetCheckInXML

        #region SaveXML
        /// <summary>
        /// Save the values
        /// </summary>
        /// <param name="insertValue">insertValue</param>
        /// <param name="updateValue">updateValue</param>
        [OperationContract]
        void F9066_SaveData(string insertValue, string updateValue);
        #endregion SaveXML

        #region DeleteData
        ///// <summary>
        ///// Delete the values
        ///// </summary>
        ///// <returns>Integer</returns>
        //[OperationContract]
        //int F9066_DeleteData();
        #endregion DeleteData

        #endregion F9066CheckIn

        #region F1430 Interest Calculator

        /// <summary>
        /// F1430_GetCalculatorDetails gets the calculator details on load.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>TypedDataSet</returns>
        [OperationContract]
        string F1430_GetCalculatorDetails(int statementId);

        /// <summary>
        /// F1430_GetInterestDetails get the interest and deliquency details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="taxAmount">The tax amount.</param>
        /// <returns>TypedDataSet</returns>
        [OperationContract]
        string F1430_GetInterestDetails(int statementId, DateTime interestDate, decimal taxAmount);

        #endregion F1430 Interest Calculator

        #region F1440 Batch Button SmartPart

        #region F1440_SaveRecieptinSnapShotBatchButtonControl

        /// <summary>
        /// F1440_SaveRecieptinSnapShotBatchButtonControl        
        /// To Insert or Update the newly created Receipt id  to the particular snapshot id.
        /// </summary>
        /// <param name="snapshotId">the snapshotId</param>
        /// <param name="receiptId">the receiptId</param>
        /// <param name="userId">the userId</param>
        /// <returns>returns the no of items count in snapshot</returns>
        [OperationContract]
        int F1440_SaveRecieptinSnapShotBatchButtonControl(int snapshotId, int? receiptId, int userId);

        #endregion F1440_SaveRecieptinSnapShotBatchButtonControl

        #endregion F1440 Batch Button SmartPart

        #region F82001 BuildingPermit

        #region GetBuildingPermitDetails

        /// <summary>
        /// F82001_s the get building permit details.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns>BuildingPermitDetails</returns>
        [OperationContract]
        string F82001_GetBuildingPermitDetails(int eventID);

        #endregion GetBuildingPermitDetails

        #region InsertBuildingPermitDetails

        /// <summary>
        /// F82001_s the insert building permit details.
        /// </summary>
        /// <param name="permitId">The permit id.</param>
        /// <param name="buildingPermitItems">The building permit items.</param>
        /// <returns>ID</returns>
        [OperationContract]
        int F82001_InsertBuildingPermitDetails(int permitId, string buildingPermitItems, int userId);

        #endregion InsertBuildingPermitDetails

        #endregion F82001 BuildingPermit

        #region F82002ContractorManagement
        /// <summary>
        /// F82002_ListContractorManagementData
        /// </summary>
        /// <param name="iContractorID">iContractorID</param>
        /// <param name="ContractorXML">ContractorXML</param>
        /// <returns>string</returns>
        [OperationContract]
        string F82002_ListContractorManagementData(int? iContractorID, string ContractorXML);

        /// <summary>
        /// F82002_s the delete contractor management.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="UserID">The user ID.</param>
        [OperationContract]
        void F82002_DeleteContractorManagement(int contractorId, int UserID);

        /// <summary>
        /// F82002_s the insert building permit details.
        /// </summary>
        /// <param name="ContractorID">The contractor ID.</param>
        /// <param name="ContractorItems">The contractor items.</param>
        /// <param name="UserID">The user ID.</param>
        /// <returns>integer</returns>
        [OperationContract]
        int F82002_InsertBuildingPermitDetails(int? ContractorID, string ContractorItems, int UserID);

        #endregion

        #region F36060DepreciationComp

        #region F36060_GetDepreciationTables

        /// <summary>
        /// To get the Depreciation  tables
        /// </summary>
        /// <param name="deprTableId">Deprtable id</param>
        /// <returns>Typed dataset containing the Deprecition and Deprecition items datatable</returns>
        [OperationContract]
        string F36060_GetDepreciationTables(int deprTableId);

        #endregion F36060_GetDepreciationTables

        #region F36060_SaveDepreciationTables

        /// <summary>
        /// To save depreciation tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="deprecationItem">The deprecation item.</param>
        /// <param name="otherDeprItem">The other depr item.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Inserted or update key id is returned</returns>
        [OperationContract]
        int F36060_SaveDepreciationTables(int deprTableId, string deprecationItem, string otherDeprItem, int userId);

        #endregion F36060_SaveDepreciationTables

        #region F36060_DeleteDepreciationTables

        /// <summary>
        /// To delete Depreciation Tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F36060_DeleteDepreciationTables(int deprTableId, int userId);

        #endregion F36060_DeleteDepreciationTables

        #endregion F36060DepreciationComp

        #region F49910InstrumentHeader

        #region GetinstrumentHeader

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F49910_GetInstrumentHeaderDetails(int instId);

        #endregion GetinstrumentHeader

        #region ListInstrumentType

        /// <summary>
        /// F49910_GetInstrumentTypeDetails
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F49910_GetInstrumentTypeDetails();

        #endregion ListInstrumentType

        #region SaveInstrumentHeaderDetails

        /// <summary>
        /// F49910_SaveInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="paymentItems">paymentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        [OperationContract]
        int F49910_SaveInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId);

        #endregion SaveInstrumentHeaderDetails

        #region F49910CheckInstrumentHeader Deatils

        /// <summary>
        /// F49910CheckInstrumentHeaderDetails
        /// Used to validate whether the records can be saved
        /// </summary>        
        /// <param name="instId"></param>
        /// <param name="instrumentItems"></param>
        /// <param name="paymentItems"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// 0 - When the records can be saved
        /// -1 - when Instrument Number already exists in the Database
        /// </returns>
        [OperationContract]
        int F49910CheckInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId);

        #endregion F49910CheckInstrumentHeader Deatils

        #region DeleteInstrumentHeaderDetails

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        [OperationContract]
        int F49910_DeleteInstrumentHeader(int instId, int userId);

        #endregion DeleteInstrumentHeaderDetails

        #region CopyInstrumentDetails

        /// <summary>
        /// F49910_CopyInstrumentHeaderDetails
        /// </summary>
        /// <param name="instrumentId">instrumentId</param>
        /// <param name="instrumentValue">instrumentValue</param>
        /// <param name="grantorValue">grantorValue</param>
        /// <param name="granteeValue">granteeValue</param>
        /// <param name="legalValue">legalValue</param>
        /// <returns>dataSet</returns>
        [OperationContract]
        string F49910_CopyInstrumentHeaderDetails(int instrumentId, int instrumentValue, int grantorValue, int granteeValue, int legalValue);

        #endregion CopyInstrumentDetails

        #region F49910_GetGranterAddressDetails

        /// <summary>
        /// F49910_GetGranterAddressDetails
        /// </summary>
        /// <param name="grantId">grantId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F49910_GetGranterAddressDetails(int grantId);

        #endregion F49910_GetGranterAddressDetails

        #region GetFeeDetails

        /// <summary>
        /// F49910_GetFeeDetails
        /// </summary>
        /// <param name="insTypeId">insTypeId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F49910_GetFeeDetails(int insTypeId);

        #endregion GetFeeDetails

        #endregion F49910InstrumentHeader

        #region F2200EditSchedule

        #region ListEditSchedule

        /// <summary>
        /// F2200_ListEditScheduleDetails
        /// </summary>
        /// <param name="SheduleID">SheduleID</param>
        /// <returns>string</returns>
        [OperationContract]
        string F2200_ListEditScheduleDetails(int SheduleID);


        [OperationContract]
        string F25050_GetScheduleDetails(int ScheduleID);


        [OperationContract]
        int F2005_GetValidUser(int scheduleId, int userId );

        [OperationContract]
        int F2005_UpdateParcelLockDetails(int scheduleId, int lockValue, int userId);

        [OperationContract]
        string F2005_GetScheduleUserName(int userId);
        
        #endregion

        #region F2200_InsertEditSchedule

        [OperationContract]
        string F2200_InsertEditSchedule(int? ScheduleID, string ScheduleItems, int UserID);

        #endregion

        #region F2200_UpdateEditSchedule

        [OperationContract]
        int F2200_UpdateEditSchedule(int ScheduleID, string ScheduleItems, int UserID);

        #endregion

        #region F2200_DeleteEditSchedule

        [OperationContract]
        int F2200_DeleteEditSchedule(int ScheduleID, int UserID);

        #endregion

        #region List Assessment Type Details
        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        [OperationContract]
        string F2200_GetAssessmentTypeDetails(string assessmentType);
        #endregion

        #region Get Penalty Percent
        /// <summary>
        /// Gets the penalty percent.
        /// </summary>
        /// <param name="filingDate">The filing date.</param>
        /// <returns>Penalty Percent</returns>
        [OperationContract]
        decimal GetPenaltyPercent(string filingDate);

        /// <summary>
        /// F2200_s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        [OperationContract]
        decimal F2200_GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear, bool isEx259, decimal ex259Amount);

        /// <summary>
        /// Get Farm Exemption Amount.
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <returns></returns>
        [OperationContract]
        string F2200_Get259ExemptionAmount(int scheduleId);
        #endregion Get Penalty Percent

        #endregion

        #region F49920Instrument Search Engine

        /// <summary>
        /// F49920_s the list instrument load.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F49920_ListInstrumentLoad();


        /// <summary>
        /// F49920_s the list instrument search.
        /// </summary>
        /// <param name="instrumentcondition">The instrumentcondition.</param>
        /// <returns></returns>
        [OperationContract]
        string F49920_ListInstrumentSearch(string instrumentcondition);

        #endregion

        #region F49911 PartiesField Listing

        #region  List PartiesField

        /// <summary>
        /// F49911_s the list parties field.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F49911_ListPartiesField();

        #endregion  List PartiesField

        #region Insert

        /// <summary>
        /// F49911_s the insert parties field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="grantorItems">The grantor items.</param>
        /// <param name="granteeItems">The grantee items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F49911_InsertPartiesFieldDetails(int instid, string grantorItems, string granteeItems, int userId, int isCopy);
        #endregion Insert

        #endregion F49911 PartiesField Listing

        #region F49912 LegalField Listing

        #region  List LegalField
        /// <summary>
        /// F49912_s the list parties field.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F49912_ListLegalField(int instID);

        #endregion  List LegalField

        #region  Insert LegalField

        /// <summary>
        /// F49912_s the insert legal field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F49912_InsertLegalFieldDetails(int instid, string legalItems, int userId, int isCopy);

        #endregion  Insert LegalField

        /// <summary>
        /// F49912_s the list sub division combo.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F49912_ListSubDivisionCombo();

        #endregion F49912 LegalField Listing

        #region F36061 Depreciation Control

        #region F36061_ListDepr

        /// <summary>
        /// Used to List the Depr Details
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depr Details
        /// </returns>
        [OperationContract]
        string F36061_ListDepr(int nbhdId);

        #endregion F36061_ListDepr

        #region F36061_ListDeprControlItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        [OperationContract]
        string F36061_ListDeprControlItems(int nbhdId);

        #endregion F36061_ListDeprControlItems

        #region F36061_SaveDeprControlItems

        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        [OperationContract]
        int F36061_SaveDeprControlItems(int? nbhdId, string deprControlItems, int userId);

        #endregion F36061_SaveDeprControlItems

        #endregion F36061 Depreciation Control

        #region F36062 Influence Control

        #region F36062_LandInfluenceItems

        /// <summary>
        /// Used to Get the Depreciation Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Depreciation Control Items Details
        /// </returns>
        [OperationContract]
        string F36062_LandInfluenceItems(int nbhdId);

        #endregion F36062_LandInfluenceItems

        #region F36062_SaveInfluenceControl
        /// <summary>
        /// Used to save the Depreciation Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="deprControlItems">The depr control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        [OperationContract]
        int F36062_SaveInfluenceControl(int? nbhdId, string InfluenceItems, int userId);

        #endregion F36062_SaveInfluenceControl

        #endregion F36062 Influence Control

        #region F35050ScheduleLineItem

        #region F35050_GetScheduleLineItemDetails

        /// <summary>
        /// F35050_GetScheduleLineItemDetails
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <returns>string</returns>
        [OperationContract]
        string F35050_GetScheduleLineItemDetails(int scheduleId);

        #endregion F35050_GetScheduleLineItemDetails

        #region F35050_GetScheduleItem

        /// <summary>
        /// F35050_GetScheduleItem
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F35050_GetScheduleItem();

        #endregion F35050_GetScheduleItem


        #region F35050ListTableDetails

        [OperationContract]
        string F35050_GetListTableDetails(int itemcategoryID);


        #endregion

        #region F35050ListOutTableDetails

        [OperationContract]
        string F35050_GetListOutTableDetails(int ScheduleID);

        #endregion



        #region F35050_GetScheduleCategory

        /// <summary>
        /// F35050_GetScheduleCategory
        /// </summary>
        /// <returns>string</returns>
        [OperationContract]
        string F35050_GetScheduleCategory();

        #endregion F35050_GetScheduleCategory

        #region F35050_SaveScheduleLineItem

        /// <summary>
        /// F35050_SaveScheduleLineItem
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="scheduleItems">scheduleItems</param>
        /// <param name="userId">userId</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        int F35050_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId);

        #endregion F35050_SaveScheduleLineItem

        #region  #region F35050_CalculateAmount

        /// <summary>
        /// F35050_CalculateAmount
        /// </summary>
        /// <param name="ScheduleItemID">ScheduleItemID</param>
        /// <param name="Rollyear">Rollyear</param>
        /// <param name="Year">Year</param>
        /// <param name="Qnty">Qnty</param>
        /// <returns></returns>
        [OperationContract]
        string F35050_CalculateAmount(int ScheduleItemID, int Rollyear, int Year, int DeprDescription);

        #endregion

        #region F35050_GetDepreciationValue

        /// <summary>
        /// F35050_GetDepreciationValue
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="recv">recv</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>dataset</returns>
        [OperationContract]
        string F35050_GetDepreciationValue(int scheduleId, int recv, int rollYear);

        #endregion F35050_GetDepreciationValue

        #region F35050_DeleteScheduleLineItem

        /// <summary>
        /// DeleteScheduleLineItem
        /// </summary>
        /// <param name="scheduleLineId">scheduleLineId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        [OperationContract]
        int F35050_DeleteScheduleLineItem(int scheduleLineId, int userId);

        #endregion F35050_DeleteScheduleLineItem

        [OperationContract]
        string F35050_GetDeprPercentage(int rollyear, int deprtableID, int year);

        #region DeleteSchedule

        /// <summary>
        /// F35050_s the delete schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status</returns>
        int F35050_DeleteSchedule(int scheduleId, int userId);

        #endregion

        #endregion F35050ScheduleLineItem

        #region F1402 Schedule Search
        [OperationContract]
        string F1402_ListScheduleSearch(string ScheduleConditionXML);
        #endregion

        #region CustomGridFunctionality

        /// <summary>
        /// Lists the query engine grid function.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Count</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        System.Data.DataSet ListQueryEngineGridFunction(int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, string isFilter, string maxRecord);

        /// <summary>
        /// Lists the query engine grid snapshot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Count</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        System.Data.DataSet ListQueryEngineGridSnapshot(int snapShotId, int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter, string maxRecord);

        /// <summary>
        /// Lists the columns.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        System.Data.DataSet ListColumns(int queryViewId);

        #endregion CustomGridFunctionality

        #region F27010 MiscAssessment

        #region GetRollYear
        /// <summary>
        /// F27010s the get roll year.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F27010GetRollYear(int parcelId);
        #endregion GetRollYear

        #region Get Assessment Type
        /// <summary>
        /// F27010s the type of the get assessment.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F27010GetAssessmentType(int rollYear);
        #endregion Get Assessment Type

        #region GetDistrict
        /// <summary>
        /// F27010s the get district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F27010GetDistrict(int parcelId, int madTypeId, int rollYear);
        #endregion GetDistrict

        #region Check Default District
        /// <summary>
        /// F27010s the check default district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F27010CheckDefaultDistrict(int parcelId, int madTypeId, int rollYear);
        #endregion Check Default District

        #region Get ToolTip Message
        /// <summary>
        /// F27010s the get message.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F27010GetMessage(int parcelId, int madTypeId, int madDistrictId);
        #endregion Get ToolTip Message

        #region GetMiscAssessment (MADType1)
        /// <summary>
        /// F27010s the get misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        [OperationContract]
        string F27010GetMiscData(int madDistrictId, int parcelId);
        #endregion GetMiscAssessment (MADType1)

        #region GetMiscAssessment (Other MADType)
        /// <summary>
        /// F27010s the get others misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F27010GetOthersMiscData(int madDistrictId, int parcelId, string procedureName);
        #endregion GetMiscAssessment (Other MADType)

        #region GetDefaultMiscData

        /// <summary>
        /// F27010s the get default misc data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <returns></returns>
        [OperationContract]
        string F27010GetDefaultMiscData(int parcelId, int madTypeId);
        #endregion GetDefaultMiscData

        #region SaveMiscAssessment
        /// <summary>
        /// F27010_s the save misc assessment.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="miscType">Type of the misc.</param>
        /// <param name="madItem">The mad item.</param>
        /// <param name="miscItems">The misc items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F27010_SaveMiscAssessment(int parcelId, string miscType, string madItem, string miscItems, int userId);
        #endregion SaveMiscAssessment

        #endregion F27010 MiscAssessment

        #region F84401 Signs Properties

        #region Get Signs Properties
        /// <summary>
        /// F84401_s the get signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>DataSet contains Signs Property</returns>
        [OperationContract]
        string F84401_GetSignsProperties(int featureId);
        #endregion Get Signs Properties

        #region Save Signs Properties
        /// <summary>
        /// F84401_s the save signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="signsProperties">The signs properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F84401_SaveSignsProperties(int featureId, string signsProperties, int userId);
        #endregion Save Signs Properties

        #region Delete Signs Properties
        /// <summary>
        /// F84401_s the delete signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F84401_DeleteSignsProperties(int featureId, int userId);
        #endregion Delete Signs Properties

        #endregion F84401 Signs Properties

        #region F29531 AssociationLink-LinkType

        /// <summary>
        /// F29531s the type of the association link.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F29531AssociationLinkType(int userid);

        /// <summary>
        /// F29530_s the fill association Link grid.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns></returns>
        [OperationContract]
        string F29531_FillAssociationLinkGrid(int keyid, int formId);

        /// <summary>
        /// F29531_s the save association link.
        /// </summary>
        /// <param name="associationid">The associationid.</param>
        /// <param name="associationLinkItems">The association link items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F29531_SaveAssociationLink(int associationid, string associationLinkItems, int userId);

        /// <summary>
        /// Updates the association link details.
        /// </summary>
        /// <param name="associationDetails">The association details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        void UpdateAssociationLinkDetails(string associationDetails, int userId);
        /// <summary>
        /// F29531_s the fill association link grid.
        /// </summary>
        /// <param name="cfgid">The cfgid.</param>
        /// <param name="keyid">The keyid.</param>
        /// <returns></returns>
        [OperationContract]
        string F29531_GetLinkText(int cfgid, int keyid);

        /// <summary>
        /// F29531_s the delete association link.
        /// </summary>
        /// <param name="associationId">The association id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29531_DeleteAssociationLink(int associationId, int userId);

        #endregion

        #region F29610 HoHExemptionDetails

        /// <summary>
        /// F29610_s the get ho H exemption details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        [OperationContract]
        string F29610_GetHoHExemptionDetails(int eventid);

        /// <summary>
        /// F29610_s the get calculation of ho H.
        /// </summary>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <param name="exemptionid">The exemptionid.</param>
        /// <returns></returns>
        [OperationContract]
        string F29610_GetCalculationOfHoH(int scheduleid, int exemptionid);

        /// <summary>
        /// F29610_s the get owner percent.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <returns></returns>
        [OperationContract]
        string F29610_GetOwnerPercent(int ownerId, int scheduleid);

        /// <summary>
        /// F29610_s the save ho H exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="HoHItems">The ho H items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F29610_SaveHoHExemptionDetails(int eventId, string HoHItems, int userId);

        #endregion

        #region F9610 QuickFind

        /// <summary>
        /// F9610s the quick find.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        [OperationContract]
        string F9610QuickFind(int form, string keyword);

        #endregion

        #region F9110MasterNameSearch
        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F9110GetMasterNameSearch(string lastName, string firstName, string address);
        #endregion

        #region F1411ParcelStmtSearch
        /// <summary>
        /// Gets the Parcel Statement search.
        /// </summary>
        /// <param name="Search Number">Search Number.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string F1411ParcelStatementSearch(string searchNumber);

        #endregion

        #region F29620Agland Application Details

        [OperationContract]
        string F29620_GetAglandApplicationDetails(int eventid);

        [OperationContract]
        int F29620_SaveAglandApplicationDetails(int eventId, int ownerId, int userId);

        #endregion

        #region StateAssessedOwner
        /// <summary>
        /// F35075_s the get state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35075_GetStateAssessedOwnerDetails(int stateId);


        [OperationContract]
        int F35075_SaveStateAssessedOwner(int? stateId, string assessedItems, int userId);


        [OperationContract]
        int F35076_SaveStateAssessedGrid(int? stateId, string codeItems, int userId);

        /// <summary>
        /// F35075_s the delete state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F35075_DeleteStateAssessed(int stateId, int userId);

        /// <summary>
        /// F35076_s the delete state assessed details.
        /// </summary>
        /// <param name="stateIemId">The state iem id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F35076_DeleteStateAssessedDetails(int stateIemId, int userId);


        #endregion

        #region F2204 CopySchedule

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F25050GetParcelTypeDetails();

        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F25050GetScheduleTypeDetails();

        /// <summary>
        /// Creates the new parcel copy.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        [OperationContract]
        int F2204CreateNewScheduleCopy(int scheduleId, string scheduleItems, int userId);

        #endregion F2204 CopySchedule

        #region F24630 BoardOfEqualization

        /// <summary>
        /// F29630s the get board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F29630GetBoardOfEqualizationDetails(int boeId);

        /// <summary>
        /// F29630s the save board of equalization details.
        /// </summary>
        /// <param name="boeElements">The boe elements.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29630SaveBoardOfEqualizationDetails(string boeElements, string boeValues, int userId);

        /// <summary>
        /// F29630s the delete board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29630DeleteBoardOfEqualizationDetails(int boeId, int userId);

        /// <summary>
        /// F29630s the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29630PushBoardOfEqualizationDetails(int boeId, int userId);

        #endregion F24630 BoardOfEqualization

        #region F9041 Query View Description

        #region Get QueryDescription

        /// <summary>
        /// F9041s the get query description.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        [OperationContract]
        string F9041GetQueryDescription(int queryViewId);

        #endregion Get QueryDescription

        #endregion F9041 Query View Description

        #region F82006 Contractor Management

        /// <summary>
        /// F82006_s the get contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <returns>contractManagementData</returns>
        [OperationContract]
        string F82006_GetContractorList(int contractorId);

        /// <summary>
        /// F82006_s the save contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="contractorXml">The contractor XML.</param>
        /// <param name="contractorEmployeeXml">The contractor employee XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>errorId</returns>
        [OperationContract]
        int F82006_SaveContractorList(int? contractorId, string contractorXml, string contractorEmployeeXml, int userId);

        /// <summary>
        /// F82006_s the delete contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F82006_DeleteContractorList(int contractorId, int userId);

        /// <summary>
        /// F82006_s the delete contractor employee.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F82006_DeleteContractorEmployee(int contractorId, int employeeId, int userId);

        #endregion F82006 Contractor Management

        #region F9042 Analytics Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        [OperationContract]
        string F9042_GetTemplate(int templateId);

        /// <summary>
        /// F9042_s the list template.
        /// </summary>
        /// <param name="queryView">The query view.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        [OperationContract]
        string F9042_ListTemplate(string queryView);

        #endregion F9042 Analytics Template Selection

        [OperationContract]
        string GetSnapshotDetails(int FormNum, int UserId);
        /// <summary>
        /// F9044_GetSnapshotOperationCount.
        /// </summary>
        /// <returns>GetSnapshotOperationCount</returns>
        [OperationContract]
        string GetSnapshotOperationCount(int OperationId, int LOSnapshotId, int ROSnapshotId, int UserId);
        /// <summary>
        /// F9044_insertSnapshotDetailst.
        /// </summary>
        /// <returns>insertSnapshotDetails</returns>
        [OperationContract]
        void insertSnapshotDetails(int OperationId, int LOSnapshotId, int ROSnapshotId, int RecordCount, string NewSnapshotName, int UserId);

        #region F81003 Selection Catalog

        /// <summary>
        /// F81003_s the get selection catalog details.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <returns>selection catalog details</returns>
        [OperationContract]
        string F81003_GetSelectionCatalogDetails(int catalogId);

        /// <summary>
        /// F81003_s the list selection category.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>category details</returns>
        [OperationContract]
        string F81003_ListSelectionCategory(int userId);

        /// <summary>
        /// F81003_s the save selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <returns>key id</returns>
        [OperationContract]
        int F81003_SaveSelectionCatalog(int? catalogId, string selectionItemsXml);

        /// <summary>
        /// F81003_s the delete selection catalog.
        /// </summary>
        /// <param name="catalogId">The catalog id.</param>
        [OperationContract]
        void F81003_DeleteSelectionCatalog(int catalogId);

        #endregion F81003 Selection Catalog

        #region F9510GetWebFormXML

        /// <summary>
        /// Gets the web form XML.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>String</returns>
        [OperationContract]
        string F9510GetWebFormXML(int form, int userId);

        #endregion F9510GetWebFormXML

        #region F9075 List Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        [OperationContract]
        string F9075_ListTemplate(int form, int userid);

        #region F9075_DeleteCommentsIds

        /// <summary>
        /// F9075_s the delete comments.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F9075_DeleteCommentIds(string commentIds, int userId);

        #endregion F9075_DeleteCommentsIds

        #endregion F9075 List Template Selection

        #region F9076New Comment Template

        #region F9076 list Template Selection

        [OperationContract]
        string F9076_getTemplate(int templateid);

        #endregion F9076 list Template Selection

        #region F9076 SaveNewCommentTemplate Selection

        [OperationContract]
        int F9076SaveNewCommentTemplate(int? templateId, string commentItemsXml, int isOverwrite);

        #endregion F9076 SaveNewCommentTemplate Selection

        #region F9076 DeleteNewCommentTemplate Selection

        [OperationContract]
        void F9076_DeleteNewCommentTemplate(int templateId);

        #endregion F9076 DeleteNewCommentTemplate Selection

        #endregion New Comment Template

        #region F29505CreateSubdivision
        /// <summary>
        /// F429505_s the list all comoboxes.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F429505_ListAllComoboxes(int eventId);

        /// <summary>
        /// F429505_s the list all landCodes.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string ListLandCodes(int nbhdid, int rollyear);

        /// <summary>
        /// F29505_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        [OperationContract]
        string F29505_GetBaseParcelValue(int eventId);

        /// <summary>
        /// F29505_s the create parcel.
        /// </summary>
        /// <param name="makeSubId">The make sub id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        string F29505_CreateParcel(int makeSubId, int userId);

        /// <summary>
        /// F29505_s the save division parcels.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="makeSubItemsXml">The make sub items XML.</param>
        /// <param name="makeSubParcelsXml">The make sub parcels XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F29505_SaveDivisionParcels(int eventId, string makeSubItemsXml, string makeSubParcelsXml, int userId);

        /// <summary>
        /// F29505_s the save sub division.
        /// </summary>
        /// <param name="makeSubID">The make sub ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F29505_SaveSubDivision(int makeSubID, int userId);

        /// <summary>
        /// F29505_s the get land code.
        /// </summary>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="nbhdid">The nbhdid.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        [OperationContract]
        string F29505_GetLandCode(int landType1, int landType2, int landType3, int nbhdid, int rollYear);

        #endregion

        #region F9025ValidationControl

        #region F9025 FormValidationDetails Selection

        [OperationContract]
        int F9025FormValidationDetails(int formid, int userid);

        #endregion F9025 FormValidationDetails Selection

        #region F9025 SaveValidationDetails Selection

        [OperationContract]
        int F9025SaveValidationDetails(int formid, int userid, int keyid);

        #endregion F9025 SaveValidationDetails Selection

        #endregion F9025ValidationControl

        #region Selection

        /// <summary>
        /// F81004_s the get selection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>selection dataset</returns>
        [OperationContract]
        string F81004_GetSelectionDetails(int eventId, int form);

        /// <summary>
        /// F81004_s the get selection catalog details.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>selection catalog data table</returns>
        [OperationContract]
        string F81004_GetSelectionCatalogDetails(int categoryId);

        /// <summary>
        /// F81004_s the save selection items.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>eventId</returns>
        [OperationContract]
        int F81004_SaveSelectionItems(int eventId, string selectionItemsXml, int userId);

        #endregion Selection

        #region F24640 Frozen Value

        #region Get Frozen Value

        /// <summary>
        /// Gets the frozen value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Frozen value</returns>
        [OperationContract]
        string GetFrozenValue(int eventId);

        #endregion Get Frozen Value

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        /// <param name="frozenElements">The frozen elements.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveFrozenDetails(string frozenElements, int userId);

        #endregion Save Frozen Value

        #region Delete Frozen Value

        /// <summary>
        /// Deletes the frozen details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="frozenId">The frozen id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void DeleteFrozenDetails(int eventId, int frozenId, int userId);

        #endregion Delete Frozen Value

        #endregion F24640 Frozen Value

        #region F24650 Exemption

        #region Get Exemption Type

        /// <summary>
        /// Get Exemption Type
        /// </summary>
        /// <param name="eventId">The Event ID</param>
        /// <returns>DataSet contains Exemption Types</returns>
        [OperationContract]
        string GetExemptionType(int eventId);

        #endregion Get Exemption Type

        #region Get Exemption

        /// <summary>
        /// Get Exemption Details
        /// </summary>
        /// <param name="eventId">The Event ID</param>
        /// <returns>DataSet contains Exemption Details</returns>
        [OperationContract]
        string GetExemptionDetails(int eventId);

        #endregion Get Exemption

        #region Get Exemption Loss\

        /// <summary>
        /// Get Exemption Loss
        /// </summary>
        /// <param name="lossValue">The Loss Value</param>
        /// <param name="maxValue">The Maximum Value</param>
        /// <returns>Exemption Loss</returns>
        [OperationContract]
        decimal GetExemptionLoss(decimal lossValue, decimal maxValue);

        #endregion Get Exemption Loss

        #region Save Exemption

        /// <summary>
        /// Save Exemption Deatils
        /// </summary>
        /// <param name="exemptionElements">Exemption Details</param>
        /// <param name="userId">The User ID</param>
        [OperationContract]
        void SaveExemptionDetails(string exemptionElements, int userId);

        #endregion Save exemption

        #region Delete Exemption

        /// <summary>
        /// Delete Exemption Details
        /// </summary>
        /// <param name="eventId">The Event ID</param>
        /// <param name="exemptionEventId">Exemption Event ID</param>
        /// <param name="userId">User ID</param>
        [OperationContract]
        void DeleteExemptionDetails(int eventId, int exemptionEventId, int userId);

        #endregion Delete Exemption

        #endregion F24650 Exemption

        #region F35060 Schedule Item Code

        #region Get Schedule Item Code

        /// <summary>
        /// Gets the schedule item codes.
        /// </summary>
        /// <returns>DataSet contains Schedule Item Codes</returns>
        [OperationContract]
        string GetScheduleItemCodes();

        #endregion Get Schedule Item Code

        #region Save Schedule Item Code

        /// <summary>
        /// Saves the schedule item codes.
        /// </summary>
        /// <param name="scheduleCodeElements">The schedule code elements.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void SaveScheduleItemCodes(string scheduleCodeElements, int userId);

        #endregion Save Schedule Item Code

        #region Delete Schedule Item Code

        /// <summary>
        /// Deletes the schedule item codes.
        /// </summary>
        /// <param name="itemCodeId">The item code id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void DeleteScheduleItemCodes(string itemCodeId, int userId);

        #endregion Delete Schedule Item Code

        #endregion F35060 Schedule Item Code

        #region Calling WCF Trigger Test Method

        /// <summary>
        /// WCFs the trigger test method.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string WCFTriggerTestMethod();

        #endregion Calling WCF Trigger Test Method

        #region F2409Review Status
        /// <summary>
        /// F2409_s the type of the reviewstatus inspection.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F2409_ReviewstatusInspectionType();


        /// <summary>
        /// F2409_Reviewstatus
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F2409_Reviewstatus();

        /// <summary>
        /// F2409_s the reviewstatus inspection by user.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F2409_ReviewstatusInspectionByUser(int applicationId);

        /// <summary>
        /// F2409_s the list reviewstatus.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        [OperationContract]
        string F2409_ListReviewstatus(int parcelId);

        /// <summary>
        /// F2409s the update parcel review details.
        /// </summary>
        /// <param name="reviewXML">The review XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        void F2409UpdateParcelReviewDetails(string reviewXML, int userId);

        #endregion

        #region F2205 Move Schedule

        /// <summary>
        /// F2205s the create schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isNewSchedule">if set to <c>true</c> [is new schedule].</param>
        /// <param name="scheduleHeaderItems">The schedule header items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for created schedule</returns>
        [OperationContract]
        int F2205CreateSchedule(int? scheduleId, bool isNewSchedule, string scheduleHeaderItems, string scheduleItems, int userId);

        #endregion F2205 Move Schedule

        #region F35055PPLine Items
        /// <summary>
        /// F35055_s the get PP line items details.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns>returns F35055PPLineItemData</returns>
        [OperationContract]
        string F35055_GetPPLineItemsDetails(int scheduleID);

        /// <summary>
        /// F35055_s the get value calculation.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="ppDeprTableId">The pp depr table id.</param>
        /// <param name="originalValue">The original value.</param>
        /// <param name="trend">The trend.</param>
        /// <param name="year">The year.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns F35055PPLineItemData</returns>
        [OperationContract]
        string F35055_GetValueCalculation(int scheduleId, int ppDeprTableId, Int64 originalValue, int trend, Int16 year, Int16 rollYear);

        /// <summary>
        /// F35055_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Integer</returns>
        [OperationContract]
        int F35055_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId);

        /// <summary>
        /// F35055_s the update schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns Integer</returns>
        [OperationContract]
        int F35055_UpdateScheduleLineItem(int scheduleId, string scheduleItems, int userId, Int16 rollYear);

        /// <summary>
        /// F35055_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns Integer</returns>
        [OperationContract]
        int F35055_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId);

        #endregion

        #region F36066 Trend

        #region Check Trend

        /// <summary>
        /// Checks the trend roll year.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Confirmation value</returns>
        [OperationContract]
        int CheckTrendRollYear(int? trendYearId, int rollYear);

        #endregion Check Trend

        #region Get Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <returns>Trend Details</returns>
        [OperationContract]
        string GetTrendDetails(int trendYearId);

        #endregion Get Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendYearItems">The trend year items.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for save</returns>
        [OperationContract]
        int SaveTrend(int? trendYearId, string trendYearItems, string trendItems, int userId);

        #endregion Save Trend

        #region Delete Trend

        /// <summary>
        /// Deletes the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void DeleteTrend(int? trendYearId, string trendItems, int userId);

        #endregion Delete Trend

        #endregion F36066 Trend

        #region F35051 Schedule Line Items

        /// <summary>
        /// F35051_s the get schedule line item details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The schedule line items.</returns>
        [OperationContract]
        string F35051_GetScheduleLineItemDetails(int scheduleId);

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        [OperationContract]
        int F35051_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId);

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        [OperationContract]
        int F35051_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId);

        /// <summary>
        /// F35051_s the get depr percentage.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="year">The year.</param>
        /// <returns>The schedule line items dataset.</returns>
        [OperationContract]
        string F35051_GetDeprPercentage(Int16 rollYear, int deprTableId, Int16 year);

        #endregion F35051 Schedule Line Items

        #region F25055 Personal Property Header

        /// <summary>
        /// Gets the property header details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>Personal property header details</returns>
        [OperationContract]
        string GetPropertyHeaderDetails(int scheduleId);

        #endregion F25055 Personal Property Header

        #region F36065 Personal Property Depreciation

        #region Check Depreciation RollYear

        /// <summary>
        /// F36065_s the check depr roll year.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>confirmation value</returns>
        [OperationContract]
        int F36065_CheckDeprRollYear(int? deprYearId, int rollYear);

        #endregion Check Depreciation RollYear

        #region Get Depreciation Details

        /// <summary>
        /// F36065_s the get depr details.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <returns>Dataset contains depreciation details</returns>
        [OperationContract]
        string F36065_GetDeprDetails(int deprYearId);

        #endregion Get Depreciation Details



        #region Save Depreciation

        /// <summary>
        /// F36065_s the save depreciation.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="deprYearItems">The depr year items.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>confirmation value for save</returns>
        [OperationContract]
        int F36065_SaveDepreciation(int? deprYearId, string deprYearItems, string depreciationItems, int userId);

        #endregion Save Depreciation

        #region Delete Depreciation

        /// <summary>
        /// F36065_s the delete depreciattion.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F36065_DeleteDepreciattion(int? deprYearId, string depreciationItems, int userId);

        #endregion Delete Depreciation

        #endregion F36065 Personal Property Depreciation

        #region F15020 Receipt Type

        /// <summary>
        /// F15020_s the type of the G get receipt.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Receipt Types</returns>
        [OperationContract]
        string F15020_GetReceiptTypes(int userId, short formId, int keyId);

        #endregion F15020 Receipt Type

        #region F1504 Copy Account
        /// <summary>
        /// F1504_s the get copy account sub fund.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F1504_GetCopyAccountSubFund();

        /// <summary>
        /// F1504_s the get account detail.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns></returns>
        [OperationContract]
        string F1504_GetAccountDetail(int accountId);

        /// <summary>
        /// F1504_s the save copy account details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="function">The function.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="accObject">The acc object.</param>
        /// <param name="line">The line.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        string F1504_SaveCopyAccountDetails(int rollYear, string subFund, string description, string function, string bars, string accObject, string line, string userId);

        #endregion

        #region F32012 Catalog

        /// <summary>
        /// F32012_s the get catalog data.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Catalog Data</returns>
        [OperationContract]
        string F32012_GetCatalogData(int valueSliceId);

        /// <summary>
        /// F32012_s the save catalog.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="catalogData">The catalog data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation value for save</returns>
        [OperationContract]
        DataSet F32012_SaveCatalog(int objectId, string catalogData, int userId);

        #endregion F32012 Catalog

        #region F3205 Apex Sketch

        ///<summary>
        /// ApexSketchImage
        /// </summary>
        [OperationContract]
        string F3205pcgetSketchFilePath(int parcelId, int userId);

        /// <summary>
        ///F3205 pcget SketchLinks Exist.
        /// </summary>
        [OperationContract]
        string F3205pcgetSketchLinksExist(int parcelId, int userId);

        /// <summary>
        /// Saves the sketch Image Path.
        /// </summary>
        [OperationContract]
        string F3205pcinsSketchImage(int parcelId, int userId, int pageCount);

        /// <summary>
        /// insert Apex Sketch
        /// </summary>
        [OperationContract]
        void SaveApexSketch(string SketchDataXML, int parcelId, int userId);

        ///<summary>
        /// ReCalc Values
        /// </summary>
        [OperationContract]
        string F3205_pcexeReCalcValues(int userId, int parcelId);

        #endregion Apex Sketch

        #region F1403 ParcelSection

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1403ParcelSearch</returns>
        [OperationContract]
        string F1403_GetParcelType(int? parcelId);

        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1403ParcelSearch</returns>
        [OperationContract]
        string F1403_GetSearchResult(string parcelSearchXml);

        /// <summary>
        /// F1403_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        [OperationContract]
        string F1403_GetSaleTrackingRollYear(int eventID);


        #endregion F1403 ParcelSection

        #region F1404 Schedule Search
        /// <summary>
        /// F1404_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns></returns>
        [OperationContract]
        string F1404_ListScheduleSearch(string ScheduleConditionXML);

        /// <summary>
        /// F1404_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        [OperationContract]
        string F1404_GetScheduleType(int? scheduleId);

        /// <summary>
        /// F1403_s the get Schedule tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        [OperationContract]
        string F1404_GetScheduleTrackingRollYear(int eventID);

        #endregion

        #region F1405 State Search
        /// <summary>
        /// F1405_s the list state search.
        /// </summary>
        /// <param name="StateConditionXML">The state condition XML.</param>
        /// <returns></returns>
        [OperationContract]
        string F1405_ListStateSearch(string StateConditionXML);

        /// <summary>
        /// F1405_s the type of the get state.
        /// </summary>
        /// <param name="scheduleId">The state id.</param>
        /// <returns></returns>
        //[OperationContract]
        //string F1405_GetStateType(int? stateId);

        #endregion

        #region F28000 Discretionary Details

        #region Get Discretionary Details

        /// <summary>
        /// Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Discretionary Details</returns>
        [OperationContract]
        string F28000_GetDiscretionaryDetails(int eventId);

        #endregion Get Discretionary Details

        #region Get Class

        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        [OperationContract]
        string F28000_GetClass(int stateId, int eventId);

        #endregion Get Class

        #region Exemption Amount

        /// <summary>
        /// Exemption Amount
        /// </summary>
        /// <param name="rollYear">roll Year</param>
        /// <param name="exemptionYear">Exemption Year</param>
        /// <param name="subjectAmount">Subject Amount</param>
        /// <returns>Exemption Amount</returns>
        [OperationContract]
        string F28000_GetExemptionAmount(int rollYear, int exemptionYear, decimal subjectAmount);

        #endregion Exemption Amount

        #region Save Discretionary Details

        /// <summary>
        /// Save Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML string</param>
        /// <param name="userId">User ID</param>
        /// <returns>Confirmation Value</returns>
        [OperationContract]
        int F28000_SaveDiscretionaryDetail(int eventId, int? discretionaryId, string discretionaryItems, int userId);

        #endregion Save Discretionary Details

        #region Delete Discretionary Details

        /// <summary>
        /// Delete Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML String</param>
        /// <param name="userId">USer ID</param>
        [OperationContract]
        void F28000_DeletediscretionaryDetails(int? discretionaryId, string discretionaryItems, int userId);

        #endregion Delete Discretionary Details

        #endregion F28000 Discretionary Details

        #region F28100 BOE

        #region Get BOE Details

        /// <summary>
        /// Get BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>BOE Details</returns>
        [OperationContract]
        string F28100_GetBOEDetails(int eventId);

        #endregion Get BOE Details

        #region Get Total Amount

        /// <summary>
        /// Get Total amounts
        /// </summary>
        /// <param name="boeId">boe ID</param>
        /// <param name="eventId">Event ID</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Total values</returns>
        [OperationContract]
        string F28100_GetTotalAmount(int boeId, int eventId, string assessedValues);

        #endregion Get Total Amount

        #region Save BOE Details

        /// <summary>
        /// Save BOE Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="boeItems">BOE Items</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <param name="userId">User ID</param>
        /// <returns>Primary Key</returns>
        [OperationContract]
        int F28100_SaveBOEDetails(int eventId, string boeItems, string assessedValues, int userId);

        #endregion Save BOE Details

        #region Delete BOE Details

        /// <summary>
        /// Delete BOE
        /// </summary>
        /// <param name="boeId">BOE ID</param>
        /// <param name="userId">The User ID</param>
        [OperationContract]
        void F28100_DeleteBOEDetails(int? boeId, int userId);

        #endregion Delete BOE Details

        #region Push Value
        /// <summary>
        /// F28100 the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28100_PushBOEDetails(int boeId, int userId);

        #endregion Push Value

        #region Local Values

        /// <summary>
        /// Get Local Values
        /// </summary>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assesed Value</returns>
        [OperationContract]
        string F28100_GetLocalValues(string assessedValues);


        #endregion Local Values

        #region County Values

        /// <summary>
        /// Get County Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="assessedValues">Assessed Values</param>
        /// <returns>Assessed Value</returns>
        [OperationContract]
        string F28100_GetCountyValues(bool isLocal, string assessedValues);

        #endregion County Values

        #region State Values

        /// <summary>
        /// Get State Values
        /// </summary>
        /// <param name="isLocal">Is Local</param>
        /// <param name="isCounty">Is Couny</param>
        /// <param name="assessedValues">Assessed Value</param>
        /// <returns>Assessed Value</returns>
        [OperationContract]
        string F28100_GetStateValues(bool isLocal, bool isCounty, string assessedValues);

        #endregion State Values

        #endregion F28100 BOE

        #region F29551 Parcel Sale Tracking

        /// <summary>
        /// DataSet to populate combo values
        /// </summary>
        /// <param name="userId">The User Id</param>
        /// <returns>DataSet to populate combos</returns>
        [OperationContract]
        string F29551_GetParcelSaleComboDetails(int userId);

        /// <summary>
        /// DataSet to Populate Grid and other controls
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User ID</param>
        /// <returns>DataSet to populate Controls</returns>
        [OperationContract]
        string F29551_GetParcelSaleTrackingDetails(int eventId, int userId);

        /// <summary>
        /// Data to populate Owner Grid
        /// </summary>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="ownerId">The Owner Id</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Owner Details DataSet</returns>
        [OperationContract]
        string F29551_GetOwnerDetails(int? saleId, int? ownerId, int? parcelId, int userId);

        /// <summary>
        /// Save ParcelSale Details
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="saleItems">saleItems</param>
        /// <param name="parcelItems">parcelItems</param>
        /// <param name="ownerItems">ownerItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value</returns>
        [OperationContract]
        int F29551_SaveParcelSaleDetails(int eventId, string saleItems, string parcelItems, string ownerItems, int userId);

        /// <summary>
        /// Parcel and Owner details
        /// </summary>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="parcelCollection">Parcel Collections</param>
        /// <param name="saleId">The Sale Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Parcel and Owner details</returns>
        [OperationContract]
        string F29551_GetParcelOwnerDetails(int? parcelId, string parcelCollection, int? saleId, int userId);

        /// <summary>
        /// Create Sale Versions
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <param name="checkedParcels">Checked Parcels List</param>
        /// <returns>Message returned from SP</returns>
        [OperationContract]
        string F29551_CreateSaleVersions(int eventId, int userId, string checkedParcels);

        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        [OperationContract]
        string F29551_TransferOwnership(int eventId, int userId);

        /// <summary>
        /// F29551_s the update sale parcel.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message returned from SP</returns>
        [OperationContract]
        string F29551_UpdateSaleParcel(int eventId, int userId);

        #endregion F29511 Parcel Sale Tracking

        #region F9045 Generic Search

        /// <summary>
        /// F9045s the get configuration.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <returns>Configuration Details</returns>
        [OperationContract]
        string F9045GetConfiguration(int genericSearchId);

        /// <summary>
        /// F9045s the get search results.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Search Results</returns>
        [OperationContract]
        string F9045GetSearchResults(int genericSearchId, string searchString, int userId);

        #endregion F9045 Generic Search

        #region F3201 Sketch Link

        /// <summary>
        /// F3201_s the get sketch link data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Sketch Link Data</returns>
        [OperationContract]
        string F3201_GetSketchLinkData(int parcelId, int userId);

        /// <summary>
        /// F3201_s the save sketch link data.
        /// </summary>
        /// <param name="linkXML">The link XML.</param>
        /// <param name="parcelId">The parcel Id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Message</returns>
        [OperationContract]
        string F3201_SaveSketchLinkData(string linkXML, int parcelId, int userId);

        #endregion F3201 Sketch Link

        #region F1500_GetSampleFormDetails
        /// <summary>
        /// F1500_s the get sample form details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <returns></returns>
        [OperationContract]
        string F1500_GetSampleFormDetails(int FormID);
        #endregion

        /// <summary>
        /// InsertSampleFormDetails
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="SampleFormDetails"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertSampleFormDetails(int FormID, string SampleFormDetails, int UserID);

        /// <summary>
        /// Gets the application id.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetApplicationId();

        /// <summary>
        /// Gets the menu id details.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string GetMenuIdDetails();

        /// <summary>
        /// F1500_s the delete form ID details.
        /// </summary>
        /// <param name="FormID">The form ID.</param>
        /// <param name="GroupID">The group ID.</param>
        [OperationContract]
        void F1500_DeleteFormIDDetails(int FormID, int GroupID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="KeyID"></param>
        /// <param name="KeyField"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [OperationContract]
        int InsertFieldUseDetails(int KeyID, string KeyField, int UserID);

        [OperationContract]
        System.Data.DataSet ClassCode_RGB(string storedProcedureName);

        [OperationContract]
        string f2200GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear);

        #region F35080
        /// <summary>
        /// F35080_s the central assessed owner details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35080_CentralAssessedOwnerDetails(int CentralId);

        /// <summary>
        /// F35080_s the property class combo.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F35080_PropertyClassCombo();

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        void F35080_DeleteOwnerDetails(int centralId, int userId);

        /// <summary>
        /// F35080_s the insert owner central details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="centralXML">The central XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F35080_InsertOwnerCentralDetails(int? centralId, string centralXML, int userId);

        /// <summary>
        /// F35080_s the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35080_OwnerDetails(int ownerId); 
        #endregion

        #region F35081
        /// <summary>
        /// F35081_s the central assessed grid details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35081_CentralAssessedGridDetails(int CentralId);

        /// <summary>
        /// F35081_s the central assessed rate details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="personalProperty">The personal property.</param>
        /// <param name="realProperty">The real property.</param>
        /// <returns></returns>
        [OperationContract]
        string F35081_CentralAssessedRateDetails(int subFundId, decimal personalProperty, decimal realProperty, string description, string centralXMLList);

        /// <summary>
        /// F35081_s the insert owner assessed grid.
        /// </summary>
        /// <param name="centralXMLItems">The central XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        void F35081_InsertOwnerAssessedGrid(string centralXMLItems, int centralId, int userId);

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="removeXMLItems">The remove XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F35081_DeleteOwnerGridDetails(string removeXMLItems, int centralId, int userId); 
        #endregion

        #region F35085

        /// <summary>
        /// F35085_s the import type combo.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string  F35085_ImportTypeCombo();

        /// <summary>
        /// F35085_s the central assessed owner details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        [OperationContract]
        string F35085_CentralAssessedImportDetails(int importId);

        /// <summary>
        /// F35085_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F35085_DeletetemplateDetails(int importId, int userId);

        /// <summary>
        /// F35085_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        DataSet F35085_InsertCentralTemplateDetails(int? importId, string importXML, int userId);

        /// <summary>
        /// F35085_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F35085_ExecuteImport(int importId, string importXML, int userId, bool isProcess);
        /// <summary>
        /// F35085_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F35085_ExecuteCheckForErrors(int importId, int userId);
        /// <summary>
        /// F35085_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F35085_CreateImportRecords(int importId, int userId, bool isProcess);
        #endregion

        #region F16072

        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <returns></returns>
        [OperationContract]
        string F16072_GetMiscteplateDetails(int misctemplateId);

        /// <summary>
        /// F16072_s the save misc receipt template.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscHeaderDetails">The misc header details.</param>
        /// <param name="accountDetails">The account details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F16072_SaveMiscReceiptTemplate(int? misctemplateId, string miscHeaderDetails, string accountDetails, int userId);

        /// <summary>
        /// F16072_s the delete misctemplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F16072_DeleteMisctemplateDetails(int misctemplateId, int userId);

        /// <summary>
        /// F16072_s the delete misc gridtemplate.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <param name="miscIds">The misc ids.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F16072_DeleteMiscGridtemplate(int misctemplateId, string miscIds, int userId);

        #endregion

        #region F16071


        /// <summary>
        /// F16071_s the get journal teplate details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns></returns>
        [OperationContract]
        string F16071_GetJournalTeplateDetails(int templateId);

        /// <summary>
        /// F16071_s the save header template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F16071_SaveHeaderTemplateDetails(int? templateId, int rollYear, string description, int userId);

        /// <summary>
        /// F16071_s the save grid template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F16071_SaveGridTemplateDetails(int? templateId, string gridDetails, int userId);

        /// <summary>
        /// F16071_s the delete journal header details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F16071_DeleteJournalHeaderDetails(int templateId, int userId);

        /// <summary>
        /// F16071_s the delete journal grid details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F16071_DeleteJournalGridDetails(int templateId, string gridDetails, int userId);

        #endregion

        #region F19062

        /// <summary>
        /// F14062_s the grid result details.
        /// </summary>
        /// <param name="ownerIds">The owner ids.</param>
        /// <param name="statementIds">The statement ids.</param>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="stateIds">The state ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
         string F14062_GridResultDetails(string ownerIds,string statementIds,string parcelIds,string scheduleIds,string stateIds,int userId);

        /// <summary>
        /// F14062_s the get statement pull list details.
        /// </summary>
        /// <returns></returns>
		[OperationContract]
	    string F14062_GetStatementPullListDetails();

        /// <summary>
        /// F1407_s the get pull list status.
        /// </summary>
        /// <returns></returns>
	   [OperationContract]
	   string  F1407_GetPullListStatus();


       /// <summary>
       /// F1407_s the type of the get pull list.
       /// </summary>
       /// <returns></returns>
	   [OperationContract]
	    string F1407_GetPullListType();

       /// <summary>
       /// F14062_s the save grid details.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
	   [OperationContract]
       void F14062_SaveGridDetails(string pullListItems, int userId);

       /// <summary>
       /// F14062_s the delete statement pull list.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="isProcess">if set to <c>true</c> [is process].</param>
       /// <returns></returns>
       [OperationContract]
       string F14062_DeleteStatementPullList(string pullListItems, int userId, bool isProcess);
       

        #endregion


        #region 11204

       /// <summary>
       /// F11024_s the get multiple journal template details.
       /// </summary>
       /// <param name="jetTemplateID">The jet template ID.</param>
       /// <returns></returns>
       [OperationContract]
       string F11024_GetMultipleJournalTemplateDetails(int jetTemplateID);

       /// <summary>
       /// F11024_s the save multiple journal template.
       /// </summary>
       /// <param name="transferDate">The transfer date.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="description">The description.</param>
       /// <param name="journalTemplateDetails">The journal template details.</param>
       [OperationContract]
       void F11024_SaveMultipleJournalTemplate(string transferDate, int userId, string description, string journalTemplateDetails);
        
       /// <summary>
       /// F11024_s the search template details.
       /// </summary>
       /// <returns></returns>
       [OperationContract]
       string F11024_SearchTemplateDetails();
        #endregion


        #region F29555

       /// <summary>
       /// F29555_s the deedtype combo box.
       /// </summary>
       /// <returns></returns>
        [OperationContract]
        string F29555_DeedtypeComboBox();

        /// <summary>
        /// F29555_s the save transfer ownership.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        string F29555_SaveTransferOwnership(int eventId, int userId);

        /// <summary>
        /// F29555_s the get personal sales owner.
        /// </summary>
        /// <param name="pSsaleId">The p ssale id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="scheduleString">The schedule string.</param>
        /// <returns></returns>
        [OperationContract]
        string F29555_GetPersonalSalesOwner(int? pSsaleId, int? ownerId, int? scheduleId, int userid, string scheduleString);

        /// <summary>
        /// F29555_s the get sales scheduleand owners.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="pSsaleId">The p ssale id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        [OperationContract]
        string F29555_GetSalesScheduleandOwners(int? scheduleId, string scheduleIds, int? pSsaleId, int userid);

        /// <summary>
        /// F29555_s the schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        [OperationContract]
        string  F29555_ScheduleSaleTracking(int eventId, int userid);

        /// <summary>
        /// F29555_s the save sales owner.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29555_SaveSalesOwner(int pSaleId, string ownerDetails, int userId);

        /// <summary>
        /// F29555_s the save sales schedule.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29555_SaveSalesSchedule(int pSaleId, string scheduleItems, int userId);

        /// <summary>
        /// F29555_s the save schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="pSaleItems">The p sale items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F29555_SaveScheduleSaleTracking(int eventId, string pSaleItems, string scheduleItems, string ownerDetails, int userId);

        #endregion


        #region F2201

        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        [OperationContract]
         string F2201_GetPersonalPropertyDescription(string code);

        /// <summary>
        /// F2201_s the get personal property search.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        [OperationContract]
        string F2201_GetPersonalPropertySearch(string code, string description);
        
        #endregion


        #region F1406

        /// <summary>
        /// F2550_s the state of the get configured.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F2550_GetConfiguredState();

        /// <summary>
        /// F1406_s the central assessed grid details.
        /// </summary>
        /// <param name="centralSearchXML">The central search XML.</param>
        /// <returns></returns>
        [OperationContract]
         string F1406_CentralAssessedGridDetails(string centralSearchXML);

        /// <summary>
        /// F1406_s the load propert class combo.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
         string F1406_LoadPropertClassCombo();
        
        #endregion

        #region F1203


        /// <summary>
        /// F1203s the load due date management.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string F1203LoadDueDateManagement();

        /// <summary>
        /// F1203_s the save due date management.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dueDateXML">The due date XML.</param>
        [OperationContract]
        void F1203_SaveDueDateManagement(int userId, string dueDateXML);

        #endregion


        #region F29636


        /// <summary>
        /// F29636_s the get BOE details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        [OperationContract]
        string F29636_GetBOEDetails(int eventId);

        /// <summary>
        /// F29636_s the BOE type details.
        /// </summary>
        /// <returns></returns>
       [OperationContract]
       string F29636_BOETypeDetails();

       /// <summary>
       /// F29636_s the save BOE details.
       /// </summary>
       /// <param name="boeElemenets">The boe elemenets.</param>
       /// <param name="boeValues">The boe values.</param>
       /// <param name="userId">The user id.</param>
        [OperationContract]
        void F29636_SaveBOEDetails(string boeElemenets,string boeValues, int userId);

        /// <summary>
        /// F29636_s the push BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        string F29636_PushBOEDetails(int boeId,int userId);

        /// <summary>
        /// F29636_s the delete BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
         void F29636_DeleteBOEDetails(int boeId, int userId);

        #endregion

        #region F9105

        /// <summary>
        /// F9105_s the name of the execute copy.
        /// </summary>
        /// <param name="copyData">The copy data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F9105_ExecuteCopyName(string copyData, int userId);       
            
        #endregion


        #region F23210


        /// <summary>
        /// F28210_s the Permit Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        [OperationContract]
        string F28210_PermitImportDetails(int importId);

        /// <summary>
        /// F28210_s the deletetemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28210_DeletetemplateDetails(int importId, int userId);

        /// <summary>
        /// F28210_s the insert central template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F28210_InsertImportPermitDetails(int? importId, string importXML, int userId);

        /// <summary>
        /// F28210_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28210_ExecuteImport(int importId, string importXML, int userId, bool isProcess);
        /// <summary>
        /// F28210_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28210_ExecuteCheckForErrors(int importId, int userId);
        /// <summary>
        /// F28210_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28210_CreateImportRecords(int importId, int userId, bool isProcess);

        /// <summary>
        /// Gets the mortgage import template details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetPermitImportTemplateDetails(string TemplateName, string Description, string FileType);


        #endregion

        #region F23310


        /// <summary>
        /// F28310_s the MAD Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        [OperationContract]
        string F28310_MADImportDetails(int importId);

        /// <summary>
        /// F28310_s the deleteMADtemplate details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28310_DeleteMADImportDetails(int importId, int userId);

        /// <summary>
        /// F28310_s the insert MAD template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F28310_InsertImportMADDetails(int? importId, string importXML, int userId);

        /// <summary>
        /// F28310_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28310_ExecuteImport(int importId, string importXML, int userId, bool isProcess);

        /// <summary>
        /// F28310_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28310_ExecuteCheckForErrors(int importId, int userId);

        /// <summary>
        /// F28310_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28310_CreateImportRecords(int importId, int userId, bool isProcess);

        /// <summary>
        /// Gets the MAD import template details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetMADImportTemplateDetails(string TemplateName, string Description, string FileType);


        #endregion

        /// <summary>
        /// Gets the Snapshot Import template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSnapshotImportTemplate(int templateId);

        /// <summary>
        /// F28510_s the deleteSnapshot details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28510_DeleteSnapshotImportDetails(int importId, int userId);

        /// <summary>
        /// F28510_s the insert Snapshot template details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        [OperationContract]
        int F28510_InsertImportSnapshotDetails(int? importId, string importXML, int userId);

        /// <summary>
        /// F28510_s the execute import.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="importXML">The import XML.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28510_ExecuteImport(int importId, string importXML, int userId, bool isProcess);

        /// <summary>
        /// F28510_s the execute check for errors.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        [OperationContract]
        void F28510_ExecuteCheckForErrors(int importId, int userId);


        /// <summary>
        /// F28510_s the create import records.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isProcess">if set to <c>true</c> [is process].</param>
        /// <returns></returns>
        [OperationContract]
        string F28510_CreateImportRecords(int importId, int userId, bool isProcess);

        /// <summary>
        /// F28510_s the Snapshot Import details.
        /// </summary>
        /// <param name="importId">The import id.</param>
        /// <returns></returns>
        [OperationContract]
        string F28510_SnapshotImportDetails(int importId);

        /// <summary>
        /// Gets the mortgage import template details.
        /// </summary>
        /// <returns>String or dataset</returns>
        [OperationContract]
        string GetSnapshotImportTemplateDetails(string TemplateName, string Description, string FileType);
    }
}