// -------------------------------------------------------------------------------------------
// <copyright file="CountyConfigurationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update CountyConfiguration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 May 06		GUHAN S	            Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;
    #endregion

    /// <summary>
    /// CountyConfigurationComp
    /// </summary>
    public static class CountyConfigurationComp
    {
        #region GetCountyConfigDetails

        /// <summary>
        /// Gets the County Configuration
        /// </summary>
        /// <param name="applicationId">The application id</param>
        /// <param name="userId">The User id</param>
        /// <returns>The dataset containing the County Configuration.</returns>
        public static DataSet GetCountyConfiguration(int applicationId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            ht.Add("@UserID", userId);
            return DataProxy.FetchDataSet("f9020_pclst_Configuration", ht);
        }
        #endregion

        #region UpdateCountyConfigDetails

        /// <summary>
        /// Gets the County Configuration
        /// </summary>
        /// <param name="configId">The configId.</param>
        /// <param name="configDescription">The config description.</param>
        /// <param name="userId">The userId.</param>
        public static void UpdateCountyConfigDetails(int configId, string configDescription, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CfgID", configId);
            ht.Add("@CfgValue", configDescription);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9020_pcupd_Configuration", ht);
        }
        #endregion
    }
}
