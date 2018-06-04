// -------------------------------------------------------------------------------------------
// <copyright file="F8058ResourceConfigComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F8058ResourcesConfigComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 18/05/07         Kuppusamy.B       Created
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
    /// F8058ResourceConfigComp class file
    /// </summary>
    public static class F8058ResourceConfigComp
    {
        #region List ResourcesConfiguration

        /// <summary>
        /// F8058_s the list resources configuration.
        /// </summary>        
        /// <returns>Typed dataset</returns>
        public static F8058ResourcesConfigData F8058_ListResourcesConfigDetails()
        {
            F8058ResourcesConfigData resourcesConfigData = new F8058ResourcesConfigData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(resourcesConfigData.F8058ListResourceConfigDetails, "f8058_pclst_ResourcesConfiguration", ht);
            return resourcesConfigData;
        }
        #endregion List ResourcesConfiguration

        #region Insert ResourcesConfiguration

        /// <summary>
        /// F8058_s the insert reosurces config details.
        /// </summary>
        /// <param name="equipmentId">The equipment id.</param>
        /// <param name="equiptResource">The equipt resource.</param>
        /// <param name="applicationId">The application id.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F8058_InsertReosurcesConfigDetails(int equipmentId, string equiptResource, int applicationId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EquipmentID", equipmentId);
            ht.Add("@EquiptResource", equiptResource);
            ht.Add("@ApplicationID", applicationId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f8058_pcins_ResourcesConfiguration", ht);
        }
        #endregion Insert ResourcesConfiguration

        #region Delete ResourcesConfiguration
        
        /// <summary>
        /// F8058_s the delete resources config details.
        /// </summary>
        /// <param name="equipmentId">The equipmentId.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer Value</returns>
        public static int F8058_DeleteResourcesConfigDetails(int equipmentId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EquipmentID", equipmentId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f8058_pcdel_ResourcesConfiguration", ht);
        }
        #endregion Delete ResourcesConfiguration
    }
}
