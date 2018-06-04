//--------------------------------------------------------------------------------------------
// <copyright file="F8106WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8106WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Oct 06        JAYANTHI              Created
//*********************************************************************************/

namespace D8106
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
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Work Item for F8106 
    /// </summary>
    public class F8106WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the details of F8106_Stoppage Event 
        /// </summary>
        /// <param name="eventId">Event Id</param>
        /// <returns>Typed Dataset</returns>
        public StoppageEventData F8106_GetStoppageEventDetails(int eventId)
        {
            return WSHelper.F8106_GetStoppageEventDetails(eventId);
        }

        /// <summary>
        /// Stoppage Event Details are passed to the Helper Class 
        /// </summary>
        /// <param name="eventItems">XML as a string (Stoppage Event Details)</param>
        /// <returns>typed Dataset</returns>
        public StoppageEventData F8106_SaveStoppageEventDetails(string eventItems,int userId)
        {
            return WSHelper.F8106_SaveStoppageEventDetails(eventItems,userId);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
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
