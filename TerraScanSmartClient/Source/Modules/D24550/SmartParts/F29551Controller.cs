// -------------------------------------------------------------------------------------------
// <copyright file="F29551Controller.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 21-June-2011     LathaMaheswari.D    Added Controller for F29551
// -------------------------------------------------------------------------------------------
namespace D24550
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F29551Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F29551WorkItem WorkItem
        {
            get { return base.WorkItem as F29551WorkItem; }
        }
    }
}
