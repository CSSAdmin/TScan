//----------------------------------------------------------------------------------
// <copyright file="F9076Controller.cs" company="Congruent">
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
// 13 Dec 2008    A.Shanmuga Sundaram  Created
//*********************************************************************************/

namespace D9075
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F9076Controller
    /// </summary>
    public class F9076Controller : Controller
    {
        /// <summary>
        /// Created Property for F9040WorkItem
        /// </summary>
        public new F9076WorkItem WorkItem
        {
            get { return base.WorkItem as F9076WorkItem; }
        }
    }
}
