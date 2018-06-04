// -------------------------------------------------------------------------------------------
// <copyright file="F36065Controller.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 03/08/2009        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D36065
{
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller F36065
    /// </summary>
    public class F36065Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36065WorkItem WorkItem
        {
            get { return base.WorkItem as F36065WorkItem; }
        }
    }
}
