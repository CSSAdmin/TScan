//--------------------------------------------------------------------------------------------
// <copyright file="F36000Controller.cs" company="Congruent">
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
// 28 Mar 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36000Controller
    /// </summary>
    public class F36000Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F36000WorkItem WorkItem
        {
            get { return base.WorkItem as F36000WorkItem; }
        }
    }
}
