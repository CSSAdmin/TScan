//--------------------------------------------------------------------------------------------
// <copyright file="F28000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F28000 Discretionary Exemption
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/04/2011        D.LathaMaheswari  Created
//***********************************************************************************************/
namespace D23000
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

    /// <summary>
    /// F28000WorkItem
    /// </summary>
    public class F28000WorkItem : WorkItem
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

        #region Get Discretionary Details

        /// <summary>
        /// Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Discretionary Details</returns>
        public F28000DiscretionaryData F28000_GetDiscretionaryDetails(int eventId)
        {
            return WSHelper.F28000_GetDiscretionaryDetails(eventId);
        }

        #endregion Get Discretionary Details

        #region Get Class Details

        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        public F28000DiscretionaryData F28000_GetClass(int stateId, int eventId)
        {
            return WSHelper.F28000_GetClass(stateId, eventId);
        }

        #endregion Get Class Details

        #region Exemption Amount

        /// <summary>
        /// Exemption Amount
        /// </summary>
        /// <param name="rollYear">roll Year</param>
        /// <param name="exemptionYear">Exemption Year</param>
        /// <param name="subjectAmount">Subject Amount</param>
        /// <returns>Exemption Amount</returns>
        public F28000DiscretionaryData F28000_GetExemptionAmount(int rollYear, int exemptionYear, decimal subjectAmount)
        {
            return WSHelper.F28000_GetExemptionAmount(rollYear, exemptionYear, subjectAmount);
        }

        #endregion Exemption Amount

        #region Save Discretionary Details

        /// <summary>
        /// Save Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML string</param>
        /// <param name="userId">User ID</param>
        /// <returns>Confirmation Value</returns>
        public int F28000_SaveDiscretionaryDetail(int eventId, int? discretionaryId, string discretionaryItems, int userId)
        {
            return WSHelper.F28000_SaveDiscretionaryDetail(eventId, discretionaryId, discretionaryItems, userId);
        }

        #endregion Save Discretionary Details

        #region Delete Discretionary Details

        /// <summary>
        /// Delete Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML String</param>
        /// <param name="userId">USer ID</param>
        public void F28000_DeletediscretionaryDetails(int? discretionaryId, string discretionaryItems, int userId)
        {
            WSHelper.F28000_DeletediscretionaryDetails(discretionaryId, discretionaryItems, userId);
        }

        #endregion Delete Discretionary Details
    }
}
