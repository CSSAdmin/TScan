// --------------------------------------------------------------------------------------------
// <copyright file="F35051Controller.cs" company="Congruent">
//        Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the Schedule Line Items.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author           Description
// ----------        ---------         ---------------------------------------------------------
// 16 July 2008    Sadha Shivudu M     Created
// *********************************************************************************/

namespace D20050
{
    #region Namespace

    using Microsoft.Practices.CompositeUI;

    #endregion

    /// <summary>
    /// F35051 Controller class
    /// </summary>
    public class F35051Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F35051WorkItem WorkItem
        {
            get { return base.WorkItem as F35051WorkItem; }
        }
    }
}
