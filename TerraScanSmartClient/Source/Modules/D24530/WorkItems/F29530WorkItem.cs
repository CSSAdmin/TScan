// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36032WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36032 LandCodes</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 23/09/2007       Sriparameswari              Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D24530
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F29530WorkItem:WorkItem
    {
        #region Work Item Methods


        #region F29530

        public F29530EventAssociationData F29530_FillAssociationEventGrid(int eventId)
        {
            return WSHelper.F29530_FillAssociationEventGrid(eventId);
        }



        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return WSHelper.GetGDocEventHeader(eventId);
        }

        #endregion

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

        #endregion Work Item Methods
    }
}
