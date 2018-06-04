//--------------------------------------------------------------------------------------------
// <copyright file="F2205Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2204Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16/07/09         LathaMaheswari.D   Created
//*********************************************************************************/

namespace D20050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2205 Controller
    /// </summary>
    public class F2205Controller : Controller
    {
        /// <summary>
        /// From the form F2205 WorkItem
        /// </summary>
        public new F2205WorkItem WorkItem
        {
            get { return base.WorkItem as F2205WorkItem; }
        }
    }
}
