// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36035WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36035 LandCodes</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 14/09/2007       Kuppu              Created
// 
// ----------------------------------------------------------------------------------------------------------------
namespace D36030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36099 WorkItem Class file
    /// </summary>
    public class F36099WorkItem : WorkItem
    {
        #region F36035LandDetails

        /// <summary>
        /// F36035_s the list land details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Type Dataset Returns LandDetails</returns>
        public F36035LandData F36035_ListLandDetails(int valueSliceId)
        {
            return WSHelper.F36035_ListLandDetails(valueSliceId);
        }

        /// <summary>
        /// F36035_s the list land type details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Type Dataset Returns LandDetails</returns>
        public F36035LandData F36035_ListLandTypeDetails(int valueSliceId)
        {
            return WSHelper.F36035_ListLandTypeDetails(valueSliceId);
        }

        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public int F36035_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return WSHelper.F36035_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }

        /// <summary>
        /// F36035_s the delete land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="userId">The user id.</param>
        public void F36035_DeleteLandDetails(int luid, int userId)
        {
            WSHelper.F36035_DeleteLandDetails(luid, userId);
        }

        /// <summary>
        /// F36035_s the get land code.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="valuesliceId">The valueslice id.</param>
        /// <returns>F36035LandData</returns>
        public F36035LandData F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId,int? AglandID)
        {
            return WSHelper.F36035_GetLandCode(rollYear, landType1, landType2, landType3, valuesliceId,AglandID);
        }

        /// <summary>
        /// F36035_s the get land code base value.
        /// </summary>
        /// <param name="landCode">The land code.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>F36035LandData</returns>
        public F36035LandData F36035_GetLandCodeBaseValue(string landCode, int valueSliceId,int? AglandID)
        {
            return WSHelper.F36035_GetLandCodeBaseValue(landCode, valueSliceId,AglandID);
        }

        #endregion F36035LandDetails

        #region Delete Value Slice

        /// <summary>
        /// F35001_s the delete value slice.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        public void F35001_DeleteValueSlice(int valueSliceId, int userId)
        {
            WSHelper.F35001_DeleteValueSlice(valueSliceId, userId);
        }

        #endregion Delete Value Slice

        #region SliceEvents
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
        #endregion SliceEvents

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
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
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Get Form Slice Permission Details
    }
}
