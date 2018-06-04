//--------------------------------------------------------------------------------------------
// <copyright file="F8042Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8042Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Oct 06        JAYANTHI              Created
//*********************************************************************************/

namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller class to call the workitem of F8042
    /// </summary>
    public class F8042Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8042WorkItem WorkItem
        {
            get { return base.WorkItem as F8042WorkItem; }
        }
    }
}
