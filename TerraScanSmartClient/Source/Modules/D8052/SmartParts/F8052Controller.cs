//--------------------------------------------------------------------------------------------
// <copyright file="F8052Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8052Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8052
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8052Controller
    /// </summary>
    public class F8052Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8052WorkItem WorkItem
        {
            get { return base.WorkItem as F8052WorkItem; }
        }
    }
}
