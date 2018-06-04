//--------------------------------------------------------------------------------------------
// <copyright file="F35000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Appraisal Summary.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07		Shiva M     	    Created
//*********************************************************************************/

namespace D35000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F35000 Apprisal Summary Controller
    /// </summary>
    public class F35000Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F35000WorkItem WorkItem
        {
            get { return base.WorkItem as F35000WorkItem; }
        }
    }
}
