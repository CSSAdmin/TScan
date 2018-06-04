//--------------------------------------------------------------------------------------------
// <copyright file="F82006Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82006Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Nov 06        Sadha Shivudu M    Created
//*********************************************************************************/

namespace D82006
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion namespace

    /// <summary>
    /// F82006 Controller
    /// </summary>
    public class F82006Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F82006WorkItem WorkItem
        {
            get { return base.WorkItem as F82006WorkItem; }
        }
    }
}
