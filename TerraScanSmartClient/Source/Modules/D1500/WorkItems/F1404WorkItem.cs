//--------------------------------------------------------------------------------------------
// <copyright file="F1404WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Fund Selection. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 9/10/2009    	R.Malliga           Created
//*********************************************************************************/

namespace D1500
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F1404WorkItem
    /// </summary>
    public class F1404WorkItem : WorkItem
    {

        /// <summary>
        /// F1404_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns></returns>
        public F1404ScheduleSelectionData F1404_ListScheduleSearch(string scheduleConditionXML)
        {
            return WSHelper.F1404_ListScheduleSearch(scheduleConditionXML);
        }

        /// <summary>
        /// F1404_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public F1404ScheduleSelectionData F1404_GetScheduleType(int? scheduleId)
        {
            return WSHelper.F1404_GetScheduleType(scheduleId);
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
