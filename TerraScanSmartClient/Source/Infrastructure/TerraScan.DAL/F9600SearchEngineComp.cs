// -------------------------------------------------------------------------------------------
// <copyright file="F9600SearchEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;

    /// <summary>
    /// Class contains method for 9600 Search Engine
    /// </summary>
    public static class F9600SearchEngineComp
    {
        #region ListSearchResult

        /// <summary>
        /// F9600_s the list search result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <returns>F9600SearchData Dataset</returns>
        public static F9600SearchData F9600_ListSearchResult(string searchValue, int appId)
        {
            F9600SearchData searchDataset = new F9600SearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@SearchValue", searchValue);
            ht.Add("@ApplicationID", appId);
            string[] tableName = new string[] { searchDataset.ListSearchTable.TableName };
            Utility.LoadDataSet(searchDataset, "f9600_pclst_Search", ht, tableName);
            return searchDataset;
        }

        #endregion

        #region ListSearchResult

        /// <summary>
        /// F9600_s the list sort result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <param name="searchOrder">if set to <c>true</c> [search order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        /// <returns>F9600SearchData DataSet</returns>
        public static F9600SearchData F9600_ListSortResult(string searchValue, int appId, bool searchOrder, bool groupOrder)
        {
            F9600SearchData searchDataset = new F9600SearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@SearchValue", searchValue);
            ht.Add("@ApplicationID", appId);
            ht.Add("@GroupOrder", groupOrder);
            ht.Add("@SearchOrder", searchOrder);
            string[] tableName = new string[] { searchDataset.ListSearchTable.TableName };
            Utility.LoadDataSet(searchDataset, "f9600_pclst_SearchSort", ht, tableName);
            return searchDataset;
        }

        #endregion
    }
}
