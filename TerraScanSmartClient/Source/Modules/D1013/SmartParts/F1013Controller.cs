//--------------------------------------------------------------------------------------------
// <copyright file="F1013Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1013 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-05-2007       Shiva              Created
//*********************************************************************************/
namespace D1013
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1013 Controller Class.
    /// </summary>
    public class F1013Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1013WorkItem WorkItem
        {
            get
            {
                return base.WorkItem as F1013WorkItem;
            }
        }
    }
}
