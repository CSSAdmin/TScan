//--------------------------------------------------------------------------------------------
// <copyright file="F96000Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F96000.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

namespace D91000
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    #endregion NameSpaces

    /// <summary>
    /// Class F96000Controller
    /// </summary>
    public class F96000Controller : Controller
    {
        /// <summary>
        /// Created Property for F96000WorkItem
        /// </summary>
        public new F96000WorkItem WorkItem
        {
            get { return base.WorkItem as F96000WorkItem; }
        }        
    }
}
