//--------------------------------------------------------------------------------------------
// <copyright file="F1212Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1212 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1212 Controller Class
    /// </summary>
    public class F1212Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1212WorkItem WorkItem
        {
            get { return base.WorkItem as F1212WorkItem; }
        }
    }
}
