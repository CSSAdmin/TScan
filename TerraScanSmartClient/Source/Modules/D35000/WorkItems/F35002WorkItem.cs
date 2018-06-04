//--------------------------------------------------------------------------------------------
// <copyright file="F35002WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the General Adjustment Slice.
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
    /// F35002 General Adjustment Slice WorkItem
    /// </summary>
    public class F35002WorkItem : WorkItem
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

        #region List Adjustment Types

        /// <summary>
        /// F35002_s the type of the list adjustment.
        /// </summary>
        /// <param name="masterFromNo">The master from no.</param>
        /// <returns>Adjustment Types dataTable</returns>
        public F35001ValueSliceHeaderData.ListAdjustmentTypeDataTable F35002_ListAdjustmentType(int? masterFromNo)
        {
            return WSHelper.F35002_ListAdjustmentType(masterFromNo);
        }

        #endregion

        #region Get Adjustment Slice Value

        /// <summary>
        /// F35001_s the get adjustment slice value.
        /// </summary>
        /// <param name="valueSliceID">The value slice ID.</param>
        /// <param name="type">The type.</param>
        /// <param name="isvalue">The is value.</param>
        /// <param name="adjustmentValue">The adjustment value.</param>
        /// <returns>Object Contains the Adjustment Value.</returns>
        public string F35001_GetAdjustmentSliceValue(int valueSliceID, byte type, bool isvalue, decimal adjustmentValue)
        {
            return WSHelper.F35001_GetAdjustmentSliceValue(valueSliceID, type, isvalue, adjustmentValue);
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
    }
}
