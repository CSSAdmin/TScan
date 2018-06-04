//--------------------------------------------------------------------------------------------
// <copyright file="F36001Controller.cs" company="Congruent">
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
// 11 Jun 07		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D36001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F36001Controller
    /// </summary>
    public class F36001Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F36001WorkItem WorkItem
        {
            get { return base.WorkItem as F36001WorkItem; }
        }
    }
}
