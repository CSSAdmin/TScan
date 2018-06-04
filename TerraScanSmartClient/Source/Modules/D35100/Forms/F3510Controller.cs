//--------------------------------------------------------------------------------------------
// <copyright file="F3510Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3510Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 6/12/2007    	R.Malliga      Created
//                  
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F3510Controller
    /// </summary>
    public class F3510Controller : Controller
    {
        /// <summary>
        /// From the form F3510 workitem
        /// </summary>
        public new F3510WorkItem WorkItem
        {
            get { return base.WorkItem as F3510WorkItem; }
        }
    }
}
