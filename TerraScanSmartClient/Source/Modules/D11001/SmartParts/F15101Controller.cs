//--------------------------------------------------------------------------------------------
// <copyright file="F15101Controller.cs" company="Congruent">
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
    /// Class F15101Controller
    /// </summary>
   public class F15101Controller : Controller
    {
        /// <summary>
        /// Property for F15001WorkItem
        /// </summary>
        public new F15101WorkItem WorkItem
        {
            get { return base.WorkItem as F15101WorkItem; }
        }
    }
}
