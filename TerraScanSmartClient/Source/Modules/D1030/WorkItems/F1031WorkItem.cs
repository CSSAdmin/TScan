//--------------------------------------------------------------------------------------------
// <copyright file="F1031WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06        JYOTHI              Created
//*********************************************************************************/
namespace D1030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1031WorkItem
    /// </summary>
    public class F1031WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentDetails(int statementId)
        {
            return WSHelper.F1031_ListDistrictAssessmentDetails(statementId);
        }

        /// <summary>
        /// Lists the Special District Assessment IDs
        /// </summary>
        /// <returns>returns dataset containing District Assessment IDs</returns>
        public F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentIDs()
        {
            return WSHelper.F1031_ListDistrictAssessmentIDs();
        }

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>returns dataset containing District Assessment ParcelID</returns>
        public F1031SpecialDistrictAssessmentData F1031_GetDistrictAssessmentParcelID(string parcelNumber, int? parcelId,int? rollYear)
        {
            return WSHelper.F1031_GetDistrictAssessmentParcelID(parcelNumber, parcelId, rollYear);
        }

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="saDistrictId">The sa district id.</param>
        /// <returns>returns dataset containing specialDistrict Details</returns>
        public F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessment(int saDistrictId)
        {
            return WSHelper.F1031_ListDistrictAssessment(saDistrictId);
        }

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="isOverride">if set to <c>true</c> [is override].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>Key ID</returns>
        public int F1031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool isOverride, bool ownerRide, int userID)
        {
            return WSHelper.F1031_SaveDistrictAssessmentDetails(districtProperty, districtRates, isOverride, ownerRide, userID);
        }

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public void F1031_DeleteDistrictAssessment(int statementId, int userId)
        {
            WSHelper.F1031_DeleteDistrictAssessment(statementId, userId);
        }

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

        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
