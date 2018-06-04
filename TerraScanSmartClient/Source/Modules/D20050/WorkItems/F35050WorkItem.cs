//--------------------------------------------------------------------------------------------
// <copyright file="F35050WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//21 Feb 2008		Ramya.D	            Created
//*********************************************************************************/

namespace D20050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F35050WorkItem
    /// </summary>
    public class F35050WorkItem : WorkItem    
    {
        #region Get Form Slice Permission Details

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion Get Form Slice Permission Details

        /// <summary>
        /// Gets the config details.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #region ScheduleLineItem

        #region GetScheduleLineItem

        /// <summary>
        /// F35050_GetScheduleLineItemDetails
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <returns>DataSet</returns>
        public F35050ScheduleLineItemDataSet GetScheduleLineItemDetails(int scheduleId)
        {
            return WSHelper.F35050_GetScheduleLineItemDetails(scheduleId);
        }

        #endregion GetScheduleLineItem

        #region F35050_GetScheduleItem

        /// <summary>
        /// F35050_GetScheduleItem
        /// </summary>
        /// <returns>DataSet</returns>
        public F35050ScheduleLineItemDataSet F35050_GetScheduleItem()
        {
            return WSHelper.F35050_GetScheduleItem();
        }

        #endregion F35050_GetScheduleItem

        #region F35050_GetScheduleCategory

        /// <summary>
        /// F35050_GetScheduleCategory
        /// </summary>
        /// <returns>DataSet</returns>
        public F35050ScheduleLineItemDataSet F35050_GetScheduleCategory()
        {
            return WSHelper.F35050_GetScheduleCategory();
        }

        #endregion F35050_GetScheduleCategory

        #region F35050_SaveScheduleLineItem

        /// <summary>
        /// F35050_SaveScheduleLineItem
       /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="scheduleItems">scheduleItems</param>
        /// <param name="userId">userId</param>
       /// <returns>int</returns>
        public int SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return WSHelper.F35050_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        #endregion F35050_SaveScheduleLineItem


        #region F35050_CalculateAmount

        public F35050ScheduleLineItemDataSet F35050_CalculateAmount(int ScheduleItemID, int Rollyear, int Year, int DeprDescription)
        {
            return WSHelper.F35050_CalculateAmount(ScheduleItemID, Rollyear, Year, DeprDescription);
        }

        #endregion

        #region GetDepreciationValue

        /*  /// <summary>
        /// F35050_GetDepreciationValue
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="recv">recv</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>DataSet</returns>
        public F35050ScheduleLineItemDataSet F35050_GetDepreciationValue(int scheduleId,int recv,int rollYear)
        {
            return WSHelper.F35050_GetDepreciationValue(scheduleId, recv, rollYear);
        } */

        /// <summary>
        /// F36000_GetDeprPercentage
        /// </summary>
        /// <param name="age"></param>
        /// <param name="objectCondition"></param>
        /// <param name="deprTableId"></param>
        /// <returns>string</returns>
        //public string GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        //{
        //    return WSHelper.F36000_GetDeprPercentage(age, objectCondition, deprTableId);
        //}

        #endregion GetDepreciationValue

        #region DeleteScheduleLineItem

        /// <summary>
        /// DeleteScheduleLineItem
        /// </summary>
        /// <param name="scheduleLineId">scheduleLineId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int F35050_DeleteScheduleLineItem(int scheduleLineId, int userId)
        {
            return WSHelper.F35050_DeleteScheduleLineItem(scheduleLineId,  userId);
        }

        #endregion DeleteScheduleLineItem

        public F35050ScheduleLineItemDataSet GetDeprPercentage(int rollyear, int deprtableID, int year)
        {
            return WSHelper.F35050_GetDeprPercentage(rollyear, deprtableID, year);
        }

        #endregion ScheduleLineItem



        #region F35050ListTableDetails

        public  F35050ScheduleLineItemDataSet F35050_GetListTableDetails(int itemcategoryID)
        {
            return WSHelper.F35050_GetListTableDetails(itemcategoryID);


        }
        #endregion



        #region F35050ListOutTableDetails
        public F35050ScheduleLineItemDataSet F35050_GetListOutTableDetails(int ScheduleID)
        {
            return WSHelper.F35050_GetListOutTableDetails(ScheduleID);
        }

        #endregion

        #region DeleteSchedule

        /// <summary>
        /// F35050_s the delete schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status</returns>
        public int F35050_DeleteSchedule(int scheduleId, int userId)
        {
            return WSHelper.F2200_DeleteEditSchedule(scheduleId, userId);
        }

        #endregion
    }
}
