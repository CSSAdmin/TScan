//--------------------------------------------------------------------------------------------
// <copyright file="F1200Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1201 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 06-09-2006       Ranjani        Created
//*********************************************************************************/
namespace D1200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1200 Controller Class
    /// </summary>
    public class F1200Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item</value>
        public new F1200WorkItem WorkItem
        {
            get { return base.WorkItem as F1200WorkItem; }
        }       
    }
}
