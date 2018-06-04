// -------------------------------------------------------------------------------------------
// <copyright file="F1401ParcelSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1401ParcelSelection
    /// </summary>
    public static class F1401ParcelSelectionComp
    {
        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1401ParcelSearch</returns>
        public static F1401ParcelSearch F1401_GetParcelType(int? parcelId)
        {
            F1401ParcelSearch parcelSearchDataSet = new F1401ParcelSearch();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] tableName = new string[] { parcelSearchDataSet.ParcelTypeDataTable.TableName, parcelSearchDataSet.ParcelLabelDataTable.TableName };
            Utility.LoadDataSet(parcelSearchDataSet, "f1401_pclst_ParcelType", ht, tableName);
            return parcelSearchDataSet;
        }

        /// <summary>
        /// F1401_s the get search result.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1401ParcelSearch</returns>
        public static F1401ParcelSearch F1401_GetSearchResult(string parcelSearchXml)
        {
            F1401ParcelSearch parcelSearchDataSet = new F1401ParcelSearch();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelConditionXML", parcelSearchXml);

            Utility.LoadDataSet(parcelSearchDataSet.ParcelSearchDataTable, "f1401_pclst_Parcel", ht);
            return parcelSearchDataSet;
        }
    }
}
