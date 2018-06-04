//--------------------------------------------------------------------------------------------
// <copyright file="F9103Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9103Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/11/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D9500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F9103Controller
    /// </summary>
    public class F9103Controller : Controller
    {
        /// <summary>
        /// From the form F9103 workitem
        /// </summary>
        public new F9103WorkItem WorkItem
        {
            get { return base.WorkItem as F9103WorkItem; }
        }
    }
}
