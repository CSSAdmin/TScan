// -------------------------------------------------------------------------------------------
// <copyright file="F35055PPLineItemComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35055PPLineItemComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//23 Jul 2009       R.Malliga             Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// F35055PPLineItemComp
    /// </summary>
    public class F35055PPLineItemComp
    {

        /// <summary>
        /// F35055_s the get PP line items details.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns>F35055PPLineItemData</returns>
        public static F35055PPLineItemData F35055_GetPPLineItemsDetails(int scheduleId)
        {
            F35055PPLineItemData ppLineItemDetailsData = new F35055PPLineItemData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            string[] tableName = new string[] { ppLineItemDetailsData.F35055_GetSchedlueLineItem.TableName, ppLineItemDetailsData.F35055_DeprSchedlueLineItem.TableName, ppLineItemDetailsData.F35055_Fuel.TableName, ppLineItemDetailsData.F35055_ScheduleItemCode.TableName, ppLineItemDetailsData.F35055_YearSchedlueLineItem.TableName, ppLineItemDetailsData.F35055_VisibleRows.TableName };
            Utility.LoadDataSet(ppLineItemDetailsData, "f35055_pclst_SchedlueLineItem", ht, tableName);
            return ppLineItemDetailsData;
        }

        /// <summary>
        /// F35055_s the get value calculation.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="ppDeprTableId">The pp depr table id.</param>
        /// <param name="originalValue">The original value.</param>
        /// <param name="trend">The trend.</param>
        /// <param name="year">The year.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static F35055PPLineItemData F35055_GetValueCalculation(int scheduleId,int ppDeprTableId,Int64 originalValue,int trend,Int16 year, Int16 rollYear)
        {
            F35055PPLineItemData ppLineItemDetailsData = new F35055PPLineItemData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@PPDeprTableID", ppDeprTableId);
            ht.Add("@OriginalValue", originalValue);
            ht.Add("@Trend", trend);
            ht.Add("@Year", year);
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(ppLineItemDetailsData.F35055_ValueCalculation, "f35055_pcget_ValueCalculation", ht);
            return ppLineItemDetailsData;
        }

        /// <summary>
        /// F35051_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35055_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
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
            return Utility.FetchSPExecuteKeyId("f35055_pcins_ScheduleItem", ht);
        }

        /// <summary>
        /// F35051_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35055_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@ScheduleItemIDs", scheduleItemIds);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35055_pcdel_ScheduleItem", ht);
        }

        /// <summary>
        /// F35051_s the update schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static int F35055_UpdateScheduleLineItem(int scheduleId, string scheduleItems, int userId,Int16 rollYear)
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
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f35055_pcupd_ScheduleItem", ht);
        }
    }
}
