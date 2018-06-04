//--------------------------------------------------------------------------------------------
// <copyright file="F84721Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84721Controller.
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
    /// F84721Controller class file
    /// </summary>
    public class F84721Controller : Controller
    {
        /// <summary>
        /// From the form F84721 workitem
        /// </summary>
        public new F84721WorkItem WorkItem
        {
            get { return base.WorkItem as F84721WorkItem; }
        }
    }
}
