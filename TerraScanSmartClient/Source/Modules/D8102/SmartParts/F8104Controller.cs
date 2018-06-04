//--------------------------------------------------------------------------------------------
// <copyright file="F8104Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8104Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8102
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8104Controller
    /// </summary>
    public class F8104Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8104WorkItem WorkItem
        {
            get { return base.WorkItem as F8104WorkItem; }
        }
    }
}
