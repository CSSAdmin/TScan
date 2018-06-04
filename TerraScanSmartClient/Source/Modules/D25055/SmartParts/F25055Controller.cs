// -------------------------------------------------------------------------------------------
// <copyright file="F29640Controller.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------

namespace D25055
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F25055 Controller
    /// </summary>
    public class F25055Controller : Controller 
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F25055WorkItem WorkItem
        {
            get { return base.WorkItem as F25055WorkItem; }
        }
    }
}
