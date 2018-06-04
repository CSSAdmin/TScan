//--------------------------------------------------------------------------------------------
// <copyright file="F9041Controller.cs" company="Congruent">
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
// 25 Nov 2008      D.LathaMaheswari     Created
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
    /// Class F9041Controller
    /// </summary>
    public class F9041Controller : Controller
    {
        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9041WorkItem WorkItem
        {
            get { return base.WorkItem as F9041WorkItem; }
        }
    }
}
