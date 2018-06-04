//--------------------------------------------------------------------------------------------
// <copyright file="F1016Controller.cs" company="Congruent">
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
// 27 Jul 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D1016
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class F1016Controller
    /// </summary>
    public class F1016Controller : Controller
    {
        /// <summary>
        /// Created Property for F1016WorkItem
        /// </summary>
        public new F1016WorkItem WorkItem
        {
            get { return base.WorkItem as F1016WorkItem; }
        }
    }
}
