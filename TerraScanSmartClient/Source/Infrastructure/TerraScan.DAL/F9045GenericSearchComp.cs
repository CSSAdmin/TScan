// -------------------------------------------------------------------------------------------
// <copyright file="F9045GenericSearchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F9041QueryViewDescriptionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 14/10/2011       D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;
    
    /// <summary>
    /// F9045GenericSearchComp
    /// </summary>
    public static class F9045GenericSearchComp
    {
        /// <summary>
        /// F9045s the get configuration.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <returns>Configuration Details</returns>
        public static F9045GenericSearchData F9045GetConfiguration(int genericSearchId)
        {
            F9045GenericSearchData configurationITems = new F9045GenericSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@GenericSearchID", genericSearchId);
            Utility.LoadDataSet(configurationITems.ConfigurationData, "f9045_pcget_GenericSearch", ht);
            return configurationITems;
        }

        /// <summary>
        /// F9045s the get search results.
        /// </summary>
        /// <param name="genericSearchId">The generic search id.</param>
        /// <param name="searchString">The search string.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Search Results</returns>
        public static F9045GenericSearchData F9045GetSearchResults(int genericSearchId, string searchString, int userId)
        {
            F9045GenericSearchData configurationITems = new F9045GenericSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@GenericSearchID", genericSearchId);
            ht.Add("@Search", searchString);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(configurationITems.SearchResults, "f9045_pclst_GenericSearchResults", ht);
            return configurationITems;
        }
    }
}
