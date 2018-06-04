//--------------------------------------------------------------------------
// <copyright file="F49912Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49912Controller.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 05/02/2008       KUPPUSAMY.B              Created
//                  
//**************************************************************************

namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F49912Controller
    /// </summary>
    public class F49912Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F49912WorkItem WorkItem
        {
            get { return base.WorkItem as F49912WorkItem; }
        }
    }
}
