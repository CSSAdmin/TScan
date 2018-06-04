// -------------------------------------------------------------------------------------------
// <copyright file="F25000ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25000ParcelHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/10/2013       Purushotham.A       Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// 
    /// </summary>
   public static class F2101LocationSelectionComp
    {

        /// <summary>
        /// F2101_s the get location selection.
        /// </summary>
        /// <param name="locationCode">The location code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
       public static F2101LocationSelectionData f2101_GetLocationSelection(string locationCode,string description )
       {
           F2101LocationSelectionData locationSelectionData = new F2101LocationSelectionData();
           Hashtable ht = new Hashtable();
           ht.Add("@LocationCode", locationCode);
           ht.Add("@Description", description);
           Utility.LoadDataSet(locationSelectionData.F2101LocationSelection, "f2101_pclst_LocationSelection", ht);
           return locationSelectionData;
       }

    }
}
