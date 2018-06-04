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
    /// F35055 Controller
    /// </summary>
    public class F35055Controller : Controller 
    {

        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F35055WorkItem WorkItem
        {
            get { return base.WorkItem as F35055WorkItem; }
        }
    }
}
