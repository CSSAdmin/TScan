//--------------------------------------------------------------------------------------------
// <copyright file="F9065Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9060Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// Nov 02           Karthikeyan V       Created
//                  
//*********************************************************************************/

namespace D9065
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9065Controller
    /// </summary>
    public class F9065Controller : Controller
    {
        /// <summary>
        ///  From the form F9060 workitem
        /// </summary>
        public new F9065WorkItem WorkItem
        {
            get { return base.WorkItem as F9065WorkItem; }
        }
    }
}
