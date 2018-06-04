// -------------------------------------------------------------------------------------------
// <copyright file="F35050ScheduleLineItemComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35050ScheduleLineItemComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//20 Feb 2008       Ramya.D              Created
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
    /// F35050ScheduleLineItemComp
    /// </summary>
    public class F35050ScheduleLineItemComp
    {
        #region ListScheduleLineItem

        /// <summary>
         /// F35050_GetScheduleLineItemDetails
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <returns>DataSet</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleLineItemDetails(int scheduleId)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            string[] tableName = new string[] { scheduleLineItemdata.SchedlueLineItemTable.TableName, scheduleLineItemdata.SchedlueItemTable.TableName, scheduleLineItemdata.SchedlueCategoryTable.TableName, scheduleLineItemdata.RollYearTable.TableName };
            Utility.LoadDataSet(scheduleLineItemdata, "f35050_pclst_SchedlueLineItem", ht, tableName);
            return scheduleLineItemdata;
        }

        #endregion ListScheduleLineItem  
        
        #region ListTableDetails

        /// <summary>
        /// F35050_s the get list table details.
        /// </summary>
        /// <param name="itemcategoryID">The itemcategory ID.</param>
        /// <returns>scheduleline item dataset.</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetListTableDetails(int itemcategoryID)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            if (itemcategoryID == 0)
            {
                ht.Add("@ItemCategoryID", DBNull.Value);
            }
            else
            {
                ht.Add("@ItemCategoryID", itemcategoryID);
            }

            Utility.LoadDataSet(scheduleLineItemdata.ListTableDetails, "f35050_pclst_DeprName", ht);
            return scheduleLineItemdata;
        }
        
        #endregion
        
        #region ListOutTableDetails

        /// <summary>
        /// F35050_s the get list out table details.
        /// </summary>
        /// <param name="scheduleId">The schedule ID.</param>
        /// <returns>schedulelineitem dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetListOutTableDetails(int scheduleId)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            if (scheduleId == 0)
            {
                ht.Add("@ScheduleID", DBNull.Value);
            }
            else
            {
                ht.Add("@ScheduleID", scheduleId);
            }

            Utility.LoadDataSet(scheduleLineItemdata.pclstDeprTable, "f35050_pclst_DeprTable", ht);
            return scheduleLineItemdata;
        }

        #endregion
        
        #region ListScheduleItem
        
        /// <summary>
        /// F35050_GetScheduleItem
       /// </summary>
       /// <returns>Dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleItem()
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(scheduleLineItemdata.SchedlueItemTable, "f35050_pclst_SchedlueItem", ht);
            return scheduleLineItemdata;
        }

        #endregion ListScheduleItem

        #region ListScheduleCategory

        /// <summary>
        /// F35050_GetScheduleCategory
        /// </summary>
        /// <returns>Dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetScheduleCategory()
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(scheduleLineItemdata.SchedlueCategoryTable, "f35050_pclst_SchedlueCategory", ht);
            return scheduleLineItemdata;
        }

        #endregion ListScheduleCategory

        #region SaveScheduleLineItem 

        /// <summary>
        /// F35050_SaveScheduleLineItem
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="scheduleItems">scheduleItems</param>
        /// <param name="userId">userId</param>
        /// <returns>Int</returns>
        public static int F35050_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (scheduleId > 0)
            {
                ht.Add("@ScheduleItemID", scheduleId);
            }
            else
            {
                ht.Add("@ScheduleItemID", null);
            }

            ht.Add("@ScheduleItemItems", scheduleItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35050_pcins_ScheduleItem", ht);
        }

        #endregion SaveScheduleLineItem

        #region F35050_CalculateAmount

        /// <summary>
        /// F35050_s the calculate amount.
        /// </summary>
        /// <param name="scheduleItemId">The schedule item id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="year">The year.</param>
        /// <param name="deprDescription">The depr description.</param>
        /// <returns>schedulelineitem dataset</returns>
        public static F35050ScheduleLineItemDataSet F35050_CalculateAmount(int scheduleItemId, int rollYear, int year, int deprDescription)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleItemID", scheduleItemId);
            ht.Add("@Rollyear", rollYear);

            if (year >= 0)
            {
                ht.Add("@Year", year);
            }
            else
            {
                ht.Add("@Year", null);
            }

            if (deprDescription >= 0)
            {
                ht.Add("@DeprTableID", deprDescription);
            }
            else
            {
                ht.Add("@DeprTableID", null);
            }

            Utility.LoadDataSet(scheduleLineItemdata.AccountDetails, "f35050_pcget_Amount", ht);
            return scheduleLineItemdata;
        }

        #endregion
        
        #region GetDepreciationValue

        /// <summary>
        /// GetDepreciationValue
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="recv">recv</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>DataSet</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetDepreciationValue(int scheduleId, int recv, int rollYear)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@Recovery", recv);
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(scheduleLineItemdata, "f35050_pcget_RollYear", ht);
            return scheduleLineItemdata;
        }

        #endregion GetDepreciationValue

        #region DeleteScheduleLineItem

       /// <summary>
        /// DeleteScheduleLineItem
       /// </summary>
        /// <param name="scheduleLineId">scheduleLineId</param>
        /// <param name="userId">userId</param>
       /// <returns>int</returns>
        public static int F35050_DeleteScheduleLineItem(int scheduleLineId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleItemID", scheduleLineId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35050_pcdel_ScheduleItem", ht);
        }

        #endregion DeleteScheduleLineItem

        #region GetDeprPercentage
        
        /// <summary>
        /// F35050_s the get depr percentage.
        /// </summary>
        /// <param name="rollyear">The rollyear.</param>
        /// <param name="deprtableID">The deprtable ID.</param>
        /// <param name="year">The year.</param>
        /// <returns>schedulelineitem dataset.</returns>
        public static F35050ScheduleLineItemDataSet F35050_GetDeprPercentage(int rollyear, int deprtableID, int year)
        {
            F35050ScheduleLineItemDataSet scheduleLineItemdata = new F35050ScheduleLineItemDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@Rollyear", rollyear);
            ht.Add("@DeprTableID", deprtableID);
            ht.Add("@Year", year);
            Utility.LoadDataSet(scheduleLineItemdata.ListDeprTable, "f36050_pcget_PPDepreciationPercent", ht);
            return scheduleLineItemdata;
        }

        #endregion GetDeprPercentage

        #region DeleteSchedule
        
        /// <summary>
        /// F35050_s the delete schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status</returns>
        public static int F35050_DeleteSchedule(int scheduleId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25050_pcdel_Schedule", ht);
        }

        #endregion
    }
}
