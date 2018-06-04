//--------------------------------------------------------------------------------------------
// <copyright file="F1401WorkItem.cs" company="Congruent">
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
// 14 Aug 07        karthikeyan V            Created
//*********************************************************************************/

namespace D2000
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
    /// F1401WorkItem
    /// </summary>
    public class F1401WorkItem : WorkItem 
    {
        #region WorkItemEvents

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

        #endregion WorkItemEvents

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public F1401ParcelSearch F1401_GetParcelType(int? parcelId)
        {
            return WSHelper.F1401_GetParcelType(parcelId);
        }

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public F1401ParcelSearch.ParcelSearchDataTableDataTable F1401_GetSearchResult(string parcelSearchXml)
        {
            return WSHelper.F1401_GetSearchResult(parcelSearchXml).ParcelSearchDataTable;
        }
    }
}
