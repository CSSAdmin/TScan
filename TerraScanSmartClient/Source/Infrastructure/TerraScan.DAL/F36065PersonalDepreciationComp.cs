// -------------------------------------------------------------------------------------------
// <copyright file="F36065PersonalDepreciationComp.cs" company="Congruent">
//    Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36066TrendComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------         -------------------------------------------------------
// 11/08/2009     D.LathaMaheswari       Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F36065 Personal property DepreciationComp 
    /// </summary>
    public class F36065PersonalDepreciationComp
    {
        #region Check Depreciation RollYear

        /// <summary>
        /// F36065_s the check depreciation roll year.
        /// </summary>
        /// <param name="deprYearId">The depreciation year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>confirmation value</returns>
        public static int F36065_CheckDeprRollYear(int? deprYearId, int rollYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DeprYearID", deprYearId);
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f36065_pcchk_DeprRollYear", ht);
        }

        #endregion Check Depreciation RollYear

        #region Get Depreciation Details

        /// <summary>
        /// F36065_s the get depr details.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <returns>Dataset contains Depreciation Details</returns>
        public static F36065PersonalDeprData F36065_GetDeprDetails(int deprYearId)
        {
            F36065PersonalDeprData depreciationData = new F36065PersonalDeprData();
            Hashtable ht = new Hashtable();
            ht.Add("@DeprYearID", deprYearId);
            string[] tableName = new string[] { depreciationData.DepreciationYearTable.TableName, depreciationData.DepreciationTable.TableName };
            Utility.LoadDataSet(depreciationData, "f36065_pcget_Depr", ht, tableName);
            return depreciationData;
        }

        #endregion Get Depreciation Details

        #region Save Depreciation

        /// <summary>
        /// F36065_s the save depreciation.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="deprYearItems">The depr year items.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>confirmation for save</returns>
        public static int F36065_SaveDepreciation(int? deprYearId, string deprYearItems, string depreciationItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DeprYearID", deprYearId);
            ht.Add("@DeprYearItems", deprYearItems);
            ht.Add("@DeprItems", depreciationItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36065_pcins_Depr", ht);
        }

        #endregion Save Depreciation

        #region Delete Depreciation

        /// <summary>
        /// F36065_s the delete depreciation.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        public static void F36065_DeleteDepreciattion(int? deprYearId, string depreciationItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DeprYearID", deprYearId);
            ht.Add("@DeprItems", depreciationItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f36065_pcdel_Depr", ht);
        }

        #endregion Delete Depreciation
    }
}
