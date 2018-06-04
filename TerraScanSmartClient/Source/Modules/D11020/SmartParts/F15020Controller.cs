//--------------------------------------------------------------------------------------------
// <copyright file="F15020Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15020Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Dec 06       Ranjani             created// 
//*********************************************************************************/
namespace D11020
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15020 Controller class file
    /// </summary>
    public class F15020Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15020WorkItem WorkItem
        {
            get { return base.WorkItem as F15020WorkItem; }
        }
    }
}
