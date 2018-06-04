// -------------------------------------------------------------------------------------------
// <copyright file="1411ParcelStatementSearchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access MasterName Search related information</summary>
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
    /// ParcelStatementSearchComp class file
    /// </summary>
    public static class F1411ParcelStatementSearchComp
    {
        /// <summary>
        /// Gets the Parcel Statement search.
        /// </summary>
        /// <param name="Parcel or Statement">Parcel or Statement</param>
        public static F1411ParcelStatementSearchData f1411ParcelStatementSearch(string searchNumber)
        {
            F1411ParcelStatementSearchData parcelStmtSearch = new F1411ParcelStatementSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@Search", searchNumber);
            Utility.LoadDataSet(parcelStmtSearch.ParcelStatementSearchDataTable, "f1411_pcget_StatementSearch", ht);
            return parcelStmtSearch;
        }
    }
}
