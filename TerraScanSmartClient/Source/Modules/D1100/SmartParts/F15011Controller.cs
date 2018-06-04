//--------------------------------------------------------------------------------------------
// <copyright file="F15011Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15011.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24 JAN 07        Ranjani             Created// 
//*********************************************************************************/
namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15011 Controller class file
    /// </summary>
    public class F15011Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F15011WorkItem WorkItem
        {
            get { return base.WorkItem as F15011WorkItem; }
        }
    }
}
