//--------------------------------------------------------------------------------------------
// <copyright file="F29500Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Sep 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D24500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F29500Controller
    /// </summary>
    public class F29500Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F29500WorkItem WorkItem
        {
            get { return base.WorkItem as F29500WorkItem; }
        }
    }
}
