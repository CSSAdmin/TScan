//--------------------------------------------------------------------------------------------
// <copyright file="F27000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27000Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Apr 07       Ranjani             created
//*********************************************************************************/
namespace D22000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27000 Controller class file
    /// </summary>
    public class F27000Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F27000WorkItem WorkItem
        {
            get { return base.WorkItem as F27000WorkItem; }
        }
    }
}
