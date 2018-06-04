//--------------------------------------------------------------------------------------------
// <copyright file="F1031Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06        JYOTHI              Created
//*********************************************************************************/
namespace D1030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1031Controller
    /// </summary>
    public class F1031Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F1031WorkItem WorkItem
        {
            get { return base.WorkItem as F1031WorkItem; }
        }
    }
}
