//--------------------------------------------------------------------------------------------
// <copyright file="F16031Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F16031 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08-06-2007       Shiva              Created
//*********************************************************************************/
namespace D10030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F16031 Controller Class.
    /// </summary>
    public class F16031Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F16031WorkItem WorkItem
        {
            get
            {
                return base.WorkItem as F16031WorkItem;
            }
        }
    }
}
