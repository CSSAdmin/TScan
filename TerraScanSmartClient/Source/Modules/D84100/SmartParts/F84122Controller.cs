//--------------------------------------------------------------------------------------------
// <copyright file="F84122Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84122Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Dec 06        JYOTHI              Created
//*********************************************************************************/
namespace D84100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84122Controller
    /// </summary>
    public class F84122Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F84122WorkItem WorkItem
        {
            get { return base.WorkItem as F84122WorkItem; }
        }
    }
}
