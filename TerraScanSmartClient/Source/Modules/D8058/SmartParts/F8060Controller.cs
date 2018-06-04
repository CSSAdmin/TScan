//--------------------------------------------------------------------------------------------
// <copyright file="F8060Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8060Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14 May 06        JYOTHI              Created
//*********************************************************************************/
namespace D8058
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8060Controller
    /// </summary>
    public class F8060Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8060WorkItem WorkItem
        {
            get { return base.WorkItem as F8060WorkItem; }
        }
    }
}
