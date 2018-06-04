//--------------------------------------------------------------------------------------------
// <copyright file="F8106Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8106Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Oct 06        JAYANTHI              Created
//*********************************************************************************/

namespace D8106
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller class to call the workitem of F8106
    /// </summary>
    public class F8106Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8106WorkItem WorkItem
        {
            get { return base.WorkItem as F8106WorkItem; }  
        }
    }
}
