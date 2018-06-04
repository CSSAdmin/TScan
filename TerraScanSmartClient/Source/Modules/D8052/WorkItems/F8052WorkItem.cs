//--------------------------------------------------------------------------------------------
// <copyright file="F8052WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8052WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8052
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
    /// F8052WorkItem
    /// </summary>
    public class F8052WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the Linear Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Linear Event Properties</returns>
        public LinearEventData GetLinearEventType(int eventId)
        {
            return WSHelper.GetLinearEventType(eventId);
        }

        /// <summary>
        /// Save the Linear Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        public void SaveLinearEventType(string eventItems,int userId)
        {
            WSHelper.SaveLinearEventType(eventItems,userId);
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
