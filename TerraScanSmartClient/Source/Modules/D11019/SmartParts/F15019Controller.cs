//--------------------------------------------------------------------------------------------
// <copyright file="F11019Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F11019.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

namespace D11019
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F15019Controller
    /// </summary>
    public class F15019Controller : Controller
    {
        /// <summary>
        /// Created Property for F15019WorkItem
        /// </summary>
        public new F15019WorkItem WorkItem
        {
            get { return base.WorkItem as F15019WorkItem; }
        }
    }
}
