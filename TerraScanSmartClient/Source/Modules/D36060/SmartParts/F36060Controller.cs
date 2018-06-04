//--------------------------------------------------------------------------
// <copyright file="F36060Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36060 FS Depreciation
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 14/12/2007       M.Vijayakumar      Created
//                  
//**************************************************************************

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36060Controller
    /// </summary>
    public class F36060Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36060WorkItem WorkItem
        {
            get { return base.WorkItem as F36060WorkItem; }
        }
    }
}
