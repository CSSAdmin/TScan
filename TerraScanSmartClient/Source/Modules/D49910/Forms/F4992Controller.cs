//--------------------------------------------------------------------------------------------
// <copyright file="F4992Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F4992Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11/02/2008       KUPPUSAMY.B         Created
//*********************************************************************************/

namespace D49910
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F4992Controller
    /// </summary>
    public class F4992Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F4992WorkItem WorkItem
        {
            get {return base.WorkItem as F4992WorkItem; }
        }
    }
}
