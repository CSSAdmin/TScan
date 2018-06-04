//--------------------------------------------------------------------------------------------
// <copyright file="F9080Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Roll Year Management Form.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22 Nov 11    		Manoj P 	    Created
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D9080
{
   
    /// <summary>
    /// F9080Controller
    /// </summary>
    public class F9080Controller : Controller 
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9080WorkItem WorkItem
        {
            get { return base.WorkItem as F9080WorkItem; }
        }
    }
}
