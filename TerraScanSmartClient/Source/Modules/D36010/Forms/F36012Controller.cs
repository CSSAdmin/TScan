// -------------------------------------------------------------------------------------------
// <copyright file="F36012Controller.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36012Controller.
// </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19/2/2009        M.Sadha Shivudu    ///Created
// -------------------------------------------------------------------------------------------

namespace D36010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36012Controller
    /// </summary>
    public class F36012Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F36012WorkItem WorkItem
        {
            get { return base.WorkItem as F36012WorkItem; }
        }
    }
}
