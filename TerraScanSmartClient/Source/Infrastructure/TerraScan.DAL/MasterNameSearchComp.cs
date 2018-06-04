// -------------------------------------------------------------------------------------------
// <copyright file="MasterNameSearchComp.cs" company="Congruent">
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
    public static class MasterNameSearchComp
    {
        /// <summary>
        /// Gets the master name search.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="address">The address.</param>
        /// <returns>Returns MasterNameSearchData dataset</returns>
        public static MasterNameSearchData GetMasterNameSearch(string lastName, string firstName, string address)
        {
            MasterNameSearchData masterNameSearch = new MasterNameSearchData();
            Hashtable ht = new Hashtable();
            ht.Add("@LastName", lastName);
            ht.Add("@FirstName", firstName);
            ht.Add("@Address", address);
            Utility.LoadDataSet(masterNameSearch.ListMasterNameDataTable, "f9101_pclst_MasterName", ht);
            return masterNameSearch;
        }
    }
}
