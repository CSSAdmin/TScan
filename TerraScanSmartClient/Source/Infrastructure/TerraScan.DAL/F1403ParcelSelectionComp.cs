// -------------------------------------------------------------------------------------------
// <copyright file="F1403ParcelSelectionComp.cs" company="Congruent">
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
    /// F1403ParcelSelection
    /// </summary>
    public static class F1403ParcelSelectionComp
    {
        /// <summary>
        /// Gets the type of the parcel.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F1403ParcelSearch</returns>
        public static F1403ParcelSearch F1403_GetParcelType(int? parcelId)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            Hashtable ht = new Hashtable();
            ht.Add("@CallingForm", parcelId);
            string[] tableName = new string[] { parcelSearchDataSet.ParcelTypeDataTable.TableName, parcelSearchDataSet.ParcelLabelDataTable.TableName, parcelSearchDataSet.ParcelRollYearDataTable.TableName };
            Utility.LoadDataSet(parcelSearchDataSet, "f1401_pclst_ParcelType", ht, tableName);
            return parcelSearchDataSet;
        }

        /// <summary>
        /// F1403_s the get search result.
        /// </summary>
        /// <param name="parcelSearchXml">The parcel search XML.</param>
        /// <returns>F1403ParcelSearch</returns>
        public static F1403ParcelSearch F1403_GetSearchResult(string parcelSearchXml)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelConditionXML", parcelSearchXml);

            Utility.LoadDataSet(parcelSearchDataSet.ParcelSearchDataTable, "f1403_pclst_Parcel", ht);
            return parcelSearchDataSet;
        }

        /// <summary>
        /// F1403_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public static F1403ParcelSearch F1403_GetSaleTrackingRollYear(int eventID)
        {
            F1403ParcelSearch parcelSearchDataSet = new F1403ParcelSearch();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventID);
            Utility.LoadDataSet(parcelSearchDataSet.SaleTrakingRollYear, "f1403_pcget_SaleTrakingRollYear", ht);
            return parcelSearchDataSet;
        }
    }
}
