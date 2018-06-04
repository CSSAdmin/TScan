//--------------------------------------------------------------------------------------------
// <copyright file="F29510Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Sep 07		D.LathaMaheswari    Created
//*********************************************************************************/

namespace D24500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F29500Controller
    /// </summary>
    public class F29510Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F29510WorkItem WorkItem
        {
            get { return base.WorkItem as F29510WorkItem; }
        }
    }
}
