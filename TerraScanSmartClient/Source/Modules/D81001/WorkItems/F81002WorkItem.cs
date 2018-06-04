// -------------------------------------------------------------------------------------------
// <copyright file="F81002WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F81002</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/11/2007       Jaya Prakash.k     ///Created
// 
// -------------------------------------------------------------------------------------------

namespace D81001
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
    /// F81002WorkItem class file
    /// </summary>
    public class F81002WorkItem : WorkItem
    {
        #region F81002 Event Fee
        /// <summary>
        /// Get the Event Fee data
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <returns>DataSet</returns>
        public F81002EventFeeData F81002_GetEventFee(int eventId,int form)
        {
            return WSHelper.F81002_GetEventFee(eventId, form);
        }

        /// <summary>
        /// To Save the Event Fee
        /// </summary>
        /// <param name="eventId">eventID</param>
        /// <param name="feeItems">xml string containing the Event Fee Details</param>
        /// <returns>Integer value containing the key id</returns>
        public int F81002_SaveEventFee(int eventId, string feeItems, int userId)
        {
            return WSHelper.F81002_SaveEventFee(eventId, feeItems, userId);
        }

        /// <summary>
        /// Delete the Event Fee data
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="userId">UserID</param>
        public void F81002_DeleteEventFee(int eventId, int userId)
        {
            WSHelper.F81002_DeleteEventFee(eventId, userId);
        }
        #endregion F81002 Event Fee

        #region GDoc EventHeader

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return WSHelper.GetGDocEventHeader(eventId);
        }

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>Typed status containing Event Engine status.</returns>
        public GDocEventHeaderData ListGDocEventHeaderStatus(int eventId)
        {
            return WSHelper.ListGDocEventHeaderStatus(eventId);
        }

        #endregion  ListGDocEventHeaderStatus

        #endregion GDoc EventHeader

        #region WorkItemEvents

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItemEvents
    }
}
