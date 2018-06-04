//--------------------------------------------------------------------------------------------
// <copyright file="F1530Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1530Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Nov 06        Ranjani             created// 
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1530 Controller class file
    /// </summary>
    public class F1530Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1530WorkItem WorkItem
        {
            get { return base.WorkItem as F1530WorkItem; }
        }
    }
}
