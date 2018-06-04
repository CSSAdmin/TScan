//--------------------------------------------------------------------------------------------
// <copyright file="F9005Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9005 Attachment Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 Aug 06        Vinoth              Created
// 
//*********************************************************************************/

namespace D9005
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9005Controller class file
    /// </summary>
    public class F9005Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F9005WorkItem WorkItem
        {
            get { return base.WorkItem as F9005WorkItem; }
        }
    }
}
