// -------------------------------------------------------------------------------------------
// <copyright file="F36065WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 03/08/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36065
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F36065 WorkItem
    /// </summary>
    public class F36065WorkItem : WorkItem
    {

        #region F36065 Personal Property Depreciation

        #region Check Depreciation RollYear

        /// <summary>
        /// F36065_s the check depr roll year.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>confirmation value</returns>
        public int F36065_CheckDeprRollYear(int? deprYearId, int rollYear)
        {
            return WSHelper.F36065_CheckDeprRollYear(deprYearId, rollYear);
        }

        #endregion Check Depreciation RollYear

        #region Get Depreciation Details

        /// <summary>
        /// F36065_s the get depr details.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <returns>Dataset contains depreciation details</returns>
        public F36065PersonalDeprData F36065_GetDeprDetails(int deprYearId)
        {
            return WSHelper.F36065_GetDeprDetails(deprYearId);
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
        /// <returns>confirmation value for save</returns>
        public int F36065_SaveDepreciation(int? deprYearId, string deprYearItems, string depreciationItems, int userId)
        {
            return WSHelper.F36065_SaveDepreciation(deprYearId, deprYearItems, depreciationItems, userId);
        }

        #endregion Save Depreciation

        #region Delete Depreciation
        /// <summary>
        /// F36065_s the delete depreciattion.
        /// </summary>
        /// <param name="deprYearId">The depr year id.</param>
        /// <param name="depreciationItems">The depreciation items.</param>
        /// <param name="userId">The user id.</param>
        public void F36065_DeleteDepreciattion(int? deprYearId, string depreciationItems, int userId)
        {
            WSHelper.F36065_DeleteDepreciattion(deprYearId, depreciationItems, userId);
        }
        #endregion Delete Depreciation

        #endregion F36065 Personal Property Depreciation

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
