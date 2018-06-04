// -------------------------------------------------------------------------------------------------
// <copyright file="F2005WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------


namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;


    /// <summary>
    /// F2005WorkItem class file
    /// </summary> 
    public class F2005WorkItem:WorkItem
    {

        /// <summary>
        /// Gets the details of F250500 ScheduleDetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F2200EditScheduleData F25050_GetScheduleDetails(int scheduleId)
        {
            return WSHelper.F25050_GetScheduleDetails(scheduleId);
        }

        /// <summary>
        /// F2005_GetScheduleLockUserName
        /// </summary>
        /// <param name="userId">userId</param>        
        /// <returns>F2005_GetScheduleLockUserName</returns>
        public F2200EditScheduleData F2005_GetScheduleUserName(int userId)
        {
            return WSHelper.F2005_GetScheduleUserName(userId);
        }

        public F2001ParcelLockingData F2001_getParcelLockingUsername(int userId)
        {
            return WSHelper.F2001_getParcelLockingUsername(userId);
        }

        /// <summary>
        /// F2005_GetValidUser
        /// </summary>
        /// <param name="scheduleId">scheduleId</param>
        /// <param name="userId">userId</param>
        /// <returns>F2005_GetValidUser</returns>
        public int F2005_GetValidUser(int scheduleId, int userId)
        {
            return WSHelper.F2005_GetValidUser(scheduleId, userId );
        }

        public int F2005_UpdateParcelLockDetails(int scheduleId, int lockValue, int userId)
        {
            return WSHelper.F2005_UpdateParcelLockDetails(scheduleId, lockValue, userId);
        }
        
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
    }
}
