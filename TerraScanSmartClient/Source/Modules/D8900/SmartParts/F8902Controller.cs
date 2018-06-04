//--------------------------------------------------------------------------------------------
// <copyright file="F8902Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8902Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06        VINOTH              Created
//*********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F8902Controller Class
    /// </summary>
    public class F8902Controller : Controller
    {
        /// <summary>
        /// From the form F8910 workitem
        /// </summary>
        public new F8902WorkItem WorkItem
        {
            get { return base.WorkItem as F8902WorkItem; }
        }
    }
}
