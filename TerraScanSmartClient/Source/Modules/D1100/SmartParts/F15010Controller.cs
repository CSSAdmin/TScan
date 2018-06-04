//--------------------------------------------------------------------------------------------
// <copyright file="F15010Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15010Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                   M.Vijayakumar      Created// 
//                  
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    
    /// <summary>
    /// F15010Controller Class file
    /// </summary>
    public class F15010Controller : Controller
    {
        /// <summary>
        /// From the form F15010 workitem
        /// </summary>
        public new F15010WorkItem WorkItem
        {
            get { return base.WorkItem as F15010WorkItem; }
        }
    }
}
