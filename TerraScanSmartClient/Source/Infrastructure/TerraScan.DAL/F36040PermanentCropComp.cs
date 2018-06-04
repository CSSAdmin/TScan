// -------------------------------------------------------------------------------------------
// <copyright file="F36040PermanentCropComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36040PermanentCropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 30/10/07         Shiva               Created
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
    /// F36040PermanentCropComp Class File.
    /// </summary>
    public static class F36040PermanentCropComp
    {
        #region Crop Catalog

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public static F36040PermanentCropData F36040_ListNeighborhoodType()
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(permanentCropData.ListNeighborhoodType, "f35100_pclst_NeighborhoodType", ht);
            return permanentCropData;
        }

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public static F36040PermanentCropData F36040_ListCropCatalog()
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] { permanentCropData.GetAppRollYear.TableName, permanentCropData.ListCropCatalogDetials.TableName, permanentCropData.GetCropNBHD.TableName };
            Utility.LoadDataSet(permanentCropData, "f36040_pclst_CropCatalog", ht, optionalParameter);
            return permanentCropData;
        }
        
        /// <summary>
        /// F36040_s the type of the list crop neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static F36040PermanentCropData F36040_ListCropNeighborhoodType(int rollYear)
        {
            F36040PermanentCropData permanentCropData = new F36040PermanentCropData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(permanentCropData.GetCropNBHD, "f36040_pcget_CropNBHD", ht);
            return permanentCropData;
        }
        
        /// <summary>
        /// F36040_s the delete crop catalog.
        /// </summary>
        /// <param name="cropVId">The crop V id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public static int F36040_DeleteCropCatalog(int cropVId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CropVID", cropVId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36040_pcdel_CropCatalog", ht);
        }

        /// <summary>
        /// F36040_s the save crop catalog.
        /// </summary>
        /// <param name="cropUnqiueId">The crop unqiue id.</param>
        /// <param name="cropCatalogItems">The crop catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public static int F36040_SaveCropCatalog(int? cropUnqiueId, string cropCatalogItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CropVID", cropUnqiueId);
            ht.Add("@CropCatalogItems", cropCatalogItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36040_pcins_CropCatalog", ht);
        }

        #endregion
    }
}
