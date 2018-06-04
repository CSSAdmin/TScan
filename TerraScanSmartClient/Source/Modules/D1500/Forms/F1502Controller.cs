//--------------------------------------------------------------------------------------------
// <copyright file="F1502Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1502Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/11/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1502Controller class file
    /// </summary>
    public class F1502Controller : Controller
    {
        /// <summary>
        /// From the form F1502 workitem
        /// </summary>
        public new F1502WorkItem WorkItem
        {
            get { return base.WorkItem as F1502WorkItem; }
        }
    }
}
