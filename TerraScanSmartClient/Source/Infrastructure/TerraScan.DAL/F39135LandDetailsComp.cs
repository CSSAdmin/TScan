// -------------------------------------------------------------------------------------------
// <copyright file="F39135LandDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F39135LandDetailsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
namespace TerraScan.Dal
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F39135LandDetailsComp Class File.
    /// </summary>
    public static class F39135LandDetailsComp
    {
        #region GetLandTypeDetails

        /// <summary>
        /// F39135_s the LandTypeDetails.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        ///  <param name=""></param> 
        /// <returns>the landTypeDetails</returns>
        public static F39135LandData F39135_Landtypes(int valueSliceId, int rollYear)
        {
            F39135LandData landDetails = new F39135LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@RollYear", rollYear);
            string[] tableName = new string[] 
            { 
                landDetails.ListLandTypes1.TableName, 
                landDetails.ListLandTypes2.TableName, 
                landDetails.ListLandTypes3.TableName, 
                
            };
            Utility.LoadDataSet(landDetails, "f36035_pclst_LandTypes", ht, tableName);
            return landDetails;
        }

        #endregion GetLandTypeDetails

        #region GetLandUseTypes
        /// <summary>
        /// F39135_s the LandUseType.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        public static F39135LandData F39135_LandUseTypes(int valueSliceId)
        {
            F39135LandData landDetails = new F39135LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValuesliceID", valueSliceId);
            Utility.LoadDataSet(landDetails.GetLandUseTypes_, "f34135_pcget_LandUseTypes", ht);
            return landDetails;
        }
      #endregion

        #region GetLandTotals
        /// <summary>
        /// F39135_s the LandUseType.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        public static F39135LandData F39135_GetLandTotals(int valueSliceId)
        {
            F39135LandData landDetails = new F39135LandData();
            //Hashtable ht = new Hashtable();
            //ht.Add("@ValuesliceID", valueSliceId);
            //Utility.FillDataSet(landDetails.GetLandTotals_, "f34135_pcget_LandTotals", ht);
            return landDetails;
        }
        #endregion

        #region GetWeightedRating
        /// <summary>
        /// F39135_s the get WeightedRating.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="landCode">landCode</param>
        /// <param name="units">units</param>
        /// <param name="landUse">landUse</param>
       /// <returns>the landTypeDetails</returns>
        public static F39135LandData F39135_WeightedRating(string landCode, decimal units, int? landUse, int valueSliceId)
        {
            F39135LandData landDetails = new F39135LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@LandCode", landCode);
            ht.Add("@Units", units);
            ht.Add("@LandUse", landUse);
            ht.Add("@ValuesliceID", valueSliceId);
            Utility.LoadDataSet(landDetails.WeightedRating_, "f34135_pcget_WeightedRating", ht);
            return landDetails;
        }
          
        #endregion

        #region  CalculatedBaseValue
        /// <summary>
        /// F39135_s the get CalculatedBaseValue.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <param name="adjustmentTypeID">adjustmentTypeID</param>
        /// <param name="units">units</param>
        /// <param name="baseCostUnit">baseCostUnit</param>
        /// <param name="adjustment">adjustment</param>
        /// <returns>the landTypeDetails</returns>
        public static F39135LandData F39135_CalculatedBaseValue(string LandCode, int adjustmentTypeID, decimal units, decimal baseCostUnit, decimal adjustment, int? AglandID, int valueSliceId)
        {
            F39135LandData landDetails = new F39135LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@LandCode", LandCode);
            ht.Add("@AdjustmentTypeID", adjustmentTypeID);
            ht.Add("@Units", units);
            ht.Add("@BaseCostUnit", baseCostUnit);
            ht.Add("@Adjustment", adjustment);
            ht.Add("@AglandID", AglandID); 
            ht.Add("@ValuesliceID", valueSliceId);
            Utility.LoadDataSet(landDetails.GetCalculateBaseValue, "f34135_pcget_CalculateBaseValue", ht);
            return landDetails;
        }

         #endregion

        #region GetLandInfo
       
       ///<summary>
       /// Get the Land Value slice
       /// </summary>
       /// <param name="valueSliceId">valueSliceID</param>
       /// <returns>the landInfoDetails</returns>
      
        public static F39135LandData  F39135_LandDetails(int valueSliceId)
        {
            F39135LandData landDetails = new F39135LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableName = new string[] 
            {
                landDetails.GetLandValuesSliceDetails.TableName,
                landDetails.GetRollYear.TableName,
                landDetails.GetValueSliceValid.TableName, 
                landDetails.ListGridInfluences.TableName,
                landDetails.GetTotalUnits.TableName,
                landDetails.GetTotalValue.TableName,
                landDetails.GetTotalRating.TableName
            };
            Utility.LoadDataSet(landDetails, "f34135_pcget_LandValuesSliceDetails", ht, tableName);
            return landDetails; 
        }

        #endregion

        #region ListAdjustmentType

        ///<summary>
        /// F34135_s List Adjustment Type.
        ///<summary>
        public static F39135LandData F39135_adjustmentTypes()
        {
            F39135LandData adjustmentType = new F39135LandData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(adjustmentType.ListAdjustmentType, "f34135_pclst_AdjustmentType", ht);
            return adjustmentType; 
        }


        #endregion ListAdjustmentType

        #region InsertLandDetails

        /// <summary>
        /// F39135_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>landDetailsID</returns>
        public static int F39135_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            
            Hashtable ht = new Hashtable();
            if (luid == 0)
            {
                ht.Add("@LUID", DBNull.Value);
            }
            else
            {
                ht.Add("@LUID", luid);
            }

            ht.Add("@LandCodeItems", landUnitItems);
            ht.Add("@InfluenceItems", influenceItems);
            ht.Add("@UserID", userId);

            int landDetailsID;
            landDetailsID = Utility.FetchSPExecuteKeyId("f34135_pcins_LandValueSlice", ht);
            return landDetailsID;
        }

        #endregion InsertLandDetails

    }
}
