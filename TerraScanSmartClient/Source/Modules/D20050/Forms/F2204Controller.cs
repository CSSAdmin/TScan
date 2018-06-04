//--------------------------------------------------------------------------------------------
// <copyright file="F2204Controller.cs" company="Congruent">
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
// 11/09/07         LathaMaheswari.D   Created
//*********************************************************************************/

namespace D20050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2204 Controller
    /// </summary>
    public class F2204Controller : Controller
    {
        /// <summary>
        /// From the form F2004 WorkItem
        /// </summary>
        public new F2204WorkItem WorkItem
        {
            get { return base.WorkItem as F2204WorkItem; }
        }
    }
}
