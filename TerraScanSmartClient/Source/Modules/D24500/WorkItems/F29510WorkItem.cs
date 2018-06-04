//--------------------------------------------------------------------------------------------
// <copyright file="F29510WorkItem.cs" company="Congruent">
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
// 17 Sep 07		D.LathaMaheswari	Created
//*********************************************************************************/

namespace D24500
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
    /// F29500WorkItem
    /// </summary>
    public class F29510WorkItem : WorkItem
    {
        #region Get Parcel Details

        /// <summary>
        /// Gets Parcel Details
        /// </summary>
        /// <param name="eventId">eventID</param>
        /// <returns>DataSet</returns>
        public static F29510ParcelCombineData F29510GetParcelDetails(int eventId)
        {
            return WSHelper.F29510_GetBaseParcelValue(eventId);
        }

        /// <summary>
        /// Get Combine Parcel Details
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>DataSet</returns>
        public static DataSet F29510_GetCombineParcelDetails(int parcelId)
        {
            return WSHelper.F29510_GetCombineParcelDetails(parcelId);
        }

        #endregion Get Parcel Details

        #region F29510_SaveCombineParcelDetails

        /// <summary>
        /// Save Combine Parcel Details
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="combineItems">CombineItems</param>
        /// <returns>int</returns>
        public static int F29510_SaveCombineParcelDetails(int? combineId, string parcelNumber, string combineItems, int userId,bool IsAttachment,bool IsComment,bool IsPermit,bool IsAssociation,bool IsNewConstruction)
        {
            return WSHelper.F29510_SaveCombineParcelDetails(combineId, parcelNumber, combineItems, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction);
        }

        #endregion F29510_SaveCombineParcelDetails

        #region F29510_CreateCombinedParcel

        /// <summary>
        /// Create Combined Parcel Value
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="eventId">EventID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <returns>F29510ParcelCombineData</returns>
        public static F29510ParcelCombineData.OutputValuesDataTable F29510_CreateCombinedParcel(int combineId, string eventId, string parcelNumber, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction)
        {
            return WSHelper.F29510_CreateCombinedParcel(combineId, eventId, parcelNumber, userId, IsAttachment, IsComment, IsPermit, IsAssociation, IsNewConstruction).OutputValues;
        }

        #endregion F29510_CreateCombinedParcel

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
