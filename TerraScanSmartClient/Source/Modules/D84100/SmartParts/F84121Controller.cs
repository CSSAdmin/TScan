//--------------------------------------------------------------------------------------------
// <copyright file="F84121Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84121Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Dec 06        JYOTHI              Created
//*********************************************************************************/
namespace D84100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84121Controller
    /// </summary>
    public class F84121Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F84121WorkItem WorkItem
        {
            get { return base.WorkItem as F84121WorkItem; }
        }
    }
}
