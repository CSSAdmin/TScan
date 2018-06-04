//--------------------------------------------------------------------------
// <copyright file="F36040Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36040 FS Permanent Crop
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 26/10/2007        Shiva              Created
//                  
//**************************************************************************
namespace D36040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36040 Permanent Crop Controller Class.
    /// </summary>
    public class F36040Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36040WorkItem WorkItem
        {
            get { return base.WorkItem as F36040WorkItem; }
        }
    }
}
