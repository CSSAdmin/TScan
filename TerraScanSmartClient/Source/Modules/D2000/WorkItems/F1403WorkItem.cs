//--------------------------------------------------------------------------------------------
// <copyright file="F1403WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1403WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Oct 07        R.Malliga            Created
//*********************************************************************************/

namespace D2000
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1403WorkItem
    /// </summary>
    public class F1403WorkItem : WorkItem 
    {

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

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public F1403ParcelSearch F1403_GetParcelType(int? parcelId)
        {
            return WSHelper.F1403_GetParcelType(parcelId);
        }

        /// <summary>
        /// F1401_s the type of the get parcel.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public F1401ParcelSearch.ParcelSearchDataTableDataTable F1403_GetSearchResult(string parcelSearchXml)
        {
            return WSHelper.F1401_GetSearchResult(parcelSearchXml).ParcelSearchDataTable;
        }
    }
}
