//--------------------------------------------------------------------------------------------
// <copyright file="F1410Controller.cs" company="Congruent">
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
// 14 Dec 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1405
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1410Controller
    /// </summary>
    public class F1410Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1410WorkItem WorkItem
        {
            get { return base.WorkItem as F1410WorkItem; }
        }
    }
}
