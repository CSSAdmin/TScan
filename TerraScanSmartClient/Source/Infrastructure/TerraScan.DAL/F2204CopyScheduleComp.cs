// -------------------------------------------------------------------------------------------
// <copyright file="F2204CopySchedule.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11/09/2008       LathaMaheswari.D    Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// Data Access Layer which talks to the DB directly for F2204
    /// </summary>
    public static class F2204CopyScheduleComp
    {

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F2204CopyScheduleData F25050GetParcelTypeDetails()
        {
            F2204CopyScheduleData assessmentType = new F2204CopyScheduleData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(assessmentType.f25050_AssessmentType, "f25050_pclst_AssessmentType", ht);
            return assessmentType;
        }

        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F2204CopyScheduleData F25050GetScheduleTypeDetails()
        {
            F2204CopyScheduleData scheduleType = new F2204CopyScheduleData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(scheduleType.f25050_ScheduleType, "f25050_pclst_ScheduleType", ht);
            return scheduleType;
        }

        /// <summary>
        /// Creates the new parcel copy.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public static int F2204CreateNewScheduleCopy(int scheduleId, string scheduleItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SheduleID", scheduleId);
            ht.Add("@ScheduleItems", scheduleItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25050_pcexe_CreateNewSchedule", ht);
        }
    }
}
