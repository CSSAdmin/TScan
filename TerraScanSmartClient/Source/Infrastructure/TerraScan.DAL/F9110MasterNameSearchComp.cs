// -------------------------------------------------------------------------------------------
// <copyright file="9110MasterNameSearchComp.cs" company="Congruent">
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
    /// MasterNameSearchComp class file
    /// </summary>
    public static class F9110MasterNameSearchComp
    {

        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>MasterNameSearchData</returns>
        public static F9110MasterNameSearchData F9110GetMasterNameSearch(string lastName, string firstName, string address)
        {
            F9110MasterNameSearchData masterNameSearch = new F9110MasterNameSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@LastName", lastName);
            ht.Add("@FirstName", firstName);
            ht.Add("@Address", address);
            Utility.LoadDataSet(masterNameSearch.ListMasterNameDataTable, "f9110_pclst_MasterName", ht);
            return masterNameSearch;
        }
    }
}
