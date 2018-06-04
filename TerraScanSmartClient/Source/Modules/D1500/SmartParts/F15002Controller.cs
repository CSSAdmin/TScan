//--------------------------------------------------------------------------------------------
// <copyright file="F15002Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15002 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22-12-2006       Shiva              Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15002 Controller
    /// </summary>
    public class F15002Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15002WorkItem WorkItem
        {
            get { return base.WorkItem as F15002WorkItem; }
        }
    }
}
