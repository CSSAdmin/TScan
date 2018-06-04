//--------------------------------------------------------------------------
// <copyright file="F36061Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36061 FS Depreciation Control Tables
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 11/02/2008       M.Vijayakumar      Created
//                  
//**************************************************************************

namespace D36060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F36061Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36061WorkItem WorkItem
        {
            get { return base.WorkItem as F36061WorkItem; }
        }
    }
}
