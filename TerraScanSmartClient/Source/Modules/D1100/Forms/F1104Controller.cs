//--------------------------------------------------------------------------------------------
// <copyright file="F1104Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26 July 06       M.VIJAYAKUMAR       Created
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Form F1104 Controller
    /// </summary>
    public class F1104Controller : Controller
    {
        /// <summary>
        /// From the form F1104 workitem
        /// </summary>
        public new F1104WorkItem WorkItem
        {
            get { return base.WorkItem as F1104WorkItem; }
        }
    }
}
