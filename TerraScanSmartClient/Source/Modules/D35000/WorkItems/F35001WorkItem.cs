//--------------------------------------------------------------------------------------------
// <copyright file="F35001WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Value Slice Header.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07   	Shiva M     	    Created
//*********************************************************************************/
namespace D35000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F35001 Value Slice Header WorkItem
    /// </summary>
    public class F35001WorkItem : WorkItem
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

        #region Insert/Update Value Slice

        /// <summary>
        /// F35000_s the update value slice.
        /// </summary>
        /// <param name="valueSliceID">The value slice ID.</param>
        /// <param name="valueSliceHeaderItems">The value slice header items.</param>
        /// <returns>Primary Key Id or Error Id.</returns>
        public int F35000_InsertOrUpdateValueSlice(int? valueSliceID, string valueSliceHeaderItems,int userID)
        {
            return WSHelper.F35000_InsertOrUpdateValueSlice(valueSliceID, valueSliceHeaderItems,userID);
        }

        #endregion

        #region Get Value Slice Header

        /// <summary>
        /// F35001_s the get value slice header.
        /// </summary>
        /// <param name="valueSliceID">The value slice ID.</param>
        /// <returns>the DataSet with the Header and Adjustment Values.</returns>
        public F35001ValueSliceHeaderData F35001_GetValueSliceHeader(int valueSliceID)
        {
            return WSHelper.F35001_GetValueSliceHeader(valueSliceID);
        }

        #endregion

        #region Delete Value Slice

        /// <summary>
        /// F35001_s the delete value slice.
        /// </summary>
        /// <param name="valueSliceID">The value slice ID.</param>
        public void F35001_DeleteValueSlice(int valueSliceID,int userId)
        {
            WSHelper.F35001_DeleteValueSlice(valueSliceID,TerraScan.Common.TerraScanCommon.UserId);
        }

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

        #region F35000_CheckAppraisalSummaryUser

        /// <summary>
        /// F35000_s the get appraisal summary objects.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F35000AppraisalSummary DataSet</returns>
        public F35000AppraisalSummaryData F35000_CheckAppraisalSummaryUser(int valueSliceId, int objectId, int userId)
        {
            return WSHelper.F35000_CheckAppraisalSummaryUser(valueSliceId, objectId, userId);
        }

        #endregion F35000_CheckAppraisalSummaryUser
    }
}
