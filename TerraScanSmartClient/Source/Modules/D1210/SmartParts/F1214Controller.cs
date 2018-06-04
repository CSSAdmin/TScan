//--------------------------------------------------------------------------------------------
// <copyright file="F1211Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1211 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10-10-2006       Krishna Abburi       Created
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1210
{
    /// <summary>
    /// F1214 Controller Class
    /// </summary>
    public class F1214Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1214WorkItem WorkItem
        {
            get { return base.WorkItem as F1214WorkItem; }
        }
    }
}
