//--------------------------------------------------------------------------------------------
// <copyright file="F35101Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35101Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May 16 2007      Karthikeyan.B      Created                
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F35101 Controller
    /// </summary>
    public class F35101Controller : Controller
    {
        /// <summary>
        /// From the form F35101 workitem
        /// </summary>
        public new F35101WorkItem WorkItem
        {
            get { return base.WorkItem as F35101WorkItem; }
        }
    }
}
