// -------------------------------------------------------------------------------------------
// <copyright file="F8060PartsConfigComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Parts Config</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 May 06		JYOTHI P	            Created
// 27 July 06       JYOTHI P                Added GetExciseTaxStatement method
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
    /// Main class for Parts Config Component
    /// </summary>
    public static class F8060PartsConfigComp
    {
        #region List Parts Configuration

        /// <summary>
        /// Lists the Parts Configuration details
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <returns>returns dataset contains Parts Configuration details</returns>
        public static F8060PartsConfigData F8060_ListPartsConfig(int componentId)
        {
            F8060PartsConfigData partsConfigData = new F8060PartsConfigData();
            Hashtable ht = new Hashtable();
            ht.Add("@ComponentID", componentId);
            Utility.LoadDataSet(partsConfigData.ListPartsConfiguration, "f8060_pclst_PartsConfiguration", ht);
            return partsConfigData;
        }

        #endregion

        #region List Components

        /// <summary>
        /// Lists the Components detail
        /// </summary>
        /// <returns>returns dataset contains Components details</returns>
        public static F8060PartsConfigData F8060_ListComponents()
        {
            F8060PartsConfigData partsConfigData = new F8060PartsConfigData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(partsConfigData.ListComponents, "f8060_pclst_Components", ht);
            return partsConfigData;
        }

        #endregion

        #region Save Parts Configuration

        /// <summary>
        /// F8062_s the save Parts configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="partsConfig">The parts config.</param>
        /// <param name="userId">userId</param>
        public static void F8060_SavePartsConfiguration(int partId, string partsConfig, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PartID", partId);
            ht.Add("@PartsConfig", partsConfig);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8060_pcins_PartsConfiguration", ht);
        }

        #endregion Save Parts Configuration

        #region Delete Parts Configuration

        /// <summary>
        /// Deletes the Parts Configuration.
        /// </summary>
        /// <param name="partId">The part id.</param>
        /// <param name="userId">userId</param>
        /// <returns>status of deletion of record</returns>
        public static int F8060_DeletePartsConfiguration(int partId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PartID", partId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPOutput("f8060_pcdel_PartsConfiguration", ht);
        }
        #endregion
    }
}
