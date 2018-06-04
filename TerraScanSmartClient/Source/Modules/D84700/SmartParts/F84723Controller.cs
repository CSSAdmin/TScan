//--------------------------------------------------------------------------------------------
// <copyright file="F84723Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84723Controller.
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
    /// F84723Controller class file
    /// </summary>
    public class F84723Controller : Controller
    { 
        /// <summary>
        /// From the form F84723 workitem
        /// </summary>
        public new F84723WorkItem WorkItem
        {
            get { return base.WorkItem as F84723WorkItem; }
        }
    }
}
