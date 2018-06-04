//--------------------------------------------------------------------------------------------
// <copyright file="F1010Controller.cs" company="Congruent">
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
// 01 Aug 06        SuganthMani        Created
//*********************************************************************************/

namespace D1010
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion Namespaces

    /// <summary>
    /// controller for F1010 mortgage import
    /// </summary>
    public class F1010Controller : Controller 
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F1010WorkItem WorkItem
        {
            get { return base.WorkItem as F1010WorkItem; }
        }
    }
}
