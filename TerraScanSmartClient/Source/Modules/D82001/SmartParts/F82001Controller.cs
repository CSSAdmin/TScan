//--------------------------------------------------------------------------
// <copyright file="F82001Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82001 FS Building Permit.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 12/12/2007       Kuppusamy.B              Created
//                  
//**************************************************************************

namespace D82001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F82001Controller
    /// </summary>
    public class F82001Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F82001WorkItem WorkItem
        {
            get { return base.WorkItem as F82001WorkItem; }
        }
    }
}

