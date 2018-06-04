// -------------------------------------------------------------------------------------------
// <copyright file="F36033LandCodeValuesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36033LandCodeValuesComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 14/09/07         M.Vijayakumar       Created
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
    /// F36033LandCodeValuesComp Class file
    /// </summary>
    public static class F36033LandCodeValuesComp
    {
        #region F36033 Land Code Values 

        #region F36033_ListLandCodeValues 

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public static F36033LandCodesValuesData F36033_ListLandCodeValues()
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(landCodesValuesData.ListLandCodeValueDetails, "f36033_pclst_LandCodeValue", ht);
            return landCodesValuesData;
        }

        #endregion F36033_ListLandCodeValues 

        #region F36033_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public static F36033LandCodesValuesData F36033_ListIndividualLandCodeValuesItems()
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] { landCodesValuesData.GetAppRollYear.TableName, landCodesValuesData.ListNeighborhoodType.TableName, landCodesValuesData.ListLandCode.TableName, landCodesValuesData.ListUnitType.TableName };
            Utility.LoadDataSet(landCodesValuesData, "f36033_pclst_LoadLandCodeValuesItems", ht, optionalParameter);
            return landCodesValuesData;
        }

        #endregion F36033_ListIndividualLandCodeValuesItems

        #region F36033_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public static F36033LandCodesValuesData F36033_ListNeighborhoodType(int rollYear)
        {
            F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(landCodesValuesData.ListNeighborhoodType, "f35100_pclst_NeighborhoodType", ht);
            return landCodesValuesData;
        }
        #endregion F36033_ListNeighborhood

        #region F36033_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F36033_DeleteLandCodevalue(int luvId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LuVID", luvId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36033_pcdel_LandCodeValue", ht);
        }

        #endregion F36033_DeleteLandCodeValue

        #region F36033_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">userId</param>
        /// <returns>saved key id</returns>
        public static int F36033_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LuVID", landUnqiueId);
            ht.Add("@LandValueItems", landValueItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36033_pcins_LandCodeValue", ht);
        }

        #endregion F36033_SaveLandCodeValue

        #endregion F36033 Land Code Values
    }
}
