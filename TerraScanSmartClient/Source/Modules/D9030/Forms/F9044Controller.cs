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
// 06 March 2013      Purushotham.A        Created
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
    /// Class F9044Controller
    /// </summary>
    public class F9044Controller : Controller
    {
        /// <summary>
        /// Created Property for F9044WorkItem
        /// </summary>
        public new F9044WorkItem WorkItem
        {
            get { return base.WorkItem as F9044WorkItem; }
        }
    }
}
