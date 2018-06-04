//--------------------------------------------------------------------------------------------
// <copyright file="F1224Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1224 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19-10-2006       Krishna Abburi       Created
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1210
{
    /// <summary>
    /// F1224 Controller
    /// </summary>
    public class F1224Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1224WorkItem WorkItem
        {
            get { return base.WorkItem as F1224WorkItem; }
        }

    }
}
