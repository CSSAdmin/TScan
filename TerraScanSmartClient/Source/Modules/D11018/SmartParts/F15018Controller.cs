//--------------------------------------------------------------------------------------------
// <copyright file="F15018Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15018Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07       Ranjani             created// 
//*********************************************************************************/
namespace D11018
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15018 Controller class file
    /// </summary>
    public class F15018Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15018WorkItem WorkItem
        {
            get { return base.WorkItem as F15018WorkItem; }
        }
    }
}
