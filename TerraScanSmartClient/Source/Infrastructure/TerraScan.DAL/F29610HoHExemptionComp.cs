// -------------------------------------------------------------------------------------------
// <copyright file="F29610.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36041CropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/4/08          Malliga             Created
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
    /// F29610HoHExemptionComp Class File.
    /// </summary>
    public class F29610HoHExemptionComp
    {
        /// <summary>
        /// F29610_s the get ho H exemption details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public static F29610HoHExemptionData F29610_GetHoHExemptionDetails(int eventid)
        {
            F29610HoHExemptionData HoHExemptionDetails = new F29610HoHExemptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventid);
            Utility.LoadDataSet(HoHExemptionDetails.GetHoHExemptionDetails, "f24610_pcget_HoH", ht);
            return HoHExemptionDetails;
        }

        /// <summary>
        /// F29610_s the get calculation of ho H.
        /// </summary>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <param name="exemptionid">The exemptionid.</param>
        /// <returns></returns>
        public static F29610HoHExemptionData F29610_GetCalculationOfHoH(int scheduleid,int exemptionid)
        {
            F29610HoHExemptionData HoHExemptionDetails = new F29610HoHExemptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleid);
            ht.Add("@ExemptionID", exemptionid);
            Utility.LoadDataSet(HoHExemptionDetails.GetCalculationHeadOfHousehold, "f24610_pcget_CalculationHeadOfHousehold", ht);
            return HoHExemptionDetails;
        }

        /// <summary>
        /// F29610_s the owner percent.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public static F29610HoHExemptionData F29610_GetOwnerPercent(int ownerId, int scheduleId)
        {
            F29610HoHExemptionData HoHExemptionDetails = new F29610HoHExemptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerID", ownerId);
            ht.Add("@ScheduleID", scheduleId);
            Utility.LoadDataSet(HoHExemptionDetails.GetOwnerPercent, "f24610_pcget_OwnerPercent", ht);
            return HoHExemptionDetails;
        }


        /// <summary>
        /// F29610_s the save ho H exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="HoHItems">The ho H items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29610_SaveHoHExemptionDetails(int eventId, string HoHItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@HeadOfHouseholdItems", HoHItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f24610_pcins_HeadOfHousehold", ht);
        }

    }
}
