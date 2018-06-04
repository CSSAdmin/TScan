//--------------------------------------------------------------------------------------------
// <copyright file="F15035Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F15035 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/
namespace D11035
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15035 Controller.
    /// </summary>
    public class F15035Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15035WorkItem WorkItem
        {
            get
            {
                return base.WorkItem as F15035WorkItem;
            }
        }
    }
}
