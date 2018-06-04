//--------------------------------------------------------------------------------------------
// <copyright file="F2200WorkItem.cs" company="Congruent">
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
//31 Jan 08		Sriparameswari A	    Created
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
    /// F36000WorkItem
    /// </summary>
    public class F2200WorkItem : WorkItem
    {
        /// <summary>
        /// F2200_ListEditScheduleDetails
        /// </summary>
        /// <param name="SheduleID">SheduleID</param>
        /// <returns>F2200EditScheduleData</returns>
        public F2200EditScheduleData F2200_ListEditScheduleDetails(int SheduleID)
        {
            return WSHelper.F2200_ListEditScheduleDetails(SheduleID);
        }

        /// <summary>
        /// F2200_InsertEditSchedule
        /// </summary>
        /// <param name="ScheduleID">ScheduleID</param>
        /// <param name="ScheduleItems">ScheduleItems</param>
        /// <param name="UserID">UserID</param>
        /// <returns>int</returns>
        public F2200EditScheduleData  F2200_InsertEditSchedule(int? ScheduleID, string ScheduleItems, int UserID)
        {
            return WSHelper.F2200_InsertEditSchedule(ScheduleID, ScheduleItems, UserID);
        }

        /// <summary>
        /// F2200_UpdateEditSchedule
        /// </summary>
        /// <param name="ScheduleID">ScheduleID</param>
        /// <param name="ScheduleItems">ScheduleItems</param>
        /// <param name="UserID">UserID</param>
        /// <returns>int</returns>
        public int F2200_UpdateEditSchedule(int ScheduleID, string ScheduleItems, int UserID)
        {
            return WSHelper.F2200_UpdateEditSchedule(ScheduleID, ScheduleItems, UserID);
        }

        /// <summary>
        /// F2200_DeleteEditSchedule
        /// </summary>
        /// <param name="ScheduleID">ScheduleID</param>
        /// <param name="UserID">UserID</param>
        /// <returns>int</returns>
        public int F2200_DeleteEditSchedule(int ScheduleID, int UserID)
        {
            return WSHelper.F2200_DeleteEditSchedule(ScheduleID, UserID);
        }


        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesF2200_InsertEditScheduleOwnerDetails Dataset</returns>
        public F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            return WSHelper.F15010_GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>returns dataset containing District Assessment ParcelID</returns>
        public F2200EditScheduleData F2200_GetDistrictAssessmentParcelId(string parcelNumber, int parcelId)
        {
            return WSHelper.f25050GetDistrictAssessmentParcelID(parcelNumber, parcelId);
        }


        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>typed dataset</returns>
        public F1512DistrictSelectionData F2200_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            return WSHelper.F1512_GetDistrictSelectionData(districtId, district, description, rollYear);
        }

        /// <summary>
        /// F2200_s the get assessment type details.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        public F2200EditScheduleData F2200_GetAssessmentTypeDetails(string assessmentType)
        {
            return WSHelper.F2200_GetAssessmentTypeDetails(assessmentType);
        }

        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public F2204CopyScheduleData.f25050_ScheduleTypeDataTable F25050GetScheduleTypeDetails()
        {
            return WSHelper.F25050GetScheduleTypeDetails();
        }

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public F2204CopyScheduleData.f25050_AssessmentTypeDataTable F25050GetParcelTypeDetails()
        {
            return WSHelper.F25050GetParcelTypeDetails();
        }

        /// <summary>
        /// F2200s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public F2200EditScheduleData f2200GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear)
        {
            return WSHelper.f2200GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear);
        }

        /// <summary>
        /// F2200_s the get farm exempt amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public decimal F2200_GetFarmExemptAmount(int scheduleId, bool isFarmExempt, int ExemptRollYear,bool isEx259 ,decimal ex259Amount)
        {
            return WSHelper.F2200_GetFarmExemptAmount(scheduleId, isFarmExempt, ExemptRollYear, isEx259, ex259Amount);
        }

        /// <summary>
        /// F2200_s Ex 259 Exemption Amount.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isFarmExempt">if set to <c>true</c> [is farm exempt].</param>
        /// <param name="ExemptRollYear">The exempt roll year.</param>
        /// <returns></returns>
        public F2200EditScheduleData F2200_Get259ExemptionAmount(int scheduleId)
        {
            return WSHelper.F2200_Get259ExemptionAmount(scheduleId);
        }

        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public F2201CentrallyAssessedSearchData F2201_GetPersonalPropertyDescription(string code)
        {
            return WSHelper.F2201_GetPersonalPropertyDescription(code);
        }

        #region Get Penalty Percent

        /// <summary>
        /// Gets the penalty percent.
        /// </summary>
        /// <param name="filingDate">The filing date.</param>
        /// <returns>Penalty percent</returns>
        public decimal GetPenaltyPercent(string filingDate)
        {
            return WSHelper.GetPenaltyPercent(filingDate);
        }

        #endregion Get Penalty Percent

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

    }
}
