//--------------------------------------------------------------------------------------------
// <copyright file="F84722Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84722Controller.
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
    /// F84722Controller class file
    /// </summary>
    public class F84722Controller : Controller
    {
        /// <summary>
        /// From the form F84722 workitem
        /// </summary>
        public new F84722WorkItem WorkItem
        {
            get { return base.WorkItem as F84722WorkItem; }
        }
    }
}
