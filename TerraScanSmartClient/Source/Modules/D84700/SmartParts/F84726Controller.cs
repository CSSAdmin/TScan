//--------------------------------------------------------------------------------------------
// <copyright file="F84726Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84726Controller.
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

    public class F84726Controller : Controller
    {
        /// <summary>
        /// From the form F84726 workitem
        /// </summary>
        public new F84726WorkItem WorkItem
        {
            get { return base.WorkItem as F84726WorkItem; }
        }
    }
}
