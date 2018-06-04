//--------------------------------------------------------------------------------------------
// <copyright file="F1025Controllerr.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1025 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D1025
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1025 Controller
    /// </summary>
    public class F1025Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1025WorkItem WorkItem
        {
            get { return base.WorkItem as F1025WorkItem; }
        }
    }
}
