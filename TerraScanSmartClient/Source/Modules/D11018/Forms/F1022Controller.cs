//--------------------------------------------------------------------------------------------
// <copyright file="F1022Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1022Controller.
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
    /// F1022 Controller class file
    /// </summary>
    public class F1022Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1022WorkItem WorkItem
        {
            get { return base.WorkItem as F1022WorkItem; }
        }
    }
}