//--------------------------------------------------------------------------------------------
// <copyright file="F15103Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15103.
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
    /// Class F15103Controller
    /// </summary>
    public class F15103Controller : Controller
    {
        /// <summary>
        /// Created Property for F15003WorkItem
        /// </summary>
        public new F15103WorkItem WorkItem
        {
            get { return base.WorkItem as F15103WorkItem; }
        }
    }
}
