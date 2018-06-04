//--------------------------------------------------------------------------------------------
// <copyright file="F1207Controller.cs" company="Congruent">
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
// 12 May 2009      LathaMaheswari      Created
//*********************************************************************************/
namespace D1200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller for F1207
    /// </summary>
    public class F1207Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>F1207WorkItem</value>
        public new F1207WorkItem WorkItem
        {
            get { return base.WorkItem as F1207WorkItem; }
        }
    }
}
