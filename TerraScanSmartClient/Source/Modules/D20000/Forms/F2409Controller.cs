//--------------------------------------------------------------------------------------------
// <copyright file="F2409Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2409Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/07/2009       R.Malliga      Created
//*********************************************************************************/


namespace D20000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F2409Controller
    /// </summary>
    public class F2409Controller : Controller 
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F2409WorkItem WorkItem
        {
            get { return base.WorkItem as F2409WorkItem; }
        }
    }
}
