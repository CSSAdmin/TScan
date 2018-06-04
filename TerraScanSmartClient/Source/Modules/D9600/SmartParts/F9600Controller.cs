//----------------------------------------------------------------------------------
// <copyright file="F9600Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the workitem.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------
// 13 Nov 06        VINOTH              Created
//**********************************************************************************/

namespace D9600
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9600Controller Class
    /// </summary>
    public class F9600Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9600WorkItem WorkItem
        {
            get { return base.WorkItem as F9600WorkItem; }
        }
    }
}
