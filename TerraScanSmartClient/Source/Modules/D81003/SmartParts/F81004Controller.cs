//--------------------------------------------------------------------------------------------
// <copyright file="F81004Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81004Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26 Dec 08        Sadha Shivudu M    Created
//*********************************************************************************/

namespace D81003
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion namespace

    /// <summary>
    /// F81004Controller
    /// </summary>
    public class F81004Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F81004WorkItem WorkItem
        {
            get { return base.WorkItem as F81004WorkItem; }
        }
    }
}
