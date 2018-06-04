//--------------------------------------------------------------------------------------------
// <copyright file="F1108Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1108.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Nov 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1108Controller class file
    /// </summary>
    public class F1108Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1108WorkItem WorkItem
        {
            get { return base.WorkItem as F1108WorkItem; }
        }
    }
}
