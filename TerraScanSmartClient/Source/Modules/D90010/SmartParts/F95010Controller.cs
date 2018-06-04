//--------------------------------------------------------------------------------------------
// <copyright file="F95010Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F95010Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  Suganth Mani       Created// 
//                  
//*********************************************************************************/

namespace D90010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F95010Controller : Controller
    {
        /// <summary>
        /// From the form F95010 workitem
        /// </summary>
        public new F95010WorkItem WorkItem
        {
            get { return base.WorkItem as F95010WorkItem; }
        }
    }
}
