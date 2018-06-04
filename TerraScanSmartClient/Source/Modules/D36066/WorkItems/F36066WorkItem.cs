// -------------------------------------------------------------------------------------------
// <copyright file="F36066WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 27/07/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36066
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// WorkItem F36066
    /// </summary>
    public class F36066WorkItem : WorkItem
    {
        #region Check Trend

        /// <summary>
        /// Checks the trend roll year.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Confirmation value</returns>
        public int CheckTrendRollYear(int? trendYearId, int rollYear)
        {
            return WSHelper.CheckTrendRollYear(trendYearId, rollYear);
        }

        #endregion Check Trend

        #region Get Trend Details

        /// <summary>
        /// Gets the trend details.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <returns>Trend Details</returns>
        public F36066TrendData GetTrendDetails(int trendYearId)
        {
            return WSHelper.GetTrendDetails(trendYearId);
        }

        #endregion Check Trend Details

        #region Save Trend

        /// <summary>
        /// Saves the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendYearItems">The trend year items.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation value for save</returns>
        public int SaveTrend(int? trendYearId, string trendYearItems, string trendItems, int userId)
        {
            return WSHelper.SaveTrend(trendYearId, trendYearItems, trendItems, userId);
        }

        #endregion Save Trend

        #region Delete Trend

        /// <summary>
        /// Deletes the trend.
        /// </summary>
        /// <param name="trendYearId">The trend year id.</param>
        /// <param name="trendItems">The trend items.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteTrend(int? trendYearId, string trendItems, int userId)
        {
            WSHelper.DeleteTrend(trendYearId, trendItems, userId);
        }

        #endregion Delete Trend

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
