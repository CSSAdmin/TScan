//--------------------------------------------------------------------------------------------
// <copyright file="F15100Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

namespace D11001
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F15100Controller
    /// </summary>
    public class F15100Controller : Controller 
    {
        /// <summary>
        /// Created Property for F15100WorkItem
        /// </summary>
        public new F15100WorkItem WorkItem
        {
            get { return base.WorkItem as F15100WorkItem; }
        }
    }
}
