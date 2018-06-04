// -------------------------------------------------------------------------------------------
// <copyright file="F8044MaterialsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F8044 Materials Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06		JYOTHI P	            Created
// 16 Oct 06        JYOTHI P                Added F8044_ListMatetialDetails method
// 16 Oct 06        JYOTHI P                Added F8044_ListMaterialsResource method
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
    /// Main class for Material Details Component
    /// </summary>
    public static class F8044MaterialsComp
    {
        #region List Material Details

        /// <summary>
        /// Lists the EventEngineTVDetails
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>returns dataset contains EventEngine TVDetails</returns>
        public static F8044MaterialsData F8044_ListMaterialDetails(int formId, int keyId)
        {
            F8044MaterialsData materialsData = new F8044MaterialsData();
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(materialsData.ListMaterials, "f8044_pclst_FS_Materials", ht);
            return materialsData;
        }

        #endregion

        #region List Materials Resource Types

        /// <summary>
        /// Lists the Materials Resource Type
        /// </summary>
        /// <param name="flagActiveAndAll">The flag active and all.</param>
        /// <param name="eventId">eventId</param>
        /// <returns>
        /// returns dataset contains Materials Resource Type
        /// </returns>
        public static F8044MaterialsData F8044_ListMaterialsResourceType(int flagActiveAndAll, int eventId)
        {
            F8044MaterialsData materialsData = new F8044MaterialsData();
            Hashtable ht = new Hashtable();
            ht.Add("@IsActive", flagActiveAndAll);
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(materialsData.ListMaterialsResource, "f8044_pclst_FS_MaterialsResource", ht);
            return materialsData;
        }

        #endregion

        #region Save Material Details

        /// <summary>
        /// Save the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">userId</param>
        public static void F8044_SaveMaterialDetails(string materialItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MaterialItems", materialItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8044_pcins_FS_Materials", ht);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// Updates the Material Details
        /// </summary>
        /// <param name="materialItems">The material items.</param>
        /// <param name="userId">userId</param>
        public static void F8044_UpdateMaterialDetails(string materialItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MaterialItems", materialItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8044_pcupd_FS_Materials", ht);
        }

        #endregion

        #region Delete a Material Item

        /// <summary>
        /// Deletes the Material Item
        /// </summary>
        /// <param name="materialId">The material id.</param>
        /// <param name="userId">userId</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F8044_DeleteMaterialItem(int materialId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MaterialID", materialId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f8044_pcdel_Materials", ht);
        }

        #endregion
    }
}
