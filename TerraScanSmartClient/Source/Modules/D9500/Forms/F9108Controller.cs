//--------------------------------------------------------------------------------------------
// <copyright file="F9108Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property to Load WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 August 06        KRISHNA A       Created
//*********************************************************************************/
namespace D9500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9108Controller class file
    /// </summary>
    public class F9108Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9108WorkItem WorkItem
        {
            get { return base.WorkItem as F9108WorkItem; }
        }
    }
}
