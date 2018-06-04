//--------------------------------------------------------------------------------------------
// <copyright file="F8002Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8002Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  
//*********************************************************************************/

namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Form F8002 Controller
    /// </summary>
    public class F8002Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item.</value>
        public new F8002WorkItem WorkItem
        {
            get { return base.WorkItem as F8002WorkItem; }
        }
    }
}
