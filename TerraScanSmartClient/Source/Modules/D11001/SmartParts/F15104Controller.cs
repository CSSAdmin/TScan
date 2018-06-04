//--------------------------------------------------------------------------------------------
// <copyright file="F15104Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28 Dec 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15104Controller
    /// </summary>
    public class F15104Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F15104WorkItem WorkItem
        {
            get { return base.WorkItem as F15104WorkItem; }
        }
    }
}
