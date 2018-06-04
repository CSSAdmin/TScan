//--------------------------------------------------------------------------------------------
// <copyright file="F1070Controller.cs" company="Congruent">
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
// 20 Aug 2009      LathaMaheswari      Created
//*********************************************************************************/
namespace D11020
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Controller for F1070
    /// </summary>
    public class F1070Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>F1070WorkItem</value>
        public new F1070WorkItem WorkItem
        {
            get { return base.WorkItem as F1070WorkItem; }
        }

    }
}
