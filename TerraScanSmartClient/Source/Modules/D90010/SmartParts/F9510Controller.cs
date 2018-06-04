//--------------------------------------------------------------------------------------------
// <copyright file="F9510Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2550Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Dec 08        D.LathaMaheswari   Created
//*********************************************************************************/
namespace D90010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller for F9510
    /// </summary>
    public class F9510Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9510WorkItem WorkItem
        {
            get { return base.WorkItem as F9510WorkItem; }
        }
    }
}
