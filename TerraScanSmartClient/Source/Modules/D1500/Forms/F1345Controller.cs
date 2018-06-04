//--------------------------------------------------------------------------------------------
// <copyright file="F1345Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property to Load WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28 Jul 06        KRISHNA A       Created
//*********************************************************************************/
namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1345Controller class file
    /// </summary>
    public class F1345Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>returns F1345WorkItem</value>
        public new F1345WorkItem WorkItem
        {
            get { return base.WorkItem as F1345WorkItem; }
        }
    }
}
