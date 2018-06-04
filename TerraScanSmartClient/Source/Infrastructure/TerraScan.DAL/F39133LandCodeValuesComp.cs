// -------------------------------------------------------------------------------------------
// <copyright file="F39133LandCodeValuesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36033LandCodeValuesComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 19/07/11         Manoj      Created
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
    /// F39133LandCodeValuesComp Class file
    /// </summary>
    public static class F39133LandCodeValuesComp
    {
        #region F39133 Land Code Values

        #region F39133_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public static F39133LandCodeValueData F39133_ListLandCodeValues()
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(landCodesValuesData.ListLandCodeValueDetails, "f39133_pclst_LandCodeValues", ht);
            return landCodesValuesData;
        }

        #endregion F39133_ListLandCodeValues

        #region F39133_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public static F39133LandCodeValueData F39133_ListIndividualLandCodeValuesItems()
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] { landCodesValuesData.GetAppRollYear.TableName, landCodesValuesData.ListNeighborhoodType.TableName, landCodesValuesData.ListLandCode.TableName, landCodesValuesData.ListUnitType.TableName };
            Utility.LoadDataSet(landCodesValuesData, "f39133_pclst_LoadLandCodeValuesItems", ht, optionalParameter);
            return landCodesValuesData;
        }

        #endregion F39133_ListIndividualLandCodeValuesItems

        #region F39133_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public static F39133LandCodeValueData F39133_ListNeighborhoodType(int rollYear)
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(landCodesValuesData.ListNeighborhoodType, "f35100_pclst_NeighborhoodType", ht);
            return landCodesValuesData;
        }
        #endregion F39133_ListNeighborhood

        #region F39133_CalculateNonCropValues
        ///<summary>
        /// F39133_CalculateNonCropValues
        /// </summary>
        /// <param name="RollYear">RollYear</param>
        /// <param name="CropRate">CropRate</param>
        /// <param name="NonCropRate">NonCropRate</param>
        public static F39133LandCodeValueData F39133_CalculateNonCropValue(int rollYear, decimal? CropRate, decimal? NonCropRate)
        {
            F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@CropRate", CropRate);
            ht.Add("@NonCropRate", NonCropRate);
            Utility.LoadDataSet(landCodesValuesData.CalculateNonCropValue, "f39133_pcget_CropNonCropValues", ht);
            return landCodesValuesData;
  
        }




        #endregion

        #region F39133_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F39133_DeleteLandCodevalue(int luvId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LuVID", luvId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f39133_pcdel_LandCodeValue", ht);
        }

        #endregion F39133_DeleteLandCodeValue

        #region F39133_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">userId</param>
        /// <returns>saved key id</returns>
        public static int F39133_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LuVID", landUnqiueId);
            ht.Add("@LandValueItems", landValueItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f39133_pcins_LandCodeValue", ht);
        }

        #endregion F39133_SaveLandCodeValue

        #endregion F39133 Land Code Values
    }
}
