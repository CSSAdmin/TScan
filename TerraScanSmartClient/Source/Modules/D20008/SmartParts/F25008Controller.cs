//--------------------------------------------------------------------------------------------
// <copyright file="F25008Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25008Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//                  
//*********************************************************************************/

namespace D20008
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F25008Controller class file
    /// </summary>
    public class F25008Controller : Controller
    {
        /// <summary>
        /// From the form F25008 workitem
        /// </summary>
        public new F25008WorkItem WorkItem
        {
            get { return base.WorkItem as F25008WorkItem; }
        }
    }
}
