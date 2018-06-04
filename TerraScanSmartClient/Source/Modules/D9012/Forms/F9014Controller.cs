//--------------------------------------------------------------------------------------------
// <copyright file="F9014Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9014Controller.
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

    public class F9014Controller : Controller
    {
        /// <summary>
        /// From the form F84725 workitem
        /// </summary>
        public new F9014WorkItem WorkItem
        {
            get { return base.WorkItem as F9014WorkItem; }
        }
    }
}
