//--------------------------------------------------------------------------------------------
// <copyright file="F1500Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1500 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08-11-2006       Krishna Abburi       Created
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1500
{
    /// <summary>
    /// F1500 Controller
    /// </summary>
    public class F1500Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1500WorkItem WorkItem
        {
            get { return base.WorkItem as F1500WorkItem; }
        }
    }
}
