//--------------------------------------------------------------------------------------------
// <copyright file="F2004Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2004Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20/08/07         Ramya.D             Created
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2004 Controller
    /// </summary>
    public class F2004Controller : Controller
    {
        /// <summary>
        /// From the form F2004 WorkItem
        /// </summary>
        public new F2004WorkItem WorkItem
        {
            get { return base.WorkItem as F2004WorkItem; }
        }
    }
}
