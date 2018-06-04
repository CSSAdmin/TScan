//--------------------------------------------------------------------------------------------
// <copyright file="F1210Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1213 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-10-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1210 Controller
    /// </summary>
    public class F1210Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1210WorkItem WorkItem
        {
            get 
            { 
                return base.WorkItem as F1210WorkItem; 
            }
        }
    }
}
