//--------------------------------------------------------------------------------------------
// <copyright file="F25009Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25009Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//                  
//*********************************************************************************/

namespace D20009
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F25009 Controller
    /// </summary>
    public class F25009Controller : Controller
    {
        /// <summary>
        /// From the form F25009 workitem
        /// </summary>
        public new F25009WorkItem WorkItem
        {
            get { return base.WorkItem as F25009WorkItem; }
        }
    }
}
