//--------------------------------------------------------------------------------------------
// <copyright file="F1501Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1501 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20-11-2006       Krishna Abburi       Created
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1501 Controller
    /// </summary>
    public class F1501Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1501WorkItem WorkItem
        {
            get { return base.WorkItem as F1501WorkItem; }
        }
    }
}
