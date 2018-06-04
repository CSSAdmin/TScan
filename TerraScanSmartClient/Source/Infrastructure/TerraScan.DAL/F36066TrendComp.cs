// -------------------------------------------------------------------------------------------
// <copyright file="F36066TrendComp.cs" company="Congruent">
//    Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36066TrendComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------         -------------------------------------------------------
// 27/07/2009     D.LathaMahewari       Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F36066TrendComp class
    /// </summary>
    public static class F36066TrendComp
    {
        #region Check Trend

        /// <summary>
        /// Checks the trend roll year.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Confirmation value</returns>
        public static int CheckTrendRollYear(int? trendYearId, int rollYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TrendYearID", trendYearId);
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f36066_pcchk_TrendRollYear", ht);
        }

        #endregion Check Trend

        #region Get Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <returns>Trend Details</returns>
        public static F36066TrendData GetTrendDetails(int trendYearId)
        {
            F36066TrendData trendData = new F36066TrendData();
            Hashtable ht = new Hashtable();
            ht.Add("@TrendYearID", trendYearId);
            string[] tableName = new string[] { trendData.GetTrendDetails.TableName, trendData.GetTrendList.TableName, trendData.ListDepreciation.TableName };
            Utility.LoadDataSet(trendData, "f36066_pcget_Trend", ht, tableName);
            return trendData;
        }

        #endregion Get Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendYearItems">The trend year items.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for save</returns>
        public static int SaveTrend(int? trendYearId, string trendYearItems, string trendItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TrendYearID", trendYearId);
            ht.Add("@TrendYearItems", trendYearItems);
            ht.Add("@TrendItems", trendItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36066_pcins_Trend", ht);
        }

        #endregion Save Trend

        #region Delete Trend

        /// <summary>
        /// Deletes the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteTrend(int? trendYearId, string trendItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TrendYearID", trendYearId);
            ht.Add("@TrendItems", trendItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f36066_pcdel_Trend", ht);
        }

        #endregion Delete Trend
    }
}
