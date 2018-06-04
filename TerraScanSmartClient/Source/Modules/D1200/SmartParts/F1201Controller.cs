//--------------------------------------------------------------------------------------------
// <copyright file="F1201Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1201 Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 05-09-2006       Krishna              Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D1200
{
    /// <summary>
    /// F1201 Controller Class
    /// </summary>
    public class F1201Controller : Controller 
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item</value>
        public new F1201WorkItem WorkItem
        {
            get { return base.WorkItem as F1201WorkItem; }
        }
    }
}
