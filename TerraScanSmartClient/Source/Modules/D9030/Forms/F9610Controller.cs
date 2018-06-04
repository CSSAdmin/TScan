//--------------------------------------------------------------------------------------------
// <copyright file="F9610Controller.cs" company="Congruent">
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
// 31 May 2008      Malliga      Created
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
    /// Class F9610Controller
    /// </summary>
    public class F9610Controller : Controller
    {

        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9610WorkItem WorkItem
        {
            get { return base.WorkItem as F9610WorkItem; }
        }
    }
}
