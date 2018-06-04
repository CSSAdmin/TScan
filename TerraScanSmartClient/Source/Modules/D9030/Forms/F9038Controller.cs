//--------------------------------------------------------------------------------------------
// <copyright file="F9038Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08 Sep 06        Suganth Mani       Created
// 25 Sep 06        Suganth Mani       Modified for style cop 
//*********************************************************************************/

namespace D9030 
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F9038Controller
    /// </summary>
    public class F9038Controller : Controller
    {
        /// <summary>
        /// Created Property for F9038WorkItem
        /// </summary>
        public new F9038WorkItem WorkItem
        {
            get { return base.WorkItem as F9038WorkItem; }
        }
    }
}
