// -------------------------------------------------------------------------------------------
// <copyright file="F36035LandValueSliceDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36035LandValueSliceDetailsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 19/09/07         Kuppu             Created
// 24/05/09         Sadha Shivudu     Added methods to implement the TSCO 7395 
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
    /// F36035LandValueSliceDetailsComp Class File.
    /// </summary>
    public static class F36035LandValueSliceDetailsComp
    {
        #region GetLandDetails

        /// <summary>
        /// F36035_s the list land details.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landDetails</returns>
        public static F36035LandData F36035_ListLandDetails(int valueSliceId)
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableName = new string[] { landDetails.ListLandValueSliceDetailsNew.TableName, landDetails.GetValueSliceValidTable.TableName, landDetails.GetCfgLandTypeLabel.TableName, landDetails.ListGridInfluences.TableName };
            Utility.LoadDataSet(landDetails, "f36035_pcget_LandValuesSliceDetails", ht, tableName);
            return landDetails;
        }
        #endregion GetLandDetails

        /// <summary>
        /// F36035_s the list shape details.
        /// </summary>
        /// <returns></returns>
        public static F36035LandData F36035_ListShapeDetails()
        {
            F36035LandData depreciationData = new F36035LandData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(depreciationData.LandShapesTable, "f36035_pclst_LandShapes", ht);
            return depreciationData;
        }

        #region GetLandTypeDetails

        /// <summary>
        /// F36035_s the LandTypeDetails.
        /// </summary>
        /// <param name="valueSliceId">valueSliceID</param>
        /// <returns>the landTypeDetails</returns>
        public static F36035LandData F36035_ListLandTypeDetails(int valueSliceId)
        {
            F36035LandData landTypeDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            //string[] tableName = new string[] 
            //{ 
            //    landTypeDetails.ListLandType1.TableName, 
            //    landTypeDetails.ListLandType2.TableName, 
            //    landTypeDetails.ListLandType3.TableName, 
            //    landTypeDetails.ListLandCode.TableName ,
            //   // landTypeDetails.ListUseLandCode.TableName  
            //};

            Utility.LoadDataSet(landTypeDetails.ListLandCodeLandType, "f36035_pclst_LoadingLandValueSliceItems", ht);
            return landTypeDetails;
        }
        #endregion GetLandTypeDetails

        #region InsertLandDetails

        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>landDetailsID</returns>
        public static int F36035_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            ////F36035LandData landDetailsData = new F36035LandData();
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
            landDetailsID = Utility.FetchSPExecuteKeyId("f36035_pcins_LandValueSlice", ht);
            return landDetailsID;
        }

        #endregion InsertLandDetails

        #region DeleteLandDetails

        /// <summary>
        /// To Delete LandDetails.
        /// </summary>
        /// <param name="luid">The lUID.</param>  
        /// <param name="userId">userId</param>
        public static void F36035_DeleteLandDetails(int luid, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LUID", luid);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f36035_pcdel_LandValuesSlice", ht);
        }
        #endregion DeleteLandDetails

        #region GetLandcode

        /// <summary>
        /// F36035_s the get land code.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="valuesliceId">The valueslice id.</param>
        /// <returns></returns>
        public static F36035LandData F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId,int? AglandID)
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@LandTypeID1", landType1);
            ht.Add("@LandTypeID2", landType2);
            ht.Add("@LandTypeID3", landType3);
            ht.Add("@ValueSliceID", valuesliceId);
            ht.Add("@AglandID", AglandID);
            Utility.LoadDataSet(landDetails.Get_LandCode, "f36033_pcget_LandCodeValue", ht);
            return landDetails;
        }
        #endregion GetLandcode

        #region GetLandcode BaseValue
        /// <summary>
        /// F36035_s the list Landcode.
        /// </summary>
        /// <param name="landCode">landCode</param>
        /// <param name="valueSliceId">valueSliceId</param>
        /// <returns>the landDetails</returns>
        public static F36035LandData F36035_GetLandCodeBaseValue(string landCode, int valueSliceId,int? AglandID)
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@LandCode", landCode);
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@AglandID", AglandID);
            Utility.LoadDataSet(landDetails.Get_LandCodeBaseValue, "f36033_pcget_LandCodeBaseValue", ht);
            return landDetails;
        }
        #endregion GetLandcode BaseValue

        #region List Influence Types

        /// <summary>
        /// F36035_s the list influence type.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>The influence type dataset</returns>
        public static F36035LandData F36035_ListInfluenceType(int valueSliceId)
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            Utility.LoadDataSet(landDetails.ListInfluenceType, "f36035_pclst_InfluenceType", ht);
            return landDetails;
        }

        #endregion List Influence Types

        #region List Land Program

        /// <summary>
        /// F36035_s the list land program.
        /// </summary>
        /// <returns>The land program dataset.</returns>
        public static F36035LandData F36035_ListLandProgram()
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(landDetails.ListLandProgram, "f36035_pclst_LandProgram", ht);
            return landDetails;
        }

        #endregion List Land Program

        #region Get UseBaseDollarPerUnit Value

        /// <summary>
        /// F36035_s the get use base dollar per unit.
        /// </summary>
        /// <param name="programId">The program id.</param>
        /// <param name="useAdjustmentType">Type of the use adjustment.</param>
        /// <param name="useAdjustment">The use adjustment.</param>
        /// <param name="useBaseValue">The use base value.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="useMultiplier">The use multiplier.</param>
        /// <param name="units">The units.</param>
        /// <returns>The use base dollerper unit dataset.</returns>
        public static F36035LandData F36035_GetUseBaseDollarPerUnit(byte programId, byte useAdjustmentType, string useAdjustment, decimal useBaseValue, int rollYear, decimal useMultiplier, decimal units)
        {
            F36035LandData landDetails = new F36035LandData();
            Hashtable ht = new Hashtable();

            ht.Add("@ProgramID", programId);
            ht.Add("@UseAdjustmentType", useAdjustmentType);
            ht.Add("@UseAdjustment", useAdjustment);
            ht.Add("@UseBaseValue", useBaseValue);
            ht.Add("@RollYear", rollYear);
            ht.Add("@UseMultiplier", useMultiplier);
            ht.Add("@Units", units);

            Utility.LoadDataSet(landDetails.GetUseBaseDollarPerUnit, "f36035_pcget_UseBaseDollarPerUnit", ht);
            return landDetails;
        }

        #endregion Get UseBaseDollarPerUnit Value

        #region Execute VFormula

        /// <summary>
        /// F36035_s the execute V formula.
        /// </summary>
        /// <param name="vformula">The vformula.</param>
        /// <param name="units">The units.</param>
        /// <returns></returns>
        public static DataSet F36035_ExecuteVFormula(string vformula, decimal units)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@VFormula", vformula);
            ht.Add("@Units", units);
            return DataProxy.FetchDataSet("f36035_pcexe_VFormula", ht);
        }

        #endregion Execute VFormula
    }
}
