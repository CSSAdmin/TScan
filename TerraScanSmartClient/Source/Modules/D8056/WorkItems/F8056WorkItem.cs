//--------------------------------------------------------------------------------------------
// <copyright file="F8056WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8056WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Oct 06        JYOTHI              Created
//*********************************************************************************/
namespace D8056
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
    /// F8056WorkItem
    /// </summary>
    public class F8056WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Inspection Details</returns>
        public InspectionEventData F8056_ListInspectionDetails(int eventId)
        {
            return WSHelper.F8056_ListInspectionDetails(eventId);
        }

        /// <summary>
        /// Save the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        public void F8056_SaveInspectionDetails(string eventItems,int userId)
        {
            WSHelper.F8056_SaveInspectionDetails(eventItems, userId);
        }

        /// <summary>
        /// Updates the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        public void F8056_UpdateInspectionDetails(string eventItems, int userId)
        {
            WSHelper.F8056_UpdateInspectionDetails(eventItems, userId);
        }

        /// <summary>
        /// Deletes the Inspection Item
        /// </summary>
        /// <param name="inspectionId">The inspection id.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public int F8056_DeleteInspectionDetails(int inspectionId, int userId)
        {
            return WSHelper.F8056_DeleteInspectionDetails(inspectionId, userId);
        }

        /// <summary>
        /// F8056_s the check event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns></returns>
        public int F8056_CheckEventId(int formId, int keyId)
        {
            return WSHelper.F8040_CheckEventId(formId, keyId);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
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
