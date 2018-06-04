//--------------------------------------------------------------------------------------------
// <copyright file="F8056Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8056Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Oct 06        JYOTHI              Created
//*********************************************************************************/
namespace D8056
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8056Controller
    /// </summary>
    public class F8056Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8056WorkItem WorkItem
        {
            get { return base.WorkItem as F8056WorkItem; }
        }
    }
}
