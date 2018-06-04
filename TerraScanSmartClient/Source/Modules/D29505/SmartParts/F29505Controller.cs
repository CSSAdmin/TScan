//--------------------------------------------------------------------------------------------
// <copyright file="F29505Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29505Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 29Dec 08         R.Malliga    Created
//*********************************************************************************/

namespace D29505
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F29505Controller
    /// </summary>
    public class F29505Controller : Controller
    {
        /// <summary>
        /// From the form F29505 workitem
        /// </summary>
        public new F29505WorkItem WorkItem
        {
            get { return base.WorkItem as F29505WorkItem; }
        }
    }
}
