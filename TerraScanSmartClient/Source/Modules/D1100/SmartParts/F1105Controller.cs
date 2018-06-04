//--------------------------------------------------------------------------------------------
// <copyright file="F1105Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1105 Controller class file
    /// </summary>
    public class F1105Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1105WorkItem WorkItem
        {
            get { return base.WorkItem as F1105WorkItem; }
        }
    }
}
