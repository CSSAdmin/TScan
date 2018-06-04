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

namespace D24620
{
    public class F29620WorkItem : WorkItem
    {

        public F29620AglandApplicationData F29620_GetAglandApplicationDetails(int eventid)
        {
            return WSHelper.F29620_GetAglandApplicationDetails(eventid);
        }

        public int F29620_SaveAglandApplicationDetails(int eventId, int ownerId, int userId)
        {
            return WSHelper.F29620_SaveAglandApplicationDetails(eventId, ownerId, userId);
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
