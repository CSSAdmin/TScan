// -------------------------------------------------------------------------------------------
// <copyright file="F36041CropComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36041CropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 05/11/07         Malliga             Created
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
    /// F36041CropCode Class File.
    /// </summary>
    public static class F36041CropComp
    {
        #region F36041_CropDetails

        #region Get Crop Details
        /// <summary>
        /// F3604 _s the Crop Details.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the Crop Details</returns>
        public static F36041CropData F36041_ListCropDetails(int valueSliceId)
        {
            F36041CropData cropDetails = new F36041CropData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableNames = new string[] 
            { 
                cropDetails.GetCropDetails.TableName, 
                cropDetails.RollYear.TableName, 
            };
            Utility.LoadDataSet(cropDetails, "f36041_pcget_Crop", ht, tableNames);
            return cropDetails;
        }
        #endregion GetLandTypeDetails

        #region Get CropCode Details
        /// <summary>
        /// F3604 _s the Crop Code Details.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the Crop Code Details</returns>
        public static F36041CropData F36041_ListCropCodeDetails(int valueSliceId)
        {
            F36041CropData cropCodeDetails = new F36041CropData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            Utility.LoadDataSet(cropCodeDetails.GetCropCodeDetails, "f36041_pcget_CropCodeDetails", ht);
            return cropCodeDetails;
        }
        #endregion GetLandTypeDetails

        #region Save CropDetails

        /// <summary>
        /// To save the Crop code deatils
        /// </summary>
        /// <param name="valueSliceId">ValueSliceId</param>
        /// <param name="cropItems">CropItems</param>
        ///  <param name="userId">UserID</param>
        /// <returns>integer value containing the save Crop Code Id</returns>
        public static int F36041_SaveCropCodeDetails(int valueSliceId, string cropItems, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@CropItems", cropItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36041_pcins_Crop", ht);
        }
        #endregion F36041_Save CropDetails

        #endregion

        #region F36041_DeleteCrop

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public static void F36041_DeleteCrop(int cropId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CropID", cropId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f36041_pcdel_Crop", ht);
        }

        #endregion F36041_DeleteCrop

        #region F36041_DeleteCropIds

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public static void F36041_DeleteCropIds(string cropIds, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CropIDs", cropIds);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f36041_pcdel_Crop", ht);
        }

        #endregion F36041_DeleteCrop
    }
}
