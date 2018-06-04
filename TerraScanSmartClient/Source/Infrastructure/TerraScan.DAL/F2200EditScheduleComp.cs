// -------------------------------------------------------------------------------------------
// <copyright file="F2200EditScheduleComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and editScheduleComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
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
    /// The edit schedule comp class.
    /// </summary>
    public static class F2200EditScheduleComp
    {
        #region List

        /// <summary>
        /// F2200_s the list edit schedule details.
        /// </summary>
        /// <param name="SheduleID">The shedule ID.</param>
        /// <returns></returns>
        public static F2200EditScheduleData F2200_ListEditScheduleDetails(int SheduleID)
        {
            F2200EditScheduleData F2200_ListEditSchedule = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@SheduleID", SheduleID);
           // Utility.LoadDataSet(F2200_ListEditSchedule.f2200ListScheduleDataTable, "f25050_pcget_Schedule", ht);
            string[] tableName = new string[] { F2200_ListEditSchedule.f25050_pcget_Configuredstate.TableName, F2200_ListEditSchedule.f2200ListScheduleDataTable.TableName };
            Utility.LoadDataSet(F2200_ListEditSchedule, "f25050_pcget_Schedule", ht, tableName);
            return F2200_ListEditSchedule;
        }

        /// <summary>
        /// F2200 get schedule details.
        /// </summary>
        /// <param name="SheduleID">The shedule ID.</param>
        /// <returns>scheduleData</returns>
        public static F2200EditScheduleData F25050_GetScheduleDetails(int ScheduleID)
        {
            F2200EditScheduleData scheduleData = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", ScheduleID);
            Utility.LoadDataSet(scheduleData.f2200ListScheduleDataTable, "f2005_pcget_ScheduleLock", ht);
            return scheduleData;
        }

        /// <summary>
        /// F2005_UpdateParcelLockDetails
        /// </summary>
        /// <param name="ScheduleID">scheduleId</param>
        /// <param name="lockValue">lockValue</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer Value</returns>
        public static int F2005_UpdateParcelLockDetails(int ScheduleID, int lockValue, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", ScheduleID);
            ht.Add("@LockScheduleBy", lockValue);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f2005_pcupd_ScheduleLock", ht);
        }

        /// <summary>
        ///F2005_GetValidUser
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="userId">userId</param>
        /// <param name="usercanUnlock">usercanUnlock</param>
        /// <returns>Integer Value</returns>
        public static int F2005_GetValidUser(int scheduleId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID ", scheduleId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f2005_pcchk_UserID", ht);
        }

        /// <summary>
        /// F2005_GetScheduleLockUserName
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Typed dataset</returns>
        public static F2200EditScheduleData F2005_GetScheduleUserName(int userId)
        {
            F2200EditScheduleData parcelLockingData = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(parcelLockingData.f25050GetScheduleParcelDetailsTable, "f2001_pcget_UserName", ht);
            return parcelLockingData;
        }

        #endregion

        #region Insert

        /// <summary>
        /// F2200_s the insert edit schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static F2200EditScheduleData  F2200_InsertEditSchedule(int? scheduleId, string scheduleItems, int userId)
        {
            F2200EditScheduleData ListInput = new F2200EditScheduleData();
            ListInput.ListInputVAlue.AddListInputVAlueRow("@PrimaryKeyID", string.Empty, "int", 8);
            ListInput.ListInputVAlue.AddListInputVAlueRow("@IsError", string.Empty, "bool", 2);
            ListInput.ListInputVAlue.AddListInputVAlueRow("@Message", string.Empty, "string", 3000);
              Hashtable ht = new Hashtable();
            if (scheduleId != null || scheduleId > 0)
            {
                ht.Add("@ScheduleID", scheduleId);
            }
            ht.Add("@ScheduleItems", scheduleItems);
            ht.Add("@UserID", userId);
             Utility.SPParameters("f25050_pcins_Schedule", ListInput.ListInputVAlue, ht,ListInput.ListOutputValue  );
             return ListInput; 
        }

        #endregion

        #region UpdateEditSchedule

        /// <summary>
        /// F2200_s the update edit schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The status of update.</returns>
        public static int F2200_UpdateEditSchedule(int scheduleId, string scheduleItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@ScheduleItems", scheduleItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25050_pcupd_Schedule", ht);
        }

        #endregion

        #region DeleteeditSchedule

        /// <summary>
        /// F2200_s the delete edit schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The deleted item status.</returns>
        public static int F2200_DeleteEditSchedule(int scheduleId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25050_pcdel_Schedule", ht);
        }

        #endregion

        #region List Assessment Type Details

        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns>The edit schedule dataset.</returns>
        public static F2200EditScheduleData F2200_GetAssessmentTypeDetails(string assessmentType)
        {
            F2200EditScheduleData assessmentTypeDetails = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@AssessmentType", assessmentType);
            Utility.LoadDataSet(assessmentTypeDetails.List_AssessmentTypeDataTable, "f9001_pclst_AssessmentType", ht);
            return assessmentTypeDetails;
        }

        #endregion

        #region List Special District Assessment ParcelID

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public static F2200EditScheduleData f25050GetDistrictAssessmentParcelID(string parcelNumber, int parcelId)
        {
            F2200EditScheduleData specialDistrictAssessmentData = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(specialDistrictAssessmentData.f25050GetScheduleParcelDetailsTable, "f25050_pcget_ScheduleParcelDetails", ht);
            return specialDistrictAssessmentData;
        }

        #endregion
        
        #region Get PenaltyPercent
        /// <summary>
        /// Gets the penalty percent.
        /// </summary>
        /// <param name="filingDate">The filing date.</param>
        /// <returns>PenaltyPercent</returns>
        public static decimal GetPenaltyPercent(string filingDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@FilingDate", filingDate);

            tempvalue = DataProxy.FetchSpObject("f2200_pcget_PPPenaltyPercent", ht);

            if (tempvalue != null)
            {
                return (decimal)tempvalue;
            }

            return 0;
        }
        #endregion Get PenaltyPercent

        /// <summary>
        /// F2200s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public static F2200EditScheduleData f2200GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear)
        {
             F2200EditScheduleData ScheduledObj = new F2200EditScheduleData();
            //object tempValue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@IsFarmExempt", isFarmExempt);
            ht.Add("@FarmExemptYear", ExemptRollYear);
            //tempValue = DataProxy.FetchSpObject("f2200_pcget_GetFarmExemptAmount_NE", ht);
              Utility.LoadDataSet(ScheduledObj.f2200_GetFarmExemptAmountDataTable,"f2200_pcget_GetFarmExemptAmount_NE",ht);
            //return tempValue;
              return ScheduledObj;
        }
        public static decimal F2200_GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear, bool isEx259, decimal ex259Amount)
        {
            object tempValue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@IsFarmExempt", isFarmExempt);
            ht.Add("@FarmExemptYear", ExemptRollYear);
            ht.Add("@Is259Exempt", isEx259);
            ht.Add("@Exempt259Amount", ex259Amount);
            tempValue = DataProxy.FetchSpObject("f2200_pcget_GetFarmExemptAmount_NE", ht);
            if (!string.IsNullOrEmpty(tempValue.ToString()))
            {
                return (decimal)tempValue;
            }
            return 0;
        }

        /// <summary>
        /// Get 259 Exemption Amount.
        /// </summary>
        /// <param name="scheduleId"></param>
        /// <param name="isFarmExempt"></param>
        /// <param name="ExemptRollYear"></param>
        /// <returns></returns>
        public static F2200EditScheduleData F2200_Get259ExemptionAmount(int scheduleId)
        {
            F2200EditScheduleData ScheduledObj = new F2200EditScheduleData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            string[] tableName = new string[] { ScheduledObj.f2200Get259ExemptionAmount.TableName, ScheduledObj.Get259ExemptionAmount.TableName };
            Utility.LoadDataSet(ScheduledObj,"f2200_pcget_PersonalProperty259ExemptionAmount", ht,tableName);
            return ScheduledObj;
        }
    }
}
