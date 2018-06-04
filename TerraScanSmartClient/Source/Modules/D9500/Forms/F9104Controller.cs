//--------------------------------------------------------------------------------------------
// <copyright file="F9104Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9104Controller.
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
    /// Class file for F9104Controller
    /// </summary>
    public class F9104Controller : Controller
    {
        /// <summary>
        /// From the form F9104 workitem
        /// </summary>
        public new F9104WorkItem WorkItem
        {
            get { return base.WorkItem as F9104WorkItem; }
        }
    }
}
