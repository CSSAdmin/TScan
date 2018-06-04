//--------------------------------------------------------------------------------------------
// <copyright file="F84724Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84724Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//                  
//*********************************************************************************/

namespace D84700
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84724Controller class file
    /// </summary>
    public class F84724Controller : Controller
    {
        /// <summary>
        /// From the form F84724 workitem
        /// </summary>
        public new F84724WorkItem WorkItem
        {
            get { return base.WorkItem as F84724WorkItem; }
        }
    }
}
