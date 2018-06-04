//--------------------------------------------------------------------------------------------
// <copyright file="F15004Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15004 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28-12-2006       Krishna             Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15004 Controller for FormSlice - AgencyFundFundMgmt
    /// </summary>
    public class F15004Controller : Controller
    {

        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15004WorkItem WorkItem
        {
            get { return base.WorkItem as F15004WorkItem; }
        }
    }
}
