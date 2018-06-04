//--------------------------------------------------------------------------------------------
// <copyright file="F9503Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F9503 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17-11-2006       Shiva              Created
//*********************************************************************************/

namespace D9500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9503 Controller
    /// </summary>
    public class F9503Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F9503WorkItem WorkItem
        {
            get { return base.WorkItem as F9503WorkItem; }
        }
    }
}
