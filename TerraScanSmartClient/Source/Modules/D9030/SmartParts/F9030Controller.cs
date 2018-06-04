//--------------------------------------------------------------------------------------------
// <copyright file="F9030Controller.cs" company="Congruent">
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
    /// Class F9030Controller
    /// </summary>
    public class F9030Controller : Controller
    {
        /// <summary>
        /// Created Property for F90320WorkItem
        /// </summary>
        public new F9030WorkItem WorkItem
        {
            get { return base.WorkItem as F9030WorkItem; }
        }
    }
}
