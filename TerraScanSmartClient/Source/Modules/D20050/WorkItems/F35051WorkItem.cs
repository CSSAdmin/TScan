// --------------------------------------------------------------------------------------------
// <copyright file="F35051WorkItem.cs" company="Congruent">
//        Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the Schedule Line Items.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author           Description
// ----------        ---------         ---------------------------------------------------------
// 16 July 2008    Sadha Shivudu M     Created
// *********************************************************************************/

namespace D20050
{
    #region Namespace

    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using System;

    #endregion

    /// <summary>
    /// F35051 WorkItem class
    /// </summary>
    public class F35051WorkItem : WorkItem
    {
        #region CRUD Methods

        /// <summary>
        /// F35051_s the get schedule line item details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The schedule line items dataset.</returns>
        public F35051ScheduleLineItemsData F35051_GetScheduleLineItemDetails(int scheduleId)
        {
            return WSHelper.F35051_GetScheduleLineItemDetails(scheduleId);
        }

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public int F35051_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return WSHelper.F35051_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public int F35051_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return WSHelper.F35051_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
        }

        /// <summary>
        /// F35051_s the get depr percentage.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="year">The year.</param>
        /// <returns>The schedule line items dataset.</returns>
        public F35051ScheduleLineItemsData F35051_GetDeprPercentage(Int16 rollYear, int deprTableId, Int16 year)
        {
            return WSHelper.F35051_GetDeprPercentage(rollYear, deprTableId, year);
        }

        #endregion

        #region Base Methods

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion Base Methods.
    }
}
