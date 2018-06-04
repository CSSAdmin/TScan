//--------------------------------------------------------------------------------------------
// <copyright file="F2551Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2551Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Sep 2011        Manoj Kumar.P              Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D2550
{
    public class F2551Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F2551WorkItem WorkItem
        {
            get { return base.WorkItem as F2551WorkItem; }
        }
    }
}
