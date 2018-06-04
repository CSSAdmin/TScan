

namespace D24636
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

    public class F29636WorkItem :WorkItem
    {

        /// <summary>
        /// F29636_s the get BOE details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public F29636BOEData F29636_GetBOEDetails(int eventId)
        {
            return WSHelper.F29636_GetBOEDetails(eventId);
        }

        /// <summary>
        /// F29636_s the BOE type details.
        /// </summary>
        /// <returns></returns>
        public F29636BOEData F29636_BOETypeDetails()
        {
            return WSHelper.F29636_BOETypeDetails();
        }

        /// <summary>
        /// F29636_s the save BOE details.
        /// </summary>
        /// <param name="boeElemenets">The boe elemenets.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The user id.</param>
        public void F29636_SaveBOEDetails(string boeElemenets, string boeValues, int userId)
        {
            WSHelper.F29636_SaveBOEDetails(boeElemenets, boeValues, userId);
        }

        /// <summary>
        /// F29636_s the push BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public string F29636_PushBOEDetails(int boeId, int userId)
        {
            return WSHelper.F29636_PushBOEDetails(boeId, userId);
        }

        /// <summary>
        /// F29636_s the delete BOE details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public  void F29636_DeleteBOEDetails(int boeId, int userId)
        {
            WSHelper.F29636_DeleteBOEDetails(boeId, userId);
        }
        
        /// <summary>
        /// F9002_s the get user details.
        /// </summary>
        /// <param name="applicationId">The application id.</param>
        /// <returns></returns>
        public UserManagementData F9002_GetUserDetails(int applicationId)
        {
            return WSHelper.F9002_GetUserDetails(applicationId);
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
