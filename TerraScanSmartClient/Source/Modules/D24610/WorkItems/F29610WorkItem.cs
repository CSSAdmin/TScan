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

namespace D24610
{
    public class F29610WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
        }

        /// <summary>
        /// F29610_s the get ho H exemption details.
        /// </summary>
        /// <param name="eventid">The eventid.</param>
        /// <returns></returns>
        public F29610HoHExemptionData F29610_GetHoHExemptionDetails(int eventid)
        {
            return WSHelper.F29610_GetHoHExemptionDetails(eventid);
        }

        /// <summary>
        /// F29610_s the get calculation of ho H.
        /// </summary>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <param name="exemptionid">The exemptionid.</param>
        /// <returns></returns>
        public F29610HoHExemptionData F29610_GetCalculationOfHoH(int scheduleid, int exemptionid)
        {
            return WSHelper.F29610_GetCalculationOfHoH(scheduleid, exemptionid);
        }


        /// <summary>
        /// F29610_s the get owner percent.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="scheduleid">The scheduleid.</param>
        /// <returns></returns>
        public F29610HoHExemptionData F29610_GetOwnerPercent(int ownerId, int scheduleid)
        {
            return WSHelper.F29610_GetOwnerPercent(ownerId, scheduleid);
        }

        /// <summary>
        /// F29610_s the save ho H exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="HoHItems">The ho H items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F29610_SaveHoHExemptionDetails(int eventId, string HoHItems, int userId)
        {
            return WSHelper.F29610_SaveHoHExemptionDetails(eventId, HoHItems, userId);
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
