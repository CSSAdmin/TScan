//--------------------------------------------------------------------------------------------
// <copyright file="F84725Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84725Controller.
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
    /// F84725Controller class file
    /// </summary>
    public class F84725Controller : Controller
    {
        /// <summary>
        /// From the form F84725 workitem
        /// </summary>
        public new F84725WorkItem WorkItem
        {
            get { return base.WorkItem as F84725WorkItem; }
        }
    }
}
