//--------------------------------------------------------------------------------------------
// <copyright file="F9040Controller.cs" company="Congruent">
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
// 09 May 2007      Suganth Mani       Created
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
    /// Class F9040Controller
    /// </summary>
    public class F9040Controller : Controller
    {
        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9040WorkItem WorkItem
        {
            get { return base.WorkItem as F9040WorkItem; }
        }
    }
}
