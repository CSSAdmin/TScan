//--------------------------------------------------------------------------------------------
// <copyright file="F36001WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Jun 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36001
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
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36001WorkItem
    /// </summary>
    public class F36001WorkItem : WorkItem
    {
        #region F36001 Marshal And Swift Commercial

        #region Get Marshal And Swift Commercial

        /// <summary>
        /// To get marshal and swift commercial details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containing the Marshal And Swift Commercial details</returns>
        public F36001MarshalAndSwiftCommercialData F36001_GetMarshalAndSwiftCommercial(int valueSliceId)
        {
            return WSHelper.F36001_GetMarshalAndSwiftCommercial(valueSliceId);
        }

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
        /// <param name="userId">The user id.</param>
        /// <returns>integer Value</returns>
        public int F36001_SaveMarshalAndSwiftCommercial(int valueSliceId, string estimateDetails, string occupancyDetails, string componentDetails, string depreciationXml, int userId)
        {
            return WSHelper.F36001_SaveMarshalAndSwiftCommercial(valueSliceId, estimateDetails, occupancyDetails, componentDetails, depreciationXml, userId);
        }

        #endregion Save Marshal And Swift Commercial

        #region Depreciation Percentage

        /// <summary>
        /// Gets the depr percentage.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        public string GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        {
            return WSHelper.F36000_GetDeprPercentage(age, objectCondition, deprTableId);
        }

        #endregion Depreciation Percentage

        #region Depr Table Name

        /// <summary>
        /// F36000_s the get depr table name id.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="propertyQuality">The property quality.</param>
        /// <returns>int</returns>
        public int GetDeprTableNameId(int valueSliceId, int propertyQuality)
        {
            return WSHelper.F36000_GetDeprTableNameId(valueSliceId, propertyQuality);
        }

        #endregion Depr Table Name

        #endregion F36001 Marshal And Swift Commercial

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
    }
}
