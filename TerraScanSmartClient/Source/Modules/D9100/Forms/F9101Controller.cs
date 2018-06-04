//--------------------------------------------------------------------------------------------
// <copyright file="F9101Controller.cs" company="Congruent">
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
// 25 July 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D9100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9101Controller
    /// </summary>
    public class F9101Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9101WorkItem WorkItem
        {
            get { return base.WorkItem as F9101WorkItem; }
        }
    }
}
