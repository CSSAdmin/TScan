//--------------------------------------------------------------------------------------------
// <copyright file="F16031WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F16031 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08-06-2007       Shiva              Created
//*********************************************************************************/
namespace D10030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F16031 WorkItem Class.
    /// </summary>
    public class F16031WorkItem : WorkItem
    {
        #region GetForm Detials

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormDetails DataSet</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion

        #region Public Methods

        ///<summary>
        /// List the  Special District Assessment details for Working File
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessmentDetails(int workfileId)
        {
            return WSHelper.F16031_ListDistrictAssessmentDetails(workfileId);  
        }

        /// <summary>
        /// Lists the Special District Assessment ParcelID for Working File
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>returns dataset containing District Assessment ParcelID</returns>
        public F1031SpecialDistrictAssessmentData F16031_GetDistrictAssessmentParcelId(string parcelNumber, int? parcelId, int? rollYear)
        {
            return WSHelper.F16031_GetSpecialAssessmentParcel(parcelNumber, parcelId, rollYear);    
        }


        /// <summary>
        /// Lists the Special District details for WorkingFileID
        /// </summary>
        /// <param name="saaDistrictId">The sdistrict id.</param>
        /// <returns>returns dataset containing specialDistrict Details</returns>
        public F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessment(int saaDistrictId)
        {
            return WSHelper.F16031_ListDistrictAssessment(saaDistrictId);
        }
        /// <summary>
        /// Deletes the District Assessment for WorkingFileId
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public F1031SpecialDistrictAssessmentData F16031_DeleteDistrictAssessment(int workfileId, int userId)
        {
            return WSHelper.F16031_DeleteDistrictAssessment(workfileId, userId);
        }

        /// <summary>
        /// Saves the District Assessment Details for the working FileId
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="issOverride">if set to <c>true</c> [iss override].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Key ID</returns>
        public int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates,int userId)
        {
            return WSHelper.F16031_SaveDistrictAssessmentDetails(districtProperty, districtRates, userId);
        }


        /// <summary>
        /// F1031_s the check special Assessment for working File ID
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public F1031SpecialDistrictAssessmentData F16031_CheckSpecialAssessment(string districtProperty)
        {
            return WSHelper.F16031_CheckSpecialAssessment(districtProperty);
        }

        /// <summary>
        /// Write Statement or cancel Statement
        /// </summary>
        /// <param name="statementId">The Working id.</param>
        /// <param name="userId">The User id.</param>
        /// <param name="IsCancel">The cancel bool.</param>
        /// <returns>Executes the SP</returns>
        public void F16031_ExeWriteStatement(int workingFileId, int userId, bool isCancel)
        {
            WSHelper.F16031_ExeWriteTaxStatement(workingFileId, userId, isCancel);    
        }

      /*  /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessmentDetails(int statementId)
        {
            return WSHelper.F1031_ListDistrictAssessmentDetails(statementId);
        }

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>returns dataset containing District Assessment ParcelID</returns>
        public F1031SpecialDistrictAssessmentData F16031_GetDistrictAssessmentParcelId(string parcelNumber, int? parcelId,int? rollYear)
        {
            return WSHelper.F1031_GetDistrictAssessmentParcelID(parcelNumber, parcelId, rollYear);
        }*/

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }

        /*/// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="saaDistrictId">The sdistrict id.</param>
        /// <returns>returns dataset containing specialDistrict Details</returns>
        public F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessment(int saaDistrictId)
        {
            return WSHelper.F1031_ListDistrictAssessment(saaDistrictId);
        }

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="issOverride">if set to <c>true</c> [iss override].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Key ID</returns>
        public int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool issOverride, bool ownerRide, int userId)
        {
            return WSHelper.F1031_SaveDistrictAssessmentDetails(districtProperty, districtRates, issOverride, ownerRide, userId);
        }

        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public int F16031_CheckSpecialDistrictStatementOrOwner(string districtProperty, bool statementFlag)
        {
            return WSHelper.F1031_CheckSpecialDistrictStatementOrOwner(districtProperty, statementFlag);
        }*/

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /*/// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public void F1031_DeleteDistrictAssessment(int statementId, int userId)
        {
            WSHelper.F1031_DeleteDistrictAssessment(statementId, userId);
        }*/

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
