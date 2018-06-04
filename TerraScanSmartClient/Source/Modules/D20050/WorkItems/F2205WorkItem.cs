// --------------------------------------------------------------------------------------------
// <copyright file="F2205WorkItem.cs" company="Congruent">
//    Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// This file contains methods for the F2204WorkItem.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date              Author             Description
// ----------       ---------          ---------------------------------------------------------
// 16/07/09        LathaMaheswari.D    Created
// *********************************************************************************/

namespace D20050
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F2205WorkItem class file
    /// </summary> 
    public class F2205WorkItem : WorkItem
    {
        #region WorkItems Methods
    
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
   
        #endregion WorkItems Methods


        /// <summary>
        /// F25050s the get schedule type details.
        /// </summary>
        /// <returns>Schedule Type</returns>
        public F2204CopyScheduleData.f25050_ScheduleTypeDataTable F25050GetScheduleTypeDetails()
        {
            return WSHelper.F25050GetScheduleTypeDetails();
        }

        /// <summary>
        /// F25050s the get parcel type details.
        /// </summary>
        /// <returns>Assessment Type</returns>
        public F2204CopyScheduleData.f25050_AssessmentTypeDataTable F25050GetParcelTypeDetails()
        {
            return WSHelper.F25050GetParcelTypeDetails();
        }

        /// <summary>
        /// F2200_s the list edit schedule details.
        /// </summary>
        /// <param name="SheduleID">The shedule ID.</param>
        /// <returns>Schedule Details</returns>
        public F2200EditScheduleData F2200_ListEditScheduleDetails(int SheduleID)
        {
            return WSHelper.F2200_ListEditScheduleDetails(SheduleID);
        }

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetFormDetails</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        /// <summary>
        /// F2205s the create schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isNewSchedule">if set to <c>true</c> [is new schedule].</param>
        /// <param name="scheduleHeaderItems">The schedule header items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Confirmation for create schedule</returns>
        public int F2205CreateSchedule(int? scheduleId, bool isNewSchedule, string scheduleHeaderItems, string scheduleItems, int userId)
        {
            return WSHelper.F2205CreateSchedule(scheduleId, isNewSchedule, scheduleHeaderItems, scheduleItems, userId);
        }
    }
}
