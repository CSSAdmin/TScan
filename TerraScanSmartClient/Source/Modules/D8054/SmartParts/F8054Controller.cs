//--------------------------------------------------------------------------------------------
// <copyright file="F8054Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8054Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        VINOTH              Created
//*********************************************************************************/

namespace D8054
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8054Controller Controller
    /// </summary>
    public class F8054Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8054WorkItem WorkItem
        {
            get { return base.WorkItem as F8054WorkItem; }
        }
    }
}
