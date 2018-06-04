//--------------------------------------------------------------------------------------------
// <copyright file="F2010Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2010Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18/12/2007       Kuppusamy.B         Created
//*********************************************************************************/

namespace D20000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F2010Controller
    /// </summary>
    public class F2010Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F2010WorkItem WorkItem
        {
            get { return base.WorkItem as F2010WorkItem; }
        }
    }
}
