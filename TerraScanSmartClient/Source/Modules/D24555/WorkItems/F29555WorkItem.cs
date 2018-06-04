

namespace D24555
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F29555WorkItem : WorkItem
    {
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


        #region F29555

        /// <summary>
        /// F29555PersonalPropertySaleData
        /// </summary>
        /// <returns></returns>
        public F29555PersonalPropertySaleData F29555_DeedtypeComboBox()
        {
            return WSHelper.F29555_DeedtypeComboBox();
        }

        /// <summary>
        /// F29555_s the save transfer ownership.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  string F29555_SaveTransferOwnership(int eventId, int userId)
        {
            return WSHelper.F29555_SaveTransferOwnership(eventId, userId);
        }

        /// <summary>
        /// F29555_s the get personal sales owner.
        /// </summary>
        /// <param name="pSsaleId">The p ssale id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="scheduleString">The schedule string.</param>
        /// <returns></returns>
        public  F29555PersonalPropertySaleData F29555_GetPersonalSalesOwner(int? pSsaleId, int? ownerId, int? scheduleId, int userid, string scheduleString)
        {
            return WSHelper.F29555_GetPersonalSalesOwner(pSsaleId, ownerId, scheduleId, userid, scheduleString);
        }

        /// <summary>
        /// F29555_s the get sales scheduleand owners.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="pSsaleId">The p ssale id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public F29555PersonalPropertySaleData F29555_GetSalesScheduleandOwners(int? scheduleId, string scheduleIds, int? pSsaleId, int userid)
        {
            return WSHelper.F29555_GetSalesScheduleandOwners(scheduleId, scheduleIds, pSsaleId, userid);
        }

        /// <summary>
        /// F29555_s the schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        public F29555PersonalPropertySaleData F29555_ScheduleSaleTracking(int eventId, int userid)
        {
            return WSHelper.F29555_ScheduleSaleTracking(eventId, userid);
        }
        /// <summary>
        /// Transfer Ownership Details
        /// </summary>
        /// <param name="eventId">The Event Id</param>
        /// <param name="userId">The User Id</param>
        /// <returns>Message returned from SP</returns>
        public string F29555_TransferOwnership(int eventId, int userId)
        {
            return WSHelper.F29555_SaveTransferOwnership(eventId, userId);
        }
        /// <summary>
        /// F29555_s the save sales owner.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        public void F29555_SaveSalesOwner(int pSaleId, string ownerDetails, int userId)
        {
            WSHelper.F29555_SaveSalesOwner(pSaleId, ownerDetails, userId);
        }

        /// <summary>
        /// F29555_s the save sales schedule.
        /// </summary>
        /// <param name="pSaleId">The p sale id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        public void F29555_SaveSalesSchedule(int pSaleId, string scheduleItems, int userId)
        {
            WSHelper.F29555_SaveSalesSchedule(pSaleId, scheduleItems, userId);
        }

        /// <summary>
        /// F29555_s the save schedule sale tracking.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="pSaleItems">The p sale items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="ownerDetails">The owner details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29555_SaveScheduleSaleTracking(int eventId, string pSaleItems, string scheduleItems, string ownerDetails, int userId)
        {
            return WSHelper.F29555_SaveScheduleSaleTracking(eventId, pSaleItems, scheduleItems, ownerDetails, userId);
        }

        /// <summary>
        /// F1404_s the get sale tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public F1404ScheduleSelectionData F1404_GetScheduleTrackingRollYear(int eventID)
        {
            return WSHelper.F1404_GetScheduleTrackingRollYear(eventID);
        }
        /// <summary>
        /// F35080_s the delete Schedule details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        //public void F29555_DeleteOwnerDetails(int eventID, int userId)
        //{

        //    WSHelper.F29555_DeleteOwnerDetails(eventID, userId);
        //}

        #endregion
    }
}
