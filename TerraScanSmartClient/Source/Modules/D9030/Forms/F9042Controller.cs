//----------------------------------------------------------------------------------
// <copyright file="F9042Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			  Author		       Description
// ----------     ---------		       ---------------------------------------------
// 27 Nov 2008    A.Shanmuga Sundaram  Created
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
    public class F9042Controller : Controller
        {
        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9042WorkItem WorkItem
        {
            get { return base.WorkItem as F9042WorkItem; }
        }
    }
}
