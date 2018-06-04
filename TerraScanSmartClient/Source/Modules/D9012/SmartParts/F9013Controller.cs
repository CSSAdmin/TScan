//--------------------------------------------------------------------------------------------
// <copyright file="F9013Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9013Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  M.Vijayakumar       Created// 
//                  
//*********************************************************************************/

namespace D9012
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9013Controller Class file
    /// </summary>
    public class F9013Controller : Controller
    {
        /// <summary>
        /// From the form F84725 workitem
        /// </summary>
        public new F9013WorkItem WorkItem
        {
            get { return base.WorkItem as F9013WorkItem; }
        }
    }
}