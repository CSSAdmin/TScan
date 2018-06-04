//--------------------------------------------------------------------------------------------
// <copyright file="F8912Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8912Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/10/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class for F8912Controller
    /// </summary>
    public class F8912Controller : Controller
    {
        /// <summary>
        /// From the form F8912 workitem
        /// </summary>
        public new F8912WorkItem WorkItem
        {
            get { return base.WorkItem as F8912WorkItem; }
        }
    }
}
