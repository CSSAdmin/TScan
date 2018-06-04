//--------------------------------------------------------------------------------------------
// <copyright file="F25050WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//07 Feb 08		  Jaya prakash .k	    Created
//*********************************************************************************/

namespace D20050
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
    /// F25050 Work Item
    /// </summary>
   public class F25050WorkItem : WorkItem
    {
        #region BusinessProcesses
       
        /// <summary>
        /// F2200_s the list edit schedule details.
        /// </summary>
        /// <param name="sheduleId">The shedule ID.</param>
        /// <returns>DataSet</returns>
        public F2200EditScheduleData F2200_ListEditScheduleDetails(int sheduleId)
        {
            return WSHelper.F2200_ListEditScheduleDetails(sheduleId);
        }


        /// <summary>
        /// Gets the details of F250500 ScheduleDetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        public F2200EditScheduleData F25050_GetScheduleDetails(int scheduleId)
        {
            return WSHelper.F25050_GetScheduleDetails(scheduleId);
        }
        #endregion BusinessProcesses

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
