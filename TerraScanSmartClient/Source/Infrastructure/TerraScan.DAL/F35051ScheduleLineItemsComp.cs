// --------------------------------------------------------------------------------------------
// <copyright file="F35051ScheduleLineItemsComp.cs" company="Congruent">
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
// 17 July 2008    Sadha Shivudu M     Created
// *********************************************************************************/

namespace TerraScan.Dal
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;

    #endregion Namespace

    /// <summary>
    /// F35051 Schedule Line Items Comp
    /// </summary>
    public class F35051ScheduleLineItemsComp
    {
        /// <summary>
        /// F35051_s the get schedule line item details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The schedule line items dataset.</returns>
        public static F35051ScheduleLineItemsData F35051_GetScheduleLineItemDetails(int scheduleId)
        {
            F35051ScheduleLineItemsData scheduleLineItemsData = new F35051ScheduleLineItemsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            string[] tableName = new string[] { scheduleLineItemsData.ListSchedlueLineItem.TableName, scheduleLineItemsData.ListSchedlueCategory.TableName, scheduleLineItemsData.ListDeprTable.TableName, scheduleLineItemsData.GetRollYear.TableName, scheduleLineItemsData.VisibleRows.TableName  };
            Utility.LoadDataSet(scheduleLineItemsData, "f35051_pclst_SchedlueLineItem", ht, tableName);
            return scheduleLineItemsData;
        }

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public static int F35051_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (scheduleId > 0)
            {
                ht.Add("@ScheduleID", scheduleId);
            }
            else
            {
                ht.Add("@ScheduleID", null);
            }

            ht.Add("@ScheduleItemItems", scheduleItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35051_pcins_ScheduleItem", ht);
        }

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The result status.</returns>
        public static int F35051_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@ScheduleItemIDs", scheduleItemIds);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35051_pcdel_ScheduleItem", ht);
        }

        /// <summary>
        /// F35051_s the get depr percentage.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="year">The year.</param>
        /// <returns>The schedule line items dataset.</returns>
        public static F35051ScheduleLineItemsData F35051_GetDeprPercentage(Int16 rollYear, int deprTableId, Int16 year)
        {
            F35051ScheduleLineItemsData scheduleLineItemData = new F35051ScheduleLineItemsData();
            Hashtable ht = new Hashtable();
            ht.Add("@Rollyear", rollYear);
            ht.Add("@DeprTableID", deprTableId);
            ht.Add("@Year", year);
            Utility.LoadDataSet(scheduleLineItemData.GetDeprPercent, "f36050_pcget_PPDepreciationPercent", ht);
            return scheduleLineItemData;
        }
    }
}
