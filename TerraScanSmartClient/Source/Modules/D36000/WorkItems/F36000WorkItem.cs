//--------------------------------------------------------------------------------------------
// <copyright file="F36000WorkItem.cs" company="Congruent">
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
// 28 Mar 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36000
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
    /// F36000WorkItem
    /// </summary>
    public class F36000WorkItem : WorkItem
    {

        #region F36000 Marshal & Swift

        #region Get House Type Collection

        /// <summary>
        /// To Get Marshal and swift House Type collection.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed DataSet containing Marshal and swift House Type collection details.</returns>
        public F36000MarshalAndSwiftData GetHouseTypeCollection(int valueSliceId)
        {
            return WSHelper.F36000_GetHouseTypeCollection(valueSliceId);
        }

        #endregion Get House Type Collection

        #region  SaveNewEstiamte
        /// <summary>
        /// To Get Marshal and swift House Type collection.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>
        /// Typed DataSet containing Marshal and swift House Type collection details.
        /// </returns>
        //public object F36000_CreateNewEstiamte(int constructionType,string zip)
        //{

        //  //  return WSHelper.F36000_CreateNewEstiamte(constructionType, zip);
        //}
        #endregion

        #region Depreciation Percentage

        /// <summary>
        /// F36000_GetDeprPercentage
        /// </summary>
        /// <param name="age"></param>
        /// <param name="objectCondition"></param>
        /// <param name="deprTableId"></param>
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

        #region Depr Save

        /// <summary>
        /// Saves the depreciation details.
        /// </summary>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int SaveDepreciationDetails(string depreciationXml, int valueSliceId, int userId)
        {
            return WSHelper.F36000_SaveDepreciationDetails(depreciationXml, valueSliceId, userId);
        }

        #endregion

        #endregion F36000 Marshal & Swift

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
