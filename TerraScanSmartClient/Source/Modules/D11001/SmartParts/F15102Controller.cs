//--------------------------------------------------------------------------------------------
// <copyright file="F15102Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15102.
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
    /// Class F15102Controller
    /// </summary>
    public class F15102Controller : Controller
    {
        /// <summary>
        /// Created Property for F15102WorkItem
        /// </summary>
        public new F15102WorkItem WorkItem
        {
            get { return base.WorkItem as F15102WorkItem; }
        }
    }
}
