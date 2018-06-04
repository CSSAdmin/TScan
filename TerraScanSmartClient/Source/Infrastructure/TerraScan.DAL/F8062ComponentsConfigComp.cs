// -------------------------------------------------------------------------------------------
// <copyright file="F8062ComponentsConfigComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F8062ComponentsConfigComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 18/05/07         Jyothi        Created
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
    /// F8062ComponentsConfigComp class file
    /// </summary>
    public static class F8062ComponentsConfigComp
    {
        #region F8062 Components Configuration

        #region List Components Configuration

        /// <summary>
        /// F8062_s the list components configuration.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>typed dataset containing the components configuration</returns>
        public static F8062ComponentsConfigData F8062_ListComponentsConfiguration(int applicationId)
        {
            F8062ComponentsConfigData componentsConfigData = new F8062ComponentsConfigData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(componentsConfigData.ListComponentsConfiguration, "f8062_pclst_ComponentsConfiguration", ht);
            return componentsConfigData;
        }

        #endregion List Components Configuration

        #region List Feature Class

        /// <summary>
        /// F8062_s the list feature class.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns>typed dataset containing the Feature class</returns>
        public static F8062ComponentsConfigData F8062_ListFeatureClass(int applicationId)
        {
            F8062ComponentsConfigData componentsConfigData = new F8062ComponentsConfigData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(componentsConfigData.ListFeatureClass, "f8062_pclst_FeatureClass", ht);
            return componentsConfigData;
        }

        #endregion List Feature Clas

        #region Save Components Configuration

        /// <summary>
        /// F8062_s the save components configuration.
        /// </summary>
        /// <param name="componentsConfig">The components config.</param>
        /// <param name="userId">userId</param>
        public static void F8062_SaveComponentsConfiguration(string componentsConfig, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ComponentsConfig", componentsConfig);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8062_pcins_ComponentsConfiguration", ht);
        }

        #endregion Save Components Configuration

        #region Delete Components Configuration

        /// <summary>
        /// Deletes the Components Configuration.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F8062_DeleteComponentsConfiguration(int componentId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ComponentID", componentId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPOutput("f8062_pcdel_ComponentsConfiguration", ht);
        }
        #endregion

        #endregion F8062 Components Configuration
    }
}
