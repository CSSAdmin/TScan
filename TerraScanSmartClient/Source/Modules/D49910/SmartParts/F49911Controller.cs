//--------------------------------------------------------------------------
// <copyright file="F49911Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49911Controller.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 31/01/2008       KUPPUSAMY.B              Created
//                  
//**************************************************************************

namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F49911Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F49911WorkItem WorkItem
        {
            get { return base.WorkItem as F49911WorkItem; }
        }
       
    }
}
