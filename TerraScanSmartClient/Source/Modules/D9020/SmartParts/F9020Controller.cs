//--------------------------------------------------------------------------------------------
// <copyright file="F9020Controller.cs" company="Congruent">
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
// 25 Jul 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9020
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class F9020Controller
    /// </summary>
    public class F9020Controller : Controller
    {
        /// <summary>
        /// Created Property for F9020WorkItem
        /// </summary>
        public new F9020WorkItem WorkItem
        {
            get { return base.WorkItem as F9020WorkItem; }
        }
    }
}
