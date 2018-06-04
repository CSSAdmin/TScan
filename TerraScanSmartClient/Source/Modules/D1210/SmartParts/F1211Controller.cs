//--------------------------------------------------------------------------------------------
// <copyright file="F1107Controller.cs" company="Congruent">
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
// 09 Oct 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    
    /// <summary>
    /// F1211Controller
    /// </summary>
    public class F1211Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1211WorkItem WorkItem
        {
            get { return base.WorkItem as F1211WorkItem; }
        }
    }
}
