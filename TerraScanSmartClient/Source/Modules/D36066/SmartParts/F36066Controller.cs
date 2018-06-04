// -------------------------------------------------------------------------------------------
// <copyright file="F36066Controller.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 27/07/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36066
{
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller F36066
    /// </summary>
    public class F36066Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36066WorkItem WorkItem
        {
            get { return base.WorkItem as F36066WorkItem; }
        }
    }
}
