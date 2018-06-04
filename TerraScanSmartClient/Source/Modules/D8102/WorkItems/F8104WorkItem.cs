//--------------------------------------------------------------------------------------------
// <copyright file="F8104WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8104WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8102
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
    /// F8104WorkItem
    /// </summary>
    public class F8104WorkItem : WorkItem
    {
        /// <summary>
        /// Lists the EventEngineTVDetails
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains EventEngine TVDetails</returns>
        public SanitaryPipeInspectionDetailsData ListEventEngineTVDetails(int eventId)
        {
            return WSHelper.ListEventEngineTVDetails(eventId);
        }

        /// <summary>
        /// Lists the EventEngine DetailTypes
        /// </summary>
        /// <returns>returns dataset contains DetailTypes</returns>
        public SanitaryPipeInspectionDetailsData ListEventEngineDetailTypes()
        {
            return WSHelper.ListEventEngineDetailTypes();
        }

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        public void SaveEventEngineTVDetails(string eventItems, int userId)
        {
            WSHelper.SaveEventEngineTVDetails(eventItems, userId);
        }

        /// <summary>
        /// Save the ExciseTaxRate
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        public void UpdateEventEngineTVDetails(string eventItems, int userId)
        {
            WSHelper.UpdateEventEngineTVDetails(eventItems, userId);
        }

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="detailId">The detail id.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public int DeleteEventEngineTVDetails(int detailId, int userId)
        {
            return WSHelper.DeleteEventEngineTVDetails(detailId, userId);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userName">UserName</param>
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
