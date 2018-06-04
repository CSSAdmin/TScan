//--------------------------------------------------------------------------------------------
// <copyright file="F9039Controller.cs" company="Congruent">
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
// 16 Aug 07        VINOTH             Created
//*********************************************************************************/

namespace D9030
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpace

    /// <summary>
    /// Class F9039Controller
    /// </summary>
    public class F9039Controller : Controller
    {
        /// <summary>
        /// Created Property for F9038WorkItem
        /// </summary>
        public new F9039WorkItem WorkItem
        {
            get { return base.WorkItem as F9039WorkItem; }
        }
    }
}
