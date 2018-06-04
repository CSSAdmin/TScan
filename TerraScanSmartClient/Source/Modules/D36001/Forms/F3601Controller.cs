//--------------------------------------------------------------------------------------------
// <copyright file="F3601Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3601Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18/07/2007       M.Vijayakumar      Created
//*********************************************************************************/

namespace D36001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F3601Controller Class file 
    /// </summary>
    public class F3601Controller : Controller
    {
        /// <summary>
        /// From the form F3601 workitem
        /// </summary>
        public new F3601WorkItem WorkItem
        {
            get { return base.WorkItem as F3601WorkItem; }
        }
    }
}
