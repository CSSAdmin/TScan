//--------------------------------------------------------------------------------------------
// <copyright file="F1223Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1223Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/10/2006       Ranjani             created// 
//*********************************************************************************/
namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1223 Controller class file
    /// </summary>
    public class F1223Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1223WorkItem WorkItem
        {
            get { return base.WorkItem as F1223WorkItem; }
        }
    }
}
